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
    public class CustomerWebsiteHelper
    {
        public string SecurityconString { get; set; }
        public string LangValue { get; set; }
        public Dictionary<string, object> GetCustomerWebsiteDict(CustomerWebsitesModel custWebsiteobj)
        {
            Dictionary<string, object> CustWebsiteDictList = new Dictionary<string, object>();
            //try
            //{
            if (custWebsiteobj != null)
            {
                CustWebsiteDictList.Add("CustomerId", custWebsiteobj.CustomerId);
                CustWebsiteDictList.Add("WebSite", custWebsiteobj.WebSite);
                CustWebsiteDictList.Add("MoreInfo", custWebsiteobj.MoreInfo);
            }

            // }
            // catch (Exception ex)
            //{
            //}
            return CustWebsiteDictList;
        }
        public List<CustomerWebsitesModel> GetCustomerWebsiteListFromDS(DataSet custobj)
        {
            List<CustomerWebsitesModel> FinalDictList = new List<CustomerWebsitesModel>();

            //try
            //{
            for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
            {
                CustomerWebsitesModel CustWebsiteDictList = new CustomerWebsitesModel();
                CustWebsiteDictList.WebSite = Convert.ToString(custobj.Tables[0].Rows[i]["WebSite"]);
                CustWebsiteDictList.CustomerId = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerId"]);
                CustWebsiteDictList.MoreInfo = Convert.ToString(custobj.Tables[0].Rows[i]["MoreInfo"]);
                FinalDictList.Add(CustWebsiteDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
        public List<CustomerWebsitesModel> GetCustomerWebsitebyCustId(int CustomerId)
        {
            List<CustomerWebsitesModel> returnObj = new List<CustomerWebsitesModel>();
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select * from CustomerWebSites where CustomerId=" + CustomerId;
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnObj = GetCustomerWebsiteListFromDS(ds);
                }
            }
                return returnObj;
        }
    }
}
