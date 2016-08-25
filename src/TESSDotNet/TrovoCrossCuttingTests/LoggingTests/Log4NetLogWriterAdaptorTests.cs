using System;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Log4NetLogger;
using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoCrossCuttingTests.LoggingTests
{
    [TestClass]
    public class Log4NetLogWriterAdaptorTests
    {
        private const string PATH_TO_LOG_FILE = @"C:\Logs\log4Net_TCC.log";
        private const string PATH_TO_LOG_COPY = @"C:\Logs\Log4Net_TCC_Copy.txt";

        private StreamReader _logFileStreamReader;

        private ILogWriter _logWriter;
        private ILogEntry _logEntry;

        [TestInitialize]
        public void SetUp()
        {
            _logWriter = new LogWriterAdaptor();
            _logEntry = new TrovoLogEntry();

            _logEntry.Title = "Test title";
            _logEntry.Message = "Test message";
            _logEntry.Category = "Test category";
            _logEntry.EventId = 200;
            _logEntry.Priority = 2;
            _logEntry.Severity = TraceEventType.Critical;

        }

        [TestCleanup]
        public void TearDown()
        {
            _logFileStreamReader.Close();
            if (File.Exists(PATH_TO_LOG_COPY)) File.Delete(PATH_TO_LOG_COPY);

            _logEntry = null;
            _logWriter = null;
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryTitle()
        {
            _logWriter.Write(_logEntry);
            Assert.AreEqual("Test title", getActualValue("Title:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryMessage()
        {
            _logWriter.Write(_logEntry);
            Assert.AreEqual("Test message", getActualValue("Message:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryCategory()
        {
            _logWriter.Write(_logEntry);
            Assert.AreEqual("Test category", getActualValue("Category:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryEventId()
        {
            _logWriter.Write(_logEntry);
            Assert.AreEqual("200", getActualValue("Event Id:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryPriority()
        {
            _logWriter.Write(_logEntry);
            Assert.AreEqual("2", getActualValue("Priority:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntrySeverityOfFatal()
        {
            _logWriter.Write(_logEntry);
            Assert.AreEqual("FATAL", getActualValue("Level:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntrySeverityOfError()
        {
            _logEntry.Severity = TraceEventType.Error;
            _logWriter.Write(_logEntry);
            Assert.AreEqual("ERROR", getActualValue("Level:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntrySeverityOfWarning()
        {
            _logEntry.Severity = TraceEventType.Warning;
            _logWriter.Write(_logEntry);
            Assert.AreEqual("WARN", getActualValue("Level:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntrySeverityOfInfo()
        {
            _logEntry.Severity = TraceEventType.Information;
            _logWriter.Write(_logEntry);
            Assert.AreEqual("INFO", getActualValue("Level:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntrySeverityOfDebug()
        {
            _logEntry.Severity = TraceEventType.Verbose;
            _logWriter.Write(_logEntry);
            Assert.AreEqual("DEBUG", getActualValue("Level:"));
        }

        private string getActualValue(string fieldName)
        {
            if (!File.Exists(PATH_TO_LOG_COPY)) File.Copy(PATH_TO_LOG_FILE, PATH_TO_LOG_COPY);

            _logFileStreamReader = File.OpenText(PATH_TO_LOG_COPY);

            string logFileLine = String.Empty;
            string fieldText = String.Empty;

            while ((logFileLine = _logFileStreamReader.ReadLine()) != null)
            {
                if (logFileLine.Contains(fieldName))
                {
                    fieldText = logFileLine.Substring(logFileLine.LastIndexOf(":") + 1);
                }
            }

            return fieldText.Trim();
        }

    }
}
