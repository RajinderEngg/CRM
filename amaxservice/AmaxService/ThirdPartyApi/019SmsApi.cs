using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.ThirdPartyApi
{
    public class SmsApi019
    {

        public static string TelzarSendMessages(string xml) => TelzarSendMessages(
            ConfigurationManager.AppSettings["019SmsServiceUrl"] != null ? ConfigurationManager.AppSettings["019SmsServiceUrl"] : "https://www.019sms.co.il/api", xml);
        public static string TelzarSendMessages(string url, string xml)
        {
            if (xml == null || xml.Length <= 0)
            {
                return "UnknownError";
            }
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            webRequest.ContentType = "application/xml";
            webRequest.ContentLength = (long)bytes.Length;
            Stream requestStream = webRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            WebResponse response = webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            response.Close();
            return result;
        }

        public static string TelzarParseResult(string result)
        {
            if (result == null)
            {
                return "UnknownError";
            }
            if (result.Contains("<status>0</status>"))
            {
                return "Success";
            }
            if (result.Contains("<status>1</status>"))
            {
                return "XmlError";
            }
            if (result.Contains("<status>2</status>"))
            {
                return "MissingField";
            }
            if (result.Contains("<status>3</status>"))
            {
                return "BadLogin";
            }
            if (result.Contains("<status>4</status>"))
            {
                return "NoCredits";
            }
            if (result.Contains("<status>5</status>"))
            {
                return "NoPermission";
            }
            if (result.Contains("<status>997</status>"))
            {
                return "InvalidCommand";
            }
            if (result.Contains("<status>998</status>") || !result.Contains("<status>999</status>"))
            {
                return "UnknownError";
            }
            return "CallToSupport";
        }
    }
}