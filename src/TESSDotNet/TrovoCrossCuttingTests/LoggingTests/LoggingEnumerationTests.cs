using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrovoCrossCutting.Logging.Enumerations;

namespace TrovoLoggingTests
{
    [TestClass]
    public class LoggingEnumerationTests
    {
        [TestMethod]
        public void LoggingCategories_ErrorUnrecoverable()
        {
            Assert.AreEqual(1, (int) TrovoLoggingCategory.ErrorUnrecoverable);
        }

        [TestMethod]
        public void LoggingCategories_ErrorRecoverable()
        {
            Assert.AreEqual(2, (int)TrovoLoggingCategory.ErrorRecoverable);
        }

        [TestMethod]
        public void LoggingCategories_MaintenanceInfo()
        {
            Assert.AreEqual(3, (int)TrovoLoggingCategory.MaintenanceInfo);
        }

        [TestMethod]
        public void LoggingCategoriesTest_SearchQuery()
        {
            Assert.AreEqual(4, (int)TrovoLoggingCategory.SearchQuery);
        }

        [TestMethod]
        public void LoggingCategoriesTest_DebugTrace()
        {
            Assert.AreEqual(5, (int)TrovoLoggingCategory.DebugTrace);
        }

        [TestMethod]
        public void LoggingCategories_ErrorUnrecoverable_Name()
        {
            Assert.AreEqual("ErrorUnrecoverable", TrovoLoggingCategory.ErrorUnrecoverable.ToString());
        }

        [TestMethod]
        public void LoggingPriorities_Critical()
        {
            Assert.AreEqual(50, (int)TrovoLoggingPriority.Critical);
        }

        [TestMethod]
        public void LoggingPriorities_High()
        {
            Assert.AreEqual(55, (int)TrovoLoggingPriority.High);
        }

        [TestMethod]
        public void LoggingPriorities_Informational()
        {
            Assert.AreEqual(60, (int)TrovoLoggingPriority.Informational);
        }

        [TestMethod]
        public void LoggingPriorities_DiagnosticHigh()
        {
            Assert.AreEqual(65, (int)TrovoLoggingPriority.DiagnosticHigh);
        }

        [TestMethod]
        public void LoggingPriorities_DiagnosticAverage()
        {
            Assert.AreEqual(70, (int)TrovoLoggingPriority.DiagnosticInfo);
        }

    }
}
