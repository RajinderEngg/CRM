using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System.Data;

namespace AmaxService.HelperClasses
{
    public class CustomerEmailHelper
    {
        public string SecurityconString { get; set; }
        public Dictionary<string, object> GetCustomerEmailsDict(CustomerEmailsModel custEmailsobj)
        {
            Dictionary<string, object> CustAddressDictList = new Dictionary<string, object>();
            //try
            //{
                if (custEmailsobj != null)
                {
                    CustAddressDictList.Add("CustomerId", custEmailsobj.CustomerId);
                    CustAddressDictList.Add("Email", custEmailsobj.Email);
                    CustAddressDictList.Add("EmailName", custEmailsobj.EmailName);
                    CustAddressDictList.Add("Newslettere", custEmailsobj.Newslettere);
                    CustAddressDictList.Add("General", custEmailsobj.General);
                    CustAddressDictList.Add("MaxYearDelivery", custEmailsobj.MaxYearDelivery);
                    CustAddressDictList.Add("MaxMonthlyDelivery", custEmailsobj.MaxMonthlyDelivery);
                    CustAddressDictList.Add("LastEmail", custEmailsobj.LastEmail);
                    CustAddressDictList.Add("tempid", custEmailsobj.tempid);
                    CustAddressDictList.Add("Priority", custEmailsobj.Priority);
                    CustAddressDictList.Add("EmailSex", custEmailsobj.EmailSex);
                    CustAddressDictList.Add("publish", custEmailsobj.publish);
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return CustAddressDictList;
        }
        public List<CustomerEmailsModel> GetCustomerEmailListFromDS(DataSet custobj)
        {
            List<CustomerEmailsModel> FinalDictList = new List<CustomerEmailsModel>();

            //try
            //{
                for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
                {
                    CustomerEmailsModel CustAddressDictList = new CustomerEmailsModel();
                    CustAddressDictList.CustomerId= Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerId"]);
                    CustAddressDictList.Email= Convert.ToString(custobj.Tables[0].Rows[i]["Email"]);
                    CustAddressDictList.EmailName= Convert.ToString(custobj.Tables[0].Rows[i]["EmailName"]);
                    CustAddressDictList.Newslettere= Convert.ToBoolean(custobj.Tables[0].Rows[i]["Newslettere"]);
                    CustAddressDictList.General= Convert.ToBoolean(custobj.Tables[0].Rows[i]["General"]);
                    CustAddressDictList.MaxYearDelivery= Convert.ToInt32(custobj.Tables[0].Rows[i]["MaxYearDelivery"]);
                    CustAddressDictList.MaxMonthlyDelivery=Convert.ToInt32(custobj.Tables[0].Rows[i]["MaxMonthlyDelivery"]);
                    CustAddressDictList.LastEmail= Convert.ToString(custobj.Tables[0].Rows[i]["LastEmail"]);
                    CustAddressDictList.tempid= Convert.ToInt32(custobj.Tables[0].Rows[i]["tempid"]);
                    CustAddressDictList.Priority= Convert.ToInt32(custobj.Tables[0].Rows[i]["Priority"]);
                    CustAddressDictList.EmailSex= Convert.ToBoolean(custobj.Tables[0].Rows[i]["EmailSex"]);
                    CustAddressDictList.publish= Convert.ToInt32(custobj.Tables[0].Rows[i]["publish"]);
                    FinalDictList.Add(CustAddressDictList);
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
        public List<CustomerEmailsModel> GetCustomerEmailByCustId(int CustomerId)
        {
            List<CustomerEmailsModel> returnObj = new List<CustomerEmailsModel>();
            //try
           // {
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from CustomerEmails where CustomerId=" + CustomerId;
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                        returnObj = GetCustomerEmailListFromDS(ds);
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public CustomerEmailsModel GetCustomerEmailBytempid(int tempid)
        {
            CustomerEmailsModel returnObj = new CustomerEmailsModel();
            //try
            //{
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from CustomerEmails where tempid=" + tempid;
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                        returnObj = GetCustomerEmailListFromDS(ds).FirstOrDefault();
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
    }
}
