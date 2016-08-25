using System;
using System.ComponentModel.Composition;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

using TrovoSiteSearch.Interfaces;
using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

namespace TrovoSiteSearch.MockSearch
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export(typeof(ITrovoSearchCommand))]
    public class MockSearchCommand:TrovoLoggable, ITrovoSearchCommand, IDisposable
    {
        MemoryStream _resultStream;

        public string RequestPath { get; set; }
        public string Query { get; set; }

        public bool ResultsFound { get; set; }

        public MockSearchCommand()
        {

        }
        
        public MockSearchCommand(ILogWriter logWriter)
        {
            base.LogWriter = logWriter;
        }

        public Stream executeSearch()
        {
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", Query);

            _resultStream = new MemoryStream();

            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(RequestPath);

                XmlNode resultNode = doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

                if (resultNode != null)
                {
                    ResultsFound = true;
                }
                else
                {
                    ResultsFound = false;
                }

                XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

                serialiser.Serialize(_resultStream, resultNode);

                _resultStream.Position = 0;

                return _resultStream;
            }
            catch (Exception ex)
            {
                base.generateLogEntry("An error occurred while reading the mock search data from the XML file at the location specified.",
                                        ex.Message,
                                        1522,
                                        TrovoLoggingCategory.ErrorRecoverable.ToString(),
                                        (int) TrovoLoggingPriority.High,
                                        TraceEventType.Error);
                throw;
            }
        }

        public void Dispose()
        {
            if (_resultStream != null)
            {
                _resultStream.Close();
                _resultStream.Dispose();
            }
        }
    }
}
