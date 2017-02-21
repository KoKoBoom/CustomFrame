/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/2/15 0:18:21
** Desc：	菜单管理
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taki.Common;
using Taki.Model;

namespace Taki.DAL
{
    public class PurviewDAL
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public IList<purview> GetAllMenu()
        {
            using (takiEntities _db = new takiEntities())
            {
                var menus = _db.purview.Where(x => x.PurviewType == "menu" && x.Status != 3).OrderBy(x => x.SequenceNO).ToList();
                var newMenus = new List<purview>();
                foreach (var item in menus.Where(x => x.PPurviewID.IsNullOrWhiteSpace()))
                {
                    newMenus.Add(item);
                    newMenus.AddRange(menus.Where(x => x.PPurviewID == item.PurviewID));
                }
                return newMenus;
            }
        }

        /// <summary>
        /// 获取用户对应的权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<purview> GetPurviewsOfUser(user model)
        {
            using (takiEntities _db = new takiEntities())
            {
                if (model.IsAdministrator)
                {
                    return _db.purview.Where(x => x.Status == 1).ToList();
                }
                else
                {
                    return (from a in _db.userrole
                            join b in _db.role on a.RoleID equals b.RoleID
                            join c in _db.rolepurview on b.RoleID equals c.RoleID
                            join d in _db.purview on c.PurviewID equals d.PurviewID
                            where a.UserID == model.UserID && d.Status == 1
                            select d).ToList();
                }
            }
        }

        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        /// <returns></returns>
        public IList<purview> GetAllPurview()
        {
            using (takiEntities _db = new takiEntities())
            {
                return _db.purview.ToList();
            }
        }

        /// <summary>
        /// 删除菜单以及它的子项（伪删除）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMenu(string id, int status)
        {
            using (takiEntities _db = new takiEntities())
            {
                var allModels = _db.purview.ToList();
                var model = _db.purview.FirstOrDefault(x => x.PurviewID == id);
                if (model != null)
                {
                    UpdateChild(model, allModels, status);
                }
                return _db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除子项
        /// </summary>
        /// <param name="currentModel"></param>
        /// <param name="allModels"></param>
        public void UpdateChild(purview currentModel, IEnumerable<purview> allModels, int status)
        {
            currentModel.Status = status;
            var childModels = allModels.Where(x => x.PPurviewID == currentModel.PurviewID);
            foreach (var item in childModels.WhenNullThenDefault())
            {
                UpdateChild(item, allModels, status);
            }
        }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdatePurviewStatus(string id, int status)
        {
            using (takiEntities _db = new takiEntities())
            {
                var allModels = _db.purview.ToList();
                var model = _db.purview.FirstOrDefault(x => x.PurviewID == id);
                if (model != null)
                {
                    UpdateChild(model, allModels, status);
                }
                return _db.SaveChanges() > 0;
            }
        }

        public bool UpdateMenu(purview model)
        {
            using (takiEntities _db = new takiEntities())
            {
                var _model = _db.purview.FirstOrDefault(x => x.PurviewID == model.PurviewID);
                _model.PurviewName = model.PurviewName;
                _model.PurviewUrl = model.PurviewUrl;
                _model.PurviewIcon = model.PurviewIcon;
                _model.PPurviewID = model.PPurviewID;
                _model.SequenceNO = model.SequenceNO;
                _model.PurviewType = model.PurviewType;
                _model.Status = model.Status;
                return _db.SaveChanges() > 0;
            }
        }

    }
}
