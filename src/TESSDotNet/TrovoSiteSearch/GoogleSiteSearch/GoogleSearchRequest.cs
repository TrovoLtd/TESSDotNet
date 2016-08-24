using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using TrovoSiteSearch.Interfaces;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;


namespace TrovoSiteSearch.GoogleSiteSearch
{

    public class GoogleSearchRequest : TrovoLoggable, ITrovoSearchRequest, IDisposable 
    {

        private ITrovoSearchCommand _searchCommand;
                
        public string QueryString { get; set; }

        public Dictionary<string, string> ConfigSettings { get; set; }

        public GoogleSearchRequest()
        {

        }

        public GoogleSearchRequest(ILogWriter logWriter)
        {
            base.LogWriter = logWriter;
        }

        public ITrovoResultPage executeSearch(ITrovoQuery query, ITrovoSearchCommand searchCommand)
        {
            _searchCommand = searchCommand;
            return executeSearch(query);
        }

        public ITrovoResultPage executeSearch(ITrovoQuery query)
        {
            if (String.IsNullOrEmpty(ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()])) throw new InvalidOperationException("An attempt was made to execute a search before the Search Provider URL config setting had been set.");
            if (String.IsNullOrEmpty(ConfigSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()])) throw new InvalidOperationException("An attempt was made to execute a search without setting the number of results per page in the config settings.");
            if (String.IsNullOrEmpty(ConfigSettings[GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString()])) throw new InvalidOperationException("An attempt was made to execute a search without setting the RetainProviderFormatting switch.");
            
            if (_searchCommand == null)
            {
                _searchCommand = new GoogleSearchCommand(base.LogWriter);
            }

            if(_searchCommand.RequestPath == null)
            {
                _searchCommand.RequestPath = ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()];
            }

            GoogleResultPage resultPage;

            BuildGoogleQueryString(query);

            _searchCommand.Query = this.QueryString; 
            
            try
            {
                using (_searchCommand)
                {
                    base.generateLogEntry("The GoogleSearchRequest is about to contact Google and retrieve a stream of XML.",
                                    String.Format("Request query is: {0}", query.SearchTerm),
                                    9003,
                                    TrovoLoggingCategory.DebugTrace.ToString(),
                                    (int)TrovoLoggingPriority.DiagnosticInfo,
                                    TraceEventType.Verbose);                    

                    GoogleResultPageBuilder pageBuilder = new GoogleResultPageBuilder(base.LogWriter);
                    pageBuilder.RetainProviderFormatting = Boolean.Parse(ConfigSettings[GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString()]);
                    resultPage = pageBuilder.BuildResultPage(_searchCommand.executeSearch());

                    resultPage.MaxNumberOfResultsPerPage = Int32.Parse(ConfigSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()]);
                    resultPage.CurrentPageNumber = query.PageNumber;
 
                    base.generateLogEntry("The TessaGoogleSiteSearchAdaptor successfully parsed the results returned from Google.",
                                   String.Format("Request query is: {0}", query.SearchTerm),
                                   9004,
                                   TrovoLoggingCategory.DebugTrace.ToString(),
                                   (int)TrovoLoggingPriority.DiagnosticInfo,
                                   TraceEventType.Verbose);

                    return resultPage;
                }
            }
            catch (Exception ex)
            {
                if(!(ex.GetType() == typeof(System.Net.WebException) || ex.GetType() == typeof(System.Xml.XmlException))) CreateUnknownExceptionLogEntry(ex, query);
                throw ex;
            }
        }

        private void BuildGoogleQueryString(ITrovoQuery query)
        {
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(query, this.ConfigSettings);
            GoogleQueryStringDecorator queryStringDecorator = queryStringBuilder.BuildQuery();
            this.QueryString  = queryStringDecorator.GenerateQueryString();
        }

        private void CreateUnknownExceptionLogEntry(Exception ex, ITrovoQuery query)
        {
            string title, exceptionMessage, message;

            title = "An unexpected exception occurred when returning and parsing the XML from Google";

            exceptionMessage = ex.ToString();
            
            message = String.Format("{0} Caused by query - {1}", exceptionMessage, query.SearchTerm);

            int errorId = 1504;

            base.generateLogEntry(title,
                                message,
                                errorId,
                                TrovoLoggingCategory.ErrorUnrecoverable.ToString(),
                                (int)TrovoLoggingPriority.Critical,
                                TraceEventType.Error);
        }

        public void Dispose()
        {
            if (_searchCommand != null) _searchCommand.Dispose();
        }
    }
}
