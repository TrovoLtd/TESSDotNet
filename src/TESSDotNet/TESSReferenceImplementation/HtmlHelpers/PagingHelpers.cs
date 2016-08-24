using System;
using System.Text;
using System.Web.Mvc;

using TESSReferenceImplementation.Models;

namespace TESSReferenceImplementation.HtmlHelpers
{
    public static class PagingHelpers
    {

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo)
        {
            StringBuilder result = new StringBuilder();

            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.MergeAttribute("id", "ulPageLinkList");

            TagBuilder firstPageLiTag = new TagBuilder("li");
            firstPageLiTag.MergeAttribute("id", "liFirstPageLink");

            TagBuilder firstPageATag = new TagBuilder("a");
            firstPageATag.MergeAttribute("href", string.Format("?q={0}&pageNo=1", pagingInfo.SearchQuery));
            firstPageATag.InnerHtml = "&lt;&lt;";

            firstPageLiTag.InnerHtml = firstPageATag.ToString();

            ulTag.InnerHtml = firstPageLiTag.ToString();

            int halfMaxPageNumber = (int)Math.Floor((decimal)pagingInfo.NumberOfLinksToDisplay / 2);

            int lowerRange = 0;
            lowerRange = pagingInfo.CurrentPage - halfMaxPageNumber;
            if (lowerRange < 1) lowerRange = 1;
 
            int higherRange = 0;
            higherRange = pagingInfo.CurrentPage + halfMaxPageNumber;
            if(higherRange > pagingInfo.TotalPages) higherRange = pagingInfo.TotalPages;

            for (int i = lowerRange; i <= higherRange ; i++)
            {
                TagBuilder liTag = new TagBuilder("li");

                liTag.MergeAttribute("id", string.Format("liPageLinks{0}", i.ToString()));
                
                TagBuilder aTag = new TagBuilder("a");
                aTag.MergeAttribute("href", string.Format("?q={0}&pageNo={1}", pagingInfo.SearchQuery, i.ToString()));
                aTag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage) aTag.AddCssClass("selected");

                liTag.InnerHtml = aTag.ToString();

                ulTag.InnerHtml += liTag;

            }

            TagBuilder lastPageLiTag = new TagBuilder("li");
            lastPageLiTag.MergeAttribute("id", "liLastPageLink");

            TagBuilder lastPageATag = new TagBuilder("a");
            lastPageATag.MergeAttribute("href", string.Format("?q={0}&pageNo={1}", pagingInfo.SearchQuery, pagingInfo.TotalPages));
            lastPageATag.InnerHtml = "&gt;&gt;";

            lastPageLiTag.InnerHtml = lastPageATag.ToString();

            ulTag.InnerHtml += lastPageLiTag.ToString();

            result.Append(ulTag.ToString());


            return MvcHtmlString.Create(result.ToString());
        }

    }
}