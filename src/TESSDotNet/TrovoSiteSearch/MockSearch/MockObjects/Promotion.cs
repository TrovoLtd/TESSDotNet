using System;
using System.Xml.Serialization;

namespace TrovoSiteSearch.MockSearch.MockObjects
{
    public class Promotion
    {
        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public string Url { get; set; }
    }
}
