using System;

using TrovoSiteSearch.Interfaces;
using TrovoSiteSearch.Enumerations;

namespace TrovoSiteSearch
{
    public class TrovoFilter : ITrovoFilter 
    {
        public string Name { get; set; }
        public string ReadableName { get; set; }
        public TrovoFilterType Type { get; set; }
        public TrovoFilterMode  Mode { get; set; }

    }
}
