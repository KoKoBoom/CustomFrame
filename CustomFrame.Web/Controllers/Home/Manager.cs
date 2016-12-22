using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomFrame.Web.Controllers.Home
{
    public static class Manager
    {
        public static string conn = System.Configuration.ConfigurationManager.AppSettings[10];
    }
}