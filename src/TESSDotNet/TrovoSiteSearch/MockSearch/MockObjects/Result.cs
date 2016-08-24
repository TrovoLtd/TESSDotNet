using System;
using System.Xml.Serialization;

namespace TrovoSiteSearch.MockSearch.MockObjects
{
    public class Result
    {
        [XmlAttribute]
        public int RankWithinPage { get; set; }

        [XmlAttribute]
        public string MimeType { get; set; }

        [XmlElement]
        public string URL { get; set; }

        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public string Snippet { get; set; }

    }
}
