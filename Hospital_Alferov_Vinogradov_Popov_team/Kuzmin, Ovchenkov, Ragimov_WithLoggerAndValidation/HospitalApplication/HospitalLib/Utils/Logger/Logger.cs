using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace HospitalLib.Utils.Logger
{
    public static class HospitalLogger
    {
        private static readonly ILog Log;
        private static readonly log4net.Repository.Hierarchy.Logger Logger;
        private static readonly LoggerState _loggerLevel;
        private static readonly List<Type> AppendersTypes;

        static HospitalLogger()
        {
            Log = LogManager.GetLogger(typeof (HospitalLogger));
            Logger = (log4net.Repository.Hierarchy.Logger) Log.Logger;
            _loggerLevel = LoggerState.All;
            AppendersTypes = new List<Type>();
        }

        public static LoggerState LoggerLevel
        {
            get { return _loggerLevel; }

            set { Logger.Parent.Level = Logger.Hierarchy.LevelMap[value.ToString().ToUpper()]; }
        }

        public static void AddLoggingTarget(LoggingTarget.LoggingTarget loggingTarget)
        {
            var appender = (AppenderSkeleton) loggingTarget.Appender;

            var layout = new PatternLayout {ConversionPattern = "[%date] [%thread] %-5level %logger - %message%newline"};
            layout.ActivateOptions();

            appender.Layout = layout;
            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);

            Type appenderType = loggingTarget.Appender.GetType();

            if (!AppendersTypes.Contains(appenderType))
            {
                AppendersTypes.Add(appenderType);
                Logger.Parent.AddAppender(appender);
            }
        }

        public static void RemoveLoggingTarget(LoggingTarget.LoggingTarget loggingTarget)
        {
            Type appenderType = loggingTarget.Appender.GetType();

            AppendersTypes.Remove(appenderType);
            Logger.Parent.Appenders.ToArray().ToList().RemoveAll(item => item.GetType() == appenderType);
        }

        public static void ClearLoggingTargets()
        {
            AppendersTypes.Clear();
        }

        public static void LogError(string errorString, params object[] format)
        {
            Log.ErrorFormat(errorString, format);
        }

        public static void LogInfo(string infoString, params object[] format)
        {
            Log.InfoFormat(infoString, format);
        }

        public static void LogDebug(string debugString, params object[] format)
        {
            Log.DebugFormat(debugString, format);
        }
    }
}