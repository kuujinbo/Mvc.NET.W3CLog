using System;
using Moq;
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
        }

        public void Dispose()
        {
            if (_memoryTarget != null) _memoryTarget.Dispose();
        }

        // Should only log when at a log level greater than or equal to
        // specified level: 
        // https://github.com/NLog/NLog/wiki/Configuration-file#log-levels

        [Fact]
        public void Error_LogLevelAboveError_DoesNotLog()
        {
            _configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Fatal, _memoryTarget));
            LogManager.Configuration = _configuration;
            var logs = _memoryTarget.Logs;

            new Log().Error(new Exception());

            Assert.Equal(0, logs.Count);
        }

        [Fact]
        public void Error_LogLevelError_Logs()
        {
            _configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, _memoryTarget));
            LogManager.Configuration = _configuration;
            var message = "exception thrown";
            var logs = _memoryTarget.Logs;

            new Log().Error(new Exception(message));

            Assert.Equal(1, logs.Count);
            Assert.EndsWith(message, logs[0]);
        }

        [Fact]
        public void Warn_LogLevelAboveWarn_DoesNotLog()
        {
            _configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, _memoryTarget));
            LogManager.Configuration = _configuration;
            var logs = _memoryTarget.Logs;

            new Log().Warn("");

            Assert.Equal(0, logs.Count);
        }

        [Fact]
        public void Warn_LogLevelWarn_Logs()
        {
            _configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Warn, _memoryTarget));
            LogManager.Configuration = _configuration;
            var message = "warn";
            var logs = _memoryTarget.Logs;

            new Log().Warn(message);

            Assert.Equal(1, logs.Count);
            Assert.Equal(message, logs[0]);
        }

        [Fact]
        public void Info_LogLevelAboveInfo_DoesNotLog()
        {
            _configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Warn, _memoryTarget));
            LogManager.Configuration = _configuration;
            var logs = _memoryTarget.Logs;

            new Log().Info("");

            Assert.Equal(0, logs.Count);
        }

        [Fact]
        public void Info_LogLevelInfo_Logs()
        {
            _configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, _memoryTarget));
            LogManager.Configuration = _configuration;
            var message = "info";
            var logs = _memoryTarget.Logs;

            new Log().Info(message);

            Assert.Equal(1, logs.Count);
            Assert.Equal(message, logs[0]);
        }

        #region consumer
        public class LogConsumer : ILog
        {
            ILog _log;
            public LogConsumer(ILog log) { _log = log; }

            public void Error(Exception exception)
            {
                _log.Error(exception);
            }

            public void Warn(string message)
            {
                _log.Warn(message);
            }

            public void Info(string message)
            {
                _log.Info(message);
            }
        }


        [Fact]
        public void LogConsumer_Error_CallsInterfaceError()
        {
            var mock = new Mock<ILog>();

            new LogConsumer(mock.Object).Error(new Exception());

            mock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once());
        }

        [Fact]
        public void LogConsumer_Warn_CallsInterfaceWarn()
        {
            var mock = new Mock<ILog>();

            new LogConsumer(mock.Object).Warn(It.IsAny<string>());

            mock.Verify(x => x.Warn(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void LogConsumer_Info_CallsInterfaceInfo()
        {
            var mock = new Mock<ILog>();

            new LogConsumer(mock.Object).Info(It.IsAny<string>());

            mock.Verify(x => x.Info(It.IsAny<string>()), Times.Once());
        }
        #endregion
    }
}