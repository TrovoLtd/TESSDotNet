using System;
using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.MockSearch
{
    public class MockPromotedLinkAdaptor : CleanableDataBlock, ITrovoPromotedLink 
    {

        private MockObjects.Promotion _promotion;

        public MockPromotedLinkAdaptor(MockObjects.Promotion promotion)
        {
            _promotion = promotion;
        }

        public string Title
        {
            get { return base.FormatContent(_promotion.Title); }
            set { throw new NotImplementedException(); }
        }

        public string Url
        {
            get { return base.FormatContent(_promotion.Url); }
            set { throw new NotImplementedException(); }
        }

        public string Description {get; set;}

    }
}
