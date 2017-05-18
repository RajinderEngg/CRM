using AmaxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace AmaxServiceWeb.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SecurityAttribute : AuthorizeAttribute
    {
      
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            throw new HttpResponseException(challengeMessage);
        }
        
        private bool Authorize(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                
                //ICrmService crm
                string keyValue = AppConfig.ServiceAuthValue;
                KeyValuePair<string,IEnumerable<string>> currentUsers = (from h in actionContext.Request.Headers where h.Key == "X-Token" select h).FirstOrDefault();
                if (currentUsers.Value!=null)
                {
                    var currentUser = XTokenizer.ValidateToken(currentUsers.Value.First().ToString(), true);
                //currentUser["SecurityContext"] = StringCipher.Decrypt(currentUser["SecurityContext"].ToString());
                
                actionContext.ControllerContext.RouteData.Values.Add("SecurityContext", StringCipher.Decrypt(currentUser["SecurityContext"].ToString()));
                actionContext.ControllerContext.RouteData.Values.Add("IsBranchEnabled", currentUser["IsBranchEnabled"].ToString());
                actionContext.ControllerContext.RouteData.Values.Add("Branchid", currentUser["Branchid"].ToString());
                actionContext.ControllerContext.RouteData.Values.Add("sysdata", currentUser["sysdata"].ToString());
                actionContext.ControllerContext.RouteData.Values.Add("Language", currentUser["Language"].ToString());
                actionContext.ControllerContext.RouteData.Values.Add("OrgId", currentUser["OrgId"].ToString());
                actionContext.ControllerContext.RouteData.Values.Add("employeeid", currentUser["employeeid"].ToString());
                actionContext.ControllerContext.RouteData.Values.Add("fname", currentUser["fname"].ToString());
                
                }
                //else return false;
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string Decrypt(string input)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes("asdfewrewqrss323");
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}