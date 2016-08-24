
namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoPromotedLink
    {
        string Title { get; set; }
        string Url { get; set; }
        string Description { get; set; }
        bool RetainProviderFormatting { get; set; }   

    }
}
