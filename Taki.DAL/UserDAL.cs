/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/2/13 1:33:09
** Desc：	
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taki.Model;

namespace Taki.DAL
{
    public class UserDAL
    {
        public bool Save(user model)
        {
            using (takiEntities _db = new takiEntities())
            {
                _db.user.Add(model);
                return _db.SaveChanges() > 0;
            }
        }
    }
}
