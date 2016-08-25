using System;
using System.Xml.Serialization;

namespace TrovoSiteSearch.MockSearch.MockObjects
{
    public class Spelling
    {
        [XmlElement]
        public Suggestion[] Suggestion { get; set; }

    }
}
