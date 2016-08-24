using System;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using System.IO;
using System.Text;
using System.Data;
using System.Diagnostics;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;
using TrovoSiteSearch.Interfaces;

using TrovoSiteSearch.SP2010QueryServiceProxy;


namespace TrovoSiteSearch.SP2010Search
{
    public class SP2010SearchCommand : TrovoLoggable, ITrovoSearchCommand, IDisposable
    {
        private QueryServiceSoapClient _soapClient;

        public string AccountName { get; set; }

        public string AccountPassword { get; set; }

        public string Domain { get; set; }

        public string RequestPath { get; set; }

        public string Query { get; set; }

        public bool ResultsFound { get; set; }

        public void Connect()
        {

            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

                EndpointAddress endPoint = new EndpointAddress(RequestPath);

                _soapClient = new QueryServiceSoapClient(binding, endPoint);

                _soapClient.ClientCredentials.Windows.ClientCredential = new NetworkCredential(AccountName, AccountPassword, Domain);
                _soapClient.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            }
            catch (FaultException fEx)
            {
                base.generateLogEntry("The SharePoint 2010 Search Connector received a SOAP response in an unexpected format.",
                                        String.Format("Query {0} caused {1}", Query, fEx.ToString()),
                                        2501,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);

                throw fEx;
            }
            catch (CommunicationException comEx)
            {
                base.generateLogEntry("The SharePoint 2010 Search Connector experienced a communication error between it and the SharePoint 2010 SearchQueryWebService.",
                                        String.Format("Query {0} caused {1}", Query, comEx.ToString()),
                                        2502,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                throw comEx;
            }
            catch (TimeoutException toEx)
            {
                base.generateLogEntry("The request from the SharePoint 2010 Search Connector timed out before a response was received.",
                                        String.Format("Query {0} caused {1}", Query, toEx.ToString()),
                                        2503,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                throw toEx;
            }
            catch (Exception ex)
            {
                base.generateLogEntry("An unexpected error occurred when the SharePoint 2010 Connector called the SP2010 Search Query Web Service.",
                                        String.Format("Query {0} caused {1}", Query, ex.ToString()),
                                         2504,
                                         TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                         (int)TrovoLoggingPriority.High,
                                         TraceEventType.Error);
                throw ex;
            }
        }

        public Stream executeSearch()
        {
 
            try
            {
                base.generateLogEntry("The SP2010SearchCommand is about to send a query to the SharePoint 2010 SearchQueryWebService.",
                                    String.Format("Request query is: {0} This is being requested from the endpoint at address {1}", this.Query, this.RequestPath),
                                    9007,
                                    TrovoLoggingCategory.DebugTrace.ToString(),
                                    (int)TrovoLoggingPriority.DiagnosticInfo,
                                    TraceEventType.Verbose);


                // get the main query
                string resultXML = _soapClient.Query(this.Query);
                
                // now get the dataset, which is the only way of getting the best bets :(
                DataSet resultDataSet = _soapClient.QueryEx(this.Query);

                base.generateLogEntry("The SP2010SearchCommand successfully receieved a response from the SharePoint 2010 SearchQueryWebService.",
                    String.Format("Request query was: {0} This response was returned from the endpoint at address {1}", this.Query, this.RequestPath),
                    9007,
                    TrovoLoggingCategory.DebugTrace.ToString(),
                    (int)TrovoLoggingPriority.DiagnosticInfo,
                    TraceEventType.Verbose);


                if (resultDataSet.Tables["SpecialTermResults"] != null && resultDataSet.Tables["SpecialTermResults"].Rows.Count > 0)
                {
                    string dataSetXML = resultDataSet.GetXml();
                    ResultXMLAggregator resultAggregator = new ResultXMLAggregator(resultXML, dataSetXML);

                    return resultAggregator.aggregateXML();
                }
                else
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(resultXML);

                    MemoryStream standardResultStream = new MemoryStream(byteArray);
                    standardResultStream.Position = 0;

                    return standardResultStream;

                }



                //byte[] byteArray = Encoding.UTF8.GetBytes(dataSetXML);

                
            }
            catch(FaultException fEx)
            {
                base.generateLogEntry("The SharePoint 2010 Search Connector received a SOAP response in an unexpected format.",
                                        String.Format("Query {0} caused {1}", Query, fEx.ToString()),
                                        2501,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                
                throw fEx;
            }
            catch (CommunicationException comEx)
            {
                base.generateLogEntry("The SharePoint 2010 Search Connector experienced a communication error between it and the SharePoint 2010 SearchQueryWebService.",
                                        String.Format("Query {0} caused {1}", Query, comEx.ToString()),
                                        2502,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                throw comEx;
            }
            catch (TimeoutException toEx)
            {
                base.generateLogEntry("The request from the SharePoint 2010 Search Connector timed out before a response was received.",
                                        String.Format("Query {0} caused {1}", Query, toEx.ToString()),
                                        2503,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                throw toEx;
            }
            catch(Exception ex)
            {
                base.generateLogEntry("An unexpected error occurred when the SharePoint 2010 Connector called the SP2010 Search Query Web Service.",
                                        String.Format("Query {0} caused {1}", Query, ex.ToString()),
                                         2504,
                                         TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                         (int)TrovoLoggingPriority.High,
                                         TraceEventType.Error);
                throw ex;
            }
   
        }


        public void Dispose()
        {
            if(_soapClient == null)
            {
                InvalidOperationException exClosedBeforeOpening = new InvalidOperationException("The SP2010SearchCommand was disposed before a search took place - i.e. before executeSearch() was called.");
                
                base.generateLogEntry("Dispose was called in the SP2010SearchCommand before the connection was opened.",
                                        String.Format("Query {0} caused {1}", Query, exClosedBeforeOpening.ToString()),
                                        2505,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);

                throw exClosedBeforeOpening;
            }

            try
            {
                _soapClient.Close();
            }
            catch(CommunicationException comEx)
            {
                base.generateLogEntry("A communication error occurred while the SP2010SearchCommand attempted to close the connection to the SP2010 SearchQueryWebService",
                                        String.Format("Query {0} caused {1}", Query, comEx.ToString()),
                                        2506,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                _soapClient.Abort();
                throw comEx;
            }
            catch(TimeoutException toEx)
            {
                base.generateLogEntry("The connection timed out while the SP2010SearchCommand attempted to close the connection to the SP2010 SearchQueryWebService",
                                        String.Format("Query {0} caused {1}", Query, toEx.ToString()),
                                        2507,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                _soapClient.Abort();
                throw toEx;
            }
            catch(Exception ex)
            {
                base.generateLogEntry("An unexpected error occured while the SP2010SearchCommand attempted to close the connection to the SP2010 SearchQueryWebService",
                                        String.Format("Query {0} caused {1}", Query, ex.ToString()),
                                        2508,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int)TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                _soapClient.Abort();
                throw ex;
            }

        }




    }
}
