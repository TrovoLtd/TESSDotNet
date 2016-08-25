using TrovoSiteSearch.Enumerations;

namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoFilter
    {
        TrovoFilterMode Mode { get; set; }
        string Name { get; set; }
        string ReadableName { get; set; }
        TrovoFilterType Type { get; set; }
    }
}
