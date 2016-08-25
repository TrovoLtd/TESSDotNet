using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.EnterpriseLibrary5Logger;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.GoogleSiteSearch;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class TestSpellingAdaptation
    {
        private const string PATH_TO_RESULTS_FOR_TARFFIC_JAM = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\spellingMistakeTarfficJam.xml";

        private GoogleSearchRequest _testRequest;
        private ITrovoSearchCommand _command;
        private TrovoQuery _query;
        private Dictionary<string, string> _configSettings;

        private ILogWriter _logWriter;

        [TestInitialize]
        public void SetUp()
        {
            _configSettings = new Dictionary<string, string>();
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString(), PATH_TO_RESULTS_FOR_TARFFIC_JAM);
            _configSettings.Add(GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString(), Boolean.FalseString);
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString(), "1234");
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString(), "google-csbe");
            _configSettings.Add(GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString(), "10");

            
            _logWriter = new LogWriterAdaptor();
            _command = new GoogleSiteSearchMockSearchCommand();
            _query = new TrovoQuery();

        }

        [TestCleanup]
        public void TearDown()
        {
            _configSettings = null;
            _logWriter = null;
            _query = null;
            _command = null;
            _testRequest = null;
        }


        [TestMethod]
        public void TestSearchForTarrficReturnsASpellingSuggestion()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;

            _query.SearchTerm = "tarffic jam";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("traffic jam", testResponse.SpellingSuggestions[0].SuggestedTerm);
        }

        [TestMethod]
        public void TestSearchForTarrficReturnsTheFormattedTextForASearchSuggestion()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString()] = Boolean.TrueString;

            _query.SearchTerm = "tarffic jam";
            
            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("<strong><em>traffic</em></strong> jam", testResponse.SpellingSuggestions[0].TermForDisplay);
        }

        [TestMethod]
        public void TestSearchForTarrficReturnsTheUnformattedTextForASearchSuggestion()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString()] = Boolean.FalseString;

            _query.SearchTerm = "tarffic jam";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("traffic jam", testResponse.SpellingSuggestions[0].TermForDisplay);
        }


    }
}
