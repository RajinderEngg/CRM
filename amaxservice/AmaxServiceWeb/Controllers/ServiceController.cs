using AmaxDataService.DataModel;
using AmaxService;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Web.Http;
using System.Dynamic;
using AmaxServiceWeb.Code;
using AmaxService.HelperClasses;
using System.Diagnostics;
using System.Configuration;

namespace AmaxServiceWeb.Controllers
{
    public class ServiceController : ApiController
    {
        // GET api/service
        ICrmService crmservice;
        LangResourceHelperClass LangResHP;
        public ServiceController() {
            crmservice = new CrmService();
            LangResHP = new LangResourceHelperClass();
        }
        //[HttpGet]
        [HttpPost]
        [ActionName("Login")]
        public ResponseData Login(LoginModel model)
        {
            ResponseData resData = new ResponseData();
            try
            {
                //model = new LoginModel();
                //model.UserName = "asdadad";
                //model.Password = "asdasdasdasd";
                //model.OrgId = "11";

                //new System.Data.DataRow().
                resData.Data = crmservice.Login(model);
            }
            catch (Exception ex)
            {
                resData.Data = null;
                resData.IsError = true;
                resData.ErrMsg = ex.Message;

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "Login";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return resData;
        }
        [HttpPost]
        [ActionName("SendSms")]
        [Security]
        public ResponseData SendSms(dynamic model)
        {
            //model = new LoginModel();
            //model.UserName = "asdadad";
            //model.Password = "asdasdasdasd";
            //model.OrgId = "11";
            
            ResponseData resData = new ResponseData();

            Dictionary<string, object> secureDictionary = new Dictionary<string, object>();
            try
            {
                string databaseconnection = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                string Isbranchenabled = ControllerContext.RouteData.Values["IsBranchEnabled"].ToString();
                string branchid = ControllerContext.RouteData.Values["Branchid"].ToString();
                string sysdata = ControllerContext.RouteData.Values["sysdata"].ToString();
                secureDictionary.Add("SecurityContext", databaseconnection);
                secureDictionary.Add("IsBranchEnabled", Isbranchenabled);
                secureDictionary.Add("Branchid", branchid);
                secureDictionary.Add("sysdata", sysdata);
                crmservice.currentUser = secureDictionary;
                //new System.Data.DataRow().
                resData.Data = crmservice.SendSms(model);
            }
            catch (Exception ex)
            {
                resData.Data = null;
                resData.IsError = true;
                resData.ErrMsg = ex.Message;

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString= ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "SendSms";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return resData;
        }

        [HttpPost]
        [Security]
        [ActionName("GetTreeData")]
        public ResponseData GetTreeData(dynamic model)
        {
            
            ResponseData resData = new ResponseData();
            try
            {
                string databaseconnection = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                Dictionary<string, object> secureDictionary = new Dictionary<string, object>();
                secureDictionary.Add("SecurityContext", databaseconnection);
                crmservice.currentUser = secureDictionary;

                resData.Data = crmservice.GetTreeData(model);
            }
            catch (Exception ex)
            {
                resData.Data = null;
                resData.IsError = true;
                resData.ErrMsg = ex.Message;

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
                LogHistObj.Action = "GetTreeData";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return resData;

        }



        [HttpPost]
        [Security]
        [ActionName("DevQuery")]
        public ResponseData DevQuery(dynamic model)
        {

            
            ResponseData resData = new ResponseData();
            try
            {
                string databaseconnection = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                Dictionary<string, object> secureDictionary = new Dictionary<string, object>();
                secureDictionary.Add("SecurityContext", databaseconnection);
                crmservice.currentUser = secureDictionary;

                resData.Data = crmservice.DevQuery(model);
            }
            catch (Exception ex)
            {
                resData.Data = null;
                resData.IsError = true;
                resData.ErrMsg = ex.Message;

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
                LogHistObj.Action = "DevQuery";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return resData;
        }
        //  DevQuery(dynamic Parameter)
        [HttpGet]
        [Security]
        [ActionName("ExecuteJson")]
        public ResponseData ExecuteJson(dynamic model)
        {
            
            ResponseData resData = new ResponseData();
            try
            {
                string databaseconnection = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                Dictionary<string, object> secureDictionary = new Dictionary<string, object>();
                secureDictionary.Add("SecurityContext", databaseconnection);
                crmservice.currentUser = secureDictionary;

                resData.Data = crmservice.ExecuteJson(model);
            }
            catch (Exception ex)
            {
                resData.Data = null;
                resData.IsError = true;
                resData.ErrMsg = ex.Message;

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
                LogHistObj.Action = "ExecuteJson";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return resData;
        }

        [HttpPost]
        //[Security]
        [ActionName("ExecuteDataService")]
        public ResponseData ExecuteDataService(dynamic model)
        {
            //string databaseconnection = ControllerContext.RouteData.Values["SecurityContext"].ToString();
            ResponseData resData = new ResponseData();
            try
            {
                //Dictionary<string, object> secureDictionary = new Dictionary<string, object>();
                //secureDictionary.Add("SecurityContext", databaseconnection);
                //crmservice.currentUser = secureDictionary;

                resData.Data = crmservice.ExecuteDataService(model);
            }
            catch (Exception ex)
            {
                resData.Data = null;
                resData.IsError = true;
                resData.ErrMsg = ex.Message;

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "ExecuteDataService";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Service Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return resData;
        }

        [HttpPost]
        [Security]
        [ActionName("AmaxReportingService")]
        public ResponseData AmaxReportingService(dynamic model)
        {
            
            ResponseData resdata = new ResponseData();
            try {
                string databaseconnection = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                Dictionary<string, object> secureDictionary = new Dictionary<string, object>();
                secureDictionary.Add("SecurityContext", databaseconnection);
                crmservice.currentUser = secureDictionary;

                resdata.Data = crmservice.AmaxReportingService(model);
            } catch (Exception ex) {
                resdata.Data = null;
                resdata.IsError = true;
                resdata.ErrMsg = ex.Message;

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
            return resdata;
        }
    }
}
