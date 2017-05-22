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
    public class GeneralGroupsController : ApiController
    {
        GeneralGroupsHelper GrnGrpHP;
        LogHistoryHelper LogHistHP;
        public GeneralGroupsController()
        {
            GrnGrpHP = new GeneralGroupsHelper();
            LogHistHP = new LogHistoryHelper();
        }
        // GET api/<controller>
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
        public ResponseData GetCustomersListOfGroups(string GroupIds)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                GrnGrpHP.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                List<CustomersModel> CustList = new List<CustomersModel>();
                if(string.IsNullOrEmpty(GroupIds)==false)
                CustList= GrnGrpHP.GetCustomersListOfGrps(GroupIds);
                ReturnObj.Data = CustList;
                if (CustList != null)
                {
                    ReturnObj.IsError = false;
                    ReturnObj.ErrMsg = "";
                }
                else
                {
                    ReturnObj.IsError = true;
                    ReturnObj.ErrMsg = "You can not select the Primary group together with another group";
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
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomersListOfGroups", ex.ToString(), AppConfig.APIVersion, "GeneralGroups Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomersListOfGroups";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "GeneralGroups Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
    }
}