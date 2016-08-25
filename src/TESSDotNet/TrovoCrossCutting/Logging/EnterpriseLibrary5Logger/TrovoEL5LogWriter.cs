using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;

namespace TrovoCrossCutting.Logging.EnterpriseLibrary5Logger
{
    public class TrovoEL5LogWriter : LogWriter
    {
        public override ILogFilter GetFilter(string name)
        {
            throw new NotImplementedException();
        }

        public override T GetFilter<T>(string name)
        {
            throw new NotImplementedException();
        }

        public override T GetFilter<T>()
        {
            throw new NotImplementedException();
        }

        public override System.Collections.Generic.IEnumerable<LogSource> GetMatchingTraceSources(LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        public override bool IsLoggingEnabled()
        {
            throw new NotImplementedException();
        }

        public override bool IsTracingEnabled()
        {
            throw new NotImplementedException();
        }

        public override bool ShouldLog(LogEntry log)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.Generic.IDictionary<string, LogSource> TraceSources
        {
            get { throw new NotImplementedException(); }
        }

        public override void Write(LogEntry log)
        {
            Logger.Write(log);
        }
    }
}
