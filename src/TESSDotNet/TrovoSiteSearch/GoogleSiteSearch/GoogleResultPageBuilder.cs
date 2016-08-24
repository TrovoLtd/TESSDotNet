using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml;
using System.Diagnostics;


using TrovoSiteSearch.Interfaces;

using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

namespace TrovoSiteSearch.GoogleSiteSearch
{
    public class GoogleResultPageBuilder : TrovoLoggable 
    {
        private GoogleResultPage _resultPage;

        public GoogleResultPageBuilder() { }

        public GoogleResultPageBuilder(ILogWriter logWriter)
        {
            base.LogWriter = logWriter;
        }

        public GoogleResultPageBuilder(bool _retainProviderFormatting) 
        {
            RetainProviderFormatting = _retainProviderFormatting;
        }

        public bool RetainProviderFormatting { get; set; }

        public GoogleResultPage BuildResultPage(Stream xmlStream)
        {
            base.generateLogEntry("XML about to be parsed",
                "The GoogleResultPageBuilder is about to try and parse the XML stream from Google.",
                9005,
                TrovoLoggingCategory.DebugTrace.ToString(),
                (int)TrovoLoggingPriority.DiagnosticInfo,
                TraceEventType.Verbose);
            
            _resultPage = new GoogleResultPage();

            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.DtdProcessing = DtdProcessing.Ignore;
            readerSettings.ConformanceLevel = ConformanceLevel.Document;

            XmlReader reader;

            try
            { 
                using(reader = XmlReader.Create(xmlStream, readerSettings))
                {
                    while(reader.Read())
                    {
                        if(reader.IsStartElement())
                        {
                            switch(reader.Name)
                            {
                                case "M":
                                    _resultPage.TotalNumberOfResults = reader.ReadElementContentAsInt();
                                    _resultPage.HasResults = _resultPage.TotalNumberOfResults > 0;
                                    break;

                                case "R":
                                    XmlDocument resultDoc = new XmlDocument();
                                    BuildResult(resultDoc.ReadNode(reader));
                                    break;

                                case "Spelling":
                                    XmlDocument spellingSuggestionDoc = new XmlDocument();
                                    AddSpellingSuggestion(spellingSuggestionDoc.ReadNode(reader));
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            
            }
            catch(XmlException xmlEx)
            {
                LogXmlException(xmlEx);
                throw xmlEx;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            base.generateLogEntry("XML parsed successfully",
                "The GoogleResultPageBuilder successfully parsed the XML stream from Google into a set of results",
                9006,
                TrovoLoggingCategory.DebugTrace.ToString(),
                (int)TrovoLoggingPriority.DiagnosticInfo,
                TraceEventType.Verbose);

            return _resultPage;
        }

        /// <summary>
        /// <para>This method is a little more complicated than necessary because Google's XML returns promotion info in an extra SL_RESULTS element child of a standard result element.</para>
        /// <para>So the code has to check if that exists and if so, handle the result as a promotion, not a standard result.</para>
        /// </summary>
        /// <param name="resultPageNode">The result node read from the XML Reader</param>

        private void BuildResult(XmlNode resultPageNode)
        {
            if (_resultPage.Results == null) _resultPage.Results = new List<ITrovoResult>();

            XmlNode promotionNode = resultPageNode.SelectSingleNode("SL_RESULTS/SL_MAIN");

            if(promotionNode != null)
            {
                BuildPromotedLink(promotionNode);
            }
            else
            {
                GoogleResult result = new GoogleResult();
                result.RankWithinPage = resultPageNode.Attributes["N"] == null ? 0: Int32.Parse(resultPageNode.Attributes["N"].Value);
                result.FileType = resultPageNode.Attributes["MIME"] == null ? string.Empty : resultPageNode.Attributes["MIME"].Value;
                result.RetainProviderFormatting = RetainProviderFormatting;
                result.Title = resultPageNode.SelectSingleNode("T") == null ? "Untitled" : resultPageNode.SelectSingleNode("T").InnerText;
                result.URL = resultPageNode.SelectSingleNode("U")== null ? string.Empty : resultPageNode.SelectSingleNode("U").InnerText;
                result.Snippet = resultPageNode.SelectSingleNode("S") == null ? string.Empty : resultPageNode.SelectSingleNode("S").InnerText;
                _resultPage.Results.Add(result);
            }

        }


        private void BuildPromotedLink(XmlNode promotionNode)
        {
            if (_resultPage.PromotedLinks == null) _resultPage.PromotedLinks = new List<ITrovoPromotedLink>();

            GooglePromotedLink promotedLink = new GooglePromotedLink();

            promotedLink.RetainProviderFormatting = RetainProviderFormatting;

            promotedLink.Title = promotionNode.SelectSingleNode("T").InnerXml;
            promotedLink.Url = promotionNode.SelectSingleNode("U").InnerText;

            if (promotionNode.SelectSingleNode("BODY_LINE/BLOCK") != null)
            {
                promotedLink.Description = promotionNode.SelectSingleNode("BODY_LINE/BLOCK/T").InnerText;
            }

            _resultPage.PromotedLinks.Add(promotedLink);

        }

        private void AddSpellingSuggestion(XmlNode spellingSuggestionNode)
        {
            if (_resultPage.SpellingSuggestions == null) _resultPage.SpellingSuggestions = new List<ITrovoSpellingSuggestion>();

            GoogleSpellingSuggestion spellingSuggestion = new GoogleSpellingSuggestion();
            spellingSuggestion.RetainProviderFormatting = RetainProviderFormatting;

            spellingSuggestion.SuggestedTerm = spellingSuggestionNode.SelectSingleNode("Suggestion").Attributes["q"].Value;
            spellingSuggestion.TermForDisplay = spellingSuggestionNode.SelectSingleNode("Suggestion").InnerText;

            _resultPage.SpellingSuggestions.Add(spellingSuggestion);
        }

        private void LogXmlException(XmlException xmlEx)
        {
            string title, exceptionMessage, message;

            title = "An XML Parsing Error occurred";

            exceptionMessage = xmlEx.ToString();

            message = String.Format("{0} ... caused by error in XML at Line {1} Position {2}", exceptionMessage, xmlEx.LineNumber, xmlEx.LinePosition);

            int errorId = 1504;

            base.generateLogEntry(title,
                                message,
                                errorId,
                                TrovoLoggingCategory.ErrorUnrecoverable.ToString(),
                                (int)TrovoLoggingPriority.Critical,
                                TraceEventType.Error);
        }

    }
}
