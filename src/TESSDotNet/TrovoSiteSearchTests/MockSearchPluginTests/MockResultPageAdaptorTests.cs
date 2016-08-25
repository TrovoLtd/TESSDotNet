using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch.MockSearch;
using TrovoSiteSearch.MockSearch.MockObjects;

namespace TrovoSiteSearchTests.MockSearchPluginTests
{
    [TestClass]
    public class MockResultPageAdaptorTests
    {
        private ResultPage _resultPage;
        
        [TestInitialize]
        public void SetUp()
        {
            _resultPage = new ResultPage();
            _resultPage.Result = new Result[6];

            Result result1 = new Result() { RankWithinPage = 1, Title = "Test result title 1", Snippet = "Snippet 1", URL = "URL 1" };
            _resultPage.Result[0] = result1;
            Result result2 = new Result() { RankWithinPage = 2, Title = "Test result title 2", Snippet = "Snippet 2", URL = "URL 2" };
            _resultPage.Result[1] = result2;
            Result result3 = new Result() { RankWithinPage = 3, Title = "Test result title 3", Snippet = "Snippet 3", URL = "URL 3" };
            _resultPage.Result[2] = result3;
            Result result4 = new Result() { RankWithinPage = 4, Title = "Test result title 4", Snippet = "Snippet 4", URL = "URL 4" };
            _resultPage.Result[3] = result4;
            Result result5 = new Result() { RankWithinPage = 5, Title = "Test result title 5", Snippet = "Snippet 5", URL = "URL 5" };
            _resultPage.Result[4] = result5;
            Result result6 = new Result() { RankWithinPage = 6, Title = "Test result title 6", Snippet = "Snippet 6", URL = "URL 5" };
            _resultPage.Result[5] = result6;

            Spelling spelling = new Spelling();
            spelling.Suggestion = new Suggestion[2];

            Suggestion suggestion1 = new Suggestion() { q = "test suggestion 1", FormattedText = "Test Suggestion One" };
            spelling.Suggestion[0] = suggestion1;

            Suggestion suggestion2 = new Suggestion() { q = "test suggestion 2", FormattedText = "Test Suggestion Two" };
            spelling.Suggestion[1] = suggestion2;

            _resultPage.Spelling = spelling; 

        }

        [TestCleanup]
        public void TearDown()
        {
            _resultPage = null;
        }
        
        [TestMethod]
        public void AResultPageWithOneResultAdaptsTheTitleCorrectly()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual("Test result title 1", mockResultPage.Results[0].Title);
        }

        // Test adding six results
        [TestMethod]
        public void AResultPageWithSixResultsHasAdaptsTheSnippetInTheSixthPageCorrectly()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual("Snippet 6", mockResultPage.Results[5].Snippet);
        }

        // test adding six results, with a page size of two - the result page should only have two results in it

        [TestMethod]
        public void SettingTheMockAdaptorsNumberOfResultsPerPageToTwoCreatesAResultsPageWithOnlyTwoResults()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 2;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual(2, mockResultPage.Results.Count);
        }

        // test adding six results, with a page size of two - what's the second result of the second page

        [TestMethod]
        public void SettingTheMockAdaptorsResultsPerPageToTwoAndRequestingTheSecondPageReturnsTestTitle4AsTheSecondResult()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 2;
            testAdaptor.PageNumberRequested = 2;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual("Test result title 4", mockResultPage.Results[1].Title);
        }

        // test no results returned

        [TestMethod]
        public void AnEmptyResultCreatesAPageWithAnEmptyResultsList()
        {
            ResultPage emptyPage = new ResultPage();
            emptyPage.Result = new Result[0];

            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(emptyPage, true);
            testAdaptor.NumberOfResultsPerPage = 2;
            testAdaptor.PageNumberRequested = 1;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.IsFalse(mockResultPage.HasResults);

        }

        [TestMethod]
        public void AResultPageWithSixResultsReturnsAMockResultPageWithATotalNumberOfResultsOfSix()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 2;
            testAdaptor.PageNumberRequested = 2;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual(6, mockResultPage.TotalNumberOfResults);
        }

        [TestMethod]
        public void RequestingTheLastPageWithoutAFullNumberOfResultsDoesNotCauseAnArrayOverflowError()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 4;
            testAdaptor.PageNumberRequested = 2;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual(2, mockResultPage.Results.Count);
        }

        [TestMethod]
        public void SettingTheNumberOfResultsPerPageTo4ReturnsAMockResultPageWithATotalNumberOfPagesOfTwo()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 4;
            testAdaptor.PageNumberRequested = 1;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual(2, mockResultPage.TotalNumberOfPages);
        }

        [TestMethod]
        public void SettingTheNumberOfResultsPerPageTo10ReturnsAMockResultPageWithATotalNumberOfPagesOf1()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 10;
            testAdaptor.PageNumberRequested = 1;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual(1, mockResultPage.TotalNumberOfPages);

        }

        [TestMethod]
        public void ThePageNumberRequestedGetsPassedOntoTheResultPage()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 4;
            testAdaptor.PageNumberRequested = 2;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual(2, mockResultPage.CurrentPageNumber);
        }

        [TestMethod]
        public void TheResultPageReturnsTwoSpellingSuggestions()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 10;
            testAdaptor.PageNumberRequested = 1;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual(2, mockResultPage.SpellingSuggestions.Count);
        }

        [TestMethod]
        public void TheSecondSpellingSuggestionIsTestSuggestionTwo()
        {
            MockResultPageAdaptor testAdaptor = new MockResultPageAdaptor(_resultPage, true);
            testAdaptor.NumberOfResultsPerPage = 10;
            testAdaptor.PageNumberRequested = 1;
            MockResultPage mockResultPage = testAdaptor.CreateResultPage();
            Assert.AreEqual("Test Suggestion Two", mockResultPage.SpellingSuggestions[1].TermForDisplay);
        }

    }
}
