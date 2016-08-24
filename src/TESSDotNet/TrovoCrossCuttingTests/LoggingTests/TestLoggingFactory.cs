using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

namespace TrovoCrossCuttingTests.LoggingTests
{
    [TestClass]
    public class TestLoggingFactory : TrovoLoggable
    {
        private const string PATH_TO_LOG_FILE = @"C:\Logs\error.log";
        private const string PATH_TO_LOG_COPY = @"C:\Logs\LogCopy.txt";

        private ILogWriter _logWriter;

        private StreamReader _logFileStreamReader;

        [TestCleanup]
        public void TearDown()
        {
            if(_logFileStreamReader != null)
            { 
                _logFileStreamReader.Close();
            }

            if (File.Exists(PATH_TO_LOG_COPY)) File.Delete(PATH_TO_LOG_COPY);

            _logWriter = null;
        }

        [TestMethod]
        public void NullLoggerReturned()
        {

            _logWriter = TrovoLoggingFactory.GetLogger(LoggerType.NullLogger);

            Assert.AreEqual("TrovoCrossCutting.Logging.NullLogger.LogWriterAdaptor", _logWriter.GetType().ToString());

        }

        [TestMethod]
        public void EnterpriseLoggerReturned()
        {

            _logWriter = TrovoLoggingFactory.GetLogger(LoggerType.EnterpriseLibrary5Logger);

            Assert.AreEqual("TrovoCrossCutting.Logging.EnterpriseLibrary5Logger.LogWriterAdaptor", _logWriter.GetType().ToString());

        }

        [TestMethod]
        public void Log4NetLoggerReturned()
        {
            _logWriter = TrovoLoggingFactory.GetLogger(LoggerType.Log4Net);
            Assert.AreEqual("TrovoCrossCutting.Logging.Log4NetLogger.LogWriterAdaptor", _logWriter.GetType().ToString());
        }


        [TestMethod]
        public void SimpleLoggingTest()
        {
            base.LogWriter = TrovoLoggingFactory.GetLogger(LoggerType.EnterpriseLibrary5Logger);

            base.generateLogEntry("TestLoggingFactory_SimpleLoggingTest_Title",
                                    "TestLoggingFactory_SimpleLoggingTest_Message",
                                    1001,
                                    "TestLoggingFactory_SimpleLoggingTest_Category",
                                    99,
                                    TraceEventType.Information);

            Assert.AreEqual("TestLoggingFactory_SimpleLoggingTest_Title", getActualValue("Title:"));
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
