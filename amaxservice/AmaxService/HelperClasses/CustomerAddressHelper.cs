using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System.Configuration;
using System.Data;
namespace AmaxService.HelperClasses
{
    public class CustomerAddressHelper
    {
        public string SecurityconString { get; set; }
        public string LangValue { get; set; }
        public Dictionary<string, object> GetCustomerAddressDict(CustomerAddressModel custAddressobj)
        {
            Dictionary<string, object> CustAddressDictList = new Dictionary<string, object>();
            //try
            //{
                if (custAddressobj != null)
                {
                    CustAddressDictList.Add("AddressId", custAddressobj.AddressId);
                    CustAddressDictList.Add("CustomerId", custAddressobj.CustomerId);
                    CustAddressDictList.Add("StateId", custAddressobj.StateId);
                    CustAddressDictList.Add("CityName", custAddressobj.CityName);
                    CustAddressDictList.Add("CountryCode", custAddressobj.CountryCode);
                    CustAddressDictList.Add("Street", custAddressobj.Street);
                    CustAddressDictList.Add("Street2", custAddressobj.Street2);
                    CustAddressDictList.Add("Zip", custAddressobj.Zip);
                    CustAddressDictList.Add("remark", custAddressobj.remark);
                    CustAddressDictList.Add("ForDelivery", custAddressobj.ForDelivery);
                    CustAddressDictList.Add("MaxYearDelivery", custAddressobj.MaxYearDelivery);
                    CustAddressDictList.Add("MaxMonthlyDelivery", custAddressobj.MaxMonthlyDelivery);
                    CustAddressDictList.Add("LastDelivery", custAddressobj.LastDelivery);
                    CustAddressDictList.Add("AddToName", custAddressobj.AddToName);
                    CustAddressDictList.Add("AddressTypeId", custAddressobj.AddressTypeId);
                    CustAddressDictList.Add("MainAddress", custAddressobj.MainAddress);

                    CustAddressDictList.Add("SrteetOnly", custAddressobj.SrteetOnly);
                    CustAddressDictList.Add("StreetNo", custAddressobj.StreetNo);
                    CustAddressDictList.Add("Entrance", custAddressobj.Entrance);
                    CustAddressDictList.Add("DeliveryCode", custAddressobj.DeliveryCode);
                }

           // }
           // catch (Exception ex)
            //{
            //}
            return CustAddressDictList;
        }
        public List<CustomerAddressModel> GetCustomerAddressListFromDS(DataSet custobj)
        {
            List<CustomerAddressModel> FinalDictList = new List<CustomerAddressModel>();

            //try
            //{
                for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
                {
                    CustomerAddressModel CustAddressDictList = new CustomerAddressModel();
                    CustAddressDictList.AddressId = Convert.ToInt32(custobj.Tables[0].Rows[i]["AddressId"]);
                    CustAddressDictList.CustomerId = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerId"]);
                    CustAddressDictList.StateId = Convert.ToString(custobj.Tables[0].Rows[i]["StateId"]);
                    CustAddressDictList.CityName = Convert.ToString(custobj.Tables[0].Rows[i]["CityName"]);
                    CustAddressDictList.CountryCode = Convert.ToString(custobj.Tables[0].Rows[i]["CountryCode"]);
                    CustAddressDictList.Street = Convert.ToString(custobj.Tables[0].Rows[i]["Street"]);
                    CustAddressDictList.Street2 = Convert.ToString(custobj.Tables[0].Rows[i]["Street2"]);
                    CustAddressDictList.Zip = Convert.ToString(custobj.Tables[0].Rows[i]["Zip"]);
                    CustAddressDictList.remark = Convert.ToString(custobj.Tables[0].Rows[i]["remark"]);
                    CustAddressDictList.ForDelivery = Convert.ToBoolean(custobj.Tables[0].Rows[i]["ForDelivery"]);
                    CustAddressDictList.MaxYearDelivery = Convert.ToInt32(custobj.Tables[0].Rows[i]["MaxYearDelivery"]);
                    CustAddressDictList.MaxMonthlyDelivery = Convert.ToInt32(custobj.Tables[0].Rows[i]["MaxMonthlyDelivery"]);
                    CustAddressDictList.LastDelivery = Convert.ToString(custobj.Tables[0].Rows[i]["LastDelivery"]);
                    CustAddressDictList.AddToName = Convert.ToString(custobj.Tables[0].Rows[i]["AddToName"]);
                    CustAddressDictList.AddressTypeId = Convert.ToInt32(custobj.Tables[0].Rows[i]["AddressTypeId"]);
                    CustAddressDictList.MainAddress = Convert.ToBoolean(custobj.Tables[0].Rows[i]["MainAddress"]);

                    CustAddressDictList.SrteetOnly = Convert.ToString(custobj.Tables[0].Rows[i]["SrteetOnly"]);
                    CustAddressDictList.StreetNo = Convert.ToString(custobj.Tables[0].Rows[i]["StreetNo"]);
                    CustAddressDictList.Entrance = Convert.ToString(custobj.Tables[0].Rows[i]["Entrance"]);
                    CustAddressDictList.DeliveryCode = Convert.ToString(custobj.Tables[0].Rows[i]["DeliveryCode"]);
                    FinalDictList.Add(CustAddressDictList);
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
        public List<CustomerAddressModel> GetCustomerAddressByCustId(int CustomerId)
        {
            List<CustomerAddressModel> returnObj = new List<CustomerAddressModel>();
            //try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select * from CustomerAddress where CustomerId=" + CustomerId;
                DataSet ds = db.GetDataSet(Query, null, false);
                DataBind DbClass = new DataBind();
                DbClass.SecurityConString = SecurityconString;
                DbClass.LangValue = LangValue;
                List<KeyPair> CountryList = DbClass.GetCountries();
                List<KeyPair> StateList = DbClass.GetStates();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnObj = GetCustomerAddressListFromDS(ds);
                    foreach (var retobj in returnObj)
                    {
                        if (CountryList.Count > 0)
                        {
                            retobj.CountryName = CountryList.Where(r => r.Value == retobj.CountryCode).Select(sel => sel.Text).FirstOrDefault();
                        }
                        if (StateList.Count > 0)
                        {
                            retobj.State = StateList.Where(r => r.Value == retobj.StateId).Select(sel => sel.Text).FirstOrDefault();
                        }
                    }
                }
            }
          //  }
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public CustomerAddressModel GetCustomerAddressByAddressId(int AddressId)
        {
            CustomerAddressModel returnObj = new CustomerAddressModel();
            //try
           // {
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from CustomerAddress where AddressId=" + AddressId;
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                        returnObj = GetCustomerAddressListFromDS(ds).FirstOrDefault();
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
    }
}
