using System;
using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.MockSearch
{
    public class MockSpellingSuggestionAdaptor: CleanableDataBlock, ITrovoSpellingSuggestion
    {
        private MockObjects.Suggestion _suggestion;
        
        public MockSpellingSuggestionAdaptor(MockObjects.Suggestion suggestion)
        {
            _suggestion = suggestion;
        }
        
        public string SuggestedTerm
        {
            get { return base.FormatContent(_suggestion.q); }
            set { throw new NotImplementedException(); }
        }

        public string TermForDisplay
        {
            get { return base.FormatContent(_suggestion.FormattedText); }
            set { throw new NotImplementedException(); }
        }

    }
}
