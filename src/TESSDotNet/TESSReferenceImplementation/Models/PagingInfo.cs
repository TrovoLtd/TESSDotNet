using System;

namespace TESSReferenceImplementation.Models
{
    public class PagingInfo
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int NumberOfLinksToDisplay { get; set; }
        public string SearchQuery { get; set; }

    }
}