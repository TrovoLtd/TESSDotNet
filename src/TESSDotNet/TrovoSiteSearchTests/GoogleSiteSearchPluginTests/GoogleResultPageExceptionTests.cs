using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.GoogleSiteSearch;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.EnterpriseLibrary5Logger;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class GoogleResultPageExceptionTests
    {

        private const string LOG_FILE_NAME = @"C:\Logs\TESSA.log";
        private const string LOG_FILE_COPY = @"C:\Logs\GoogleSearchCommandTestCopy.txt";

        private ILogWriter _logWriter;
        private GoogleResultPageBuilder _pageBuilder;

        private FileStream _fs;

        private StreamReader _logFileStreamReader;

        [TestInitialize]
        public void SetUp()
        {
            //_logFilePath = String.Format(@"{0}\{1}", TestContext.DeploymentDirectory, LOG_FILE_PATH);
            //_logFileCopyPath = String.Format(@"{0}\{1}", TestContext.DeploymentDirectory, LOG_FILE_COPY);

            if (File.Exists(LOG_FILE_COPY)) File.Delete(LOG_FILE_COPY);

            _logWriter = new LogWriterAdaptor();
            _pageBuilder = new GoogleResultPageBuilder(_logWriter);

        }

        [TestCleanup]
        public void TearDown()
        {
            _logWriter = null;
            _pageBuilder = null;

            if (_logFileStreamReader != null)
            {
                _logFileStreamReader.Close();
                _logFileStreamReader = null;
            }

            if (_fs.CanRead) _fs.Close();
            _fs = null;

        }

        [TestMethod]
        public void PageBuilderPassedAFileThatIsNotXMLErrorTitleIsAnXMLParsingErrorOccurred()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\PlainText.txt";

            _fs = File.OpenRead(xmlFilePath);

            try
            {
                GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);
            }
            catch
            {

            }

            Assert.AreEqual("An XML Parsing Error occurred", getActualValue("Title:"));
        }

        [TestMethod]
        public void PageBuilderPassedAFileThatIsNotXMLMessageFirstLineAboutDataAtTheRootLevel()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\PlainText.txt";

            _fs = File.OpenRead(xmlFilePath);

            try
            {
                GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);
            }
            catch
            {

            }

            Assert.AreEqual("Data at the root level is invalid. Line 1, position 1.", getActualValue("Message:"));
        }

        // Pass it something that's badly-formed XML

        [TestMethod]
        public void PageBuilderPassedBadlyFormedXMLErrorTitleIsAnXMLParsingErrorOccurred()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\resultsWithBrokenXML.xml";

            _fs = File.OpenRead(xmlFilePath);

            try
            {
                GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);
            }
            catch
            {

            }

            Assert.AreEqual("An XML Parsing Error occurred", getActualValue("Title:"));
        }

        [TestMethod]
        public void PageBuilderPassedBadlyFormedXMLErrorMessageAboutUnexpectedEndOfFile()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\resultsWithBrokenXML.xml";

            _fs = File.OpenRead(xmlFilePath);

            try
            {
                GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);
            }
            catch
            {

            }

            Assert.AreEqual("LESS_TEXT, SEARCH_MODES, GSP. Line 20, position 19.", getActualValue("Message:"));
        }

        private string getActualValue(string fieldName)
        {
            if (!File.Exists(LOG_FILE_COPY)) File.Copy(LOG_FILE_NAME, LOG_FILE_COPY);

            _logFileStreamReader = File.OpenText(LOG_FILE_COPY);

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
