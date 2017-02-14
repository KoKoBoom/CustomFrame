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
using Taki.Model;

namespace Taki.DAL
{
    public class MenuDAL
    {
        public IList<sysmenu> GetAll()
        {
            using (takiEntities _db = new takiEntities())
            {
                return _db.sysmenu.ToList();
            }
        }
    }
}
