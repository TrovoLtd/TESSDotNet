using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using TrovoSiteSearch.GoogleSiteSearch;
using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.NullLogger;

namespace TrovoSiteSearchTests.GoogleSiteSearchPluginTests
{
    [TestClass]
    public class GoogleResultPageBuilderPromotionTests
    {
        private FileStream _fileStream;
        private GoogleResultPageBuilder _pageBuilder;
        private GoogleResultPage _result;


        [TestInitialize]
        public void SetUp()
        {
            _fileStream = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\PromotionSearchForHadronReturnsDescriptionAndImageURLs.xml");
            _pageBuilder = new GoogleResultPageBuilder();
            _pageBuilder.LogWriter = new LogWriterAdaptor();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (_fileStream.CanRead) _fileStream.Close();
        }
        
        [TestMethod]
        public void WhenAFileWithAPromotionIsLoadedThePromotionIsAddedToTheListOfPromotions()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual(1, _result.PromotedLinks.Count);
        }

        [TestMethod]
        public void PromotionTitleIsLargeHadronCollider()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual("Large Hadron Collider", _result.PromotedLinks[0].Title);
        }

        [TestMethod]
        public void PromotionUrlIsCorrect()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual("http://www.sciencemuseum.org.uk/online_science/explore_our_collections/large_hadron_collider", _result.PromotedLinks[0].Url);
        }

        [TestMethod]
        public void PromotionDescriptionCorrect()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual("Discover the most powerful particle accelerator ever built and the history of particle science.", _result.PromotedLinks[0].Description);
        }

        [TestMethod]
        public void PromotedResultNotAddedToNormalResults()
        {
            _result = _pageBuilder.BuildResultPage(_fileStream);
            Assert.AreEqual(10, _result.Results.Count);
        }


        [TestMethod]
        public void TestPromotionWithoutADescription()
        {
            GoogleResultPage result;

            using (FileStream fs = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\PromotionSearchForCommunicationReturnsNoPromotionDescription.xml"))
            {
                GoogleResultPageBuilder builder = new GoogleResultPageBuilder();
                builder.LogWriter = new LogWriterAdaptor();
                result = builder.BuildResultPage(fs);
            }

            Assert.IsTrue(String.IsNullOrEmpty(result.PromotedLinks[0].Description));

        }
        
        [TestMethod]
        public void TestResultWithTwoPromotions()
        {
            GoogleResultPage result;

            using (FileStream fs = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\PhotographyTwoPromotions.xml"))
            {
                GoogleResultPageBuilder builder = new GoogleResultPageBuilder();
                builder.LogWriter = new LogWriterAdaptor();
                result = builder.BuildResultPage(fs);
            }

            Assert.AreEqual(2, result.PromotedLinks.Count);

        }

        [TestMethod]
        public void TestResultWithNoPromotions()
        {
            GoogleResultPage result;

            using (FileStream fs = File.OpenRead(@"GoogleSiteSearchPluginTests\GoogleXMLOutput\queryForMurder.xml"))
            {
                GoogleResultPageBuilder builder = new GoogleResultPageBuilder();
                builder.LogWriter = new LogWriterAdaptor();
                result = builder.BuildResultPage(fs);
            }

            Assert.IsNull(result.PromotedLinks);

        }



    }
}
