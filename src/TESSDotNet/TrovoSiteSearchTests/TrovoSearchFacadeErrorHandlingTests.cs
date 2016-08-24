using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

using TrovoSiteSearch;
using TrovoSiteSearch.GoogleSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;

namespace TessaAPITests
{
    [TestClass]
    public class TrovoSearchFacadeErrorHandlingTests
    {

        private const string PROVIDER_URL = "http://search.provider.com/search?";

        private TrovoSearchFacade _TrovoSearchFacade;
        private TrovoQuery _query;
        private Dictionary<string, string> _configSettings;

        [TestInitialize]
        public void SetUp()
        {
            _configSettings = new Dictionary<string, string>();
            _configSettings.Add(GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString(), "20");
            _configSettings.Add(GoogleSiteSearchConfigSettings.RetainProviderFormatting.ToString(), Boolean.FalseString);
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString(), "testClientName");
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderUrl.ToString(), PROVIDER_URL);
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString(), "1234");

            _query = new TrovoQuery();
            _query.SearchTerm = "fred";
            _query.PageNumber = 1;

        }

        [TestCleanup]
        public void TearDown()
        {
            _configSettings = null;
            _query = null;

            _TrovoSearchFacade = null;
        }



        [TestMethod]
        public void SearchFacadeThrowsInvalidArgumentExceptionWhenQueryTextContainsHttp()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.GoogleSiteSearch, LoggerType.NullLogger);
            _TrovoSearchFacade.ConfigSettings = _configSettings;

            _query.SearchTerm = "http://7ro.usa.cock/rss.xml";

            try
            {
                _TrovoSearchFacade.Search(_query, null);
            }
            catch (ArgumentException argEx)
            {
                string expected = "Invalid query error: the query search term contained http or some html tags. This usually occurs because a script has entered a value into the search box. Queries of this sort are rejected as a security measure. If you are genuinely searching for a web address, remove the http from the front and search again.";

                Assert.AreEqual(expected, argEx.Message);
            }

        }

        [TestMethod]
        public void SearchFacadeThrowsInvalidArgumentExceptionWhenQueryTextContainsHtmlTags()
        {
            _TrovoSearchFacade = new TrovoSearchFacade(ProviderType.GoogleSiteSearch, LoggerType.NullLogger);
            _TrovoSearchFacade.ConfigSettings = _configSettings;

            _query.SearchTerm = "<script type='text/javascript'>";

            try
            {
                _TrovoSearchFacade.Search(_query, null);
            }
            catch (ArgumentException argEx)
            {
                string expected = "Invalid query error: the query search term contained http or some html tags. This usually occurs because a script has entered a value into the search box. Queries of this sort are rejected as a security measure. If you are genuinely searching for a web address, remove the http from the front and search again.";

                Assert.AreEqual(expected, argEx.Message);
            }

        }

    }
}
