using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TESSReferenceImplementationTests
{
    [TestClass]
    public class SeleniumBasicSearch
    {
        private static IWebDriver _driver;

        private static string _searchBoxId;
        private static string _searchButtonId;

        [ClassInitialize]
        public static void ClassInitialise(TestContext context)
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl(Properties.Settings.Default.baseTestUrl);

            _searchBoxId = Properties.Settings.Default.searchBoxId;
            _searchButtonId = Properties.Settings.Default.searchButtonId;

        }

        [ClassCleanup()]
        public static void ClassCleanUp()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void SearchForBusinessFinds300Results()
        {
            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("outcomeMessage")); }).Text;

            Assert.IsTrue(actual.Contains("300"));
        }

        [TestMethod]
        public void WhenASearchTermIsSubmittedItIsDisplayedInTheSearchBoxWhenThePageRefreshes()
        {
            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("q")); }).GetAttribute("value");

            Assert.AreEqual("business", actual);

        }

        [TestMethod]
        public void WhenASearchTermIsSubmittedItIsDisplayedInTheOutcomeMessage()
        {
            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("outcomeMessage")); }).Text;

            Assert.IsTrue(actual.Contains("business"));
        }

        [TestMethod]
        public void WhenTheTermBusinessIsSubmittedWithTheDefaultNumberOfResultsTwentyResultsAreDisplayed()
        {
            WebDriverWait wait = SendTheTermBusiness();

            IWebElement resultsDataList = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("dlResults")); });

            Assert.AreEqual(20, resultsDataList.FindElements(By.ClassName("resultTitle")).Count());
        }

        [TestMethod]
        public void WhenTheTermBusinessIsSubmitedTheTitleOfTheFirstResultIsCorrect()
        {
            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle4")); }).Text;

            Assert.AreEqual("Leicester City Council - Business Support & Advice", actual);
        }

        [TestMethod]
        public void WhenTheTermBusinessIsSubmitedTheTitleOfTheLastResultIsCorrect()
        {
            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle23")); }).Text;

            Assert.AreEqual("Leicester City Council - Business Pack", actual);

        }

        [TestMethod]
        public void WhenTheTermBusinessIsSubmittedTheSnippetOfTheTenthResultIsCorrect()
        {
            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultSnippet13")); }).Text;

            Assert.AreEqual("Leicester has a variety of business support provision for people thinking about Starting up in Business or Looking to Grow your Business. But its not always clear ...", actual);

        }

        [TestMethod]
        public void WhenTheTermBusinessIsSubmittedTheUrlOfThe8thResultIsCorrect()
        {
            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultUrl11")); }).Text;

            Assert.AreEqual("http://www.leicester.gov.uk/fit4business/", actual);


        }

        [TestMethod]
        public void WhenTheTermBusinessIsSubmittedTheFirstPageDisplaysResults1To10Of300()
        {

            WebDriverWait wait = SendTheTermBusiness();

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("currentPageMessage")); }).Text;

            Assert.AreEqual("Page 1 of 15", actual);

        }

        private static WebDriverWait SendTheTermBusiness()
        {
            IWebElement searchBoxTheFirstTime = _driver.FindElement(By.Id(_searchBoxId));
            IWebElement searchButton = _driver.FindElement(By.Id(_searchButtonId));

            searchBoxTheFirstTime.Clear();

            searchBoxTheFirstTime.SendKeys("business");
            searchButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait;
        }


    }
}
