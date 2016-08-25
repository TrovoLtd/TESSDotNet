using System;

using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

namespace TrovoCrossCutting.Logging
{
    public class TrovoLoggingFactory
    {

        public ILogWriter GetLogger(LoggerType type)
        {
            switch(type)
            {
                case LoggerType.NullLogger:
                    return new NullLogger.LogWriterAdaptor();

                case LoggerType.EnterpriseLibrary5Logger:
                    return new EnterpriseLibrary5Logger.LogWriterAdaptor();

                case LoggerType.Log4Net:
                    return new Log4NetLogger.LogWriterAdaptor();

                default:
                    throw new InvalidOperationException("An invalid type was passed to the TrovoLoggingFactory");
            }
        }
        

    }
}
