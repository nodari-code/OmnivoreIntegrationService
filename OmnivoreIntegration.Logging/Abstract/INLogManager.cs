using System;

namespace OmnivoreIntegration.Logging
{
    public interface INLogManager
    {
        #region Logging through NLog Loggers
        void LogTrace(string message);
        void LogTrace(string message, params object[] args);
        void LogDebug(string message);
        void LogDebug(string message, params object[] args);
        void LogInfo(string message);
        void LogInfo(string message, params object[] args);
        void LogWarn(string message);
        void LogWarn(string message, params object[] args);
        void LogError(string message);
        void LogError(string message, Exception ex);
        void LogError(string message, Exception ex, params object[] args);
        void LogFatal(string message, Exception ex);
        void LogFatal(string message, Exception ex, params object[] args);

        #endregion Logging through NLog Loggers    
    }
}
    
