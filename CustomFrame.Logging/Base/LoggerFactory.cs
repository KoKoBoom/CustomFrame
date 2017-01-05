using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taki.Logging
{
    public class LoggerFactory
    {
        #region Fields

        private static ILoggerFactory _currentLogFactory;

        #endregion

        #region Public Methods

        /// <summary>
        ///   Set the  log factory to use
        /// </summary>
        /// <param name="logFactory"> Log factory to use </param>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _currentLogFactory = logFactory;
        }

        /// <summary>
        ///   Createt a new <paramref name="Company.Client.Project.CrossCutting.Logging.ILog" />
        /// </summary>
        /// <returns> Created ILog </returns>
        public static ILogger CreateLog()
        {
            if (_currentLogFactory == null) { DefaultConfig(); }
            return _currentLogFactory.Create();
        }

        static void DefaultConfig()
        {
            var strDefaultLogConfig = "Taki.Logging.Logger4Factory";
            var defColl = System.Configuration.ConfigurationManager.AppSettings["DefaultLogConfig"];
            if (defColl != null && !string.IsNullOrWhiteSpace(defColl.ToString()))
            {
                strDefaultLogConfig = defColl.ToString();
            }
            (System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(strDefaultLogConfig) as ILoggerFactory).DefaultFactory();
        }

        #endregion
    }
}
