using System;
using System.Diagnostics;

using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoCrossCutting.Logging
{
    public abstract class TrovoLoggable
    {
        public ILogWriter LogWriter { get; set; }

        protected void generateLogEntry(string title,
                                        string message,
                                        int eventId,
                                        string category,
                                        int priority,
                                        TraceEventType severity)
        {
            TrovoLogEntry logEntry = new TrovoLogEntry();
            logEntry.Title = title;
            logEntry.Message = message;
            logEntry.EventId = eventId;
            logEntry.Category = category;
            logEntry.Priority = priority;
            logEntry.Severity = severity;
            LogWriter.Write(logEntry);
        }

    }
}
