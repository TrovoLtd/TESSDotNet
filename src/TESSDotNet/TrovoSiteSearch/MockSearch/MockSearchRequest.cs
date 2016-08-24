using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

using TrovoSiteSearch.MockSearch.MockObjects;
using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.MockSearch
{

    public class MockSearchRequest: TrovoLoggable, ITrovoSearchRequest, IDisposable
    {
        private ITrovoSearchCommand _searchCommand;

        public string QueryString { get; set; }

        public Dictionary<string, string> ConfigSettings { get; set; }

        public MockSearchRequest()
        {

        }

        public MockSearchRequest(ILogWriter logWriter)
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
            if (String.IsNullOrEmpty(ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()])) throw new InvalidOperationException("An attempt was made to execute a search before the Mock XML file path had been set in the SearchProviderUrl setting.");
            
            if(_searchCommand == null) _searchCommand = new MockSearchCommand(LogWriter);
            
            _searchCommand.RequestPath = ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()];

            if (query.SearchTerm.Equals("ThrowException")) throw new InvalidOperationException("Exception thrown");

            _searchCommand.Query = query.SearchTerm;

            try
            {

                using (_searchCommand)
                {

                    Stream resultStream = _searchCommand.executeSearch();
                    For resultsForQuery;

                    if (_searchCommand.ResultsFound)
                    {
                        XmlSerializer serialiser = new XmlSerializer(typeof(For));

                        resultsForQuery = (For)serialiser.Deserialize(resultStream);
                    }
                    else
                    {
                        resultsForQuery = new For();
                        resultsForQuery.ResultPage = new ResultPage();
                        resultsForQuery.ResultPage.Result = new Result[0];

                    }

                    MockResultPageAdaptor pageAdaptor = new MockResultPageAdaptor(resultsForQuery.ResultPage, Boolean.Parse(ConfigSettings[MockConfigSettings.RetainProviderFormatting.ToString()]));
                    pageAdaptor.NumberOfResultsPerPage = Int32.Parse(ConfigSettings[MockConfigSettings.NumberOfResultsPerPage.ToString()]);
                    pageAdaptor.PageNumberRequested = query.PageNumber;

                    return pageAdaptor.CreateResultPage();
                }
            }
            catch (Exception ex)
            {
                base.generateLogEntry(String.Format("An error occurred while serialising the data the mock search data for the node for query: {0}.", query),
                                        ex.Message,
                                        1523,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int) TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                throw;
            }
        }


        void IDisposable.Dispose()
        {
            if(_searchCommand != null) _searchCommand.Dispose();
        }
    }
}
