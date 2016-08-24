using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace TESSReferenceImplementationTests
{
    [TestClass]
    public class SeleniumPaginationTests
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
        public void FirstPageLinkPresentWhenSearchingForBusiness()
        {
            WebDriverWait wait = SearchForATerm("business", 10);
            string actual = wait.Until<IWebElement>((d) => { return d.FindElement(By.Id("liPageLinks1")); }).Text;

            Assert.AreEqual("1", actual);
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
