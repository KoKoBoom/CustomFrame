/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/19 9:54:34
** Desc：	操作结果（分页）
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taki.Common
{
    public class OperationPagingResult<T> : OperationResult<T>
    {
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal { get { return Math.Ceiling(DataTotal.To<decimal>(1, true) / PageSize.To<decimal>(1, true)).To<int>(1, true); } }
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int DataTotal { get; set; } = 1;
    }
}
