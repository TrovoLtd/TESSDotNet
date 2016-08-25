using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;
using TrovoSiteSearch.GoogleSiteSearch;

using System.Web;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class GoogleQueryStringBuilderWithFilteredQueriesTests
    {
        private TrovoQuery _trovoQuery;
        private Dictionary<string, string> _configSettings;

        [TestInitialize]
        public void SetUp()
        {
            _configSettings = new Dictionary<string, string>();
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderAccountId.ToString(), "002206009437565740511%3Am1b764ynfvk");
            _configSettings.Add(GoogleSiteSearchConfigSettings.NumberOfResultsPerPage.ToString(), "10");
            _configSettings.Add(GoogleSiteSearchConfigSettings.SearchProviderClientName.ToString(), "google-csbe");

            _trovoQuery = new TrovoQuery();
            
            _trovoQuery.PageNumber = 1;
            _trovoQuery.SearchTerm = "christmas";
        }

        [TestCleanup]
        public void TearDown()
        {
            _trovoQuery = null;
        }

        [TestMethod]
        public void LabelFilterInclude()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "whatson", ReadableName = "What's on", Type = TrovoFilterType.ByURLCollectionLabel, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas+more:whatson&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void LabelFilterExclude()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "whatson", ReadableName = "What's on", Type = TrovoFilterType.ByURLCollectionLabel, Mode = TrovoFilterMode.Exclude };
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas+less:whatson&num=10&start=0", testDecorator.GenerateQueryString());
        }

        /// <summary>
        /// Google Site Search does not allow for two "more" label filters, so this is invalid
        /// Add a validator class and an exception class at some point soon 
        /// </summary>

        [TestMethod]
        public void TwoLabelFilters()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "whatson", ReadableName = "What's on", Type = TrovoFilterType.ByURLCollectionLabel, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter1);

            TrovoFilter testFilter2 = new TrovoFilter() { Name = "cabinet", ReadableName = "Cabinet papers", Type = TrovoFilterType.ByURLCollectionLabel, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter2);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas+more:whatson+more:cabinet&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void FiltetypeFilterInclude()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "pdf", ReadableName = "PDF", Type = TrovoFilterType.ByDocumentType, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas+filetype:pdf&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void FiltetypeFilterExclude()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "pdf", ReadableName = "PDF", Type = TrovoFilterType.ByDocumentType, Mode = TrovoFilterMode.Exclude };
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas+-filetype:pdf&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void OneIncludedFiletypeAndOneExludedLabelFilters()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "doc", ReadableName = "DOC", Type = TrovoFilterType.ByDocumentType, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter1);

            TrovoFilter testFilter2 = new TrovoFilter() { Name = "cabinet", ReadableName = "Cabinet papers", Type = TrovoFilterType.ByURLCollectionLabel, Mode = TrovoFilterMode.Exclude };
            _trovoQuery.Filters.Add(testFilter2);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas+filetype:doc+less:cabinet&num=10&start=0", testDecorator.GenerateQueryString());
        }
        
        [TestMethod]
        public void IntitleFilter()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "intitle", ReadableName = "In Title Only", Type = TrovoFilterType.InDocumentTitle, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery,_configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=intitle:christmas&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void InTitleAndFiletypeFilters()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "doc", ReadableName = "DOC", Type = TrovoFilterType.ByDocumentType, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter1);

            TrovoFilter testFilter2 = new TrovoFilter() { Name = "intitle", ReadableName = "In title", Type = TrovoFilterType.InDocumentTitle, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter2);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=intitle:christmas+filetype:doc&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void AllintitleFilter()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "intitle", ReadableName = "In Title Only", Type = TrovoFilterType.InDocumentTitle, Mode = TrovoFilterMode.UseAllTerms };
            _trovoQuery.SearchTerm = "social+care";
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=allintitle:social+care&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void InurlFilter()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "inurl", ReadableName = "In Page URL", Type = TrovoFilterType.InDocumentUrl, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=inurl:christmas&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void AllinurlFilter()
        {
            TrovoFilter testFilter = new TrovoFilter() { Name = "inurl", ReadableName = "In Page URL", Type = TrovoFilterType.InDocumentUrl, Mode = TrovoFilterMode.UseAllTerms };
            _trovoQuery.Filters.Add(testFilter);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=allinurl:christmas&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void InUrlAndInTitleFilters()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "inurl", ReadableName = "In Url Only", Type = TrovoFilterType.InDocumentUrl, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter1);

            TrovoFilter testFilter2 = new TrovoFilter() { Name = "intitle", ReadableName = "In title", Type = TrovoFilterType.InDocumentTitle, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter2);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=inurl:christmas+intitle:christmas&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void InTitleAndInUrlFilters()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "intitle", ReadableName = "In Title Only", Type = TrovoFilterType.InDocumentTitle, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter1);

            TrovoFilter testFilter2 = new TrovoFilter() { Name = "inurl", ReadableName = "In Url Only", Type = TrovoFilterType.InDocumentUrl, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter2);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=intitle:christmas+inurl:christmas&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void OneInTitleOneInUrlOneFiletypeAndOneLabelFilters()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "intitle", ReadableName = "In Title Only", Type = TrovoFilterType.InDocumentTitle, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter1);

            TrovoFilter testFilter2 = new TrovoFilter() { Name = "inurl", ReadableName = "In Url Only", Type = TrovoFilterType.InDocumentUrl, Mode = TrovoFilterMode.UseSingleTerm };
            _trovoQuery.Filters.Add(testFilter2);

            TrovoFilter testFilter3 = new TrovoFilter() { Name = "pdf", ReadableName = "PDF", Type = TrovoFilterType.ByDocumentType, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter3);

            TrovoFilter testFilter4 = new TrovoFilter() { Name = "whatson", ReadableName = "What's on?", Type = TrovoFilterType.ByURLCollectionLabel, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter4);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=intitle:christmas+inurl:christmas+filetype:pdf+more:whatson&num=10&start=0", testDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void DefaultGoogleFiltersExcluded()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "defaultFilters", ReadableName = "Default Filters", Type = TrovoFilterType.ProviderDefaults, Mode = TrovoFilterMode.Exclude};
            _trovoQuery.Filters.Add(testFilter1);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas&filter=0&num=10&start=0", testDecorator.GenerateQueryString());

        }

        [TestMethod]
        public void DefaultGoogleFiltersSwitchedOn()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "defaultFilters", ReadableName = "Default Filters", Type = TrovoFilterType.ProviderDefaults, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter1);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas&num=10&start=0", testDecorator.GenerateQueryString());

        }

        [TestMethod]
        public void SafeSearchSwitchedOn()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "safeSearchOn", ReadableName = "Safe Search On", Type = TrovoFilterType.AdultContent, Mode = TrovoFilterMode.Exclude };
            _trovoQuery.Filters.Add(testFilter1);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas&safe=high&num=10&start=0", testDecorator.GenerateQueryString());

        }

        [TestMethod]
        public void SafeSearchSwitchedOff()
        {
            TrovoFilter testFilter1 = new TrovoFilter() { Name = "safeSearchOff", ReadableName = "Safe Search Off", Type = TrovoFilterType.AdultContent, Mode = TrovoFilterMode.Include };
            _trovoQuery.Filters.Add(testFilter1);

            GoogleQueryStringBuilder queryStringBuilder = new GoogleQueryStringBuilder(_trovoQuery, _configSettings);
            GoogleQueryStringDecorator testDecorator = queryStringBuilder.BuildQuery();
            Assert.AreEqual("client=google-csbe&output=xml_no_dtd&cx=002206009437565740511%3Am1b764ynfvk&q=christmas&safe=off&num=10&start=0", testDecorator.GenerateQueryString());

        }

    }
}
