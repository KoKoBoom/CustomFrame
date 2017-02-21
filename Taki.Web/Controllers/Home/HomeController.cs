using Taki.Common;
using Taki.Web.Filters;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Threading;
using Taki.Model;
using Taki.DAL;
using System;
using Taki.Model.DTO;

namespace Taki.Web.Controllers.Home
{
    public class HomeController : BaseRulelessController
    {
        UserDAL dal = new UserDAL();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult NoPermission()
        {
            return View(@"~\Views\Shared\NoPermission.cshtml");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginIn(string name, string password, bool? saveCookie)
        {
            OperationResult<user> result = new OperationResult<user>();
            try
            {
                result = dal.LoginIn(name, password);

                if (result.State == EmOperationState.SUCCESS)
                {
                    Session[GlobalParams.UserCookieKey] = result.Data;
                    var purviewOfUser = new PurviewDAL().GetPurviewsOfUser(result.Data);
                    Session[GlobalParams.PurviewsOfUserCookieKey] = purviewOfUser;

                    var menuDTOs = new List<MenuDTO>();
                    foreach (var item in purviewOfUser.Where(x => "menu".Equals(x.PurviewType) && x.PPurviewID.IsNullOrEmpty()))
                    {
                        menuDTOs.Add(new MenuDTO() { MenuItem = item, SubItems = purviewOfUser.Where(x => item.PurviewID.Equals(x.PPurviewID)).ToList() });
                    }
                    Session[GlobalParams.PurviewsOfMenuCookieKey] = menuDTOs;
                    Session[GlobalParams.PurviewsOfAllCookieKey] = new PurviewDAL().GetAllPurview();

                    if (saveCookie.To<bool>(false, true))
                    {
                        DataCache.SetCache(GlobalParams.UserCookieKey, result.Data);
                    }
                }
            }
            catch (System.Exception ex)
            {
                result.State = EmOperationState.ERROR;
                Taki.Logging.LoggerFactory.Create().Error(ex);
            }
            return Json(result);
        }

        public void LoginOut()
        {
            Session[GlobalParams.UserCookieKey] = null;
            DataCache.SetCache(GlobalParams.UserCookieKey, "", DateTime.UtcNow, TimeSpan.Zero);
            Response.Redirect("/Home/Login");
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
                result = dal.Save(model);
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