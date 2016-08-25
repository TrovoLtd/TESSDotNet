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
    public class GoogleResultPageBuilderTests
    {

        private const string XML_FILE_PATH = @"GoogleSiteSearchPluginTests\GoogleXMLOutput\queryForMurder.xml";
        private FileStream _fileStream;
        private GoogleResultPageBuilder _pageBuilder;
        private GoogleResultPage _result;


        [TestInitialize]
        public void SetUp()
        {
            _fileStream = File.OpenRead(XML_FILE_PATH);
            _pageBuilder = new GoogleResultPageBuilder();
            _pageBuilder.LogWriter = new LogWriterAdaptor();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (_fileStream.CanRead) _fileStream.Close();
        }

        [TestMethod]
        public void WhenTheResultsForMurderXMLStreamIsPassedToTheGoogleResultPageBuilderTheTotalNumberOfResultsIsTen()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual(10, _result.TotalNumberOfResults);
        }

        [TestMethod]
        public void WhenTheResultsForMurderXMLStreamIsPassedToTheGoogleResultPageBuilderTheResultPageHasResults()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.IsTrue(_result.HasResults);
        }

        [TestMethod]
        public void WhenTheResultsForMurderXMLStreamIsPassedToTheGoogleResultPageBuilderTheTitleOfTheFirstResultIsMurderWasFoundOut()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual("Murder Was Found Out - Trovo", _result.Results[0].Title);
        }

        [TestMethod]
        public void WhenTheResultsForMurderXMLStreamIsPassedToTheGoogleResultPageBuilderTheRankWithinPageOfTheThirdResultIsThree()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual(3, _result.Results[2].RankWithinPage);
        }
        
        [TestMethod]
        public void FirstResultURLIsMurder_Was_Found_Out_aspx()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual("http://www.trovo.co.uk/DemoContent/Our_Mutual_Friend/Murder_Was_Found_Out.aspx", _result.Results[0].URL);
        }

        // snippet with provider formatting removed

        [TestMethod]
        public void FirstSnippetIsIt_was_on_the_night_when_the_Harmon_etc_FormattingRemoved()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            string expected = "'It was on the night when the Harmon murder was found out, through father, just   above bridge. And just below bridge, as we were sculling home, Riderhood ...";
            Assert.AreEqual(expected, _result.Results[0].Snippet);
        }

        // snippet with provider formatting retained

        [TestMethod]
        public void FirstSnippetIsIt_was_on_the_night_when_the_Harmon_etc_FormattingRetained()
        {
            GoogleResultPageBuilder builder = new GoogleResultPageBuilder(true);
            builder.LogWriter =  new LogWriterAdaptor();
            GoogleResultPage rp = builder.BuildResultPage(_fileStream);
            string expected = "'It was on the night when the Harmon <strong>murder</strong> was found out, through father, just <br />  above bridge. And just below bridge, as we were sculling home, Riderhood <strong>...</strong>";
            Assert.AreEqual(expected, rp.Results[0].Snippet);
        }

        // check the fifth result

        [TestMethod]
        public void WhenTheResultsForMurderXMLStreamIsPassedToTheGoogleResultPageBuilderTheTitleOfTheFifthResultIsAsWeCameOutOfThePrison()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual("As We Came Out Of The Prison - Trovo", _result.Results[4].Title);
        }

        // check the tenth result
        [TestMethod]
        public void WhenTheResultsForMurderXMLStreamIsPassedToTheGoogleResultPageBuilderTheTitleOfTheTenthResultIsUntilGovernmentShallAbolishHim()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual("Until Government Shall Abolish Him", _result.Results[9].Title);
        }

        [TestMethod]
        public void TestResultWithoutATitle()
        {
            GoogleResultPage result;

            using (FileStream fs = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\containsResultWithMissingFields.xml"))
            {
                GoogleResultPageBuilder builder = new GoogleResultPageBuilder();
                builder.LogWriter = new LogWriterAdaptor();
                result = builder.BuildResultPage(fs);
            }

            Assert.AreEqual("Untitled", result.Results[0].Title);

        }

        [TestMethod]
        public void TestResultWithoutASnippet()
        {
            GoogleResultPage result;

            using (FileStream fs = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\containsResultWithMissingFields.xml"))
            {
                GoogleResultPageBuilder builder = new GoogleResultPageBuilder();
                builder.LogWriter = new LogWriterAdaptor();
                result = builder.BuildResultPage(fs);
            }

            Assert.AreEqual("No description available for this result.", result.Results[0].Snippet);

        }

        [TestMethod]
        public void TestResultWithAFileType()
        {
            GoogleResultPage result;

            using (FileStream fs = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\containsPDF.xml"))
            {
                GoogleResultPageBuilder builder = new GoogleResultPageBuilder();
                builder.LogWriter = new LogWriterAdaptor();
                result = builder.BuildResultPage(fs);
            }

            Assert.AreEqual("PDF", result.Results[1].FileType);
        }

        [TestMethod]
        public void TestSearchWithNoResults()
        {
            GoogleResultPage result;

            using (FileStream fs = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\panopticon.xml"))
            {
                GoogleResultPageBuilder builder = new GoogleResultPageBuilder();
                builder.LogWriter = new LogWriterAdaptor();
                result = builder.BuildResultPage(fs);
            }

            Assert.IsFalse(result.HasResults);
        }
    }
}
