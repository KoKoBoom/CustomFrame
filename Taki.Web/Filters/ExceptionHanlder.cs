using Taki.Logging;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Taki.Web.Filters
{
    public class ExceptionHanlder
    {
        public ExceptionHanlder(ExceptionContext filterContext)
        {
            LoggerFactory.Create()?.Error(filterContext.Exception.Message, filterContext.Exception.TargetSite.DeclaringType.ToString() + "." + filterContext.RouteData.Values["action"].ToString());

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                return;

            filterContext.Result = AjaxError(filterContext.Exception.Message, filterContext);

            //Let the system know that the exception has been handled
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// Ajaxes the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="filterContext">The filter context.</param>
        /// <returns>JsonResult</returns>
        protected JsonResult AjaxError(string message, ExceptionContext filterContext)
        {
            //If message is null or empty, then fill with generic message
            if (String.IsNullOrEmpty(message))
                message = "处理您的请求出了错。请刷新页面，然后再试一次。";

            //Set the response status code to 500
            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            //Needed for IIS7.0
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            return new JsonResult
            {
                //can extend more properties 
                Data = new AjaxExceptionModel() { ErrorMessage = message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

    /// <summary>
    /// AjaxExceptionModel
    /// </summary>
    public class AjaxExceptionModel
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }
    }
}