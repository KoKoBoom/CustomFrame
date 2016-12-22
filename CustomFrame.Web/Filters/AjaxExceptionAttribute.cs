using System;
using System.Net;
using System.Web.Mvc;

namespace CustomFrame.Web.Filters
{
    #region AjaxExceptionAttribute
    /// <summary>
    /// Ajax Exception Handle Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AjaxExceptionAttribute : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            new ExceptionHanlder(filterContext);
        }
    }
    #endregion

}