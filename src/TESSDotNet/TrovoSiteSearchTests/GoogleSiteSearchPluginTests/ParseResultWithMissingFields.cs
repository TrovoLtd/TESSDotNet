using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.GoogleSiteSearch;

using TrovoCrossCutting.Logging.NullLogger;
using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class ParseResultWithMissingFields
    {
        private const string PATH_TO_XML_WITH_MISSING_FIELDS = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\containsResultWithMissingFields.xml";

        private ITrovoSearchRequest _request;
        private ITrovoSearchCommand _command;
        private ITrovoQuery _query;
        private Dictionary<string, string> _configSettings;

        [TestInitialize]
        public void SetUp()
        {
            _command = new GoogleSiteSearchMockSearchCommand();
            _query = new TrovoQuery();

            _configSettings = new Dictionary<string, string>();
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString(), PATH_TO_XML_WITH_MISSING_FIELDS);
            _configSettings.Add(GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString(), "20");
            _configSettings.Add(GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString(), Boolean.FalseString);
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString(), "testClientName");
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString(), "1234");

            //_query.SearchProviderClientName = "cbe";

            ILogWriter logWriter = new LogWriterAdaptor();

            _request = new GoogleSearchRequest(logWriter);
            _request.ConfigSettings = _configSettings;
        }

        [TestCleanup]
        public void TearDown()
        {
            _configSettings = null;
            _request = null;
            _command = null;
            _query = null;
        }

        [TestMethod]
        public void MissingTitle()
        {
            _query.SearchTerm = "blah";

            ITrovoResultPage testResponse = _request.executeSearch(_query, _command);
            Assert.AreEqual("Untitled", testResponse.Results[0].Title);
        }

        [TestMethod]
        public void MissingSnippet()
        {
            _query.SearchTerm = "blah";

            ITrovoResultPage testResponse = _request.executeSearch(_query, _command);
            Assert.AreEqual("No description available for this result.", testResponse.Results[0].Snippet);
        }
    }
}
