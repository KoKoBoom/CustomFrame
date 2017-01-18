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

        public ContentResult GetData(string id)
        {
            var strDate = "2016-11-16 17:23:50d";
            strDate.GetToDayBeginDateTime(false);
            var result = new OperationResult<dynamic>
            {
                //PageDataCount = 21,
                //State = EmOperationState.SUCCESS,
                //Data = new { StrDateTime = strDate.GetToDayEndDateTime() }
            }.ToJson();
            return Content(result);
        }
    }
}