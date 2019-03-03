using System;

using NLog;

namespace OmnivoreIntegration.Logging
{
    public class NLogManager : INLogManager
    {
        private static Logger FileGeneralLogger;
        static NLogManager()
        {
            LogManager.AddHiddenAssembly(typeof(NLogManager).Assembly);
            FileGeneralLogger = LogManager.GetLogger("fileGeneralLogger");
        }

        public void LogDebug(string message)
        {
            try
            {
                FileGeneralLogger.Debug(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogDebug(string message, params object[] args)
        {
            try
            {
                FileGeneralLogger.Debug(message, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogError(string message)
        {
            try
            {
                FileGeneralLogger.Error(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LogError(string message, Exception ex)
        {
            try
            {
                FileGeneralLogger.Error(ex, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LogError(string message, Exception ex, params object[] args)
        {
            try
            {
                FileGeneralLogger.Error(ex, message, args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LogFatal(string message, Exception ex)
        {
            try
            {
                FileGeneralLogger.Fatal(ex, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LogFatal(string message, Exception ex, params object[] args)
        {
            try
            {
                FileGeneralLogger.Fatal(ex, message, args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LogInfo(string message)
        {
            try
            {
                FileGeneralLogger.Info(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            try
            {
                FileGeneralLogger.Info(message, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogTrace(string message)
        {
            try
            {
                FileGeneralLogger.Trace(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogTrace(string message, params object[] args)
        {
            try
            {
                FileGeneralLogger.Trace(message, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogWarn(string message)
        {
            try
            {
                FileGeneralLogger.Warn(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void LogWarn(string message, params object[] args)
        {
            try
            {
                FileGeneralLogger.Trace(message, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 
    }
}
