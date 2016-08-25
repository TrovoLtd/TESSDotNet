using System;
using System.Xml.Serialization;

namespace TrovoSiteSearch.MockSearch.MockObjects
{
    public class ResultPage
    {
        //TODO: add start and end numbers in here (when you do the pagination)

        [XmlElement]
        public Result[] Result { get; set; }

        [XmlElement]
        public Spelling Spelling { get; set; }

        [XmlElement]
        public Promotion[] Promotion { get; set; }

    }
}
