using System;
using System.IO;
using System.Diagnostics;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

using TrovoSiteSearch.Interfaces;


namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    public class GoogleSiteSearchMockSearchCommand : TrovoLoggable, ITrovoSearchCommand, IDisposable
    {
        private FileStream _fileStream;

        public GoogleSiteSearchMockSearchCommand ()
        {
        }

        public GoogleSiteSearchMockSearchCommand(ILogWriter logWriter)
        {
            base.LogWriter = logWriter;
        }

        public string RequestPath { get; set; }

        public string Query { get; set; }

        public bool ResultsFound { get; set; }

        public Stream executeSearch()
        {
            try
            {
                _fileStream = new FileStream(RequestPath, FileMode.Open);
                return _fileStream;
            }
            catch (FileNotFoundException fnfEx)
            {
                TrovoLogEntry logEntry = new TrovoLogEntry();
                logEntry.Title = "Search results file could not be found.";
                logEntry.Message = fnfEx.Message;
                logEntry.Category = TrovoLoggingCategory.ErrorUnrecoverable.ToString();
                logEntry.Priority = (int) TrovoLoggingPriority.Critical;
                logEntry.EventId = 1501;
                logEntry.Severity = TraceEventType.Error;
                base.LogWriter.Write(logEntry);
                throw fnfEx;
            }
        }

        public void Dispose()
        {
            _fileStream.Close();
            _fileStream.Dispose();
        }
    }
}
