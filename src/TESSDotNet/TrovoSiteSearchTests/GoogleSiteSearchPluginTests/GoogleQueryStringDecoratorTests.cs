using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrovoSiteSearch.GoogleSiteSearch;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class TrovoQueryStringDecoratorTests
    {
        [TestMethod]
        public void CSE_ID_AppendedTo_BaseQueryString()
        {
            GoogleQueryStringDecorator baseQueryString = new GoogleQueryStringDecorator() { ParameterName = GoogleSearchQueryParameterName.client, ParameterValue = "google-csbe" };

            GoogleQueryStringDecorator  queryStringDecorator = new GoogleQueryStringDecorator(baseQueryString);
            queryStringDecorator.ParameterName = GoogleSearchQueryParameterName.cx;
            queryStringDecorator.ParameterValue = "1234567890";

            Assert.AreEqual("client=google-csbe&cx=1234567890", queryStringDecorator.GenerateQueryString());
        }

        [TestMethod]
        public void Query_And_CSE_ID_AppendedTo_BaseQueryString()
        {
            GoogleQueryStringDecorator baseQueryString = new GoogleQueryStringDecorator() { ParameterName = GoogleSearchQueryParameterName.client, ParameterValue = "google-csbe" };

            GoogleQueryStringDecorator queryStringDecorator = new GoogleQueryStringDecorator(baseQueryString);
            queryStringDecorator.ParameterName = GoogleSearchQueryParameterName.cx;
            queryStringDecorator.ParameterValue = "1234567890";

            GoogleQueryStringDecorator queryStringWithQuery = new GoogleQueryStringDecorator(queryStringDecorator);
            queryStringWithQuery.ParameterName = GoogleSearchQueryParameterName.q;
            queryStringWithQuery.ParameterValue = "horseflesh";

            Assert.AreEqual("client=google-csbe&cx=1234567890&q=horseflesh", queryStringWithQuery.GenerateQueryString());
        }



    }

}
