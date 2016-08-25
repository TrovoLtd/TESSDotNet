using System;

using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;


namespace TrovoSiteSearch
{
    public class TrovoSorter : ITrovoSorter
    {
        public string Name { get; set; }
        public string ReadableName { get; set; }
        public TrovoSorterType Type { get; set; }
        public TrovoSorterMode Mode { get; set; }

    }
}
