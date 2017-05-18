using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
using System.Data;
using AmaxExtentions.DbAccess;

namespace AmaxService.HelperClasses
{
    public class CustomerPhonesHelper
    {
        public string SecurityconString { get; set; }
        public string LangValue { get; set; }
        public Dictionary<string, object> GetCustomerPhonesDict(CustomerPhonesModel custPhonesobj)
        {
            Dictionary<string, object> CustAddressDictList = new Dictionary<string, object>();
            if (custPhonesobj != null)
            {
                CustAddressDictList.Add("CustomerId", custPhonesobj.CustomerId);
                CustAddressDictList.Add("Id", custPhonesobj.Id);
                CustAddressDictList.Add("PhoneTypeId", custPhonesobj.PhoneTypeId);
                CustAddressDictList.Add("Prefix", custPhonesobj.Prefix);
                CustAddressDictList.Add("Area", custPhonesobj.Area);
                CustAddressDictList.Add("Phone", custPhonesobj.Phone);
                CustAddressDictList.Add("Comments", custPhonesobj.Comments);
                CustAddressDictList.Add("IsSms", custPhonesobj.IsSms);
                CustAddressDictList.Add("publish", custPhonesobj.phpublish);
            }
            return CustAddressDictList;
        }
        public List<CustomerPhonesModel> GetCustomerPhoneListFromDS(DataSet custobj)
        {
            List<CustomerPhonesModel> FinalDictList = new List<CustomerPhonesModel>();

           // try
           // {
                for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
                {
                    CustomerPhonesModel CustAddressDictList = new CustomerPhonesModel();
                    CustAddressDictList.CustomerId = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerId"]);
                    CustAddressDictList.Id= Convert.ToInt32(custobj.Tables[0].Rows[i]["Id"]);
                    CustAddressDictList.PhoneTypeId= Convert.ToInt32(custobj.Tables[0].Rows[i]["PhoneTypeId"]);
                    CustAddressDictList.Prefix= Convert.ToString(custobj.Tables[0].Rows[i]["Prefix"]);
                    CustAddressDictList.Area=Convert.ToString(custobj.Tables[0].Rows[i]["Area"]);
                    CustAddressDictList.Phone= Convert.ToString(custobj.Tables[0].Rows[i]["Phone"]);
                    CustAddressDictList.Comments=Convert.ToString(custobj.Tables[0].Rows[i]["Comments"]);
                    CustAddressDictList.IsSms=Convert.ToInt32(custobj.Tables[0].Rows[i]["IsSms"]);
                    CustAddressDictList.phpublish = Convert.ToInt32(custobj.Tables[0].Rows[i]["publish"]);
                    FinalDictList.Add(CustAddressDictList);
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
        public List<CustomerPhonesModel> GetCustomerPhoneByCustId(int CustomerId)
        {
            List<CustomerPhonesModel> returnObj = new List<CustomerPhonesModel>();
            // try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select * from CustomerPhones where CustomerId=" + CustomerId;
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnObj = GetCustomerPhoneListFromDS(ds);
                    DataBind DbClass = new DataBind();
                    DbClass.SecurityConString = SecurityconString;
                    DbClass.LangValue = LangValue;
                    List<KeyPair> Phonetypes = DbClass.GetPhoneTypes();
                    foreach (var robj in returnObj)
                    {
                        string phtid = robj.PhoneTypeId.ToString();
                        robj.PhoneType = Phonetypes.Where(r => r.Value == phtid).Select(sel => sel.Text).FirstOrDefault();
                    }
                }
            }
            //}
            //catch (Exception ex)
           // {
            //}
            return returnObj;
        }
        public CustomerPhonesModel GetCustomerPhoneById(int Id)
        {
            CustomerPhonesModel returnObj = new CustomerPhonesModel();
            //try
            //{
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from CustomerPhones where Id=" + Id;
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                        returnObj = GetCustomerPhoneListFromDS(ds).FirstOrDefault();
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }

    }
}
