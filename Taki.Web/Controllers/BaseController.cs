using Taki.Logging;
using Taki.Web.Filters;
using System.Web.Mvc;
using System.Collections.Generic;
using Taki.Common;
using Taki.DAL;
using Taki.Model;
using System.Linq;

namespace Taki.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var url = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName + "." + filterContext.ActionDescriptor.ActionName;
            url = ((System.Web.Mvc.Controller)filterContext.Controller).Request.Path;
            var userModel = Session[GlobalParams.UserCookieKey] as user;
            List<purview> purviewsOfUser = Session[GlobalParams.PurviewsOfUserCookieKey] as List<purview>;
            //List<purview> purviewsOfAll = Session[GlobalParams.PurviewsOfAllCookieKey] as List<purview>;

            if (userModel == null || userModel.Name.IsNullOrWhiteSpace())
            {
                Response.Redirect("/Home/Login");
            }

            if (GetAppAuthorizationAttributeValue(filterContext) == EmAppAuthorization.Allow || (purviewsOfUser.Any() && purviewsOfUser.Where(x => url.ToLower().TrimEnd('/').Equals(x.PurviewUrl?.ToLower()?.TrimEnd('/'))).Any()))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult("/Home/NoPermissionAjax/");
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home/NoPermission/");
                }
            }
            //LoggerFactory.Create()?.Info("OnActionExecuting", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName + "." + filterContext.ActionDescriptor.ActionName);
        }

        /// <summary>
        /// 获取 特性值
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private static EmAppAuthorization GetAppAuthorizationAttributeValue(ActionExecutingContext filterContext)
        {
            var attribute = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.GetMethod(filterContext.ActionDescriptor.ActionName).GetCustomAttributes(typeof(AppAuthorizationAttribute), false).FirstOrDefault();
            if (attribute == null)
            {
                return EmAppAuthorization.NoAllow;
            }
            return ((AppAuthorizationAttribute)attribute).Value;
        }

        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    //LoggerFactory.Create()?.Info("OnActionExecuted", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName + "." + filterContext.ActionDescriptor.ActionName);
        //    base.OnActionExecuted(filterContext);
        //}

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