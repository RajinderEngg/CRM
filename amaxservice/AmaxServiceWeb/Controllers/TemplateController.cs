using AmaxDataService.DataModel;
using AmaxService.HelperClasses;
using AmaxServiceWeb.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmaxServiceWeb.Controllers
{
    public class TemplateController : ApiController
    {
        // GET api/<controller>
        TemplateHelper TempHP;
        public LangResourceHelperClass LangResHP;
        public TemplateController()
        {
            TempHP = new TemplateHelper();
            LangResHP = new LangResourceHelperClass();
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        [Security]
        public ResponseData GetTemplate(int ThnksLetterId)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                //TempHP.SecurityconString= ControllerContext.RouteData.Values["SecurityContext"].ToString();
                string OrgId= ControllerContext.RouteData.Values["OrgId"].ToString();
                string lang= ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = TempHP.GetTemplate(ThnksLetterId, OrgId);

                if(ReturnObj.Data==null)
                {
                    

                    ReturnObj.IsError = true;

                    string KeyName = "APP_FNF_MSG";
                    string KeyValue = LangResHP.GetKeyValue("SCREEN_EDITTEMP", lang, KeyName);
                    if (KeyValue != "")
                    {
                        ReturnObj.ErrMsg = KeyValue;
                    }
                    else
                    {
                        if (lang == "en")
                        {
                            ReturnObj.ErrMsg = "Template not found";

                        }
                        if (lang == "he")
                        {
                            ReturnObj.ErrMsg = "תבנית לא נמצא";

                        }
                    }
                }
                else {
                    ReturnObj.IsError = false;
                    ReturnObj.ErrMsg = "";
                }
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "AmaxReportingService";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpPost]
        [Security]
        public ResponseData SaveTemplate(int ThnksLetterId, string Source)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                //TempHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                string OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                string lang = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = TempHP.SaveTemplate(ThnksLetterId, OrgId,Source);
                ReturnObj.IsError = false;
                if (Convert.ToBoolean(ReturnObj.Data) == true)
                {
                    string KeyName = "APP_EDITEMP_MSG";
                    string KeyValue = LangResHP.GetKeyValue("SCREEN_EDITTEMP", lang, KeyName);
                    if (KeyValue != "")
                    {
                        ReturnObj.ErrMsg = KeyValue;
                    }
                    else
                    {
                        if (lang == "en")
                        {
                            ReturnObj.ErrMsg = "Successfully edit template";

                        }
                        if (lang == "he")
                        {
                            ReturnObj.ErrMsg = "תבנית לערוך בהצלחה";

                        }
                    }
                }
                else
                {
                    ReturnObj.IsError = true;

                    string KeyName = "APP_FNF_MSG";
                    string KeyValue = LangResHP.GetKeyValue("SCREEN_EDITTEMP", lang, KeyName);
                    if (KeyValue != "")
                    {
                        ReturnObj.ErrMsg = KeyValue;
                    }
                    else
                    {
                        if (lang == "en")
                        {
                            ReturnObj.ErrMsg = "Template not found";

                        }
                        if (lang == "he")
                        {
                            ReturnObj.ErrMsg = "תבנית לא נמצא";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "AmaxReportingService";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
    }
}