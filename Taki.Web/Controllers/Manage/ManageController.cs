using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taki.Common;
using Taki.DAL;
using Taki.Model;
using Taki.Web.Filters;

namespace Taki.Web.Controllers.Manage
{
    public class ManageController : BaseController
    {
        PurviewDAL purviewDAL = new PurviewDAL();

        // GET: Manage
        [AppAuthorization(EmAppAuthorization.Allow)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuManage()
        {
            return View();
        }

        public ActionResult GetAllMenu()
        {
            OperationResult<IList<purview>> result = new OperationResult<IList<purview>>();
            try
            {
                result.Data = purviewDAL.GetAllMenu();
                result.State = EmOperationState.SUCCESS;
            }
            catch (System.Exception ex)
            {
                result.State = EmOperationState.ERROR;
                Taki.Logging.LoggerFactory.Create().Error(ex);
            }
            return Json(result);
        }


    }
}