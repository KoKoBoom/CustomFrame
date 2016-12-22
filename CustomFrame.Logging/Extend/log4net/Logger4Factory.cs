using System;

namespace CustomFrame.Logging
{
    internal class Logger4Factory : ILoggerFactory
    {
        public ILogger Create()
        {
            return new Logger4Helper();
        }

        public static void SetConfig(string configPath = "")
        {
            if (string.IsNullOrWhiteSpace(configPath))
            {
                log4net.Config.XmlConfigurator.Configure();
            }
            else
            {
                using (var fs = System.IO.File.OpenRead(configPath))
                {
                    log4net.Config.XmlConfigurator.Configure(fs);
                }
            }
        }

        public void DefaultFactory()
        {
            SetConfig(System.AppDomain.CurrentDomain.BaseDirectory + "/bin/Extend/log4net/log4net.config");
            Logging.LoggerFactory.SetCurrent(new Logging.Logger4Factory());
        }
    }
}
