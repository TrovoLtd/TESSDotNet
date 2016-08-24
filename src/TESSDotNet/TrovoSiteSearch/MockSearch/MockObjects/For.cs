using System;
using System.Xml.Serialization;

namespace TrovoSiteSearch.MockSearch.MockObjects
{
    [XmlRootAttribute("For")]
    public class For
    {
        [XmlAttribute]
        public string QueryTerm { get; set; }

        [XmlElement]
        public ResultPage ResultPage { get; set; }

    }
}
