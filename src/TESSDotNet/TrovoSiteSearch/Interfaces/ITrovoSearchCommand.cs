using System;
using System.IO;
using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoSiteSearch.Interfaces
{
    public interface ITrovoSearchCommand : IDisposable
    {
        string RequestPath { get; set; }
        string Query { get; set; }

        bool ResultsFound { get; set; }

        ILogWriter LogWriter { get; set; }

        Stream executeSearch();

        void Dispose();

    }
}
