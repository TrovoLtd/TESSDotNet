using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.EnterpriseLibrary5Logger;
using TrovoCrossCutting.Logging.Enumerations;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;

using TrovoSiteSearch.MockSearch;

namespace TessaSearch.Tests.Integration
{
    [TestClass]
    public class TrovoSearchFacadeTests
    {
        //private const string PATH_TO_MOCK_ADAPTOR_DLL = @"D:\TFS\Trovo\SiteSearch\TrovoEnhancedSiteSearch\TessaMockSearchProviderAdaptor\bin\Debug";
        private const string MOCK_DATA_PATH = @"MockSearchPluginTests\MockData\searchResults.xml";

        private const string ERROR_LOG_FILE_PATH = @"C:\Logs\TESSA.log";
        private const string ERROR_LOG_COPY_PATH = @"C:\Logs\TessaSearchErrorsCopy.log";

        private const string DEBUG_TRACE_LOG_FILE_PATH = @"C:\Logs\TESSADebugTrace.log";
        private const string DEBUG_TRACE_LOG_COPY_PATH = @"C:\Logs\TessaDebugTraceCopy.log";

        private TrovoSearchFacade _TrovoSearchFacade;
        private TrovoQuery _query;

        private StreamReader _logFileStreamReader;

        [TestInitialize]
        public void SetUp()
        {
            _query = new TrovoQuery();
            _query.PageNumber = 1;
        }

        [TestCleanup]
        public void TearDown()
        {
            _query = null;

            if (_logFileStreamReader != null)
            {
                _logFileStreamReader.Close();
                _logFileStreamReader = null;
            }

            if (File.Exists(ERROR_LOG_COPY_PATH)) File.Delete(ERROR_LOG_COPY_PATH);
            if (File.Exists(DEBUG_TRACE_LOG_COPY_PATH)) File.Delete(DEBUG_TRACE_LOG_COPY_PATH);

            _TrovoSearchFacade = null;
        }
        
        [TestMethod]
        public void MockResultsForCriminalReturnsThreeResults()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.MockProvider, LoggerType.NullLogger);
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()] = MOCK_DATA_PATH;
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.RetainProviderFormatting.ToString()] = Boolean.FalseString;

            _query.SearchTerm = "criminal";
            ITrovoResultPage resultPage = _TrovoSearchFacade.Search(_query, null);
            Assert.AreEqual(3, resultPage.Results.Count);
        }

        [TestMethod]
        public void MockResultsForMurderReturnsTenResults()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.MockProvider, LoggerType.NullLogger);
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()] = MOCK_DATA_PATH;
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.RetainProviderFormatting.ToString()] = Boolean.FalseString;
            _query.SearchTerm = "murder";
            ITrovoResultPage resultPage = _TrovoSearchFacade.Search(_query, null);
            Assert.AreEqual(10, resultPage.Results.Count);
        }

        [TestMethod]
        public void MockResultsForDonkeyRhubarbReturnsZeroResults()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.MockProvider, LoggerType.NullLogger);
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()] = MOCK_DATA_PATH;
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.RetainProviderFormatting.ToString()] = Boolean.FalseString;
            _query.SearchTerm = "donkey+rhubarb";
            ITrovoResultPage resultPage = _TrovoSearchFacade.Search(_query, null);
            Assert.IsFalse(resultPage.HasResults);
        }

        [TestMethod]
        public void TestMockResultsWithFormattingOn()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.MockProvider, LoggerType.NullLogger);
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()] = MOCK_DATA_PATH;
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.RetainProviderFormatting.ToString()] = Boolean.TrueString;
            _TrovoSearchFacade.RetainProviderFormatting = true;
            _query.SearchTerm = "criminal";
            ITrovoResultPage resultPage = _TrovoSearchFacade.Search(_query, null);
            Assert.AreEqual("<strong>...</strong> man crushing and striving with his neighbor, and all panting with impatience to <br/>  get near the door, and look upon the <strong>criminal</strong> as the officers brought him out.", resultPage.Results[0].Snippet);
        }

        [TestMethod]
        public void TestMockResultsWithFormattingOff()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.MockProvider, LoggerType.NullLogger);
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()] = MOCK_DATA_PATH;
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            _TrovoSearchFacade.ConfigSettings[MockConfigSettings.RetainProviderFormatting.ToString()] = Boolean.FalseString;
            _TrovoSearchFacade.RetainProviderFormatting = false;
            _query.SearchTerm = "criminal";
            ITrovoResultPage resultPage = _TrovoSearchFacade.Search(_query, null);
            Assert.AreEqual("... man crushing and striving with his neighbor, and all panting with impatience to   get near the door, and look upon the criminal as the officers brought him out.", resultPage.Results[0].Snippet);
        }

        
        [TestMethod]
        public void TheTitleOfExceptionRaisedByFailingSearchWithIdOf1521IsLoggedToErrorLog()
        {
            try
            {
                _query.SearchTerm = "test search term";
                TrovoSearchFacade brokenFacade = new TrovoSearchFacade(ProviderType.MockProvider, LoggerType.EnterpriseLibrary5Logger);
                brokenFacade.ConfigSettings[MockConfigSettings.SearchProviderUrl.ToString()] = "won't find any XML here";
                brokenFacade.ConfigSettings[MockConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
                brokenFacade.ConfigSettings[MockConfigSettings.RetainProviderFormatting.ToString()] = Boolean.TrueString;
                brokenFacade.Search(_query, null);
            }
            catch(Exception ex)
            {
                
                // swallow the exception so the test will run but we can read the log
            }

            Assert.AreEqual("A general exception occurred when the Trovo Search application attempted to execute a search. See the message for more details.", getActualValue("Title:", ERROR_LOG_FILE_PATH, ERROR_LOG_COPY_PATH));

        }

        [TestMethod]
        public void GoogleConfigSettingSearchProviderUrlReturnedInConfigDictionary()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.GoogleSiteSearch, LoggerType.NullLogger);

            Dictionary<string, string> result = _TrovoSearchFacade.ConfigSettings;

            Assert.IsTrue(result.ContainsKey("SearchProviderUrl"));

        }

        private string getActualValue(string fieldName, string logFilePath, string logFileCopyPath)
        {
            if (!File.Exists(logFileCopyPath)) File.Copy(logFilePath, logFileCopyPath);

            _logFileStreamReader = File.OpenText(logFileCopyPath);

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
