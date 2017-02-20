using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Taki.Web.Filters
{
    /// <summary>
    /// 授权
    /// </summary>
    public enum EmAppAuthorization
    {
        /// <summary>
        /// 不允许访问
        /// </summary>
        NoAllow,
        /// <summary>
        /// 允许访问
        /// </summary>
        Allow
    }

    /// <summary>
    /// 应用授权
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AppAuthorizationAttribute : Attribute
    {
        public AppAuthorizationAttribute(EmAppAuthorization value)
        {
            Value = value;
        }
        public EmAppAuthorization Value { get; private set; }
    }
}