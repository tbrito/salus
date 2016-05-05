namespace Salus.Infra.Logs
{
    using System.IO;
    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;
    using log4net.Config;
    using System;

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
                File = Path.Combine("Logs", Aplicacao.Nome + ".log"),
                Layout = new PatternLayout("[%date][%-5thread][%-5level] %message %newline"),
                Threshold = Level.Info,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                StaticLogFileName = true,
                MaxSizeRollBackups = 3
            };

            fileAppender.ActivateOptions();

            var hierarquia = (Hierarchy)LogManager.GetRepository();

            Logger loggerNH = hierarquia.GetLogger("NHibernate.SQL") as Logger;
            loggerNH.Level = Level.Error;
            loggerNH.AddAppender(fileAppender);

            loggerNH = hierarquia.GetLogger("NHibernate") as Logger;
            loggerNH.Level = Level.Error;
            loggerNH.AddAppender(fileAppender);

            hierarquia.Configured = true;

            return fileAppender;
        }

        private static ConsoleAppender BuildConsoleAppender()
        {
            var consoleAppender = new ConsoleAppender
            {
                Name = "ConsoleAppender",
                Layout = new PatternLayout("[%date][%-5thread][%-5level] %message %newline"),
                Threshold = Level.Info
            };

            consoleAppender.ActivateOptions();

            var hierarquia = (Hierarchy)LogManager.GetRepository();

            Logger loggerNH = hierarquia.GetLogger("NHibernate.SQL") as Logger;
            loggerNH.Level = Level.Error;
            loggerNH.AddAppender(consoleAppender);

            loggerNH = hierarquia.GetLogger("NHibernate") as Logger;
            loggerNH.Level = Level.Error;
            loggerNH.AddAppender(consoleAppender);

            hierarquia.Configured = true;
            return consoleAppender;
        }

        private static Logger GetApplicationLogger()
        {
            return (Logger)Root.Repository.GetLogger("App");
        }
    }
}