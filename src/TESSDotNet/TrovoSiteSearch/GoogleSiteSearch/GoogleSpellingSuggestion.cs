using System;

using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.GoogleSiteSearch
{
    public class GoogleSpellingSuggestion : CleanableDataBlock, ITrovoSpellingSuggestion
    {
        public string SuggestedTerm { get; set; }

        private string _termForDisplay;
        public string TermForDisplay
        {
            get
            {
                return FormatContent(_termForDisplay);
            }
            set
            {
                _termForDisplay = value;
            }
        }

    }
}
