namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoSpellingSuggestion
    {
        string SuggestedTerm { get; set; }
        string TermForDisplay { get; set; }
        bool RetainProviderFormatting { get; set; }
    }
}
