using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrovoSiteSearch.GoogleSiteSearch
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
            string strippedContent = String.Empty;
            
            if(!String.IsNullOrEmpty(contentToStripFormattingFrom))
            {             
                strippedContent = contentToStripFormattingFrom.Replace("\n", "");
                strippedContent = strippedContent.Trim();
                strippedContent = strippedContent.Replace("<b>", "");
                strippedContent = strippedContent.Replace("</b>", "");
                strippedContent = strippedContent.Replace("<i>", "");
                strippedContent = strippedContent.Replace("</i>", "");
                strippedContent = strippedContent.Replace("<em>", "");
                strippedContent = strippedContent.Replace("</em>", "");
                strippedContent = strippedContent.Replace("<br>", "");
                strippedContent = strippedContent.Replace("&#39;", "'");
            }

            return strippedContent;
        }

        private string StandardiseFormatting(string contentToStandardise)
        {
            string standardisedContent = string.Empty;
            
            if(!String.IsNullOrEmpty(contentToStandardise))
            {
                standardisedContent = contentToStandardise.Replace("\n", "");
                standardisedContent = standardisedContent.Trim();
                standardisedContent = standardisedContent.Replace("<b>", "<strong>");
                standardisedContent = standardisedContent.Replace("</b>", "</strong>");
                standardisedContent = standardisedContent.Replace("<i>", "<em>");
                standardisedContent = standardisedContent.Replace("</i>", "</em>");
                standardisedContent = standardisedContent.Replace("<br>", "<br />");
                standardisedContent = standardisedContent.Replace("&#39;", "'");
            }
            return standardisedContent;
        }

    }
}
