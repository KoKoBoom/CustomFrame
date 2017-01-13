﻿using Taki.Common;
using Taki.Web.Controllers.Base;
using Taki.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Taki.Web.Controllers.Home
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData(string id)
        {
            var strDate = "2016-11-16 17:23:50";
            return Json(new { OK = "OK", StrDateTime = strDate.GetToDayEndDateTime() }, JsonRequestBehavior.AllowGet);
        }
    }
}