using System;
using System.Diagnostics;

namespace TrovoCrossCutting.Logging.Interfaces
{
    public interface ILogEntry
    {
        string Title { get; set; }

        string Message { get; set; }

        string Category { get; set; }

        int EventId { get; set; }

        int Priority { get; set; }

        TraceEventType Severity { get; set; }
    }
}
