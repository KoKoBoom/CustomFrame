using System.Web.Http;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Taki.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Logging.Logger4Factory.SetConfig(Server.MapPath("~/bin/Extend/log4net/log4net.config"));
            //Logging.LoggerFactory.SetCurrent(new Logging.Logger4Factory());
        }
    }
}
