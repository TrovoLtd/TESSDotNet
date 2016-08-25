using System;
using System.Collections.Generic;

using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch
{
    public class TrovoQuery : ITrovoQuery 
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public List<ITrovoFilter> Filters { get; set; }
        public ITrovoSorter Sorter { get; set; }

        public TrovoQuery()
        {
            Filters = new List<ITrovoFilter>();
        }
    }
}
