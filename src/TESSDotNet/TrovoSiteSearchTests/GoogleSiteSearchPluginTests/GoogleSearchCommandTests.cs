using System;
using System.Collections.Generic;
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
    public class GoogleSearchCommandTests
    {
        //public TestContext TestContext { get; set; }
        
        private const string LOG_FILE_NAME = @"C:\Logs\TESSA.log";
        private const string LOG_FILE_COPY = @"C:\Logs\GoogleSearchCommandTestCopy.txt";

        //private string _logFilePath, _logFileCopyPath;
        
        private const string PATH_TO_LOCALLY_HOSTED_XML = "http://trovowebsite.local.domain/DemoContent/queryForMoney.xml";
        
        private ILogWriter _logWriter;
        private GoogleSearchRequest _request;
        private TrovoQuery _query;
        private Dictionary<string, string> _configSettings;

        private StreamReader _logFileStreamReader;

        [TestInitialize]
        public void SetUp()
        {
            
            if (File.Exists(LOG_FILE_COPY)) File.Delete(LOG_FILE_COPY);

            _configSettings = new Dictionary<string, string>();
            _configSettings.Add(GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString(), "20");
            _configSettings.Add(GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString(), Boolean.FalseString);
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString(), "testClientName");
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString(), String.Empty);
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString(), "1234");
            
            _logWriter = new LogWriterAdaptor();
            _request = new GoogleSearchRequest(_logWriter);
            _request.ConfigSettings = _configSettings;

            _query = new TrovoQuery();
            _query.SearchTerm = "Test+Query+Term";

        }

        [TestCleanup]
        public void TearDown()
        {
            _logWriter = null;
            _request = null;

            if (_logFileStreamReader != null)
            {
                _logFileStreamReader.Close();
                _logFileStreamReader = null;
            }

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
