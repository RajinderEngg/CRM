using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AmaxService.HelperClasses;
using AmaxDataService.DataModel;
using Newtonsoft.Json;
using AmaxService.ServiceModels;
using AmaxServiceWeb.Code;
using System.Diagnostics;
using System.Configuration;

namespace AmaxServiceWeb.Controllers
{
    public class DropdownController : ApiController
    {
        // GET api/<controller>
        DataBind db;
        LangResourceHelperClass LangResHP;
        LogHistoryHelper LogHistHP;
        public DropdownController()
        {
            db = new DataBind();
            LangResHP = new LangResourceHelperClass();
            LogHistHP = new LogHistoryHelper();
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
        public ResponseData BindCustType(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = lang;
                ReturnObj.Data = db.GetCustType();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindCustType", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindCustType";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindEmployees()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetEmployees();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindEmployees", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindEmployees";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindEmployee()
        {
           ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                int empid =Convert.ToInt32(Convert.ToString( ControllerContext.RouteData.Values["employeeid"]));
                
                ReturnObj.Data = db.GetEmployees().Where(r=>r.Value==empid.ToString()).ToList();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindEmployee", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindEmployees";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindEmployeeFromEmpName(string EmpName)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                
                ReturnObj.Data = db.GetEmployees().Where(r => r.Text == EmpName.ToString()).ToList();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindEmployeeFromEmpName", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindEmployees";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData BindThanksLetters(int ReceiptId)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetThnksLetters(ReceiptId);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindThanksLetters", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindThanksLetters";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindAssociation()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.Getassociation();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindAssociation", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindAssociation";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData BindSources()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();

                ReturnObj.Data = db.GetCustSources();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
                //////////For Checking Logging
                //ReturnObj.ErrMsg = "bhghg";
                //int clt = Convert.ToInt32(ReturnObj.ErrMsg);
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindSources", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindSources";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
                
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindSuffixes(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = lang;
                ReturnObj.Data = db.GetSuffixes();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindSuffixes", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindSuffixes";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindPhoneTypes(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.LangValue = lang;
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetPhoneTypes();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindPhoneTypes", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindPhoneTypes";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindAddressType(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.LangValue = lang;
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetAddressType();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindAddressType", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindAddressType";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindGroups(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.LangValue = lang;
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetGroups();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindGroups", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindGroups";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindCountries(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.LangValue = lang;
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetCountries();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindCountries", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindCountries";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindStates(string countryName = null)
        {

            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetStates(countryName);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindStates", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindStates";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindCities(string CountryCode = null, string stateName = null)
        {

            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetCities(CountryCode, stateName);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindCities", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindCities";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData GroupTree(string lang, bool IsShowAll)
        {

            ResponseData ReturnObj = new ResponseData();
            try
            {

                string language = lang;//"en";
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                List<GroupTree> x = db.GetGroupDataSet(IsShowAll);
                if (IsShowAll == true)
                {
                    var kendoTreeRoot = x.Where(e => e.GroupId == 0).Select(e => new KendoGroupTree()
                    {
                        id = e.GroupId,
                        text = language == "en" ? e.GroupNameEng : e.GroupName,
                        expanded = true
                    }).ToList<KendoGroupTree>();
                    kendoTreeRoot.ForEach(a =>
                    {
                        a.items = x.Where(e => e.GroupParenCategory == a.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                        {
                            id = e.GroupId,
                            text = language == "en" ? e.GroupNameEng : e.GroupName,
                        }).ToList<KendoGroupTree>();
                        a.items.ForEach(b =>
                        {
                            b.items = x.Where(e => e.GroupParenCategory == b.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                            {
                                id = e.GroupId,
                                text = language == "en" ? e.GroupNameEng : e.GroupName,
                            }).ToList<KendoGroupTree>();

                            b.items.ForEach(c =>
                            {
                                c.items = x.Where(e => e.GroupParenCategory == c.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                {
                                    id = e.GroupId,
                                    text = language == "en" ? e.GroupNameEng : e.GroupName,
                                }).ToList<KendoGroupTree>();

                                c.items.ForEach(d =>
                                {
                                    d.items = x.Where(e => e.GroupParenCategory == d.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                    {
                                        id = e.GroupId,
                                        text = language == "en" ? e.GroupNameEng : e.GroupName,
                                    }).ToList<KendoGroupTree>();
                                    d.items.ForEach(f =>
                                    {
                                        f.items = x.Where(e => e.GroupParenCategory == f.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                        {
                                            id = e.GroupId,
                                            text = language == "en" ? e.GroupNameEng : e.GroupName,
                                        }).ToList<KendoGroupTree>();
                                        f.items.ForEach(g =>
                                        {
                                            g.items = x.Where(e => e.GroupParenCategory == g.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                            {
                                                id = e.GroupId,
                                                text = language == "en" ? e.GroupNameEng : e.GroupName,
                                            }).ToList<KendoGroupTree>();
                                            g.items.ForEach(h =>
                                            {
                                                h.items = x.Where(e => e.GroupParenCategory == h.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                                {
                                                    id = e.GroupId,
                                                    text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                }).ToList<KendoGroupTree>();
                                                h.items.ForEach(i =>
                                                {
                                                    i.items = x.Where(e => e.GroupParenCategory == i.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                                    {
                                                        id = e.GroupId,
                                                        text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                    }).ToList<KendoGroupTree>();
                                                    i.items.ForEach(j =>
                                                    {
                                                        j.items = x.Where(e => e.GroupParenCategory == j.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                                        {
                                                            id = e.GroupId,
                                                            text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                        }).ToList<KendoGroupTree>();
                                                        j.items.ForEach(k =>
                                                        {
                                                            k.items = x.Where(e => e.GroupParenCategory == k.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                                            {
                                                                id = e.GroupId,
                                                                text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                            }).ToList<KendoGroupTree>();
                                                            k.items.ForEach(l =>
                                                            {
                                                                l.items = x.Where(e => e.GroupParenCategory == l.id && e.GroupId != 0).Select(e => new KendoGroupTree()
                                                                {
                                                                    id = e.GroupId,
                                                                    text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                                }).ToList<KendoGroupTree>();
                                                            });
                                                        });
                                                    });
                                                });
                                            });
                                        });
                                    });
                                });
                            });
                        });
                    });
                    ReturnObj.Data = kendoTreeRoot;
                    ReturnObj.IsError = false;
                    ReturnObj.ErrMsg = "";
                }
                else
                {
                    var kendoTreeRoot = x.Select(e => new KendoGroupTree()
                    {
                        id = e.GroupId,
                        text = language == "en" ? e.GroupNameEng : e.GroupName,
                        expanded = true
                    }).ToList<KendoGroupTree>();
                    ReturnObj.Data = kendoTreeRoot;
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GroupTree", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GroupTree";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;



        }
        [HttpGet]
        //[Security]
        public ResponseData GetLangRes(string FormType, string Lang)
        {
            ResponseData resData = new ResponseData();
            try
            {
                if (Lang == null) { Lang = "en"; }
                //string databaseconnection = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                //string databaseconnection =@"Server=ADMIN-PC1\SQLEXPRESS;Database=jaffanet1;User Id=sa;Password=admin@123";

                //Dictionary<string, object> secureDictionary = new Dictionary<string, object>();
                //secureDictionary.Add("SecurityContext", databaseconnection);
                //crmservice.currentUser = secureDictionary;
                //LangResHP.SecurityConString = databaseconnection;
                resData.Data = LangResHP.GetLangResDict(FormType, Lang);
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
                LogHistObj = LogHistHP.GetLogHistoryDet(null, "", "", ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetLangRes", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                ////LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetLangRes";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return resData;

        }
        [HttpGet]
        [Security]
        public ResponseData BindReceiptTypes()
        {

            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetReceiptTypes();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindReceiptTypes", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindReceiptTypes";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindAutoCompleteSrch()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.AutoCompleteSrch();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindAutoCompleteSrch", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindAutoCompleteSrch";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData BindTerminalList()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetTerminalList();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindTerminalList", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindTerminalList";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        public ResponseData BindCurrencyList()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                
                ReturnObj.Data = db.GetCurrencyList();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(null, "","", ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindCurrencyList", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                ////LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                ////LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                ////LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindCurrencyList";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [Security]
        [HttpGet]
        public ResponseData BindCurrencyListFromDb()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetCurrencyListFromDb();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["SecurityContext"].ConnectionString.ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindCurrencyListFromDb", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindCurrencyListFromDb";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindCreditTypeList()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {

                ReturnObj.Data = db.GetCreditTypeList();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(null,"","", ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindCreditTypeList", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        public ResponseData BindChargeTypeList()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {

                ReturnObj.Data = db.GetChargeTypeList();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(null, "", "", ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindChargeTypeList", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                ////LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                ////LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                ////LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindChargeTypeList";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        public ResponseData BindYears()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                DateTime CurrDate = DateTime.Now;
                int CurrYr = CurrDate.Year;
                ReturnObj.Data = db.GetYears(CurrYr);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(null,"", "", ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindYears", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                ////LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                ////LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                ////LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindYears";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        public ResponseData BindMonths()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {

                ReturnObj.Data = db.GetMonths();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(null,"","", ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindMonths", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                ////LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                ////LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                ////LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindMonths";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData GetPhoneTypeDet(int PhoneTypeId)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = db.GetPhoneTypeDet(PhoneTypeId);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetPhoneTypeDet", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindMonths";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData GetReceiptModes(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = lang;
                ReturnObj.Data = db.GetReceiptModes();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetReceiptModes", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindMonths";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData BindCustomerNoteList()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                
                ReturnObj.Data = db.GetCustomerNotes();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "BindCustomerNoteList", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "BindCustomerNoteList";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData GetPayType()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue= ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetPayType();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetPayType", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetPayType";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }
        [HttpGet]
        [Security]
        public ResponseData GetAccounts()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetAccounts();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetAccounts", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetAccounts";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }
        [HttpGet]
        [Security]
        public ResponseData GetBanks()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetBanks();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetBanks", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetBanks";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }

        [HttpGet]
        [Security]
        public ResponseData GetGoals()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetGoals();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetGoals", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetGoals";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }
        [HttpGet]
        [Security]
        public ResponseData GetPrinter()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {

                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetPrinter();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetPrinter", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetPrinter";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }
        [HttpGet]
        [Security]
        public ResponseData GetReceiptDetail()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                //db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetReceiptDetail();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetReceiptDetail", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetPrinter";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }
        [HttpGet]
        [Security]
        public ResponseData GetProjectCats()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetProjectCats();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetProjectCats", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetProjectCats";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }
        [HttpGet]
        [Security]
        public ResponseData GetProjects(int ProjectCatId)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetProjects(ProjectCatId);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetProjects", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetProjects";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }
        [HttpGet]
        [Security]
        public ResponseData GetCustTitles()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetCustTitle();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustTitles", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetProjects";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }

        [HttpGet]
        [Security]
        public ResponseData GetProductCats()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                db.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                db.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = db.GetProductCats();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetProductCats", ex.ToString(), AppConfig.APIVersion, "Dropdown Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetProductCats";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Dropdown Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;

        }

    }
}