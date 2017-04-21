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
    public class RecieptThnksLetterController : ApiController
    {
        // GET api/<controller>
        RecieptThnksLetterHelper RcptTnksLtrsHP;
        public RecieptThnksLetterController()
        {
            RcptTnksLtrsHP = new RecieptThnksLetterHelper();
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
        public ResponseData GetRecieptThnksLetters(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                RcptTnksLtrsHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                RcptTnksLtrsHP.LangValue = lang;
                ReturnObj.Data = RcptTnksLtrsHP.GetRecieptThnksLetters();
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
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "GetRecieptThnksLetters";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptThnksLetter Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData GetRecieptThnksLetter(int ThnksLetterId)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                RcptTnksLtrsHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = RcptTnksLtrsHP.GetRecieptThnksLetter(ThnksLetterId);
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
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "GetRecieptThnksLetter";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptThnksLetter Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData GetRecieptTnksLtrsByRcptId(int RecieptTypeId, string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                RcptTnksLtrsHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                RcptTnksLtrsHP.LangValue = lang;
                ReturnObj.Data = RcptTnksLtrsHP.GetRecieptThnksLettersByRcptTypId(RecieptTypeId);
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
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "GetRecieptTnksLtrsByRcptId";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptThnksLetter Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpPost]
        [Security]
        public ResponseData Save(RecieptThnksLetterModel RcptThnksLtrObj)
        {
            ResponseData returnObj = new ResponseData();
            if ( RcptThnksLtrObj.ReceiptId != 0)
            {
                RcptTnksLtrsHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                try
                {
                    if (string.IsNullOrEmpty(RcptThnksLtrObj.ThanksLetterName))
                    {
                        RcptThnksLtrObj.ThanksLetterName = "";
                    }
                    if (string.IsNullOrEmpty(RcptThnksLtrObj.ThanksLetterNameEng))
                    {
                        RcptThnksLtrObj.ThanksLetterNameEng = "";
                    }
                    if (string.IsNullOrEmpty(RcptThnksLtrObj.ThanksLetterfileName))
                    {
                        RcptThnksLtrObj.ThanksLetterfileName = "";
                    }
                    if (ModelState.IsValid)
                    {
                        int g = RcptTnksLtrsHP.SaveReceiptThnksLetter(RcptThnksLtrObj);
                        if (g > 0)
                        {
                            returnObj.IsError = false;
                            returnObj.ErrMsg = "Successfully Saved";
                        }
                        else {
                            returnObj.IsError = true;
                            returnObj.ErrMsg = "There is problem in saving";
                        }
                    }
                }
                catch (Exception ex)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = ex.Message;

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
                    LogHistObj.Action = "Save";
                    LogHistObj.FullDescription = ex.ToString();
                    LogHistObj.ExeptionType = "ERROR";
                    LogHistObj.APIVersion = AppConfig.APIVersion;
                    LogHistObj.FromPage = "RecieptThnksLetter Controller";
                    LogHistObj.OnDate = System.DateTime.Now;
                    LogHistObj.ex = ex;
                    SendEmail.SendEmailErr(LogHistObj,conString);
                }
            }
            else
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "Please select receipt type";
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData DeleteReceiptThnksLetter(int ThnksLetterId)
        {
            ResponseData returnObj = new ResponseData();
            RcptTnksLtrsHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
            try
            {

                int g = RcptTnksLtrsHP.DeleteReceiptThnksLetter(ThnksLetterId);
                if (g > 0)
                {
                    returnObj.IsError = false;
                    returnObj.ErrMsg = "Successfully Deleted";
                }
                else {
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "There is problem in deleting";
                }
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;

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
                LogHistObj.Action = "DeleteReceiptThnksLetter";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptThnksLetter Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
    }
}