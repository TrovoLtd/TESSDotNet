using System;
using System.Xml.Serialization;

namespace TrovoSiteSearch.MockSearch.MockObjects
{
    public class Suggestion
    {
        [XmlAttribute]
        public string q { get; set; }

        [XmlText]
        public string FormattedText { get; set; }

    }
}
