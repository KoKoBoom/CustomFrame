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

        public ActionResult UserManage()
        {
            return View();
        }

        [AppAuthorization(EmAppAuthorization.Allow)]
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UpdateMenu(purview model)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            try
            {
                result.Data = purviewDAL.UpdateMenu(model);
                result.State = EmOperationState.SUCCESS;
            }
            catch (System.Exception ex)
            {
                result.State = EmOperationState.ERROR;
                Taki.Logging.LoggerFactory.Create().Error(ex);
            }
            return Json(result);
        }

        /// <summary>
        /// 更新状态（status 启用/禁用）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult UpdatePurviewStatus(string id, int status)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            try
            {
                result.Data = purviewDAL.UpdatePurviewStatus(id, status);
                result.State = EmOperationState.SUCCESS;
            }
            catch (System.Exception ex)
            {
                result.State = EmOperationState.ERROR;
                Taki.Logging.LoggerFactory.Create().Error(ex);
            }
            return Json(result);
        }

        /// <summary>
        /// 更新状态（status 删除）  和 UpdatePurviewStatus 可以合并为同一方法，这里写两个是为了区别权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ActionResult DeleteMenu(string id)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            try
            {
                if (id.IsNotNullAndWhiteSpace())
                {
                    result.Data = purviewDAL.UpdatePurviewStatus(id, 3);
                    result.State = EmOperationState.SUCCESS;
                }
                else
                {
                    result.State = EmOperationState.FAIL;
                    result.Message = "未获取到参数";
                }
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