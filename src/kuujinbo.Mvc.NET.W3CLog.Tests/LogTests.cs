using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using Xunit;

namespace kuujinbo.Mvc.NET.W3CLog.Tests
{
    public class LogTests : IDisposable
    {
        MemoryTarget _memoryTarget;
        LoggingConfiguration _configuration;

        public LogTests()
        {
            _configuration = new LoggingConfiguration();

            _memoryTarget = new MemoryTarget { Name = "mem", Layout = "${message}" };

            _configuration.AddTarget(_memoryTarget);
            _configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, _memoryTarget));
            LogManager.Configuration = _configuration;
        }

        public void Dispose()
        {
            if (_memoryTarget != null) _memoryTarget.Dispose();
        }

        [Fact]
        public void Error_Exception_Logs()
        {
            var message = "exception thrown";
            new Log().Error(new Exception(message));
            var logs = _memoryTarget.Logs;

            Assert.Equal(1, logs.Count);
            Assert.EndsWith(message, logs[0]);
        }

        [Fact]
        public void Warn_String_Logs()
        {
            var message = "warning";
            new Log().Warn(message);
            var logs = _memoryTarget.Logs;

            Assert.Equal(1, logs.Count);
            Assert.EndsWith(message, logs[0]);
        }

        [Fact]
        public void Info_String_Logs()
        {
            var message = "warning";
            new Log().Info(message);
            var logs = _memoryTarget.Logs;

            Assert.Equal(1, logs.Count);
            Assert.EndsWith(message, logs[0]);
        }
    }
}