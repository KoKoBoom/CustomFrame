/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/11 10:19:10
** Desc：	Log4net帮助类
*********************************************************************************/
using System;
using System.Diagnostics;
using System.Linq;
using Taki.Logging;

//[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace Taki.Logging
{
    internal class Logger4Helper : ILogger
    {
        #region Debug
        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="exception"></param>
        public void Debug(Exception exception)
        {
            Debug("", exception);
        }

        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="message"> The debug message </param>
        /// <param name="args"> the message argument values </param>
        public void Debug(string message, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Debug(message);
            }
            catch (Exception) { }
        }

        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="message"> The message </param>
        /// <param name="exception"> Exception to write in debug message </param>
        /// <param name="args"></param>
        public void Debug(string message, Exception exception, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Debug(message, exception);
            }
            catch (Exception) { }
        }

        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="item"> The item with information to write in debug </param>
        public void Debug(object item)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(null));
                log.Debug(item);
            }
            catch (Exception) { }
        }
        #endregion

        #region Fatal

        /// <summary>
        /// Log FATAL error
        /// </summary>
        /// <param name="exception"></param>
        public void Fatal(Exception exception)
        {
            Fatal("", exception);
        }

        /// <summary>
        ///   Log FATAL error
        /// </summary>
        /// <param name="message"> The message of fatal error </param>
        /// <param name="args"> The argument values of message </param>
        public void Fatal(string message, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Fatal(message);
            }
            catch (Exception) { }
        }

        /// <summary>
        ///   log FATAL error
        /// </summary>
        /// <param name="message"> The message of fatal error </param>
        /// <param name="exception"> The exception to write in this fatal message </param>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Fatal(message, exception);
            }
            catch (Exception) { }
        }

        #endregion

        #region Info

        /// <summary>
        /// Log message information
        /// </summary>
        /// <param name="exception"></param>
        public void Info(Exception exception)
        {
            Info("", exception);
        }

        /// <summary>
        ///   Log message information
        /// </summary>
        /// <param name="message"> The information message to write </param>
        /// <param name="args"> The arguments values </param>
        public void Info(string message, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Info(message);
            }
            catch (Exception) { }
        }

        #endregion

        #region Warning

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="exception"></param>
        public void Warning(Exception exception)
        {
            Warning("", exception);
        }

        /// <summary>
        ///   Log warning message
        /// </summary>
        /// <param name="message"> The warning message to write </param>
        /// <param name="args"> The argument values </param>
        public void Warning(string message, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Warn(message);
            }
            catch (Exception) { }
        }

        #endregion

        #region Error

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="exception"></param>
        public void Error(Exception exception)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(null));
                log.Error("", exception);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 简单日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="depth"></param>
        /// <param name="maxDepth"></param>
        public void Error(string message, int depth, int maxDepth = 2)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(null));
                log.Error(message + "\r\n" + StackTraceLinkedList.GetStrLinkedList(depth + 2, maxDepth + 2));
            }
            catch (Exception) { }
        }

        /// <summary>
        ///   Log error message
        /// </summary>
        /// <param name="message"> The error message to write </param>
        /// <param name="args"> The arguments values </param>
        public void Error(string message, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Error(message);
            }
            catch (Exception) { }
        }

        /// <summary>
        ///   Log error message
        /// </summary>
        /// <param name="message"> The error message to write </param>
        /// <param name="exception"> The exception associated with this error </param>
        /// <param name="args"> The arguments values </param>
        public void Error(string message, Exception exception, params object[] args)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(Paramer(args));
                log.Error(message, exception);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 写入异常类的上级调用，一般用于公共方法、全局异常捕获等方法里面的异常写入
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="depth"></param>
        /// <param name="maxDepth"></param>
        public void Error(Exception exception, int depth, int maxDepth = 5)
        {
            try
            {
                Error(exception.Message, exception, depth + 1, maxDepth + 1);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 写入异常类的上级调用，一般用于公共方法、全局异常捕获等方法里面的异常写入
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="depth"></param>
        /// <param name="maxDepth"></param>
        public void Error(string message, Exception exception, int depth, int maxDepth = 5)
        {
            try
            {
                log4net.ILog log = log4net.LogManager.GetLogger(new StackTrace(true).GetFrame(depth + 1).GetMethod().DeclaringType);
                log.Error(message + "\r\n" + StackTraceLinkedList.GetStrLinkedList(depth + 2, maxDepth + 2), exception);
            }
            catch (Exception) { }
        }
        #endregion

        string Paramer(object[] args)
        {
            return (args != null && args.Count() > 0) ? args[0].ToString() : new StackTrace(true).GetFrame(2).GetMethod().DeclaringType.ToString();
        }
    }
}
