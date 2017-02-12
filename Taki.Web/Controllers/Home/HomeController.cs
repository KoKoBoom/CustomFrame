using Taki.Common;
using Taki.Web.Controllers.Base;
using Taki.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Threading;
using Taki.Model;
using Taki.DAL;
using System;

namespace Taki.Web.Controllers.Home
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRegister(user model)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            try
            {
                UserDAL dal = new UserDAL();
                model.UserID = Guid.NewGuid().ToString();
                model.CreateDate = DateTime.Now;
                model.Status = 1;
                result.Data = dal.Save(model);
                result.State = result.Data ? EmOperationState.SUCCESS : EmOperationState.FAIL;
            }
            catch (System.Exception ex)
            {
                result.State = EmOperationState.ERROR;
                Taki.Logging.LoggerFactory.Create().Error(ex);
            }
            return Json(result);
        }

        public ActionResult GetData(string id)
        {
            var strDate = "2016-11-16 17:23:50d";
            strDate.GetToDayBeginDateTime(true);
            var result = new OperationResult<dynamic>
            {
                State = EmOperationState.SUCCESS,
                Data = new { StrDateTime = strDate.GetToDayEndDateTime() }
            };
            return Json(result);
        }

        public ActionResult GetPagingData(string id)
        {
            var result = new OperationPagingResult<dynamic>
            {
                PageIndex = Request["pageIndex"].To<int>(1),
                PageSize = Request["pageSize"].To<int>(10)
            };

            result.Data = new { StrDateTime = "2016-11-16 17:23:50d".GetToDayBeginDateTime() };
            result.DataTotal = 21;
            result.State = EmOperationState.SUCCESS;
            return Json(result);
        }
    }
}