using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Xml;
using System.Xml.Serialization;
using System.IO;


using TrovoSiteSearch;
using TrovoSiteSearch.MockSearch;
using TrovoSiteSearch.MockSearch.MockObjects;

using TrovoSiteSearch.Interfaces;
using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.EnterpriseLibrary5Logger;
using TrovoCrossCutting.Logging.Interfaces;


namespace TrovoSiteSearchTests.MockSearchPluginTests
{
    [TestClass]
    public class MockPaginationTests
    {
        private const string MOCK_RESULTS_DATA_PATH = @"MockSearchPluginTests\MockData\searchResults.xml";

        private TrovoQuery _query;
        private ILogWriter _logWriter;
        private MockSearchRequest _request;
        private Dictionary<string, string> _configSettings;

        [TestInitialize]
        public void SetUp()
        {
            _configSettings = new Dictionary<string, string>();

            _configSettings.Add(MockConfigSettings.SearchProviderUrl.ToString(), MOCK_RESULTS_DATA_PATH);
            _configSettings.Add(MockConfigSettings.NumberOfResultsPerPage.ToString(), "10");
            _configSettings.Add(MockConfigSettings.RetainProviderFormatting.ToString(), Boolean.FalseString);

            _query = new TrovoQuery() { PageNumber = 1, SearchTerm = "prison" };
            _logWriter = new LogWriterAdaptor();
            _request = new MockSearchRequest(_logWriter);
            _request.ConfigSettings = _configSettings;
            
        }

        [TestCleanup]
        public void TearDown()
        {
            _configSettings = null;
            _query = null;
            _logWriter = null;
            _request = null;
        }

        [TestMethod]
        public void TestASearchForPrisonWithAPageNumberOfOneReturnsTheFirstPageResultFromTheMockSearchRequest()
        {
            ITrovoResultPage resultPage = _request.executeSearch(_query);
            Assert.AreEqual("As We Came Out Of The Prison", resultPage.Results[0].Title);
        }

        [TestMethod]
        public void TestASearchForPrisonWithAPageNumberOfTwoReturnsTheSecondPageResultFromTheMockSearchRequest()
        {
            _query.PageNumber = 2;

            ITrovoResultPage resultPage = _request.executeSearch(_query);

            Assert.AreEqual("Leave The Debtor", resultPage.Results[0].Title);
        }

        [TestMethod]
        public void TestASearchForPrisonHasATotalNumberOfResultsOf26()
        {
            ITrovoResultPage resultPage = _request.executeSearch(_query);

            Assert.AreEqual(26, resultPage.TotalNumberOfResults);
        }

        [TestMethod]
        public void TestASearchForPrisonWithTenResultsPerPageHasATotalNumberOfPagesOf3()
        {
            ITrovoResultPage resultPage = _request.executeSearch(_query);

            Assert.AreEqual(3, resultPage.TotalNumberOfPages);
        }

        [TestMethod]
        public void TestASearchThatFindsNoResults()
        {
            _query.SearchTerm = "Donkey Rhubarb";

            ITrovoResultPage resultPage = _request.executeSearch( _query);

            Assert.IsFalse(resultPage.HasResults);

        }

    }
}
