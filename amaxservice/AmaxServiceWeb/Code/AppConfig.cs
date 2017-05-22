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
        public static string FromEmail
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["FromEmail"];
            }
        }
        public static string strSub
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["strSub"];
            }
        }
        public static string ReplytoMail
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["ReplytoMail"];
            }
        }
        public static string ReplytoName
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["ReplytoName"];
            }
        }
        public static string ToEmail
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["ToEmail"];
            }
        }
        public static string Port
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];
            }
        }
        public static string SMT
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["SMT"];
            }
        }
        public static string SenderEmail
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"];
            }
        }
        public static string APIVersion
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["APIVersion"];
            }
        }
        public static string CreditUrl
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["CreditUrl"];
            }
        }
        public static string CreditPrintUrl
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["CreditPrintUrl"];
            }
        }
        public static string DBVersion
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["DBVersion"];
            }
        }
    }
}