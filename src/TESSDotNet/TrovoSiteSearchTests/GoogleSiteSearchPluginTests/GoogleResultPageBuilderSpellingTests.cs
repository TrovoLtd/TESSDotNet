using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using TrovoSiteSearch.GoogleSiteSearch;
using TrovoCrossCutting.Logging;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.NullLogger;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class GoogleResultPageBuilderSpellingTests
    {
        private GoogleResultPageBuilder _pageBuilder;
        private FileStream _fs;
        
        [TestInitialize]
        public void SetUp()
        {
            _pageBuilder = new GoogleResultPageBuilder();
            _pageBuilder.LogWriter = new LogWriterAdaptor();
        }

        [TestCleanup]
        public void TearDown()
        {
            _pageBuilder = null;
            if (_fs.CanRead) _fs.Close();
        }

        [TestMethod]
        public void IndividualWordTarfficReturnsTrafficAsSuggestedTerm()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\spellingMistakeTarffic.xml";

            _fs = File.OpenRead(xmlFilePath);
            GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);

            Assert.AreEqual("traffic", result.SpellingSuggestions[0].SuggestedTerm);
        }

        [TestMethod]
        public void IndividualWordTarfficReturnsTrafficFormattedAsTermForDisplay()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\spellingMistakeTarffic.xml";

            _fs = File.OpenRead(xmlFilePath);
            _pageBuilder.RetainProviderFormatting = true;
            GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);

            Assert.AreEqual("<strong><em>traffic</em></strong>", result.SpellingSuggestions[0].TermForDisplay);
        }

        [TestMethod]
        public void IndividualWordTarfficHasFormattingStrippedFromTermForDisplay()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\spellingMistakeTarffic.xml";

            _fs = File.OpenRead(xmlFilePath);
            _pageBuilder.RetainProviderFormatting = false;
            GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);

            Assert.AreEqual("traffic", result.SpellingSuggestions[0].TermForDisplay);
        }

        [TestMethod]
        public void PhraseTarfficJamReturnsTrafficJamAsSuggestedTerm()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\spellingMistakeTarfficJam.xml";

            _fs = File.OpenRead(xmlFilePath);
            GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);

            Assert.AreEqual("traffic jam", result.SpellingSuggestions[0].SuggestedTerm);
        }

        [TestMethod]
        public void PhraseTarfficJamReturnsFormattedTrafficJamAsTermForDisplay()
        {
            string xmlFilePath = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\spellingMistakeTarfficJam.xml";

            _fs = File.OpenRead(xmlFilePath);
            _pageBuilder.RetainProviderFormatting = true;
            GoogleResultPage result = _pageBuilder.BuildResultPage(_fs);

            Assert.AreEqual("<strong><em>traffic</em></strong> jam", result.SpellingSuggestions[0].TermForDisplay);
        }

    }
}
