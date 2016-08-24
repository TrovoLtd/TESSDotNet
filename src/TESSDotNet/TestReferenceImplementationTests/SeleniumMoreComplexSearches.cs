using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TESSReferenceImplementationTests
{
    [TestClass]
    public class SeleniumMoreComplexSearches
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
        public void SearchWithTermBusinessGrantReturnsTheCorrect7thResult()
        {
            WebDriverWait wait = SearchForATerm("business grant");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle7")); }).Text;

            Assert.AreEqual("Leicester City Council - Local Sustainable Transport Fund", actual);
        }

        [TestMethod]
        public void SearchWithTermBusinessAlcoholLicenseApplicationReturnsTheCorrect12thResult()
        {
            WebDriverWait wait = SearchForATerm("business alcohol license application");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle12")); }).Text;

            Assert.AreEqual("Starting Up a Catering Business", actual);
        }

        [TestMethod]
        public void SearchWithFrenchCharactersReturnsTheCorrect1stResult()
        {
            WebDriverWait wait = SearchForATerm("raison d'être");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle1")); }).Text;

            Assert.AreEqual("supporting information - Meetings, agendas, and minutes", actual);
        }

        [TestMethod]
        public void SearchWithExactPhraseReturnsTheCorrect1stResult()
        {
            WebDriverWait wait = SearchForATerm(@"""business grant""");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle1")); }).Text;

            Assert.AreEqual("Leicester City Council - Business Travel Package", actual);
        }

        [TestMethod]
        public void SearchWithANDReturnsTheCorrect4thResult()
        {
            WebDriverWait wait = SearchForATerm("park AND warden");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle4")); }).Text;

            Assert.AreEqual("Leicester City Council - Agenda item - CITY WARDEN SERVICE", actual);
        }

        [TestMethod]
        public void SearchWithORReturnsTheCorrect9thResult()
        {
            WebDriverWait wait = SearchForATerm("park OR warden");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle9")); }).Text;

            Assert.AreEqual("Leicester City Council - Humberstone Park", actual);
        }

        [TestMethod]
        public void SearchWithNOTReturnsTheCorrect17thResult()
        {
            WebDriverWait wait = SearchForATerm("park NOT warden");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle17")); }).Text;

            Assert.AreEqual("Leicester City Council - Bede Park", actual);
        }

        [TestMethod]
        public void SearchWithMINUSReturnsTheCorrect17thResult()
        {
            WebDriverWait wait = SearchForATerm("park -warden");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle17")); }).Text;

            Assert.AreEqual("Leicester City Council - Bede Park", actual);
        }

        [TestMethod]
        public void SearchForDocumentsOfDifferentTypesReturnsCorrectDocTypeFor2ndResult()
        {
            WebDriverWait wait = SearchForATerm("St Georges new community");
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("resultTitle2")); }).Text;

            Assert.IsTrue(actual.Contains("[PDF]"));
        }


        private static WebDriverWait SearchForATerm(string termToSearchFor)
        {
            IWebElement searchBoxTheFirstTime = _driver.FindElement(By.Id(_searchBoxId));
            IWebElement searchButton = _driver.FindElement(By.Id(_searchButtonId));

            searchBoxTheFirstTime.Clear();

            searchBoxTheFirstTime.SendKeys(termToSearchFor);
            searchButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait;
        }
        

    }
}
