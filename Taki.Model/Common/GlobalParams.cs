/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/2/14 0:55:12
** Desc：	
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taki.Model
{
    public class GlobalParams
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public const string UserCookieKey = "UserSession";
        /// <summary>
        /// 权限菜单
        /// </summary>
        public const string PurviewsOfMenuCookieKey = "PurviewsOfMenuSession";
        /// <summary>
        /// 用户权限
        /// </summary>
        public const string PurviewsOfUserCookieKey = "PurviewsOfUserSession";
        /// <summary>
        /// 所有权限
        /// </summary>
        public const string PurviewsOfAllCookieKey = "PurviewsOfAllSession";
    }
}
