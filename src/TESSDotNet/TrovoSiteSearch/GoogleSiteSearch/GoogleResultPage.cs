using System;
using System.Collections.Generic;

using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.GoogleSiteSearch
{
    public class GoogleResultPage : ITrovoResultPage
    {
        public List<ITrovoResult> Results { get; set; }

        public List<ITrovoSpellingSuggestion> SpellingSuggestions { get; set; }

        public List<ITrovoPromotedLink> PromotedLinks { get; set; }

        public List<ITrovoRelatedQuery> RelatedQueries { get; set; }

        public bool HasResults {get; set;}

        public bool RetainProviderFormatting { get; set; }
    
        public int TotalNumberOfResults { get; set; }

        private int _totalNumberOfPages;
        public int TotalNumberOfPages
        {
            get
            {
                if (!HasResults)
                {
                    return 0;
                }
                else if (Results.Count == 0)
                {
                    return 0;
                }
                else
                {
                    int remainder;
                    int quotient = Math.DivRem(TotalNumberOfResults, MaxNumberOfResultsPerPage, out remainder);
                    return remainder == 0 ? quotient : quotient + 1;
                }
            }
            set
            {
                _totalNumberOfPages = value;
            }
        }

        public int MaxNumberOfResultsPerPage {get; set;}

        public int CurrentPageNumber { get; set; }

    }
}
