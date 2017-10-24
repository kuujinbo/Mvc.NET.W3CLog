using System;
using NLog;

namespace kuujinbo.Mvc.NET.W3CLog
{
    /// <summary>
    /// A dead-simple, non-adaptable, single-purpose thin wrapper for NLog
    /// that logs using WC3 Extended Log File Format recommendations.
    /// https://www.w3.org/TR/WD-logfile.html
    /// </summary>
    public class Log
    {
        /// <summary>
        /// The NLog Logger instance. 
        /// </summary>
        internal static readonly Logger _instance = LogManager.GetCurrentClassLogger();

        static Log() { }

        /// <summary>
        /// Write to error log at NLog Error level
        /// </summary>
        public void Error(Exception exception) { _instance.Error(exception); }

        /// <summary>
        /// Write to warn log at NLog Warn level
        /// </summary>
        public void Warn(string message) { _instance.Warn(message); }

        /// <summary>
        /// Write to info log at NLog Info level
        /// </summary>
        public void Info(string message) { _instance.Info(message); }
    }
}