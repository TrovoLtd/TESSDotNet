using System;

using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.GoogleSiteSearch
{
    public class GoogleResult : CleanableDataBlock, ITrovoResult
    {
        public int RankWithinPage { get; set; }

        public string URL  { get; set; }

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

        private string _snippet;
        public string Snippet
        {
            get
            {
                if (String.IsNullOrEmpty(_snippet))
                {
                    return "No description available for this result.";
                }
                else
                {
                    return base.FormatContent(_snippet);
                }
            }
            set
            {
                _snippet = value;
            }
        }

        private string _fileType;

        public string FileType 
        { 
            get 
            { 
                TrovoMimeTypeConverter converter = new TrovoMimeTypeConverter();
                return converter.convert(_fileType);
            
            }  
            set
            {
                _fileType = value;
            }
        }

    }
}
