using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmaxServiceWeb.Code
{
    public class AppConfig
    {
        public static string ServiceAuthValue
        {
            get
            {

                return System.Web.Configuration.WebConfigurationManager.AppSettings["authkey"] + "_" + DateTime.Now.ToString("MM-dd-yyyy");
            }
        }
    }
}