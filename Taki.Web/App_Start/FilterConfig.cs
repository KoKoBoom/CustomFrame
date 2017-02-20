using Taki.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace Taki.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //添加自定义全局异常
            //filters.Add(new AjaxExceptionAttribute());
            //filters.Add(new AppAuthorizationAttribute(EmAppAuthorization.NoAllow));
        }
    }
}
