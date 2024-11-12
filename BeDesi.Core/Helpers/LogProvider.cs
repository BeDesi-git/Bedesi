using log4net;
using System;

namespace BeDesi.Core.Helpers
{
    public interface ILogger
    {
        void Debug(object message);

        void Info(object message);

        void Warn(object message);

        void Fatal(object message);

        void Error(object message, Exception ex);

        void SetApiPath(string apiPath);

        // continue for all methods like Error, Fatal ...
    }

    public class Log4NetWrapper : ILogger
    {
        private readonly ILog _log;

        public Log4NetWrapper(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public Log4NetWrapper(string type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void SetApiPath(string apiPath)
        {
            LogicalThreadContext.Properties["apiPath"] = apiPath;
        }

        public void Debug(object message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }

        public void Info(object message)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(message);
            }
        }

        public void Warn(object message)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(message);
            }
        }

        public void Error(object message, Exception ex)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(message, ex);
            }
        }

        public void Fatal(object message)
        {
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(message);
            }
        }
    }

    public static class LogProvider
    {
        public static ILogger GetLogger(Type type)
        {
            return new Log4NetWrapper(type);
        }

        public static ILogger GetLogger(string type)
        {
            return new Log4NetWrapper(type);
        }
    }
}
