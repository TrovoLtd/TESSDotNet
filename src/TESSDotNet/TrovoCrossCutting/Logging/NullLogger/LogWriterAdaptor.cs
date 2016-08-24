using System;
using TrovoCrossCutting.Logging.Interfaces;

namespace TrovoCrossCutting.Logging.NullLogger
{

    public class LogWriterAdaptor : ILogWriter, IDisposable
    {
       
        void ILogWriter.Write(ILogEntry trovoLogEntry)
        {
        
        }

        public void Dispose()
        {

        }
    }

}
