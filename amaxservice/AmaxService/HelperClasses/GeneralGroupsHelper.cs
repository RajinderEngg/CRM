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
                    //string Query = "select Customers.CustomerId as CustomerId,FileAs from Customers left outer join " +
                    // " CustomerGroupsGeneralSet on CustomerGroupsGeneralSet.CustomerId=Customers.CustomerId " +
                    // " where CustomerGeneralGroupId in(" + GroupIds + ") and  (Customers.Deceased = 0) AND (Customers.Deleted = 0) AND (Customers.ActiveStatus = 0)"+
                    // " group by Customers.CustomerId,Customers.FileAs " +
                    // " order by Customers.CustomerId";
                    bool IsGroupFilter = true;
                    string[] Grps = GroupIds.Split(',');
                    if (Grps.Length <= 1)
                    {
                        for (int i = 0; i < Grps.Length; i++)
                        {
                            if (Grps[i] == "0")
                            {
                                IsGroupFilter = false;
                                break;
                            }
                        }
                    }
                    string Query = "Select distinct   Deceased,ActiveStatus,RowDate,HALIA,MemberID,birthdate,birthdate2,hebdate1,hebdate2,CardType," +
" RGBCOLOR,Gender, fname,Lname,title,salutation,foundationdates,CustomerId,FileAs,CountryCode,Addr,AddressId,AddToName," +
" AddressTypeName,AddressTypeNameEng,StateID,cityName,Zip,ForDelivery,TypeNameHeb,TypeNameEng ,memberend,memberStart," +
" EmployeeId,SpouseName,CustomerCode,CustomerCode2,Company,MiddleName,Suffix,TMPINFO,TMPINFO2, TMPINFO3,TMPINFO4, job, " +
" jobPartner, TitleSpouse,n1,n2,n3,n4,n5,n6,n7,n8,n9,n10,n11,n12,n13,n14,n15,n16,n17,n18,n19,n20,Street2," +
" ClientCameFromName From (SELECT Deceased, ActiveStatus, RowDate, HALIA, Customers.MemberID, Customers.hebdate1," +
" Customers.hebdate2, Customers.birthdate, Customers.birthdate2, Customers.CardType, CustomerType.RGBCOLOR, Customers.Gender," +
 " salutation, Customers.title,(SELECT     COUNT(CustomerId) AS countfund  FROM  FoundationDates" +
 " WHERE(CustomerId = Customers.CustomerId)) AS foundationdates, Customers.memberend,Customers.memberStart," +
 " Customers.Deleted,Customers.IsNewsLetter, Customers.fname, Customers.lname, Customers.FileAs,Customers.SpouseName," +
" Customers.CustomerCode,Customers.CustomerCode2,Customers.Company, nd_MainCustomerAddress.Street + ' ' + nd_MainCustomerAddress.Street2 AS Addr," +
 " nd_MainCustomerAddress.CountryCode, CustomerEmails.EmailName, CustomerEmails.Email, CustomerEmails.LastEmail," +
 " CustomerEmails.MaxYearDelivery, CustomerEmails.MaxMonthlyDelivery, CustomerEmails.General, CustomerEmails.Newslettere, nd_MainCustomerAddress.AddressId, nd_MainCustomerAddress.CityName, " +
 " nd_MainCustomerAddress.Zip, nd_MainCustomerAddress.ForDelivery, nd_MainCustomerAddress.LastDelivery, nd_MainCustomerAddress.AddToName, nd_MainCustomerAddress.AddressTypeId, CustomerAddressType.AddressTypeName, CustomerAddressType.AddressTypeNameEng, Customers.CustomerId, nd_MainCustomerAddress.StateId, nd_MainCustomerAddress.MainAddress, CustomerEmails.Priority, CustomerType.TypeNameHeb, CustomerType.TypeNameEng, CustomerGroupsGeneral.GroupName, CustomerGroupsGeneral.GroupNameEng, CustomerGroupsGeneral.GroupId, Customers.BankCode, Customers.SnifNo, Customers.AccountType, Customers.AccountNo, CustomerGroupsGeneralSet.MountToCharge, Customers.ID, Customers.lname + ' ' + Customers.fname AS FullName, nd_MainCustomerAddress.Street, nd_MainCustomerAddress.Street2, nd_MainCustomerAddress.Remark,Customers.EmployeeId,  Customers.MiddleName,Customers.Suffix,Customers.TMPINFO, Customers.TMPINFO2, Customers.TMPINFO3, Customers.TMPINFO4," +
 " Customers.custposition as job, Customers.jobtitlePartner as jobPartner, Customers.TitleSpouse,Customers.n1," +
" Customers.n2,Customers.n3,Customers.n4,Customers.n5,Customers.n6,Customers.n7,Customers.n8,Customers.n9,Customers.n10," +
" Customers.n11,Customers.n12,Customers.n13,Customers.n14,Customers.n15,Customers.n16,Customers.n17," +
" Customers.n18,Customers.n19,Customers.n20, '' as JoinDate,ClientCameFrom.ClientCameFromName FROM " +
" ClientCameFrom INNER JOIN Customers ON ClientCameFrom.id = Customers.CamefromSource LEFT OUTER JOIN CustomerGroupsGeneral " +
" RIGHT OUTER JOIN CustomerGroupsGeneralSet ON CustomerGroupsGeneral.GroupId = CustomerGroupsGeneralSet.CustomerGeneralGroupId ON " +
" Customers.CustomerId = CustomerGroupsGeneralSet.Customerid LEFT OUTER JOIN CustomerType ON Customers.CustomerType = CustomerType.TypeId LEFT OUTER JOIN " +
" States RIGHT OUTER JOIN nd_MainCustomerAddress ON States.StateName = nd_MainCustomerAddress.StateId " +
" LEFT OUTER JOIN CustomerAddressType ON nd_MainCustomerAddress.AddressTypeId = CustomerAddressType.AddressTypeId ON Customers.CustomerId = nd_MainCustomerAddress.CustomerId " +
" LEFT OUTER JOIN CustomerEmails ON Customers.CustomerId = CustomerEmails.CustomerId Where Customers.ActiveStatus = 0  And Customers.IsNewsLetter = 0  And Deleted = 0  )DerivedTBL Where   Deleted = 0 And(MainAddress = 1 or MainAddress is null)  ";
                    if(IsGroupFilter==true)
                        Query+=" AND((GroupId in ("+GroupIds+")) )";


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
