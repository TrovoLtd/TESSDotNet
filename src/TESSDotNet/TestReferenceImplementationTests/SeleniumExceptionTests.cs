using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TESSReferenceImplementationTests
{
    [TestClass]
    public class SeleniumExceptionTests
    {
        private static IWebDriver _driver;

        private static string _searchBoxId;
        private static string _searchButtonId;

        [ClassInitialize]
        public static void ClassInitialise(TestContext context)
        {

            FirefoxProfile profile = new FirefoxProfile();
            profile.SetPreference("browser.startup.page", 0);
            profile.SetPreference("browser.startup.homepage_override.mstone", "ignore");

            _driver = new FirefoxDriver(profile);
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
        public void SearchForPanopticonReturnsNoResults()
        {
            WebDriverWait wait = SearchForATerm("panopticon", 10);

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("outcomeMessage")); }).Text;

            Assert.AreEqual("Your search for panopticon did not find any results.", actual);
        }

        [TestMethod]
        public void EmptySearchMade()
        {
            WebDriverWait wait = SearchForATerm("", 10);

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("outcomeMessage")); }).Text;

            Assert.AreEqual("Please enter a search term in the box above.", actual);
        }


        // search throws exception

        [TestMethod]
        public void SearchForThrowExceptionThrowsException()
        {
            WebDriverWait wait = SearchForATerm("ThrowException", 10);

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("outcomeMessage")); }).Text;

            Assert.AreEqual("An error has occurred.", actual);
        }

        [TestMethod]
        public void ExceptionDisplaysErrorType()
        {
            WebDriverWait wait = SearchForATerm("ThrowException", 10);

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("errorType")); }).Text;

            Assert.AreEqual(@"Error type: System.Exception", actual);
        }

        [TestMethod]
        public void ExceptionGeneratesLinkToGoogle()
        {
            WebDriverWait wait = SearchForATerm("ThrowException", 10);

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("aErrorGoogleLink")); }).GetAttribute("href");

            Assert.AreEqual(@"https://www.google.co.uk/#hl=en&q=ThrowException+site:www.leicester.gov.uk", actual);
        }

        [TestMethod]
        public void SearchTermContainingHttpThrowsError()
        {
            WebDriverWait wait = SearchForATerm("http://7ro.usa.cock/rss.xml", 10);

            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("errorMessage")); }).Text;

            Assert.AreEqual(@"Error message: Invalid query error: the query search term contained http or some html tags. This usually occurs because a script has entered a value into the search box. Queries of this sort are rejected as a security measure. If you are genuinely searching for a web address, remove the http from the front and search again.", actual);
        }


        private static WebDriverWait SearchForATerm(string termToSearchFor, int timespan)
        {
            IWebElement searchBoxTheFirstTime = _driver.FindElement(By.Id(_searchBoxId));
            IWebElement searchButton = _driver.FindElement(By.Id(_searchButtonId));

            searchBoxTheFirstTime.Clear();

            searchBoxTheFirstTime.SendKeys(termToSearchFor);
            searchButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timespan));
            return wait;
        }


    }



}
