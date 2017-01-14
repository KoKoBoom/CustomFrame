/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/11 10:19:10
** Desc：	日志基类接口 所有实现都必须继承该接口
*********************************************************************************/
using System;

namespace Taki.Logging
{
    public interface ILogger
    {

        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="exception"></param>
        void Debug(Exception exception);

        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="message"> The debug message </param>
        /// <param name="args"> the message argument values </param>
        void Debug(string message, params object[] args);

        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="message"> The message </param>
        /// <param name="exception"> Exception to write in debug message </param>
        /// <param name="args"></param>
        void Debug(string message, Exception exception, params object[] args);

        /// <summary>
        ///   Log debug message
        /// </summary>
        /// <param name="item"> The item with information to write in debug </param>
        void Debug(object item);

        /// <summary>
        /// Log FATAL error
        /// </summary>
        /// <param name="exception"></param>
        void Fatal(Exception exception);

        /// <summary>
        ///   Log FATAL error
        /// </summary>
        /// <param name="message"> The message of fatal error </param>
        /// <param name="args"> The argument values of message </param>
        void Fatal(string message, params object[] args);

        /// <summary>
        ///   log FATAL error
        /// </summary>
        /// <param name="message"> The message of fatal error </param>
        /// <param name="exception"> The exception to write in this fatal message </param>
        void Fatal(string message, Exception exception, params object[] args);

        /// <summary>
        /// Log message information
        /// </summary>
        /// <param name="exception"></param>
        void Info(Exception exception);

        /// <summary>
        ///   Log message information
        /// </summary>
        /// <param name="message"> The information message to write </param>
        /// <param name="args"> The arguments values </param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="exception"></param>
        void Warning(Exception exception);

        /// <summary>
        ///   Log warning message
        /// </summary>
        /// <param name="message"> The warning message to write </param>
        /// <param name="args"> The argument values </param>
        void Warning(string message, params object[] args);

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="exception"></param>
        void Error(Exception exception);

        /// <summary>
        /// 简单异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="depth"></param>
        /// <param name="maxDepth"></param>
        void Error(string message, int depth, int maxDepth = 2);

        /// <summary>
        ///   Log error message
        /// </summary>
        /// <param name="message"> The error message to write </param>
        /// <param name="args"> The arguments values </param>
        void Error(string message, params object[] args);

        /// <summary>
        ///   Log error message
        /// </summary>
        /// <param name="message"> The error message to write </param>
        /// <param name="exception"> The exception associated with this error </param>
        /// <param name="args"> The arguments values </param>
        void Error(string message, Exception exception, params object[] args);

        /// <summary>
        /// 写入异常，一般用于公共方法、全局异常捕获写入日志，depth表示向上的调用方法的深度，当前方法为0，每上一层加1，默认向上最大查找5层
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="depth"></param>
        /// <param name="maxDepth"></param>
        void Error(Exception exception, int depth, int maxDepth = 5);

        /// <summary>
        /// 写入异常，一般用于公共方法、全局异常捕获写入日志，depth表示向上的调用方法的深度，当前方法为0，每上一层加1，默认向上最大查找5层
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="depth"></param>
        /// <param name="maxDepth"></param>
        void Error(string message, Exception exception, int depth, int maxDepth = 5);
    }
}
