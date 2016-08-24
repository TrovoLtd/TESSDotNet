using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using TESSReferenceImplementation.HtmlHelpers;
using TESSReferenceImplementation.Models;

namespace TESSReferenceImplementationTests
{
    [TestClass]
    public class PaginationHelperTests
    {
        [TestMethod]
        public void TestLinksGeneratedForOnePage()
        {
            PagingInfo pagingInfo = new PagingInfo { SearchQuery = "test", CurrentPage = 1, TotalPages = 1, NumberOfLinksToDisplay= 1 };

            HtmlHelper myHelper = null;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo);

            string expected = @"<ul id=""ulPageLinkList"">";
            expected += @"<li id=""liFirstPageLink"">";
            expected += @"<a href=""?q=test&amp;pageNo=1"">&lt;&lt;</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks1"">";
            expected += @"<a class=""selected"" href=""?q=test&amp;pageNo=1"">1</a>";
            expected += @"</li>";
            expected += @"<li id=""liLastPageLink"">";
            expected += @"<a href=""?q=test&amp;pageNo=1"">&gt;&gt;</a>";
            expected += @"</li>";
            expected += @"</ul>";

            Assert.AreEqual(expected, result.ToString());

        }

        [TestMethod]
        public void TestLinksGeneratedForThreePages()
        {
            PagingInfo pagingInfo = new PagingInfo { SearchQuery = "test", CurrentPage = 2, TotalPages = 3, NumberOfLinksToDisplay= 3 };

            HtmlHelper myHelper = null;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo);

            string expected = @"<ul id=""ulPageLinkList"">";
            expected += @"<li id=""liFirstPageLink"">";
            expected += @"<a href=""?q=test&amp;pageNo=1"">&lt;&lt;</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks1"">";
            expected += @"<a href=""?q=test&amp;pageNo=1"">1</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks2"">";
            expected += @"<a class=""selected"" href=""?q=test&amp;pageNo=2"">2</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks3"">";
            expected += @"<a href=""?q=test&amp;pageNo=3"">3</a>";
            expected += @"</li>";
            expected += @"<li id=""liLastPageLink"">";
            expected += @"<a href=""?q=test&amp;pageNo=3"">&gt;&gt;</a>";
            expected += @"</li>";
            expected += @"</ul>";

            Assert.AreEqual(expected, result.ToString());

        }

        [TestMethod]
        public void MiddleFiveOfTenPagesDisplayed()
        {
            PagingInfo pagingInfo = new PagingInfo { SearchQuery = "test", CurrentPage = 5, TotalPages = 10, NumberOfLinksToDisplay = 5 };

            HtmlHelper myHelper = null;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo);

            string expected = @"<ul id=""ulPageLinkList"">";
            expected += @"<li id=""liFirstPageLink"">";
            expected += @"<a href=""?q=test&amp;pageNo=1"">&lt;&lt;</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks3"">";
            expected += @"<a href=""?q=test&amp;pageNo=3"">3</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks4"">";
            expected += @"<a href=""?q=test&amp;pageNo=4"">4</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks5"">";
            expected += @"<a class=""selected"" href=""?q=test&amp;pageNo=5"">5</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks6"">";
            expected += @"<a href=""?q=test&amp;pageNo=6"">6</a>";
            expected += @"</li>";
            expected += @"<li id=""liPageLinks7"">";
            expected += @"<a href=""?q=test&amp;pageNo=7"">7</a>";
            expected += @"</li>";
            expected += @"<li id=""liLastPageLink"">";
            expected += @"<a href=""?q=test&amp;pageNo=10"">&gt;&gt;</a>";
            expected += @"</li>";
            expected += @"</ul>";

            Assert.AreEqual(expected, result.ToString());
        }


        // if lower range is below 0

        // if upper range > TotalNumberOfPages

    }
}
