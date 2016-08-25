using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch.GoogleSiteSearch;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class ResultFormattingTests
    {
        private GoogleResult _googleResult;
        
        [TestInitialize]
        public void Setup()
        {
            _googleResult = new GoogleResult();
            _googleResult.Title = "Not So <b>Murder</b> - Trovo"; 
            _googleResult.Snippet = "Not So <b>Murder</b>. Mr Inspector dipped a pen in his inkstand, and deftly laid it on a <br>  piece of paper close beside him; then resumed his former attitude. The stranger <b>...</b>";

        }

        [TestCleanup]
        public void TearDown()
        {
            _googleResult = null;
        }

        [TestMethod]
        public void OpeningBTagsRemovedInSnippet()
        {
            _googleResult.RetainProviderFormatting = false;
            Assert.IsFalse(_googleResult.Snippet.Contains("<b>"));
        }

        [TestMethod]
        public void ClosingBTagsRemovedInSnippet()
        {
            _googleResult.RetainProviderFormatting = false;
            Assert.IsFalse(_googleResult.Snippet.Contains("</b>"));
        }

        [TestMethod]
        public void BRTagsRemovedInSnippet()
        {
            _googleResult.RetainProviderFormatting = false;
            Assert.IsFalse(_googleResult.Snippet.Contains("<br>"));
        }

        [TestMethod]
        public void TestAllGoogleFormattingRemovedFromString()
        {
            _googleResult.RetainProviderFormatting = false;
            Assert.AreEqual("Not So Murder. Mr Inspector dipped a pen in his inkstand, and deftly laid it on a   piece of paper close beside him; then resumed his former attitude. The stranger ...", _googleResult.Snippet);
        }

        [TestMethod]
        public void OpeningBTagsReplacedWithStrongInSnippet()
        {
            _googleResult.RetainProviderFormatting = true;
            Assert.IsTrue(_googleResult.Snippet.Contains("<strong>"));
        }

        [TestMethod]
        public void ClosingBTagsReplacedWithStrongInSnippet()
        {
            _googleResult.RetainProviderFormatting = true;
            Assert.IsTrue(_googleResult.Snippet.Contains("</strong>"));
        }

        [TestMethod]
        public void BRTagReplacedWithXHTMLStandardisedBreakInSnippet()
        {
            _googleResult.RetainProviderFormatting = true;
            Assert.IsTrue(_googleResult.Snippet.Contains("<br />"));
        }

        [TestMethod]
        public void TestAllGoogleFormattingStandardisedWithinString()
        {
            _googleResult.RetainProviderFormatting = true;
            Assert.AreEqual("Not So <strong>Murder</strong>. Mr Inspector dipped a pen in his inkstand, and deftly laid it on a <br />  piece of paper close beside him; then resumed his former attitude. The stranger <strong>...</strong>", _googleResult.Snippet);
        }

        [TestMethod]
        public void TitleAlsoFormatted()
        {
            _googleResult.RetainProviderFormatting = true;
            Assert.AreEqual("Not So <strong>Murder</strong> - Trovo", _googleResult.Title);
        }

        [TestMethod]
        public void EscapedApostropheRemovedFromSnippet()
        {
            _googleResult.Snippet = "Supported with funding under Leicester&#39;s European Structural Funds Programme<br>, the <b>Food</b> Park provides 19,250 sq. ft (1,790 sq.m) of high quality <b>food</b> ...";
            Assert.AreEqual("Supported with funding under Leicester's European Structural Funds Programme, the Food Park provides 19,250 sq. ft (1,790 sq.m) of high quality food ...", _googleResult.Snippet);
        }

    }
}
