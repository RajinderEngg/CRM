using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
namespace AmaxService.HelperClasses
{
    public class DataBind
    {
        public List<KeyPair> GetCustType()
        {

            List<KeyPair> CustTypeList = new List<KeyPair>();
            string Query = "select * from CustomerType";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CustTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["TypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["TypeNameEng"])));
                }
                
            }
            return CustTypeList;
        }
        public List<KeyPair> GetEmployees()
        {
            List<KeyPair> EmployeeList = new List<KeyPair>();
            string Query = "select * from Employees";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["employeeid"]), Convert.ToString(ds.Tables[0].Rows[i]["fname"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["lname"])));
                }
            }
            return EmployeeList;
        }
        public List<KeyPair> GetCustSources()
        {
            List<KeyPair> ClientCameFromList = new List<KeyPair>();
            string Query = "select * from ClientCameFrom";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ClientCameFromList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["id"]), Convert.ToString(ds.Tables[0].Rows[i]["ClientCameFromName"])));
                }
            }
            return ClientCameFromList;
        }
        public List<KeyPair> GetSuffixes()
        {
            object li = null;
            List<KeyPair> SafixList = new List<KeyPair>();
            string Query = "select * from CustomerSafix";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SafixList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["SafixId"]), Convert.ToString(ds.Tables[0].Rows[i]["SafixEng"])));
                }
            }
            return SafixList;
        }
        public List<KeyPair> GetPhoneTypes()
        {
            List<KeyPair> PhtypeList = new List<KeyPair>();
            string Query = "select * from PhoneTypes";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //PhTypeObj.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                    //PhTypeObj.contenteng = Convert.ToString(ds.Tables[0].Rows[i]["contenteng"]);
                    //PhTypeObj.contentHeb = Convert.ToString(ds.Tables[0].Rows[i]["contentHeb"]);
                    //PhTypeObj.CellPhone = Convert.ToInt32(ds.Tables[0].Rows[i]["CellPhone"]);
                    PhtypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["id"]), Convert.ToString(ds.Tables[0].Rows[i]["contenteng"])));
                }
            }
            return PhtypeList;
        }
        public List<KeyPair> GetAddressType()
        {
            List<KeyPair> AddressTypeList = new List<KeyPair>();
            string Query = "select * from CustomerAddressType";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AddressTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["AddressTypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["AddressTypeNameEng"])));
                }
            }
            return AddressTypeList;
        }
        public List<KeyPair> GetGroups()
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            string Query = "select * from CustomerGroupsGeneral";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["GroupId"]), Convert.ToString(ds.Tables[0].Rows[i]["GroupNameEng"])));
                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetStates(string countryName=null)
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            
            string Query = "select * from States where 1=1";
            if (string.IsNullOrEmpty(countryName) == false)
                Query += " and countryName='"+countryName+"'";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["StateShort"]), Convert.ToString(ds.Tables[0].Rows[i]["StateName"])));
                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetCities(string CountryCode=null,string StateName=null)
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            string Query = "select * from Cities where 1=1 ";
            if (string.IsNullOrEmpty(CountryCode) == false)
                Query += " and countrycode='"+CountryCode+"'";
            if (string.IsNullOrEmpty(StateName) == false)
                Query += " and StateName='"+StateName+"'";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CityName"]), Convert.ToString(ds.Tables[0].Rows[i]["CityName"])));
                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetCountries()
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            string Query = "select * from Countries";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CountryShortCode"]), Convert.ToString(ds.Tables[0].Rows[i]["CountryName"])));
                }
            }
            return GroupsList;
        }
    }
}
