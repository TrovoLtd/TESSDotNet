using System.Collections.Generic;

namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoQuery
    {
        System.Collections.Generic.List<ITrovoFilter> Filters { get; set; }
        ITrovoSorter Sorter { get; set; }
        int PageNumber { get; set; }
        string SearchTerm { get; set; }
    }
}
