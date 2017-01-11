using Taki.Logging;
using Taki.Web.Filters;
using System.Web.Mvc;

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
            new ExceptionHanlder(filterContext);
            //base.OnException(filterContext);
        }
    }
}