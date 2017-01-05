using System;
using System.Linq;

//[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace Taki.Logging
{
    internal class Logger4Helper : ILogger
    {
        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="message"> The debug message </param>
        /// <param name="args"> the message argument values </param>
        public void Debug(string message, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Debug(message);
        }

        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="message"> The message </param>
        /// <param name="exception"> Exception to write in debug message </param>
        /// <param name="args"></param>
        public void Debug(string message, Exception exception, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Debug(message, exception);
        }
        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="item"> The item with information to write in debug </param>
        public void Debug(object item)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(null));
            log.Debug(item);
        }

        /// <summary>
        ///   Log FATAL error
        /// </summary>
        /// <param name="message"> The message of fatal error </param>
        /// <param name="args"> The argument values of message </param>
        public void Fatal(string message, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Fatal(message);
        }

        /// <summary>
        ///   log FATAL error
        /// </summary>
        /// <param name="message"> The message of fatal error </param>
        /// <param name="exception"> The exception to write in this fatal message </param>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Fatal(message, exception);
        }

        /// <summary>
        ///   Log message information
        /// </summary>
        /// <param name="message"> The information message to write </param>
        /// <param name="args"> The arguments values </param>
        public void Info(string message, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Info(message);
        }

        /// <summary>
        ///   Log warning message
        /// </summary>
        /// <param name="message"> The warning message to write </param>
        /// <param name="args"> The argument values </param>
        public void Warning(string message, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Warn(message);
        }

        /// <summary>
        ///   Log error message
        /// </summary>
        /// <param name="message"> The error message to write </param>
        /// <param name="args"> The arguments values </param>
        public void Error(string message, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Error(message);
        }

        /// <summary>
        ///   Log error message
        /// </summary>
        /// <param name="message"> The error message to write </param>
        /// <param name="exception"> The exception associated with this error </param>
        /// <param name="args"> The arguments values </param>
        public void Error(string message, Exception exception, params object[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
            log.Error(message, exception);
        }

        string Paramer(object[] args)
        {
            return (args != null && args.Count() > 0) ? args[0].ToString() : System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();
            //return (args != null && args.Count() > 0) ? (Type)args[0] : System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
        }

    }
}
