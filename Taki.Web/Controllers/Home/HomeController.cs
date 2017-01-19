using Taki.Common;
using Taki.Web.Controllers.Base;
using Taki.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Threading;

namespace Taki.Web.Controllers.Home
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
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