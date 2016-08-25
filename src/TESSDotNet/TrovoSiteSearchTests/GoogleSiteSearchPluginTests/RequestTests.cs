using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.GoogleSiteSearch;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.EnterpriseLibrary5Logger;
using TrovoCrossCutting.Logging.Interfaces;

using TrovoSiteSearch;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class RequestTests
    {
        private const string PATH_TO_RESULTS_FOR_CRIMINAL = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\queryForCriminal.xml";
        private const string PATH_TO_RESULTS_FOR_MURDER = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\queryForMurder.xml";
        
        private GoogleSearchRequest _testRequest;
        private ITrovoSearchCommand _command;
        private TrovoQuery _query;
        private Dictionary<string, string> _configSettings;
        
        private ILogWriter _logWriter;


        [TestInitialize]
        public void SetUp()
        {
            _configSettings = new Dictionary<string, string>();

            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString(), String.Empty);
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString(), "google-csbe");
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString(), "1234");
            _configSettings.Add(GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString(), "10");
            _configSettings.Add(GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString(), Boolean.FalseString);
            
            _logWriter = new LogWriterAdaptor();
            _command = new GoogleSiteSearchMockSearchCommand();
            _query = new TrovoQuery();
        }

        [TestCleanup]
        public void TearDown()
        {
            _configSettings = null;
            _logWriter = null;
            _command = null;
            _query = null;
            _testRequest = null;
        }
        
        [TestMethod]
        public void QueryForCriminalReturnsThreeResults()
        {
 
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_CRIMINAL;
            _query.SearchTerm = "criminal";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(3, testResponse.Results.Count);
        }

        [TestMethod]
        public void QueryForMurderReturnsTenResults()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_MURDER;

            _query.SearchTerm = "murder";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(10, testResponse.Results.Count);
        }

        [TestMethod]
        public void QueryForMurderReturnsATotalNumberOfResultsOfTen()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_MURDER;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";

            _query.SearchTerm = "murder";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(10, testResponse.TotalNumberOfResults);

        }

        [TestMethod]
        public void QueryForMurderWhenTenResultsPerPageAreRequestedReturnsATotalNumberOfPagesOfOne()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_MURDER;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";

            _query.SearchTerm = "murder";
            _query.PageNumber = 1;

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(1, testResponse.TotalNumberOfPages);
        }

        [TestMethod]
        public void QueryForCriminalWhenTenResultsPerPageAreRequestedReturnsATotalNumberOfPagesOfOne()
        {
 
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_CRIMINAL;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";

            _query.SearchTerm = "criminal";
            _query.PageNumber = 1;

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(1, testResponse.TotalNumberOfPages);
        }

        [TestMethod]
        public void TitleOfFirstResultForQueryForCriminalIsSikesMeetHisEnd()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_CRIMINAL;

            _query.SearchTerm = "criminal";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("Sikes Meets His End - Trovo", testResponse.Results[0].Title);
        }

        [TestMethod]
        public void InPageRankingOfFourthResultForQueryForMurderIs4()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_MURDER;

            _query.SearchTerm = "murder";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(4, testResponse.Results[3].RankWithinPage);
        }

        [TestMethod]
        public void URLOfSeventhResultForQueryForMurderIsThe_Prison_Ships()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_MURDER;
            _query.SearchTerm = "murder";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(@"http://www.trovo.co.uk/DemoContent/Great_Expectations/The_Prison_Ships.aspx", testResponse.Results[6].URL);
        }

        [TestMethod]
        public void SnippetOfThirdResultForQueryForCriminalIsSomethingAboutJoeGargery()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_CRIMINAL;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString()] = Boolean.TrueString;

            _query.SearchTerm = "criminal";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(@"Jo's ideas of a <strong>criminal</strong> trial, or a judge, or a bishop, or a government, or that <br />  inestimable jewel to him (if he only knew it) the Constitution, should be strange!", testResponse.Results[2].Snippet);
        }

        [TestMethod]
        public void SnippetOfThirdResultHasFormattingRemoved()
        {
            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_CRIMINAL;

            _query.SearchTerm = "criminal";
 
            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual(@"Jo's ideas of a criminal trial, or a judge, or a bishop, or a government, or that   inestimable jewel to him (if he only knew it) the Constitution, should be strange!", testResponse.Results[2].Snippet);
        }

        [TestMethod]
        public void FinalResultOfSearchForMurderHasFileTypeOfPDF()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = PATH_TO_RESULTS_FOR_MURDER;

            _query.SearchTerm = "murder";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.AreEqual("PDF", testResponse.Results[9].FileType);

        }

        [TestMethod]
        public void SearchForPanopticonHasNoResults()
        {

            _testRequest = new GoogleSearchRequest(_logWriter);
            _testRequest.ConfigSettings = _configSettings;
            _testRequest.ConfigSettings[GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString()] = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\panopticon.xml";

            _query.SearchTerm = "panopticon";

            ITrovoResultPage testResponse = _testRequest.executeSearch(_query, _command);
            Assert.IsFalse(testResponse.HasResults);

        }



    }
}
