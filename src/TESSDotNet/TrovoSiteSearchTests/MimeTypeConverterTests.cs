using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TrovoSiteSearch;

namespace TessaAPITests
{
    [TestClass]
    public class MimeTypeConverterTests
    {
        private TrovoMimeTypeConverter _converter;

        [TestInitialize]
        public void SetUp()
        {
            _converter = new TrovoMimeTypeConverter();
        }
        
        [TestMethod]
        public void TestApplicationPDFMimeTypeReturnsPDF()
        {
            Assert.AreEqual("PDF", _converter.convert("application/pdf"));
        }

        [TestMethod]
        public void TestApplicationMSWordReturnsMSWord()
        {
            Assert.AreEqual("MS WORD", _converter.convert("application/msword"));
        }

        [TestMethod]
        public void TestApplicationMSWordDocxReturnsMSWord()
        {
            Assert.AreEqual("MS WORD", _converter.convert("application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
        }

        [TestMethod]
        public void TestApplicationMSWordDocmReturnsMSWord()
        {
            Assert.AreEqual("MS WORD", _converter.convert("application/vnd.ms-word.document.macroEnabled.12"));
        }

        [TestMethod]
        public void TestApplicationMSWordDotxReturnsMSWord()
        {
            Assert.AreEqual("MS WORD", _converter.convert("application/vnd.openxmlformats-officedocument.wordprocessingml.template"));
        }

        [TestMethod]
        public void TestApplicationMSWordDotmReturnsMSWord()
        {
            Assert.AreEqual("MS WORD", _converter.convert("application/vnd.ms-word.template.macroEnabled.12"));
        }

        [TestMethod]
        public void TestConverterIgnoresCaseOfMimeType()
        {
            Assert.AreEqual("MS WORD", _converter.convert("Application/MSWord"));
        }


        [TestMethod]
        public void TestApplicationPostscriptReturnsPostScript()
        {
            Assert.AreEqual("POSTSCRIPT", _converter.convert("application/postscript"));
        }

        [TestMethod]
        public void TestApplicationExcelReturnsMSExcel()
        {
            Assert.AreEqual("MS EXCEL", _converter.convert("application/excel"));
        }

        [TestMethod]
        public void TestApplicationVNDMSExcelReturnsMSExcel()
        {
            Assert.AreEqual("MS EXCEL", _converter.convert("application/vnd.ms-excel"));
        }

        [TestMethod]
        public void TestApplicationXLSXReturnsMSExcel()
        {
            Assert.AreEqual("MS EXCEL", _converter.convert("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
        }

        [TestMethod]
        public void TestApplicationXLSMReturnsMSExcel()
        {
            Assert.AreEqual("MS EXCEL", _converter.convert("application/vnd.ms-excel.sheet.macroEnabled.12"));
        }

        [TestMethod]
        public void TestApplicationXLTXReturnsMSExcel()
        {
            Assert.AreEqual("MS EXCEL", _converter.convert("application/vnd.openxmlformats-officedocument.spreadsheetml.template"));
        }

        [TestMethod]
        public void TestApplicationXLTMReturnsMSExcel()
        {
            Assert.AreEqual("MS EXCEL", _converter.convert("application/vnd.ms-excel.template.macroEnabled.12"));
        }

        [TestMethod]
        public void TestApplicationMsPowerPointReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/mspowerpoint"));
        }

        [TestMethod]
        public void TestApplicationVndPowerPointReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/vnd.ms-powerpoint"));
        }

        [TestMethod]
        public void TestApplicationPPTXReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/vnd.openxmlformats-officedocument.presentationml.presentation"));
        }

        [TestMethod]
        public void TestApplicationPPTMReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/vnd.ms-powerpoint.presentation.macroEnabled.12"));
        }

        [TestMethod]
        public void TestApplicationPPSXReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/vnd.openxmlformats-officedocument.presentationml.slideshow"));
        }

        [TestMethod]
        public void TestApplicationPPSMReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/vnd.ms-powerpoint.slideshow.macroEnabled.12"));
        }

        [TestMethod]
        public void TestApplicationPOTXReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/vnd.openxmlformats-officedocument.presentationml.template"));
        }

        [TestMethod]
        public void TestApplicationPOTMReturnsMSPowerpoint()
        {
            Assert.AreEqual("MS POWERPOINT", _converter.convert("application/vnd.ms-powerpoint.template.macroEnabled.12"));
        }

        [TestMethod]
        public void TestApplicationRTFReturnsRichTextFormat()
        {
            Assert.AreEqual("RICH TEXT FORMAT", _converter.convert("application/rtf"));
        }

        [TestMethod]
        public void TestApplicationXRTFReturnsRichTextFormat()
        {
            Assert.AreEqual("RICH TEXT FORMAT", _converter.convert("application/x-rtf"));
        }

        [TestMethod]
        public void TestApplicationODTReturnsOpenOfficeDocument()
        {
            Assert.AreEqual("OPEN OFFICE DOCUMENT", _converter.convert("application/vnd.oasis.opendocument.text"));
        }

        [TestMethod]
        public void TestApplicationODPReturnsOpenOfficePresentation()
        {
            Assert.AreEqual("OPEN OFFICE PRESENTATION", _converter.convert("application/vnd.oasis.opendocument.presentation"));
        }

        [TestMethod]
        public void TestApplicationODSReturnsOpenOfficeSpreadsheet()
        {
            Assert.AreEqual("OPEN OFFICE SPREADSHEET", _converter.convert("application/vnd.oasis.opendocument.spreadsheet"));
        }


        [TestMethod]
        public void TestApplicationPantsReturnsEmptyString()
        {
            Assert.AreEqual(String.Empty, _converter.convert("application/pants"));
        }



    }
}
