using System;
using System.Diagnostics;

using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoCrossCutting.Logging
{
    public class TrovoLogEntry : ILogEntry
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public int EventId { get; set; }
        public int Priority { get; set; }
        public TraceEventType Severity { get; set; }
    }
}
