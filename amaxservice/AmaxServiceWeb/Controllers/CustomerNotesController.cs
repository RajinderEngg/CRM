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
    public class CustomerNotesController : ApiController
    {
        // GET api/customernotes
        public CustomersNoteHelper CustNoteHP;
        public LogHistoryHelper LogHistHP;
        public CustomerNotesController()
        {
            CustNoteHP = new CustomersNoteHelper();
            LogHistHP = new LogHistoryHelper();
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        [HttpGet]
        [Security]
        public ResponseData GetTerminalDetByTermNo()
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustNoteHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                string Query = "select id,note from tblCustomNotesItems where subjectid=0 order by note";
                returnObj.Data = CustNoteHP.GetCustomerNoteList(Query);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetTerminalDetByTermNo", ex.ToString(), AppConfig.APIVersion, "CustomerNotes Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "CheckCustOfSameNameComp";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "CheargeCredit Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
    }
}
