using System;
using System.Diagnostics;

using log4net;

using TrovoCrossCutting.Logging.Interfaces;
using TrovoCrossCutting.Logging.Enumerations;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace TrovoCrossCutting.Logging.Log4NetLogger
{
    public class LogWriterAdaptor : ILogWriter
    {
        public void Write(ILogEntry trovoLogEntry)
        {
            ILog logger = LogManager.GetLogger("TrovoCrossCutting");

            string message = String.Format("Title:{0}{1}Message:{2}{1}Category:{3}{1}Event Id:{4}{1}Priority:{5}{1}", 
                                                trovoLogEntry.Title, 
                                                Environment.NewLine, 
                                                trovoLogEntry.Message,
                                                trovoLogEntry.Category,
                                                trovoLogEntry.EventId.ToString(),
                                                trovoLogEntry.Priority.ToString());

            switch(trovoLogEntry.Severity)
            {
                case TraceEventType.Critical:
                    logger.Fatal(message);
                    break;

                case TraceEventType.Error:
                    logger.Error(message);
                    break;

                case TraceEventType.Warning:
                    logger.Warn(message);
                    break;

                case TraceEventType.Information:
                    logger.Info(message);
                    break;

                case TraceEventType.Verbose:
                    logger.Debug(message);
                    break;
                
                default:
                    break;

            }

        }
    }
}
