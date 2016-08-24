using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Cache;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;
using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.GoogleSiteSearch
{

    public class GoogleSearchCommand : TrovoLoggable, ITrovoSearchCommand, IDisposable
    {
        private WebClient _resultsClient;
        private Stream _resultsFileStream;
        
        public string RequestPath { get; set; }
        public string Query { get; set; }

        public bool ResultsFound { get;  set; }
 
        public GoogleSearchCommand()
        {

        }

        public GoogleSearchCommand(ILogWriter logWriter)
        {
            base.LogWriter = logWriter;
        }

        /// <summary>
        /// Connects to Google Site Search and returns the XML results in a stream.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The cache policy settings are usually set in the machine.config file, though they can be overridden using application or web config.
        /// See http://msdn.microsoft.com/en-us/library/ehs1b109(v=vs.110).aspx for more details.
        /// </remarks>

        public Stream executeSearch()
        {
            string requestURL = String.Format("{0}{1}", RequestPath, Query);
            
            try
            {
                base.generateLogEntry("The TessaGoogleSiteSearchAdaptor is about to send a query to Google.",
                                    String.Format("Request query is: {0}", requestURL),
                                    9001,
                                    TrovoLoggingCategory.DebugTrace.ToString(),
                                    (int)TrovoLoggingPriority.DiagnosticInfo,
                                    TraceEventType.Verbose);

                Uri requestUri = new Uri(requestURL);
                _resultsClient = new WebClient();
                _resultsClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Default);
                _resultsFileStream = _resultsClient.OpenRead(requestUri);

                base.generateLogEntry("The TessaGoogleSiteSearchAdaptor successfully received a response from Google.",
                                    String.Format("The query for which a response was successfully returned is: {0}", requestURL),
                                    9002,
                                    TrovoLoggingCategory.DebugTrace.ToString(),
                                    (int)TrovoLoggingPriority.DiagnosticInfo,
                                    TraceEventType.Verbose);
                
                return _resultsFileStream;

            }
            catch (WebException webEx)
            {
                CreateErrorLogForWebException(webEx);
                throw webEx;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private void CreateErrorLogForWebException(WebException webEx)
        {
            string message = String.Format("{0} Caused by query {1}", webEx.Message, Query);
            string title = string.Empty;
            int eventId = 0;
            TrovoLoggingCategory eventCategory;
            int priority;

            switch (webEx.Status)
            {
                case WebExceptionStatus.NameResolutionFailure:
                    title = "The Google domain could not be accessed by the Trovo system. This might be a DNS or connectivity issue.";
                    eventId = 1505;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.ConnectFailure:
                    title = "The Trovo system could not establish a connection to the Google Site Search service. This might be a networking issue.";
                    eventId = 1506;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.ReceiveFailure:
                    title = "The Trovo system did not receive a complete response from the Google Site Search service.";
                    eventId = 1507;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.PipelineFailure:
                    title = "A pipeline error occurred when the Trovo system tried to connect to the Google Site Search service.";
                    eventId = 1508;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.RequestCanceled:
                    title = "The request made by the Trovo system to the Google Site Search Service was cancelled before completing.";
                    eventId = 1509;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.ProtocolError:
                    title = "An HTTP error occurred when the Trovo system attempted to connect to Google Site Search Service.";
                    eventId = 1510;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.ConnectionClosed:
                    title = "The HTTP connection to the Google Site Search was closed prematurely before the results were successfully returned.";
                    eventId = 1511;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.ServerProtocolViolation:
                    title = "The response from the Google Site Search service was not a valid HTTP response.";
                    eventId = 1512;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.Timeout:
                    title = "The request to the Google Site Search service timed out.";
                    eventId = 1513;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.ProxyNameResolutionFailure:
                    title = "The name resolver service could not resolve the proxy name when attempting to connect to Google Site Search.";
                    eventId = 1514;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.MessageLengthLimitExceeded:
                    title = "Either the request to Google Site Search, or the response returned, exceeded the specified message limit.";
                    eventId = 1515;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.RequestProhibitedByCachePolicy:
                    title = "A request to Google was prohibited by the Cache Policy used by the Trovo application (RequestCachePolicy.Default). This policy is usually set in the <system.net><requestCaching> element of Machine.config.";
                    eventId = 1516;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                case WebExceptionStatus.RequestProhibitedByProxy:
                    title = "The request to Google Site Search was prohibited by the proxy.";
                    eventId = 1517;
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    break;
                default:
                    title = "An unexpected type of WebException occurred when attempting to connect to Google Site Search.";
                    eventCategory = TrovoLoggingCategory.ErrorRecoverable;
                    priority = (int)TrovoLoggingPriority.High;
                    eventId = 1518;
                    break;
            }

            base.generateLogEntry(title,
                                    message,
                                    eventId,
                                    eventCategory.ToString(),
                                    priority,
                                    TraceEventType.Error);
        }

        public void Dispose()
        {
            if (_resultsFileStream != null)
            {
                _resultsFileStream.Close();
                _resultsFileStream.Dispose();
            }

            if (_resultsClient != null)
            {
                _resultsClient.Dispose();
            }

        }

        
    }
}
