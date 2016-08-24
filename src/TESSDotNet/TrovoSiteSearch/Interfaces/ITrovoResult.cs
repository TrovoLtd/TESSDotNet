
namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoResult
    {
        int RankWithinPage { get; set; }
        string URL { get; set; }
        string Title { get; set; }
        string Snippet { get; set; }
        string FileType { get; set; }
        bool RetainProviderFormatting { get; set; }

    }
}
