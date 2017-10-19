using System;
using NLog;

namespace kuujinbo.Mvc.NET.W3CLog
{
    public class Log
    {
        internal static readonly Logger _instance = LogManager.GetCurrentClassLogger();

        static Log() { }

        /// <summary>
        /// Write to error log at NLog Error level
        /// </summary>
        public void Error(Exception exception)
        {
            _instance.Error(exception);
        }

        /// <summary>
        /// Write to warn log at NLog Warn level
        /// </summary>
        public void Warn(string message)
        {
            _instance.Warn(message);
        }

        /// <summary>
        /// Write to info log at NLog Warn level
        /// </summary>
        public void Info(string message)
        {
            _instance.Info(message);
        }
    }
}