using System;
using System.Diagnostics;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;


using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoCrossCutting.Logging.EnterpriseLibrary5Logger
{

    public class LogWriterAdaptor : ILogWriter, IDisposable
    {
        private TrovoEL5LogWriter _logWriter;

        public LogWriterAdaptor()
        {
            _logWriter = new TrovoEL5LogWriter();
        }

        public void Write(ILogEntry trovoLogEntry)
        {
            LogEntry entry = new LogEntry();
        
            entry.Title = trovoLogEntry.Title;
            entry.Message = trovoLogEntry.Message;
            if(trovoLogEntry.Category != null) entry.Categories.Add(trovoLogEntry.Category);
            if(trovoLogEntry.EventId != 0) entry.EventId = trovoLogEntry.EventId;
            if(trovoLogEntry.Priority != 0) entry.Priority = trovoLogEntry.Priority;
            
            if (trovoLogEntry.Severity != 0)
            {
                entry.Severity = trovoLogEntry.Severity;
            }
            else
            {
                entry.Severity = TraceEventType.Information;
            }
    
            _logWriter.Write(entry);
        }

        void IDisposable.Dispose()
        {
            _logWriter.Dispose();
        }
    }
}
