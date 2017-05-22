using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
    public class CustomerServiceHelper
    {
        public string SecurityconString { get; set; }
        public string LangValue { get; set; }
        public Dictionary<string, object> GetCustomerServiceDict(CustomerServiceModel custServiceobj)
        {
            Dictionary<string, object> CustWebsiteDictList = new Dictionary<string, object>();
            //try
            //{
            if (custServiceobj != null)
            {
                CustWebsiteDictList.Add("CustomerServiceID", custServiceobj.CustomerServiceID);
                CustWebsiteDictList.Add("RowDate", custServiceobj.RowDate);
                CustWebsiteDictList.Add("EmployId", custServiceobj.EmployId);
                CustWebsiteDictList.Add("ServiceTypeId", custServiceobj.ServiceTypeId);
                CustWebsiteDictList.Add("StartTime", custServiceobj.StartTime);
                CustWebsiteDictList.Add("StartHour", custServiceobj.StartHour);
                CustWebsiteDictList.Add("StartMinute", custServiceobj.StartMinute);
                CustWebsiteDictList.Add("Details", custServiceobj.Details);
                CustWebsiteDictList.Add("MemoDate", custServiceobj.MemoDate);
                CustWebsiteDictList.Add("MemoHour", custServiceobj.MemoHour);
                CustWebsiteDictList.Add("MemoMinutes", custServiceobj.MemoMinutes);
                CustWebsiteDictList.Add("FileName", custServiceobj.FileName);
                CustWebsiteDictList.Add("customerid", custServiceobj.customerid);
                CustWebsiteDictList.Add("DoneiT", custServiceobj.DoneiT);
                CustWebsiteDictList.Add("Employeehandle", custServiceobj.Employeehandle);
                CustWebsiteDictList.Add("DoneDate", custServiceobj.DoneDate);
                CustWebsiteDictList.Add("CallerName", custServiceobj.CallerName);
                CustWebsiteDictList.Add("CallerPhone", custServiceobj.CallerPhone);
                CustWebsiteDictList.Add("CallerPhone1", custServiceobj.CallerPhone1);
                CustWebsiteDictList.Add("CallerEmail", custServiceobj.CallerEmail);
                CustWebsiteDictList.Add("CallerMorInfo", custServiceobj.CallerMorInfo);
                CustWebsiteDictList.Add("EmployeeMemo", custServiceobj.EmployeeMemo);
                CustWebsiteDictList.Add("ProjectMajorTaskId", custServiceobj.ProjectMajorTaskId);
                CustWebsiteDictList.Add("Title", custServiceobj.Title);
                CustWebsiteDictList.Add("StartDate", custServiceobj.StartDate);
                CustWebsiteDictList.Add("EndDate", custServiceobj.EndDate);
                CustWebsiteDictList.Add("Amount", custServiceobj.Amount);
                CustWebsiteDictList.Add("CurrencyId", custServiceobj.CurrencyId);
                CustWebsiteDictList.Add("RemindCreator", custServiceobj.RemindCreator);
                CustWebsiteDictList.Add("TaskStatus", custServiceobj.TaskStatus);
                CustWebsiteDictList.Add("FoundationID", custServiceobj.FoundationID);
                CustWebsiteDictList.Add("AmountReceived", custServiceobj.AmountReceived);
                CustWebsiteDictList.Add("ApplyDate", custServiceobj.ApplyDate);
                CustWebsiteDictList.Add("Answerdate", custServiceobj.Answerdate);
                CustWebsiteDictList.Add("DonotJump", custServiceobj.DonotJump);
                CustWebsiteDictList.Add("IsReminderSms", custServiceobj.IsReminderSms);

                CustWebsiteDictList.Add("IsReminderEmail", custServiceobj.AmountReceived);
                CustWebsiteDictList.Add("ParentServiceID", custServiceobj.ApplyDate);
                CustWebsiteDictList.Add("FileName2", custServiceobj.Answerdate);
                CustWebsiteDictList.Add("FileName3", custServiceobj.DonotJump);
                CustWebsiteDictList.Add("IsDeleted", custServiceobj.IsDeleted);
            }

            // }
            // catch (Exception ex)
            //{
            //}
            return CustWebsiteDictList;
        }
        public List<CustomerServiceModel> GetCustomerServiceListFromDS(DataSet custobj)
        {
            List<CustomerServiceModel> FinalDictList = new List<CustomerServiceModel>();

            //try
            //{
            for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
            {
                CustomerServiceModel CustServiceDictList = new CustomerServiceModel();
                CustServiceDictList.CustomerServiceID = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerServiceID"]);
                CustServiceDictList.RowDate =Convert.ToDateTime( Convert.ToString(custobj.Tables[0].Rows[i]["RowDate"])).ToString("dd-MM-yyyy");
                CustServiceDictList.EmployId = Convert.ToInt32(custobj.Tables[0].Rows[i]["EmployId"]);
                CustServiceDictList.ServiceTypeId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ServiceTypeId"]);
                CustServiceDictList.StartTime =Convert.ToDateTime( Convert.ToString(custobj.Tables[0].Rows[i]["StartTime"])).ToString("dd-MM-yyyy");
                CustServiceDictList.StartHour = Convert.ToString(custobj.Tables[0].Rows[i]["StartHour"]);
                CustServiceDictList.StartMinute = Convert.ToString(custobj.Tables[0].Rows[i]["StartMinute"]);
                CustServiceDictList.Details = Convert.ToString(custobj.Tables[0].Rows[i]["Details"]);
                CustServiceDictList.MemoDate = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["MemoDate"])).ToString("dd-MM-yyyy") ;
                CustServiceDictList.MemoHour = Convert.ToString(custobj.Tables[0].Rows[i]["MemoHour"]);
                CustServiceDictList.MemoMinutes = Convert.ToString(custobj.Tables[0].Rows[i]["MemoMinutes"]);
                CustServiceDictList.FileName = Convert.ToString(custobj.Tables[0].Rows[i]["FileName"]);
                CustServiceDictList.customerid = Convert.ToInt32(custobj.Tables[0].Rows[i]["customerid"]);
                CustServiceDictList.DoneiT = Convert.ToBoolean(custobj.Tables[0].Rows[i]["DoneiT"]);
                CustServiceDictList.Employeehandle = Convert.ToInt32(custobj.Tables[0].Rows[i]["Employeehandle"]);
                CustServiceDictList.DoneDate = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["DoneDate"])).ToString("dd-MM-yyyy") ;
                CustServiceDictList.CallerName = Convert.ToString(custobj.Tables[0].Rows[i]["CallerName"]);
                CustServiceDictList.CallerPhone = Convert.ToString(custobj.Tables[0].Rows[i]["CallerPhone"]);
                CustServiceDictList.CallerPhone1 = Convert.ToString(custobj.Tables[0].Rows[i]["CallerPhone1"]);
                CustServiceDictList.CallerEmail = Convert.ToString(custobj.Tables[0].Rows[i]["CallerEmail"]);
                CustServiceDictList.CallerMorInfo = Convert.ToInt32(custobj.Tables[0].Rows[i]["CallerMorInfo"]);
                CustServiceDictList.EmployeeMemo = Convert.ToBoolean(custobj.Tables[0].Rows[i]["EmployeeMemo"]);
                CustServiceDictList.ProjectMajorTaskId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ProjectMajorTaskId"]);
                CustServiceDictList.Title = Convert.ToString(custobj.Tables[0].Rows[i]["Title"]);
                CustServiceDictList.StartDate =Convert.ToDateTime( Convert.ToString(custobj.Tables[0].Rows[i]["StartDate"])).ToString("dd-MM-yyyy");
                CustServiceDictList.EndDate =Convert.ToDateTime( Convert.ToString(custobj.Tables[0].Rows[i]["EndDate"])).ToString("dd-MM-yyyy");
                CustServiceDictList.Amount = Convert.ToDouble(custobj.Tables[0].Rows[i]["Amount"]);
                CustServiceDictList.CurrencyId = Convert.ToString(custobj.Tables[0].Rows[i]["CurrencyId"]);
                CustServiceDictList.RemindCreator = Convert.ToBoolean(custobj.Tables[0].Rows[i]["RemindCreator"]);
                CustServiceDictList.TaskStatus = Convert.ToInt32(custobj.Tables[0].Rows[i]["TaskStatus"]);
                CustServiceDictList.FoundationID = Convert.ToInt32(custobj.Tables[0].Rows[i]["FoundationID"]);
                CustServiceDictList.AmountReceived = Convert.ToDouble(custobj.Tables[0].Rows[i]["AmountReceived"]);
                CustServiceDictList.ApplyDate = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["ApplyDate"])).ToString("dd-MM-yyyy") ;
                CustServiceDictList.Answerdate =Convert.ToDateTime( Convert.ToString(custobj.Tables[0].Rows[i]["Answerdate"])).ToString("dd-MM-yyyy");
                CustServiceDictList.DonotJump = Convert.ToBoolean(custobj.Tables[0].Rows[i]["DonotJump"]);
                CustServiceDictList.IsReminderSms = Convert.ToInt32(custobj.Tables[0].Rows[i]["IsReminderSms"]);

                CustServiceDictList.IsReminderEmail = Convert.ToInt32(custobj.Tables[0].Rows[i]["IsReminderEmail"]);
                CustServiceDictList.ParentServiceID = Convert.ToInt32(custobj.Tables[0].Rows[i]["ParentServiceID"]);
                CustServiceDictList.FileName2 = Convert.ToString(custobj.Tables[0].Rows[i]["FileName2"]);
                CustServiceDictList.FileName3 = Convert.ToString(custobj.Tables[0].Rows[i]["FileName3"]);
                CustServiceDictList.IsDeleted = Convert.ToBoolean(custobj.Tables[0].Rows[i]["IsDeleted"]);
                FinalDictList.Add(CustServiceDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
        public List<CustomerServiceModel> GetCustomerServicebyCustId(int CustomerId, int CheckShowFollow, int CheckShowAllOpend)//, int ParentServiceID
        {
            List<CustomerServiceModel> returnObj = new List<CustomerServiceModel>();
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {

                string Query = @" SELECT  
                convert(varchar(50),CustomerService.RowDate,103) as RowDate,
                CustomerService.EmployId,
                convert(varchar(50),CustomerService.StartTime,103) as StartTime,
                CustomerService.StartHour,
                CustomerService.StartMinute, 
                --left(convert(varchar(50),CustomerService.Details),150) as Details,
                CustomerService.Details, 
                convert(varchar(50),CustomerService.MemoDate,103) as MemoDate, 
                CustomerService.MemoMinutes,
                CustomerService.FileName,
                CustomerService.FileName2,
                CustomerService.FileName3,
                CustomerService.customerid,
                CustomerService.CustomerServiceID, 
                CustomerService.DoneiT,
                CustomerService.Employeehandle,
                CustomerService.RemindCreator,
                CustomerService.DonotJump,
                CustomerService.DoneDate,
                CustomerService.IsReminderSms,
                CustomerService.IsReminderEmail,
                CustomerService.ProjectMajorTaskId ,
                CustomerService.ServiceTypeId,";
                if (LangValue == "en")
                    Query = Query + "               CustomerServiceTypes.ServiceNameEng AS ServiceName  ";
                else
                    Query = Query + "                    CustomerServiceTypes.ServiceName  ";

                Query = Query + "   FROM         CustomerService  WITH (NOLOCK) INNER JOIN ";
                Query = Query + "                        CustomerServiceTypes ON CustomerService.ServiceTypeId = CustomerServiceTypes.ServiceTypeId ";
                Query = Query + "   Where (CustomerService.customerid = " + CustomerId + " )";//+" and (ParentServiceID=" + ParentServiceID + ")";

                if (CheckShowAllOpend == 1)
                    Query = Query + "   And (CustomerService.DoneiT = 0) ";

                if (CheckShowFollow == 0)
                    Query = Query + "   And (CustomerService.FileName <> '' or CustomerService.ServiceTypeId <> 2 )";
                //CustomerServiceTypes.FollowUp_Operation
                Query = Query + "  AND IsDeleted=0  ORDER BY CustomerService.RowDate DESC";

                DataSet custobj = db.GetDataSet(Query, null, false);
                if (custobj.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
                    {
                        CustomerServiceModel CustServiceDictList = new CustomerServiceModel();
                        CustServiceDictList.CustomerServiceID = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerServiceID"]);
                        if (string.IsNullOrEmpty(Convert.ToString(custobj.Tables[0].Rows[i]["RowDate"])) == false)
                            CustServiceDictList.RowDate = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["RowDate"])).ToString("dd-MM-yyyy");
                        CustServiceDictList.EmployId = Convert.ToInt32(custobj.Tables[0].Rows[i]["EmployId"]);
                        if (string.IsNullOrEmpty(Convert.ToString(custobj.Tables[0].Rows[i]["StartTime"])) == false)
                            CustServiceDictList.StartTime = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["StartTime"])).ToString("dd-MM-yyyy");
                        CustServiceDictList.StartHour = Convert.ToString(custobj.Tables[0].Rows[i]["StartHour"]);
                        CustServiceDictList.StartMinute = Convert.ToString(custobj.Tables[0].Rows[i]["StartMinute"]);
                        CustServiceDictList.Details = Convert.ToString(custobj.Tables[0].Rows[i]["Details"]);
                        if (string.IsNullOrEmpty(Convert.ToString(custobj.Tables[0].Rows[i]["MemoDate"])) == false)
                            CustServiceDictList.MemoDate = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["MemoDate"])).ToString("dd-MM-yyyy");
                        //CustServiceDictList.MemoHour = Convert.ToString(custobj.Tables[0].Rows[i]["MemoHour"]);
                        CustServiceDictList.MemoMinutes = Convert.ToString(custobj.Tables[0].Rows[i]["MemoMinutes"]);
                        CustServiceDictList.FileName = Convert.ToString(custobj.Tables[0].Rows[i]["FileName"]);
                        CustServiceDictList.FileName2 = Convert.ToString(custobj.Tables[0].Rows[i]["FileName2"]);
                        CustServiceDictList.FileName3 = Convert.ToString(custobj.Tables[0].Rows[i]["FileName3"]);
                        CustServiceDictList.customerid = Convert.ToInt32(custobj.Tables[0].Rows[i]["customerid"]);
                        CustServiceDictList.DoneiT = Convert.ToBoolean(custobj.Tables[0].Rows[i]["DoneiT"]);
                        CustServiceDictList.Employeehandle = Convert.ToInt32(custobj.Tables[0].Rows[i]["Employeehandle"]);
                        CustServiceDictList.RemindCreator = Convert.ToBoolean(custobj.Tables[0].Rows[i]["RemindCreator"]);
                        CustServiceDictList.DonotJump = Convert.ToBoolean(custobj.Tables[0].Rows[i]["DonotJump"]);
                        if(string.IsNullOrEmpty(Convert.ToString(custobj.Tables[0].Rows[i]["DoneDate"]))==false)
                        CustServiceDictList.DoneDate = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["DoneDate"])).ToString("dd-MM-yyyy");
                        CustServiceDictList.IsReminderSms = Convert.ToInt32(custobj.Tables[0].Rows[i]["IsReminderSms"]);
                        CustServiceDictList.IsReminderEmail = Convert.ToInt32(custobj.Tables[0].Rows[i]["IsReminderEmail"]);
                        CustServiceDictList.ProjectMajorTaskId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ProjectMajorTaskId"]);
                        CustServiceDictList.ServiceTypeId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ServiceTypeId"]);
                        CustServiceDictList.ServiceType = Convert.ToString(custobj.Tables[0].Rows[i]["ServiceName"]);
                        returnObj.Add(CustServiceDictList);
                    }

                    //returnObj = GetCustomerServiceListFromDS(ds);
                }
            }
            return returnObj;
        }
    }
}
