using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.EnterpriseLibrary5Logger;
using TrovoCrossCutting.Logging.Interfaces;

using System.Diagnostics;

namespace TrovoCrossCuttingTests.LoggingTests
{
    [TestClass]
    public class EnterpriseLibLogWriterAdapterTests
    {
        private const string PATH_TO_LOG_FILE = @"C:\Logs\error.log";
        private const string PATH_TO_LOG_COPY = @"C:\Logs\LogCopy.txt";
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

            _logWriter.Write(_logEntry);

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
            Assert.AreEqual("Test title", getActualValue("Title:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryMessage()
        {
            Assert.AreEqual("Test message", getActualValue("Message:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryCategory()
        {

            Assert.AreEqual("Test category", getActualValue("Category:"));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryEventId()
        {
            Assert.AreEqual(200, Int32.Parse(getActualValue("EventId:")));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntryPriority()
        {
            Assert.AreEqual(2, Int32.Parse(getActualValue("Priority:")));
        }

        [TestMethod]
        public void LogWriterAdaptorLogsAnEntrySeverity()
        {
            Assert.AreEqual("Critical", getActualValue("Severity:"));
        }


        [TestMethod]
        public void TestAdaptorDefaultsSeverityToInformation()
        {
            LogWriterAdaptor logWriter = new LogWriterAdaptor();
            TrovoLogEntry logEntry = new TrovoLogEntry();

            logEntry.Title = "Defaulted Severity entry title";
            logEntry.Message = "Defaulted Severity entry message";
            logEntry.Priority = 1;
            logEntry.EventId = 1200;
            logEntry.Category = "Test category";

            logWriter.Write(logEntry);

            Assert.AreEqual("Information", getActualValue("Severity:"));
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
