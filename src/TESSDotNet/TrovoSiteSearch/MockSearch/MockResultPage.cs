using System;
using System.Collections.Generic;

using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.MockSearch
{
    public class MockResultPage : ITrovoResultPage 
    {
        public List<ITrovoResult> Results {get; set;}
        public List<ITrovoSpellingSuggestion > SpellingSuggestions { get; set; }
        public List<ITrovoPromotedLink> PromotedLinks { get; set; }
        public List<ITrovoRelatedQuery> RelatedQueries { get; set; }

        public int TotalNumberOfResults { get; set; }
        public int MaxNumberOfResultsPerPage { get; set; }

        public int TotalNumberOfPages
        {
            get
            {
                if (Results.Count == 0)
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
                throw new NotImplementedException();
            }
        }
        public int CurrentPageNumber { get; set; }

        public bool HasResults
        {
            get
            {
                return Results.Count > 0;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool RetainProviderFormatting { get; set; }

        public MockResultPage()
        {
            Results = new List<ITrovoResult>();
        }

    }
}
