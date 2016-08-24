using System;
using System.Text;
using System.Xml;
using System.IO;

namespace TrovoSiteSearch.SP2010Search
{
    public class ResultXMLAggregator
    {
        private string _standardResultsXML;
        private string _specialResultsXML;

        public ResultXMLAggregator(string standardResultsXML, string specialResultsXML)
        {
            // TODO: Complete member initialization
            this._standardResultsXML = standardResultsXML;
            this._specialResultsXML = specialResultsXML;
        }
        
        public Stream aggregateXML()
        {
            XmlDocument outputXML = new XmlDocument();
            outputXML.LoadXml(_standardResultsXML);

            XmlNamespaceManager nsmngr = new XmlNamespaceManager(outputXML.NameTable);
            nsmngr.AddNamespace("rsp", "urn:Microsoft.Search.Response");

            XmlDocument specialResultsXML = new XmlDocument();
            specialResultsXML.LoadXml(_specialResultsXML);

            XmlNodeList specialResults = specialResultsXML.SelectNodes("/Results/SpecialTermResults");

            XmlElement keywordResultsElement = outputXML.CreateElement("KeywordResults", "urn:TrovoSiteSearch.SP2010Search.KeywordResults");
            
            foreach(XmlNode specialResult in specialResults)
            {
                XmlNode importNode = keywordResultsElement.OwnerDocument.ImportNode(specialResult, true);
                keywordResultsElement.AppendChild(importNode);
            }

            XmlNode resultsNode = outputXML.SelectSingleNode("//rsp:Results", nsmngr);

            outputXML.DocumentElement.FirstChild.FirstChild.InsertBefore(keywordResultsElement, resultsNode);

            MemoryStream outputStream = new MemoryStream();

            outputXML.Save(outputStream);

            outputStream.Position = 0;

            return outputStream;

        }
        


    }
}
