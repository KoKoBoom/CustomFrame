﻿/********************************************************************************
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
using Taki.Model;

namespace Taki.DAL
{
    public class PurviewDAL
    {
        public IList<purview> GetAllMenu()
        {
            using (takiEntities _db = new takiEntities())
            {
                return _db.purview.Where(x => x.PurviewType == "menu").OrderBy(x => x.SequenceNO).ToList();
            }
        }

        public IList<purview> GetPurviewsOfUser(user model)
        {
            using (takiEntities _db = new takiEntities())
            {
                if (model.IsAdministrator)
                {
                    return _db.purview.ToList();
                }
                else
                {
                    return (from a in _db.userrole
                            join b in _db.role on a.RoleID equals b.RoleID
                            join c in _db.rolepurview on b.RoleID equals c.RoleID
                            join d in _db.purview on c.PurviewID equals d.PurviewID
                            where a.UserID == model.UserID
                            select d).ToList();
                }
            }
        }

        public IList<purview> GetAllPurview()
        {
            using (takiEntities _db = new takiEntities())
            {
                return _db.purview.ToList();
            }
        }
    }
}
