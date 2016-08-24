using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch.MockSearch;
using TrovoSiteSearch.MockSearch.MockObjects;


namespace TrovoSiteSearchTests.MockSearchPluginTests
{
    [TestClass]
    public class MockSerialisationTests
    {
        private const string MOCK_RESULTS_DATA_PATH = @"MockSearchPluginTests\MockData\searchResults.xml";

        private MemoryStream _resultStream;
        private XmlDocument _doc;

        [TestInitialize]
        public void SetUp()
        {
            _resultStream = new MemoryStream();
            _doc = new XmlDocument();
        }

        [TestCleanup]
        public void TearDown()
        {
            _resultStream = null;
            _doc = null;
        }

        [TestMethod]
        public void SerialisationOfForElement()
        {
            
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "criminal");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForQuery = (For) deserialiser.Deserialize(_resultStream);

            Assert.AreEqual(3, resultsForQuery.ResultPage.Result.Length);

        }

        [TestMethod]
        public void MimeTypeSerialisation()
        {

            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "criminal");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForQuery = (For)deserialiser.Deserialize(_resultStream);

            Assert.AreEqual("application/pdf", resultsForQuery.ResultPage.Result[2].MimeType);

        }

        [TestMethod]
        public void SerialisationOfSpellingSuggestionForTarfficHasQOfTraffic()
        {
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "tarffic");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForSpellingError = (For)deserialiser.Deserialize(_resultStream);

            Assert.AreEqual("traffic", resultsForSpellingError.ResultPage.Spelling.Suggestion[0].q);

        }

        [TestMethod]
        public void SerialisationOfSpellingSuggestionForTarfficJamHasCorrectFormattedText()
        {
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "tarffic jam");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForSpellingError = (For)deserialiser.Deserialize(_resultStream);

            Assert.AreEqual("<b><i>traffic</i></b> jam", resultsForSpellingError.ResultPage.Spelling.Suggestion[0].FormattedText);

        }

        [TestMethod]
        public void SerialisationOfSpellingSuggestionForTarfficJamReturnsTwoSuggestions()
        {
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "tarffic jam");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForSpellingError = (For)deserialiser.Deserialize(_resultStream);

            Assert.AreEqual("terrific jam", resultsForSpellingError.ResultPage.Spelling.Suggestion[1].q);

        }


        [TestMethod]
        public void SerialisationOfPromotionsForBusinessReturnsThreePromotions()
        {
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "business");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForSpellingError = (For)deserialiser.Deserialize(_resultStream);

            Assert.AreEqual(3, resultsForSpellingError.ResultPage.Promotion.Length);

        }

        [TestMethod]
        public void SerialisationOfPromotionsForBusinessReturnsFirstPromotionTitle()
        {
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "business");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForSpellingError = (For)deserialiser.Deserialize(_resultStream);

            Assert.AreEqual("Tenders and contracts", resultsForSpellingError.ResultPage.Promotion[0].Title);

        }

        [TestMethod]
        public void SerialisationOfPromotionsForBusinessReturnsSecondPromotionUrl()
        {
            string docNodeRetrievalXPath = String.Format("/SearchResults/For[@QueryTerm='{0}']", "business");

            _doc.Load(MOCK_RESULTS_DATA_PATH);

            XmlNode resultNode = _doc.DocumentElement.SelectSingleNode(docNodeRetrievalXPath);

            XmlSerializer serialiser = new XmlSerializer(typeof(XmlNode));

            serialiser.Serialize(_resultStream, resultNode);

            _resultStream.Position = 0;

            XmlSerializer deserialiser = new XmlSerializer(typeof(For));

            For resultsForSpellingError = (For)deserialiser.Deserialize(_resultStream);

            Assert.AreEqual("http://www.leicester.gov.uk/business/business-support-advice/", resultsForSpellingError.ResultPage.Promotion[1].Url);

        }

    }
}
