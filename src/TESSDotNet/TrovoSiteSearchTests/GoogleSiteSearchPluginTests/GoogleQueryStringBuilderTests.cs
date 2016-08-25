using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.GoogleSiteSearch;

using System.Web;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class GoogleQueryStringBuilderTests
    {
        private TrovoQuery _trovoQuery;
        private Dictionary<string, string> _configSettings;
        
        [TestInitialize]
        public void SetUp()
        {
            _trovoQuery = new TrovoQuery();
            _configSettings = new Dictionary<string, string>();
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString(), string.Empty);
            _configSettings.Add(GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString(), "0");
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString(), "google-csbe");
        }

        [TestCleanup]
        public void TearDown()
        {
            _configSettings = null;
            _trovoQuery = null;
        }
        
        [TestMethod]
        public void TestClientAndOutputParametersReturned()
        {
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=&num=0&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void TestAccountIdAdded()
        {
            _configSettings[GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString()] = "12345";
            
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=12345&q=&num=0&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void SingleWordSearchTermAdded()
        {
            _trovoQuery.SearchTerm = "business";
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=business&num=0&start=0", testDecorator.GenerateQueryString());
        }
        
        [TestMethod]
        public void TwoWordSearchTermAdded()
        {
            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("business grant");
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=business+grant&num=0&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void MultiWordSearchTermAdded()
        {
            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("business alcohol license application");
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=business+alcohol+license+application&num=0&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void SearchTermWithFrenchCharacterAdded()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            
            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("raison d'être");
            _trovoQuery.PageNumber = 1;

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=raison+d%27%c3%aatre&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void SearchTermInQuotesAdded()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";

            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("\"business grant\"");
            _trovoQuery.PageNumber = 1;

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=%22business+grant%22&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void ANDSearchAdded()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            
            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("park AND warden");
            _trovoQuery.PageNumber = 1;

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=park+AND+warden&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void ORSearchAdded()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            
            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("park OR warden");
            _trovoQuery.PageNumber = 1;
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=park+OR+warden&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void MinusSearchTermAdded()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            
            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("park -warden");
            _trovoQuery.PageNumber = 1;
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=park+-warden&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void NOTSearchTermAdded()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            
            _trovoQuery.SearchTerm = HttpUtility.UrlEncode("park NOT warden");
            _trovoQuery.PageNumber = 1;
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=park+-warden&num=10&start=0", testDecorator.GenerateQueryString());
        }


        [TestMethod]
        public void TestPageInfoAddedAsIfTheFirstPageHasBeenRequested()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "20";
            
            _trovoQuery.PageNumber = 1;
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=&num=20&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void TestPageInfoAddedAsIfTheTenthPageWithASizeOf10PerPageHasBeenRequested()
        {
            _configSettings[GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString()] = "10";
            _trovoQuery.PageNumber = 10;
            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=&q=&num=10&start=90", testDecorator.GenerateQueryString());
        }


    }
}
