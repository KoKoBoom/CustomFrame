using System.Web.Http;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Optimization;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

            SetNewtonsoftDefault();
        }

        /// <summary>
        /// Newtonsoft.Json 全局配置
        /// </summary>
        private static void SetNewtonsoftDefault()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                //设置 Enum 输出 key
                setting.Converters.Add(new StringEnumConverter { AllowIntegerValues = false });

                //空值处理
                //setting.NullValueHandling = NullValueHandling.Ignore;

                //高级用法九中的Bool类型转换 设置
                //setting.Converters.Add(new BoolConvert("是,否"));
                return setting;
            });
        }
    }
}
