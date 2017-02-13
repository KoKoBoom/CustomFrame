using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taki.Common;

namespace Taki.Web.Controllers
{
    public class BaseRulelessController : Controller
    {
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