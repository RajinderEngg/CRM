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
    public class GeneralGroupsHelper
    {
        public string SecurityConString { get; set; }
        public string LangValue { get; set; }
        public List<CustomersModel> GetCustomersListOfGrps(string GroupIds)
        {
            List<CustomersModel> CustList = new List<CustomersModel>();
            try
            {
                using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select Customers.CustomerId as CustomerId,FileAs from Customers left outer join " +
                     " CustomerGroupsGeneralSet on CustomerGroupsGeneralSet.CustomerId=Customers.CustomerId " +
                     " where CustomerGeneralGroupId in(" + GroupIds + ") and  (Customers.Deceased = 0) AND (Customers.Deleted = 0) AND (Customers.ActiveStatus = 0)"+
                     " group by Customers.CustomerId,Customers.FileAs " +
                     " order by Customers.CustomerId";



                //    var Sql_SelectCustomersForSpecifiedGroups = @"SELECT Sms.CustomerId, Sms.FileAs, Sms.CelPhone FROM (SELECT C.CustomerId, C.FileAs, 
                //REPLACE(REPLACE(REPLACE(CP.Area+CP.Phone,'-',''),' ',''),'.','') AS CelPhone FROM Customers C INNER JOIN CustomerPhones CP ON c.CustomerId = CP.CustomerId 
                //WHERE (C.Deceased = 0) AND (C.Deleted = 0) AND (C.ActiveStatus = 0) AND C.CustomerId IN (SELECT DISTINCT Customerid FROM
                //CustomerGroupsGeneralSet WHERE CustomerGeneralGroupId IN ("+GroupIds+")) @BranchData) AS Sms WHERE LEN(Sms.CelPhone) = 10;";

                //    //Checking for SysData privilges and branch
                //    if (Convert.ToBoolean(currentUser["IsBranchEnabled"]) == true)////currentUser["IsBranchEnabled"]  payload.IsBranchEnabled
                //        Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@BranchData", Convert.ToBoolean(currentUser["sysdata"]) ? "" :
                //            "AND C.CustomerId IN (SELECT CustomerId FROM CustomersBranches WHERE Branchid = @BranchId)".Replace("@BranchId", currentUser["Branchid"].ToString()));///currentUser["Branchid"]  payload.Branchid
                //    else
                //        Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@BranchData", "");

                //    //Checking for PhoneType
                //    if (Convert.ToInt32(payload.phoneType) == 0) Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@PhoneTypeId", " IN  (SELECT id FROM PhoneTypes WHERE CellPhone = 1)");
                //    else Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@PhoneTypeId", " = @PhoneTypeId");


                //    DataTable dtSmsCredentials = null, dtSelectedCustomers = null;

                //    using (var da = getDbAccess())
                //    {
                //        dtSmsCredentials = da.GetDataTable(Sql_ValidateSmsCredentialsForSendingSms,
                //            new { Username = payload.username, Company = payload.company }.ToJson().ToTypeof<Dictionary<string, object>>());
                //        if (dtSmsCredentials != null && dtSmsCredentials.Rows.Count > 0)
                //        {
                //            string GroupIdArr = payload.groups.ToString();
                //            GroupIdArr = GroupIdArr.Replace("\r\n", "").Replace("[", "").Replace("]", ""); //.Replace("\r\n","").Substring(1, GroupIdArr.Length - 2);
                //            Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@GroupIdArr", GroupIdArr);

                //            dtSelectedCustomers = da.GetDataTable(Sql_SelectCustomersForSpecifiedGroups,
                //                new { PhoneTypeId = payload.phoneType }.ToJson().ToTypeof<Dictionary<string, object>>());



                            DataSet ds = db.GetDataSet(Query, null, false);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomersModel CustObj = new CustomersModel();
                        CustObj.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]);
                        CustObj.FileAs = Convert.ToString(ds.Tables[0].Rows[i]["FileAs"]);
                        CustList.Add(CustObj);
                    }
                }
            }
            catch(Exception ex)
            {
                CustList = null;
            }
            return CustList.Distinct().ToList();
        }
    }
}
