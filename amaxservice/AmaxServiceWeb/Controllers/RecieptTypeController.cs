using AmaxServiceWeb.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AmaxService.ServiceModels;
using AmaxDataService.DataModel;
using AmaxService.HelperClasses;
using System.Diagnostics;

namespace AmaxServiceWeb.Controllers
{
    public class RecieptTypeController : ApiController
    {
        // GET api/<controller>
        RecieptTypeHelper RcptTypeHP;
        public RecieptTypeController()
        {
            RcptTypeHP = new RecieptTypeHelper();
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
        public ResponseData GetRecieptTypes(string lang)
        {
            ResponseData ReturnObj = new ResponseData();
            try

            {
                RcptTypeHP.SecurityconString= ControllerContext.RouteData.Values["SecurityContext"].ToString();
                RcptTypeHP.LangValue = lang;
                ReturnObj.Data = RcptTypeHP.GetRecieptTypes();
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
                LogHistObj.Action = "GetRecieptTypes";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptType Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData GetRecieptType(int RecieptTypeId)
        {
            ResponseData ReturnObj = new ResponseData();
            try

            {
                RcptTypeHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                RcptTypeHP.LangValue = ControllerContext.RouteData.Values["Language"].ToString();
                ReturnObj.Data = RcptTypeHP.GetRecieptTypes().Where(r=>r.RecieptTypeId==RecieptTypeId).ToList();
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
                LogHistObj.Action = "GetRecieptType";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptType Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
    }
}