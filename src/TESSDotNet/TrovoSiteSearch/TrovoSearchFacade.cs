using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;

namespace TrovoSiteSearch
{
    public class TrovoSearchFacade : TrovoLoggable
    {
        private ITrovoSearchRequest _request;

        public bool RetainProviderFormatting { get; set; }

        public Dictionary<string, string> ConfigSettings { get; set; }

        public TrovoSearchFacade(ProviderType providerType, LoggerType loggerType)
        {
            TrovoLoggingFactory loggingFactory = new TrovoLoggingFactory();

            base.LogWriter = loggingFactory.GetLogger(loggerType);

            TrovoSearchRequestFactory searchRequestFactory = new TrovoSearchRequestFactory();

            ConfigSettings = searchRequestFactory.GetConfigVariables(providerType);


            _request = searchRequestFactory.GetProviderRequest(providerType);

            _request.LogWriter = base.LogWriter;

        }
        
        public ITrovoResultPage Search(ITrovoQuery query, List<string> filterCategories)
        {

            if (query.SearchTerm.ToLower().Contains("http") 
                || query.SearchTerm.ToLower().Contains("<")
                || query.SearchTerm.ToLower().Contains("&lt;")
                || query.SearchTerm.ToLower().Contains(">")
                || query.SearchTerm.ToLower().Contains("&gt;")
                )
            {
                base.generateLogEntry("Potential hacking attempt.",
                               String.Format("A potential hacking attempt was detected involving the query {0}", query.SearchTerm),
                               1100,
                               TrovoLoggingCategory.ErrorRecoverable.ToString(),
                               (int)TrovoLoggingPriority.Informational,
                               TraceEventType.Warning);    
                
                throw new ArgumentException("Invalid query error: the query search term contained http or some html tags. This usually occurs because a script has entered a value into the search box. Queries of this sort are rejected as a security measure. If you are genuinely searching for a web address, remove the http from the front and search again.");
            }

            _request.ConfigSettings = ConfigSettings;

            GenerateTraceLogForSearchExecution("The TrovoSearch TrovoSearchFacade is about to conduct a search:", 9007, query.SearchTerm, query.PageNumber, filterCategories, TraceEventType.Verbose);

            try
            {
                ITrovoResultPage resultPage = _request.executeSearch(query);
                
                // Add the search query logging here (adapt the line below).

                base.generateLogEntry("Search complete",
                               String.Format("{0} (page number {1}) - {2} results found", query.SearchTerm, query.PageNumber.ToString(), resultPage.TotalNumberOfResults.ToString()),
                               1000,
                               TrovoLoggingCategory.SearchQuery.ToString(),
                               (int)TrovoLoggingPriority.Informational,
                               TraceEventType.Information);    

                return resultPage;
            }
            catch (Exception ex)
            {
                base.generateLogEntry("A general exception occurred when the Trovo Search application attempted to execute a search. See the message for more details.",
                               String.Format("The search provider library will have logged the most common types of error into this log. Please check for errors with codes between 1500 and 1518 with timestamps similar to this. Also, the .Net exception message was: {0}", ex.Message),
                               1521,
                               TrovoLoggingCategory.ErrorRecoverable.ToString(),
                               (int)TrovoLoggingPriority.High,
                               TraceEventType.Error);

                Exception newEx = new Exception(String.Format("Search error: {0}. Search query string was: {1}", ex.Message, _request.QueryString), ex);
                               
                throw newEx;
            }
        
        }

        private void GenerateTraceLogForSearchExecution(string logMessage, int messageId, string querySearchTerm, int pageNumber, List<string> filterCategories, TraceEventType traceEventType)
        {
            string traceMessage = String.Format("The following search criteria were used: Query = {0}, Page Number = {1},", querySearchTerm, pageNumber.ToString());

            if (filterCategories != null)
            {
                traceMessage += "  Categories: ";
                foreach (string filterCategory in filterCategories)
                {
                    traceMessage += String.Format("{0} - ", filterCategory);
                }
            }

            traceMessage += String.Format(" The following search provider plugin was used: SearchRequest = {0}", _request.GetType().ToString());

            base.generateLogEntry(logMessage,
                                    traceMessage,
                                    messageId,
                                    TrovoLoggingCategory.DebugTrace.ToString(),
                                    (int)TrovoLoggingPriority.DiagnosticInfo,
                                    traceEventType);
        }

    }
}
