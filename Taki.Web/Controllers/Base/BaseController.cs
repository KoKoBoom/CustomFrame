﻿using Taki.Logging;
using Taki.Web.Filters;
using System.Web.Mvc;
using System.Collections.Generic;
using Taki.Common;

namespace Taki.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LoggerFactory.Create()?.Info("OnActionExecuting", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName + "." + filterContext.ActionDescriptor.ActionName);
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LoggerFactory.Create()?.Info("OnActionExecuted", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName + "." + filterContext.ActionDescriptor.ActionName);
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            LoggerFactory.Create()?.Error(filterContext.Exception, 1);
            new ExceptionHanlder(filterContext);
            //base.OnException(filterContext);
        }

        /// <summary>
        /// 重载 json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Json<T>(OperationResult<T> data)
        {
            return Content(data.ToJson()/*, "application/json"*/);
        }
    }
}