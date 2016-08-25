using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;

namespace TrovoSiteSearchTests
{
    [TestClass]
    public class TrovoSearchProviderFactoryTests
    {
        [TestMethod]
        public void TestFactoryReturnsGoogleSiteSearchRequestWhenProviderTypeIsGSS()
        {
            TrovoSearchRequestFactory searchRequestFactory = new TrovoSearchRequestFactory();
            
            ITrovoSearchRequest request = searchRequestFactory.GetProviderRequest(ProviderType.GoogleSiteSearch);

            Assert.AreEqual("TrovoSiteSearch.GoogleSiteSearch.GoogleSearchRequest", request.GetType().ToString());

        }


        [TestMethod]
        public void TestFactoryReturnsMockRequestWhenProviderTypeIsMock()
        {
            TrovoSearchRequestFactory searchRequestFactory = new TrovoSearchRequestFactory();

            ITrovoSearchRequest request = searchRequestFactory.GetProviderRequest(ProviderType.MockProvider);

            Assert.AreEqual("TrovoSiteSearch.MockSearch.MockSearchRequest", request.GetType().ToString());

        }

    }
}
