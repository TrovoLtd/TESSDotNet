using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.GoogleSiteSearch;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.EnterpriseLibrary5Logger;
using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class TestPromotionAdaptation
    {
        private const string PATH_TO_RESULTS_FOR_BUSINESS = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\promotionsBusiness.xml";

        private GoogleSearchRequest _testRequest;
        private ITrovoSearchCommand _command;
        private TrovoQuery _query;
        private Dictionary<string, string> _configSettings;

        private ILogWriter _logWriter;


        [TestInitialize]
        public void SetUp()
        {
            _configSettings = new Dictionary<string, string>();
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString(), PATH_TO_RESULTS_FOR_BUSINESS);
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
        public void TestSearchForBusinessReturnsThreePromotions()
        {


            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;

            _query.SearchTerm = "business";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(3, testResponse.PromotedLinks.Count);
        }

        [TestMethod]
        public void TestSearchForBusinessHasFirstPromotionWithCorrectUrl()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;

            _query.SearchTerm = "business";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("http://www.leicester.gov.uk/business/tenders--contracts/", testResponse.PromotedLinks[0].Url);
        }

        [TestMethod]
        public void TestSearchForBusinessHasSecondPromotionWithFormattedTitle()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString()] = Boolean.TrueString;

            _query.SearchTerm = "business";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("<em>Business</em> support and advice", testResponse.PromotedLinks[1].Title);
        }

        [TestMethod]
        public void TestSearchForBusinessHasThirdPromotionWithUnformattedTitle()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;

            _query.SearchTerm = "business";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("Leicester City Council Business Homepage", testResponse.PromotedLinks[2].Title);
        }

        [TestMethod]
        public void SearchIncludingPromotionWithDescriptionBugFixTest()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\promotionDescriptionBug.xml"; 

            _query.SearchTerm = "panopticon";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);

            Assert.AreEqual("Trovo and Leicester City Council", testResponse.PromotedLinks[0].Title);
        }

        [TestMethod]
        public void SearchIncludingPromotionWithImageUrlPromotionDisplayed()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\PromotionSearchForHadronReturnsDescriptionAndImageURLs.xml"; 

            _query.SearchTerm = "hadron";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);

            Assert.AreEqual("Large Hadron Collider", testResponse.PromotedLinks[0].Title);
        }

        [TestMethod]
        public void SearchIncludingPromotionWithImageUrlHasResults()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\PromotionSearchForHadronReturnsDescriptionAndImageURLs.xml"; 

            _query.SearchTerm = "hadron";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);

            Assert.IsTrue(testResponse.HasResults);
        }

        [TestMethod]
        public void SearchIncludingPromotionWithImageUrlCountOfResultsIs20()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\PromotionSearchForHadronReturnsDescriptionAndImageURLs.xml"; 

            _query.SearchTerm = "hadron";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);

            Assert.AreEqual(10, testResponse.Results.Count);
        }


    }
}
