using System.Collections.Generic;

namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoResultPage
    {
        List<ITrovoResult> Results { get; set; }
        List<ITrovoSpellingSuggestion> SpellingSuggestions { get; set; }
        List<ITrovoPromotedLink> PromotedLinks { get; set; }
        List<ITrovoRelatedQuery> RelatedQueries { get; set; }

        bool HasResults { get; set;  }
        bool RetainProviderFormatting { get; set; }
        int TotalNumberOfResults { get; set; }
        int TotalNumberOfPages { get; set; }
        int MaxNumberOfResultsPerPage { get; set; }
        int CurrentPageNumber { get; set; }
    }
}
