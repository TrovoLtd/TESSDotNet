using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrovoSiteSearch.MockSearch
{
    public abstract class CleanableDataBlock
    {

        public bool RetainProviderFormatting { get; set; }

        protected string FormatContent(string contentToFormat)
        {
            if (RetainProviderFormatting)
            {
                return StandardiseFormatting(contentToFormat);
            }
            else
            {
                return StripFormatting(contentToFormat);
            }

        }

        private string StripFormatting(string contentToStripFormattingFrom)
        {
            string strippedContent = contentToStripFormattingFrom.Replace("<b>", "");
            strippedContent = strippedContent.Replace("</b>", "");
            strippedContent = strippedContent.Replace("<i>", "");
            strippedContent = strippedContent.Replace("</i>", "");
            strippedContent = strippedContent.Replace("<em>", "");
            strippedContent = strippedContent.Replace("</em>", "");
            strippedContent = strippedContent.Replace("<br>", "");
            return strippedContent;
        }

        private string StandardiseFormatting(string contentToStandardise)
        {
            string standardisedContent = contentToStandardise.Replace("<b>", "<strong>");
            standardisedContent = standardisedContent.Replace("</b>", "</strong>");
            standardisedContent = standardisedContent.Replace("<i>", "<em>");
            standardisedContent = standardisedContent.Replace("</i>", "</em>");
            standardisedContent = standardisedContent.Replace("<br>", "<br/>");
            return standardisedContent;
        }

    }
}
