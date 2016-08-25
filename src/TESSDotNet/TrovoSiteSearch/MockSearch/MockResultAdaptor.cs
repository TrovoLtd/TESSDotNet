using System;
using TrovoSiteSearch;
using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.MockSearch
{
    public class MockResultAdaptor : CleanableDataBlock, ITrovoResult
    {
        private MockObjects.Result _result;

        private int _rankWithinPage;
        public int RankWithinPage 
        { 
            get 
            {
                if (_rankWithinPage > 0)
                {
                    return _rankWithinPage;
                }
                else
                {
                    return _result.RankWithinPage;
                }
            }
            set { _rankWithinPage = value; }
        }

        public string URL
        {
            get { return _result.URL; }
            set { _result.URL = value; }

        }

        public string Title
        {
            get { return FormatContent(_result.Title); }
            set { _result.Title = value; }
        }

        public string Snippet 
        { 
            get { return FormatContent(_result.Snippet); }
            set { _result.Snippet = value; }
        }

        public string FileType
        {
            get
            {
                if (_result.MimeType != null)
                {
                    TrovoMimeTypeConverter mimeTypeConverter = new TrovoMimeTypeConverter();
                    return mimeTypeConverter.convert(_result.MimeType);
                }
                else
                {
                    return _result.MimeType;
                }

            }
            set
            {
                _result.MimeType = value;
            }
        }

        public MockResultAdaptor(MockObjects.Result result)
        {
            _result = result;
        }

    }
}
