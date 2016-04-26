namespace Salus.Infra.Logs
{
    using System.IO;
    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;
    using log4net.Config;

    public class Log
    {
        private static Logger _root;
        private static ILog _applicationLog;

        private static Logger Root
        {
            get
            {
                return _root ?? (_root = ((Hierarchy)LogManager.GetRepository()).Root);
            }
        }

        public static ILog App
        {
            get
            {
                return _applicationLog ?? (_applicationLog = LogManager.GetLogger("App"));
            }
        }

        public static void Initialize()
        {
            Root.Repository.ResetConfiguration();
            
            var fileAppender = BuildFileAppender();
            var consoleAppender = BuildConsoleAppender();

            var applicationLogger = GetApplicationLogger();
            applicationLogger.AddAppender(fileAppender);
            applicationLogger.AddAppender(consoleAppender);

            Root.Repository.Configured = true;
            BasicConfigurator.Configure(Root.Repository);
        }

        private static RollingFileAppender BuildFileAppender()
        {
            var fileAppender = new RollingFileAppender
            {
                Name = "FileAppender",
                AppendToFile = true,
                File = Path.Combine("Logs", "Web.log"),
                Layout = new PatternLayout("[%date][%-5thread][%-5level][%message] %newline"),
                Threshold = Level.Debug,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                StaticLogFileName = true,
                MaxSizeRollBackups = 3
            };

            fileAppender.ActivateOptions();

            return fileAppender;
        }

        private static ConsoleAppender BuildConsoleAppender()
        {
            var consoleAppender = new ConsoleAppender
            {
                Name = "ConsoleAppender",
                Layout = new PatternLayout("[%date][%-5thread][%-5level][%message] %newline"),
                Threshold = Level.Debug
            };

            consoleAppender.ActivateOptions();

            return consoleAppender;
        }

        private static Logger GetApplicationLogger()
        {
            return (Logger)Root.Repository.GetLogger("App");
        }
    }
}