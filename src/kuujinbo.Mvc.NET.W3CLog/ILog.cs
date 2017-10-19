using System;

namespace kuujinbo.Mvc.NET.W3CLog
{
    public interface ILog
    {
        void Error(Exception exception);
        void Warn(string message);
        void Info(string message);
    }
}