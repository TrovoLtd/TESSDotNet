using TrovoSiteSearch.Enumerations;

namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoSorter
    {
        TrovoSorterMode Mode { get; set; }
        string Name { get; set; }
        string ReadableName { get; set; }
        TrovoSorterType Type { get; set; }
    }
}
