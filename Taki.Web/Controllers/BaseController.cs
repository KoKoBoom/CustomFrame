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

            if (purviewsOfUser.WhenNullThenDefault().Any() && purviewsOfUser.Where(x => url.ToLower().Equals(x.PurviewUrl?.ToLower()?.TrimEnd('/'))).WhenNullThenDefault().Any())
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    Response.Write(new OperationResult<string>()
                    {
                        State = EmOperationState.NoPermission
                    }.ToJson());
                }
                else
                {
                    Response.Redirect("/Home/NoPermission");
                }
            }
            //LoggerFactory.Create()?.Info("OnActionExecuting", filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName + "." + filterContext.ActionDescriptor.ActionName);
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