/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/2/18 2:10:24
** Desc：	
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taki.Model.DTO
{
    public class MenuDTO 
    {
        public purview MenuItem { get; set; }
        public List<purview> SubItems { get; set; }
    }
}
