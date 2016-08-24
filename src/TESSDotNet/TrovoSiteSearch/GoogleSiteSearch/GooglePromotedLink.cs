using System;

using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.GoogleSiteSearch
{
    public class GooglePromotedLink : CleanableDataBlock, ITrovoPromotedLink
    {
        private string _title;
        public string Title 
        { 
            get
            {
                return FormatContent(_title);
            }
            set
            {
                _title = value;
            }
        }

        public string Url { get; set; }

        private string _description;
        public string Description
        {
            get
            {
                return FormatContent(_description);
            }
            set
            {
                _description = value;
            }
        }

    }
}
