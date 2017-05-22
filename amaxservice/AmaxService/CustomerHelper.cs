using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System.Configuration;
using System.Data;
using AmaxService.HelperClasses;
using System.Web;
using System.IO;

namespace AmaxService.HelperClasses
{
    public class CustomerHelper
    {
        public string SecurityconString { get; set; }
        public string lang { get; set; }
        public string OrgId { get; set; }
        public CustomerAddressHelper CustAddHP;
        public CustomerEmailHelper CustEmailHP;
        public CustomerPhonesHelper CustPhoneHP;
        public CustomerGroupsHelper CustGrpsHP;
        public CustomerWebsiteHelper CustWebsiteHP;
        public CustomerServiceHelper CustServiceHP;
        public ReceiptCreateHelper RcptCreateHP;
        public CustomerHelper()
        {
            CustAddHP = new CustomerAddressHelper();
            CustEmailHP = new CustomerEmailHelper();
            CustPhoneHP = new CustomerPhonesHelper();
            CustGrpsHP = new CustomerGroupsHelper();
            CustWebsiteHP = new CustomerWebsiteHelper();
            CustServiceHP = new CustomerServiceHelper();
            RcptCreateHP = new ReceiptCreateHelper();
        }

        public Dictionary<string, object> GetCustomerDict(CustomersModel custobj)
        {

            Dictionary<string, object> CustDictList = new Dictionary<string, object>();
            try
            {
                if (custobj != null)
                {
                    CustDictList.Add("CustomerId", custobj.CustomerId);
                    CustDictList.Add("employeeid", custobj.employeeid);
                    CustDictList.Add("fname", custobj.fname);
                    CustDictList.Add("lname", custobj.lname);
                    CustDictList.Add("MiddleName", custobj.MiddleName);
                    CustDictList.Add("CustomerType", custobj.CustomerType);
                    CustDictList.Add("CameFromCustomer", custobj.CameFromCustomer);
                    CustDictList.Add("CustomerCode", custobj.CustomerCode);

                    CustDictList.Add("CustomerCode2", custobj.CustomerCode2);
                    CustDictList.Add("BirthDate", custobj.BirthDate);
                    CustDictList.Add("BirthDate2", custobj.BirthDate2);
                    CustDictList.Add("jobtitlePartner", custobj.jobtitlePartner);
                    CustDictList.Add("Titel", custobj.Titel);
                    CustDictList.Add("Safixid", custobj.Safixid);
                    CustDictList.Add("Suffix", custobj.Suffix);
                    CustDictList.Add("Gender", custobj.Gender);


                    //CustDictList.Add("CardType", custobj.CardType);
                    //CustDictList.Add("RowDate", custobj.RowDate);
                    //CustDictList.Add("Potentianl", custobj.Potentianl);
                    //CustDictList.Add("RelatedCustomer", custobj.RelatedCustomer);
                    //CustDictList.Add("RelationType", custobj.RelationType);
                    //CustDictList.Add("ActiveStatus", custobj.ActiveStatus);
                    CustDictList.Add("Remark", custobj.Remark);
                    //CustDictList.Add("CustPosition", custobj.CustPosition);

                    //CustDictList.Add("SpouseName", custobj.SpouseName);
                    CustDictList.Add("Company", custobj.Company);
                    //CustDictList.Add("Deleted", custobj.Deleted);
                    //CustDictList.Add("TempId", custobj.TempId);
                    //CustDictList.Add("tmp", custobj.tmp);
                    //CustDictList.Add("assistant", custobj.assistant);
                    CustDictList.Add("FileAs", custobj.FileAs);
                    //CustDictList.Add("PermitionLevel", custobj.PermitionLevel);



                    //CustDictList.Add("Salutation", custobj.Salutation);
                    //CustDictList.Add("SearchHide", custobj.SearchHide);
                    //CustDictList.Add("SecurityLock", custobj.SecurityLock);
                    //CustDictList.Add("CardStatus", custobj.CardStatus);
                    CustDictList.Add("Title", custobj.Title);
                    //CustDictList.Add("ID", custobj.ID);
                    //CustDictList.Add("BankCode", custobj.BankCode);
                    //CustDictList.Add("SnifNo", custobj.SnifNo);

                    //CustDictList.Add("AccountType", custobj.AccountType);
                    //CustDictList.Add("AccountNo", custobj.AccountNo);
                    //CustDictList.Add("ShortComment", custobj.ShortComment);
                    //CustDictList.Add("Kids", custobj.Kids);
                    //CustDictList.Add("FamlyStat", custobj.FamlyStat);
                    //CustDictList.Add("ForeignName", custobj.ForeignName);
                    //CustDictList.Add("HALIA", custobj.HALIA);
                    //CustDictList.Add("Deceased", custobj.Deceased);


                    //CustDictList.Add("DeceasedYear", custobj.DeceasedYear);
                    //CustDictList.Add("BornPlace", custobj.BornPlace);
                    //CustDictList.Add("AfterSunset1", custobj.AfterSunset1);
                    //CustDictList.Add("AfterSunset2", custobj.AfterSunset2);
                    //CustDictList.Add("MemberID", custobj.MemberID);
                    //CustDictList.Add("SpecialCust", custobj.SpecialCust);
                    //CustDictList.Add("SpouseID", custobj.SpouseID);
                    //CustDictList.Add("MemberStart", custobj.MemberStart);

                    //CustDictList.Add("MemberEnd", custobj.MemberEnd);
                    //CustDictList.Add("KEVAStart", custobj.KEVAStart);
                    //CustDictList.Add("KEVAEnd", custobj.KEVAEnd);
                    //CustDictList.Add("KEVANAME", custobj.KEVANAME);
                    //CustDictList.Add("SmallRemark", custobj.SmallRemark);
                    //CustDictList.Add("Remark2", custobj.Remark2);
                    //CustDictList.Add("HbirthMonthVal", custobj.HbirthMonthVal);
                    //CustDictList.Add("HbirthMonthVal2", custobj.HbirthMonthVal2);


                    //CustDictList.Add("HbirthDayVal", custobj.HbirthDayVal);
                    //CustDictList.Add("HbirthDayVal2", custobj.HbirthDayVal2);
                    //CustDictList.Add("HebDate1", custobj.HebDate1);
                    //CustDictList.Add("HebDate2", custobj.HebDate2);
                    //CustDictList.Add("LastUpdate", custobj.LastUpdate);
                    //CustDictList.Add("Mothername", custobj.Mothername);
                    //CustDictList.Add("Fathername", custobj.Fathername);
                    //CustDictList.Add("UpdateEmp", custobj.UpdateEmp);

                    //CustDictList.Add("ArriveDate", custobj.ArriveDate);
                    //CustDictList.Add("IDSTR", custobj.IDSTR);
                    //CustDictList.Add("CardIsueDate", custobj.CardIsueDate);
                    //CustDictList.Add("TMPINFO", custobj.TMPINFO);
                    //CustDictList.Add("TitleSpouse", custobj.TitleSpouse);
                    //CustDictList.Add("TMPINFO2", custobj.TMPINFO2);
                    //CustDictList.Add("TMPINFO3", custobj.TMPINFO3);
                    //CustDictList.Add("TMPINFO4", custobj.TMPINFO4);


                    //CustDictList.Add("xx", custobj.xx);
                    //CustDictList.Add("xxlen", custobj.xxlen);
                    CustDictList.Add("ImageFileName", custobj.ImageFileName);
                    //CustDictList.Add("IsNewsLetter", custobj.IsNewsLetter);
                    //CustDictList.Add("n1", custobj.n1);
                    //CustDictList.Add("n2", custobj.n2);
                    //CustDictList.Add("n3", custobj.n3);
                    //CustDictList.Add("n4", custobj.n4);

                    //CustDictList.Add("n5", custobj.n5);
                    //CustDictList.Add("n6", custobj.n6);
                    //CustDictList.Add("n7", custobj.n7);
                    //CustDictList.Add("n8", custobj.n8);
                    //CustDictList.Add("n9", custobj.n9);
                    //CustDictList.Add("n10", custobj.n10);
                    //CustDictList.Add("n11", custobj.n11);
                    //CustDictList.Add("n12", custobj.n12);

                    //CustDictList.Add("n13", custobj.n13);
                    //CustDictList.Add("n14", custobj.n14);
                    //CustDictList.Add("n15", custobj.n15);
                    //CustDictList.Add("n16", custobj.n16);
                    //CustDictList.Add("n17", custobj.n17);
                    //CustDictList.Add("n18", custobj.n18);
                    //CustDictList.Add("n19", custobj.n19);
                    //CustDictList.Add("n20", custobj.n20);

                }
            }
            catch (Exception ex)
            {
            }
            return CustDictList;
        }


        public List<CustomersModel> GetCustomerListFromDS(DataSet custobj)
        {
            List<CustomersModel> FinalDictList = new List<CustomersModel>();

            // try
            // {
            for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
            {
                CustomersModel CustDictList = new CustomersModel();
                CustDictList.CustomerId = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerId"]);
                CustDictList.employeeid = Convert.ToInt32(custobj.Tables[0].Rows[i]["employeeid"]);
                CustDictList.fname = Convert.ToString(custobj.Tables[0].Rows[i]["fname"]);
                CustDictList.lname = Convert.ToString(custobj.Tables[0].Rows[i]["lname"]);
                CustDictList.MiddleName = Convert.ToString(custobj.Tables[0].Rows[i]["MiddleName"]);
                CustDictList.CustomerType = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerType"]);
                CustDictList.CameFromCustomer = Convert.ToInt32(custobj.Tables[0].Rows[i]["CameFromCustomer"]);
                CustDictList.CustomerCode = Convert.ToString(custobj.Tables[0].Rows[i]["CustomerCode"]);

                CustDictList.CustomerCode2 = Convert.ToString(custobj.Tables[0].Rows[i]["CustomerCode2"]);
                CustDictList.BirthDate = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["BirthDate"])).ToShortDateString();
                CustDictList.BirthDate2 = Convert.ToDateTime(Convert.ToString(custobj.Tables[0].Rows[i]["BirthDate2"])).ToShortDateString();
                CustDictList.jobtitlePartner = Convert.ToString(custobj.Tables[0].Rows[i]["jobtitlePartner"]);
                CustDictList.Titel = Convert.ToInt32(custobj.Tables[0].Rows[i]["Titel"]);
                CustDictList.Safixid = Convert.ToInt32(custobj.Tables[0].Rows[i]["Safixid"]);
                CustDictList.Suffix = Convert.ToString(custobj.Tables[0].Rows[i]["Suffix"]);
                CustDictList.Gender = Convert.ToInt32(custobj.Tables[0].Rows[i]["Gender"]);


                //CustDictList.Add("CardType", custobj.CardType);
                //CustDictList.Add("RowDate", custobj.RowDate);
                //CustDictList.Add("Potentianl", custobj.Potentianl);
                //CustDictList.Add("RelatedCustomer", custobj.RelatedCustomer);
                //CustDictList.Add("RelationType", custobj.RelationType);
                //CustDictList.Add("ActiveStatus", custobj.ActiveStatus);
                CustDictList.Remark = Convert.ToString(custobj.Tables[0].Rows[i]["Remark"]);
                //CustDictList.Add("CustPosition", custobj.CustPosition);

                //CustDictList.Add("SpouseName", custobj.SpouseName);
                CustDictList.Company = Convert.ToString(custobj.Tables[0].Rows[i]["Company"]);
                //CustDictList.Add("Deleted", custobj.Deleted);
                //CustDictList.Add("TempId", custobj.TempId);
                //CustDictList.Add("tmp", custobj.tmp);
                //CustDictList.Add("assistant", custobj.assistant);
                CustDictList.FileAs = Convert.ToString(custobj.Tables[0].Rows[i]["FileAs"]);
                //CustDictList.Add("PermitionLevel", custobj.PermitionLevel);



                //CustDictList.Add("Salutation", custobj.Salutation);
                //CustDictList.Add("SearchHide", custobj.SearchHide);
                //CustDictList.Add("SecurityLock", custobj.SecurityLock);
                //CustDictList.Add("CardStatus", custobj.CardStatus);

                CustDictList.Title = Convert.ToString(custobj.Tables[0].Rows[i]["Title"]);
                //CustDictList.Add("ID", custobj.ID);
                //CustDictList.Add("BankCode", custobj.BankCode);
                //CustDictList.Add("SnifNo", custobj.SnifNo);

                //CustDictList.Add("AccountType", custobj.AccountType);
                //CustDictList.Add("AccountNo", custobj.AccountNo);
                //CustDictList.Add("ShortComment", custobj.ShortComment);
                //CustDictList.Add("Kids", custobj.Kids);
                //CustDictList.Add("FamlyStat", custobj.FamlyStat);
                //CustDictList.Add("ForeignName", custobj.ForeignName);
                //CustDictList.Add("HALIA", custobj.HALIA);
                //CustDictList.Add("Deceased", custobj.Deceased);


                //CustDictList.Add("DeceasedYear", custobj.DeceasedYear);
                //CustDictList.Add("BornPlace", custobj.BornPlace);
                //CustDictList.Add("AfterSunset1", custobj.AfterSunset1);
                //CustDictList.Add("AfterSunset2", custobj.AfterSunset2);
                //CustDictList.Add("MemberID", custobj.MemberID);
                //CustDictList.Add("SpecialCust", custobj.SpecialCust);
                //CustDictList.Add("SpouseID", custobj.SpouseID);
                //CustDictList.Add("MemberStart", custobj.MemberStart);

                //CustDictList.Add("MemberEnd", custobj.MemberEnd);
                //CustDictList.Add("KEVAStart", custobj.KEVAStart);
                //CustDictList.Add("KEVAEnd", custobj.KEVAEnd);
                //CustDictList.Add("KEVANAME", custobj.KEVANAME);
                //CustDictList.Add("SmallRemark", custobj.SmallRemark);
                //CustDictList.Add("Remark2", custobj.Remark2);
                //CustDictList.Add("HbirthMonthVal", custobj.HbirthMonthVal);
                //CustDictList.Add("HbirthMonthVal2", custobj.HbirthMonthVal2);


                //CustDictList.Add("HbirthDayVal", custobj.HbirthDayVal);
                //CustDictList.Add("HbirthDayVal2", custobj.HbirthDayVal2);
                //CustDictList.Add("HebDate1", custobj.HebDate1);
                //CustDictList.Add("HebDate2", custobj.HebDate2);
                //CustDictList.Add("LastUpdate", custobj.LastUpdate);
                //CustDictList.Add("Mothername", custobj.Mothername);
                //CustDictList.Add("Fathername", custobj.Fathername);
                //CustDictList.Add("UpdateEmp", custobj.UpdateEmp);

                //CustDictList.Add("ArriveDate", custobj.ArriveDate);
                //CustDictList.Add("IDSTR", custobj.IDSTR);
                //CustDictList.Add("CardIsueDate", custobj.CardIsueDate);
                //CustDictList.Add("TMPINFO", custobj.TMPINFO);
                //CustDictList.Add("TitleSpouse", custobj.TitleSpouse);
                //CustDictList.Add("TMPINFO2", custobj.TMPINFO2);
                //CustDictList.Add("TMPINFO3", custobj.TMPINFO3);
                //CustDictList.Add("TMPINFO4", custobj.TMPINFO4);


                //CustDictList.Add("xx", custobj.xx);
                //CustDictList.Add("xxlen", custobj.xxlen);
                CustDictList.ImageFileName = Convert.ToString(custobj.Tables[0].Rows[i]["ImageFileName"]);

                //CustDictList.Add("IsNewsLetter", custobj.IsNewsLetter);
                //CustDictList.Add("n1", custobj.n1);
                //CustDictList.Add("n2", custobj.n2);
                //CustDictList.Add("n3", custobj.n3);
                //CustDictList.Add("n4", custobj.n4);

                //CustDictList.Add("n5", custobj.n5);
                //CustDictList.Add("n6", custobj.n6);
                //CustDictList.Add("n7", custobj.n7);
                //CustDictList.Add("n8", custobj.n8);
                //CustDictList.Add("n9", custobj.n9);
                //CustDictList.Add("n10", custobj.n10);
                //CustDictList.Add("n11", custobj.n11);
                //CustDictList.Add("n12", custobj.n12);

                //CustDictList.Add("n13", custobj.n13);
                //CustDictList.Add("n14", custobj.n14);
                //CustDictList.Add("n15", custobj.n15);
                //CustDictList.Add("n16", custobj.n16);
                //CustDictList.Add("n17", custobj.n17);
                //CustDictList.Add("n18", custobj.n18);
                //CustDictList.Add("n19", custobj.n19);
                //CustDictList.Add("n20", custobj.n20);
                FinalDictList.Add(CustDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }

        public List<CustomersModel> GetCustomerListForSrch(string SrchVal)
        {
            List<CustomersModel> returnObj = new List<CustomersModel>();
            //try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                bool IsNumber = true;
                string Query = "";
                for (int i = 0; i < SrchVal.Length; i++)
                {
                    if (Char.IsNumber(SrchVal[i]) == false)
                    {
                        IsNumber = false;
                        break;
                    }
                }
                if (IsNumber == true)
                {
                    Query = "select top 30 Customers.CustomerId as CustomerId,Customers.FileAs as FileAs,Customers.customercode as customercode,Customers.customercode2 as customercode2,Phone from Customers left outer join " +
                    " CustomerPhones on Customers.CustomerId = CustomerPhones.CustomerId left outer join" +
                    " Reciepts on Reciepts.CustomerId=Customers.CustomerId left outer join " +
                    " RecieptLine on RecieptLine.RecieptNo=Reciepts.RecieptNo " +
                    " where 1=1 or (customers.CustomerId =" + SrchVal + " or Customers.customercode like '%" + SrchVal + "%' or Customers.customercode2 like '%" + SrchVal +
                    "%' or Phone like '%" + SrchVal + "%' or Reciepts.RecieptNo like '%" + SrchVal + "%' or Reciepts.Credit4Digit like '%" + SrchVal + "%' or RecieptLine.CheckNo like '%" + SrchVal + "%' )  group by Customers.CustomerId,Customers.FileAs,Customers.customercode,Customers.customercode2,Phone order by Customers.CustomerId,Customers.customercode,Customers.customercode2,Phone ";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //bool IsDup = false;
                        //foreach (var DupCustObj in returnObj)
                        //{
                        //    if (DupCustObj.CustomerId == Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]))
                        //    {
                        //        IsDup = true;
                        //        break;
                        //    }
                        //}
                        //if (IsDup == false)
                        //{
                        CustomersModel CustObj = new CustomersModel();
                        CustObj.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]);
                        //CustObj.fname = Convert.ToString(ds.Tables[0].Rows[i]["fname"]);
                        //CustObj.MiddleName = Convert.ToString(ds.Tables[0].Rows[i]["MiddleName"]);
                        //CustObj.lname = Convert.ToString(ds.Tables[0].Rows[i]["lname"]);
                        CustObj.FileAs = Convert.ToString(ds.Tables[0].Rows[i]["FileAs"]);
                        CustObj.jobtitlePartner = Convert.ToString(ds.Tables[0].Rows[i]["CustomerId"]) + " | " + //Convert.ToString(ds.Tables[0].Rows[i]["Phone"]) + " | " +
                                                                                                                 //Convert.ToString(ds.Tables[0].Rows[i]["customercode"]) + " | "+ Convert.ToString(ds.Tables[0].Rows[i]["customercode2"])+" | "+
                        Convert.ToString(ds.Tables[0].Rows[i]["FileAs"]);
                        returnObj.Add(CustObj);
                        //}
                    }
                }
                else
                {
                    //Query = "select top 30 Customers.CustomerId as CustomerId,Customers.FileAs as FileAs,Customers.fname as fname,Customers.lname as lname,Customers.middlename as middlename,Customers.customercode as customercode,Customers.customercode2 as customercode2,Customers.spousename as spousename,Customers.company as company from Customers left outer join " +
                    //" Reciepts on Reciepts.CustomerId=Customers.CustomerId left outer join " +
                    //" RecieptLine on RecieptLine.RecieptNo=Reciepts.RecieptNo " +
                    //    " where fileas like '" + SrchVal + "%' or fileas like '%" + SrchVal + "%' or SpouseName='" + SrchVal + "'" +
                    //    " or Customers.Company='" + SrchVal + "' or Customers.fname='" + SrchVal + "' or Customers.lname='" + SrchVal + "' or RecieptLine.CheckNo like '%" + SrchVal + "%' " +
                    //    " order by Customers.fileas,Customers.spousename,Customers.company,Customers.fname,Customers.lname";


                    Query = "select top 30 Customers.CustomerId as CustomerId,Customers.FileAs as FileAs,Customers.spousename as spousename,Customers.company as company,Customers.fname as fname,Customers.lname as lname from Customers left outer join " +
                       " Reciepts on Reciepts.CustomerId=Customers.CustomerId left outer join " +
                       " RecieptLine on RecieptLine.RecieptNo=Reciepts.RecieptNo " +
                           " where fileas like '" + SrchVal + "%' or fileas like '%" + SrchVal + "%' or SpouseName='" + SrchVal + "'" +
                           " or Customers.Company='" + SrchVal + "' or Customers.fname='" + SrchVal + "' or Customers.lname='" + SrchVal + "' or RecieptLine.CheckNo like '%" + SrchVal + "%' " +
                           " group by Customers.CustomerId,Customers.FileAs,Customers.spousename,Customers.company,Customers.fname,Customers.lname order by Customers.fileas,Customers.spousename,Customers.company,Customers.fname,Customers.lname";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //bool IsDup = false;
                        //foreach (var DupCustObj in returnObj)
                        //{
                        //    if (DupCustObj.CustomerId == Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]))
                        //    {
                        //        IsDup = true;
                        //        break;
                        //    }
                        //}
                        //if (IsDup == false)
                        //{
                        CustomersModel CustObj = new CustomersModel();
                        CustObj.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]);
                        //CustObj.fname = Convert.ToString(ds.Tables[0].Rows[i]["fname"]);
                        //CustObj.MiddleName = Convert.ToString(ds.Tables[0].Rows[i]["MiddleName"]);
                        //CustObj.lname = Convert.ToString(ds.Tables[0].Rows[i]["lname"]);
                        CustObj.FileAs = Convert.ToString(ds.Tables[0].Rows[i]["FileAs"]);
                        CustObj.jobtitlePartner = Convert.ToString(ds.Tables[0].Rows[i]["CustomerId"]) + " | " + //Convert.ToString(ds.Tables[0].Rows[i]["lname"]) + " | " +
                                                                                                                 //Convert.ToString(ds.Tables[0].Rows[i]["fname"]) + " | "+ Convert.ToString(ds.Tables[0].Rows[i]["spousename"]) + " | " +
                                                                                                                 //Convert.ToString(ds.Tables[0].Rows[i]["company"]) + " | " + 
                        Convert.ToString(ds.Tables[0].Rows[i]["FileAs"]);
                        returnObj.Add(CustObj);
                        //}
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public int GetMaxCustId(string ConString)
        {
            int returnObj = 0;
            //try
            //{
            using (DbAccess db = new DbAccess(ConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select max(customerid) from customers";
                string maxId = Convert.ToString(db.ExecuteScalarAsString(Query));
                //   if (maxId == "") maxId = 0;
                returnObj = Convert.ToInt32(maxId) + 1;

            }
            //}
            //catch (Exception ex)
            // {
            // }
            return returnObj;
        }
        public bool IsValidCustCode(string CustCode, int CustId, string ConString)
        {
            bool returnObj = true;
            //try
            //{
            using (DbAccess db = new DbAccess(ConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select * from customers where Customerid!=" + CustId + " and CustomerCode='" + CustCode + "'";
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                    returnObj = false;
            }
            // }
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public string GetPhoneType(int PhoneTypeId)
        {
            string returnObj = "";
            //try
            //{
            //using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
            //{
            //    string Query = "select * from PhoneTypes where id=" + PhoneTypeId;
            //    DataSet ds = db.GetDataSet(Query, null, false);
            //    if (ds.Tables[0].Rows.Count > 0)
            //        returnObj =Convert.ToString( ds.Tables[0].Rows[0]["contenteng"]);
            //}
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public string GetCountryName(string CountryCode)
        {
            string returnObj = "";
            //try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select * from Countries where CountryShortCode='" + CountryCode + "'";
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                    returnObj = Convert.ToString(ds.Tables[0].Rows[0]["CountryName"]);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }

        public List<CustomersModel> GetCustomersByFNameLNameComp(string fname, string lname, string company)
        {
            List<CustomersModel> returnObj = new List<CustomersModel>();
            //try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                if (string.IsNullOrEmpty(fname) == false && fname.Length > 2 && string.IsNullOrEmpty(lname) == false && lname.Length > 2 || (string.IsNullOrEmpty(company) == false && company.Length > 3))
                {
                    string Query = "select * from Customers where 1=1";
                    if (string.IsNullOrEmpty(fname) == false) Query += " and fname like '%" + fname + "%'";
                    if (string.IsNullOrEmpty(lname) == false) Query += " and lname like '%" + lname + "%'";
                    if (string.IsNullOrEmpty(company) == false) Query += " or Company like '%" + company + "%'";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                        returnObj = GetCustomerListFromDS(ds);
                    //else
                    //    returnObj = null;
                }
            }
            //}
            // catch (Exception ex)
            // {
            //}
            return returnObj;
        }

        public List<CustomersModel> GetCustomersSearchData(string fname, string lname, string company, string Phones, string Emails)
        {
            List<CustomersModel> returnObj = new List<CustomersModel>();
            // try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                bool IsWhere = false;
                if ((string.IsNullOrEmpty(fname) == false && fname.Length >= 2 && string.IsNullOrEmpty(lname) == false && lname.Length >= 2)
                    || (string.IsNullOrEmpty(company) == false && company.Length >= 3) || (string.IsNullOrEmpty(Emails) == false) || (string.IsNullOrEmpty(Phones) == false))
                {
                    string Query = "select customers.customerid as CustomerId,fname,lname,middlename,Phone,Email,Company,FileAs from Customers left outer join" +
                                   " CustomerPhones on Customers.CustomerId = CustomerPhones.CustomerId left outer join" +
                                    " CustomerEmails on Customers.CustomerId = CustomerEmails.CustomerId";

                    if ((string.IsNullOrEmpty(company) == false && company.Length >= 3))
                    {
                        if (IsWhere == true)
                            Query += " or Company like '%" + company + "%'";
                        else
                        {
                            Query += "  where Company like '%" + company + "%'";
                            IsWhere = true;
                        }
                    }
                    if ((string.IsNullOrEmpty(fname) == false && fname.Length >= 2 && string.IsNullOrEmpty(lname) == false && lname.Length >= 2))
                    {
                        if (IsWhere == true)
                        {
                            Query += " or ( fname like '%" + fname + "%'";
                            Query += " and lname like '%" + lname + "%')";
                        }
                        else
                        {
                            Query += " where ( fname like '%" + fname + "%'";
                            Query += " and lname like '%" + lname + "%')";
                            IsWhere = true;
                        }
                    }

                    if ((string.IsNullOrEmpty(Phones) == false))
                    {
                        if (IsWhere == true)
                            Query += " or Phone in ('" + Phones + "')";
                        else {
                            Query += " where Phone in ('" + Phones + "')";
                            IsWhere = true;
                        }
                    }
                    if ((string.IsNullOrEmpty(Emails) == false))
                    {
                        if (IsWhere == true)
                            Query += " or Email in ('" + Emails + "')";
                        else
                        {
                            Query += " where Email in ('" + Emails + "')";
                            IsWhere = true;
                        }
                    }

                    DataSet ds = db.GetDataSet(Query, null, false);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        CustomersModel CustObj = new CustomersModel();
                        CustObj.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]);
                        CustObj.fname = Convert.ToString(ds.Tables[0].Rows[i]["fname"]);
                        CustObj.MiddleName = Convert.ToString(ds.Tables[0].Rows[i]["MiddleName"]);
                        CustObj.lname = Convert.ToString(ds.Tables[0].Rows[i]["lname"]);
                        CustObj.FileAs = Convert.ToString(ds.Tables[0].Rows[i]["FileAs"]);
                        returnObj.Add(CustObj);
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }



        public List<CustomersModel> GetCustomerByCustId(int CustomerId)
        {
            List<CustomersModel> returnObj = new List<CustomersModel>();
            //try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "select * from Customers where CustomerId=" + CustomerId;
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                    returnObj = GetCustomerListFromDS(ds);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }

        public List<CustomersModel> GetCustomersByPhone(string Phone)
        {
            List<CustomersModel> returnObj = new List<CustomersModel>();
            // try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                if (string.IsNullOrEmpty(Phone) == false)
                {
                    string Query = "select * from CustomerPhones where 1=1 and Phone like'%" + Phone + "%'";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<CustomersModel> CustObj = new List<CustomersModel>();
                        CustObj = GetCustomerByCustId(Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]));
                        if (CustObj.Count > 0)
                        {
                            returnObj.Add(CustObj[0]);
                        }
                    }
                }
                //if (returnObj.Count == 0)
                //{
                //    returnObj = null;
                //}
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj.Distinct().ToList();
        }

        public List<CustomersModel> GetCustomersByEmail(string Email)
        {
            List<CustomersModel> returnObj = new List<CustomersModel>();
            //try
            //{
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                if (string.IsNullOrEmpty(Email) == false)
                {
                    string Query = "select * from CustomerEmails where 1=1 and Email like'%" + Email + "%'";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<CustomersModel> CustObj = new List<CustomersModel>();
                        CustObj = GetCustomerByCustId(Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerId"]));
                        if (CustObj.Count > 0)
                        {
                            returnObj.Add(CustObj[0]);
                        }
                    }

                }
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj.Distinct().ToList();
        }
        public int SaveFileAs(int CustomerId, string FileAs)
        {
            int returnObj = 0;

            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                db.Transaction = db.con.BeginTransaction();
                Dictionary<string, object> ParameterDict = new Dictionary<string, object>();
                ParameterDict.Add("CustomerId", CustomerId);
                ParameterDict.Add("FileAs", FileAs);
                string Query = "update Customers set FileAs=@FileAs where CustomerId=@CustomerId";
                returnObj = db.InsertData(Query, ParameterDict, db.Transaction);
                if (returnObj > 0)
                {
                    db.Transaction.Commit();
                }
            }
            return returnObj;
        }
        public int SaveCustomer(CustomersModel CustObj)
        {
            int returnObj = 0;
            // try
            //{
            Dictionary<string, object> CustaddressParameterDict = new Dictionary<string, object>();
            Dictionary<string, object> CustEmailsParameterDict = new Dictionary<string, object>();
            Dictionary<string, object> CustPhonesParameterDict = new Dictionary<string, object>();
            Dictionary<string, object> CustGroupsParameterDict = new Dictionary<string, object>();
            if (CustObj != null)
            {
                //if(CustObj.CustomerId)
                int tempcustId = -1;
                string constring = SecurityconString;
                if (CustObj.CustomerId < 0)
                {

                    CustObj.CustomerId = GetMaxCustId(constring);
                }
                else
                {
                    tempcustId = CustObj.CustomerId;
                }
                if (string.IsNullOrEmpty(CustObj.ImageFileName) == false)
                {
                    string custImage = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustImagePath"]);
                    string[] orgids = CustObj.ImageFileName.Split('/');
                    if (orgids.Length > 1)
                    {
                        string Path = custImage + CustObj.ImageFileName;

                        string NewPath = custImage + orgids[0] + "/" + CustObj.CustomerId + ".jpg";
                        if (File.Exists(Path))
                        {
                            if (File.Exists(NewPath) && Path != NewPath)
                            {
                                System.IO.File.Delete(NewPath);
                            }
                            File.Move(Path, NewPath);
                        }
                    }
                    CustObj.ImageFileName = CustObj.CustomerId + ".jpg";
                }
                else
                {
                    string custImage = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustImagePath"])  + OrgId + "/" + CustObj.CustomerId + ".jpg";
                    if (File.Exists(custImage))
                    {
                        System.IO.File.Delete(custImage);
                    }
                    CustObj.ImageFileName = "";
                }
                bool IsValidCC = true;
                if (string.IsNullOrEmpty(CustObj.CustomerCode) == false)
                    IsValidCustCode(CustObj.CustomerCode, CustObj.CustomerId, constring);
                if (IsValidCC == true)
                {
                    Dictionary<string, object> ParameterDict = GetCustomerDict(CustObj);
                    using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                    {
                        db.Transaction = db.con.BeginTransaction();
                        string Query = "";
                        if (tempcustId == -1)
                            Query = "insert into Customers(CustomerId,employeeid,fname,lname,MiddleName,CustomerType,CameFromCustomer,CustomerCode," +
                            "CustomerCode2,BirthDate,BirthDate2,jobtitlePartner,Titel,Safixid,Suffix," +
                            "Gender,CardType,Potentianl,RelatedCustomer,RelationType,ActiveStatus,Remark,CustPosition,SpouseName," +
                            "Company,Deleted,TempId,tmp,assistant,FileAs,PermitionLevel,Salutation,SearchHide,SecurityLock,CardStatus,Title,ID," +
                            "BankCode,SnifNo,AccountType,AccountNo,ShortComment,Kids,FamlyStat,ForeignName,HALIA,Deceased,DeceasedYear,BornPlace," +
                            "AfterSunset1,AfterSunset2,MemberID,SpecialCust,SpouseID,MemberStart,MemberEnd,KEVAStart,KEVAEnd,KEVANAME,SmallRemark," +
                            "Remark2,HbirthMonthVal,HbirthMonthVal2,HbirthDayVal,HbirthDayVal2,HebDate1,HebDate2,LastUpdate,Mothername,Fathername," +
                            "UpdateEmp,ArriveDate,IDSTR,CardIsueDate,TMPINFO,TitleSpouse,TMPINFO2,TMPINFO3,TMPINFO4,xx,xxlen,ImageFileName,IsNewsLetter) " +

                            "Values(@CustomerId,@employeeid,@fname,@lname,@MiddleName,@CustomerType,@CameFromCustomer,@CustomerCode," +
                            "@CustomerCode2,Convert(datetime,@BirthDate,103),Convert(datetime,@BirthDate2,103),@jobtitlePartner,@Titel,@Safixid,@Suffix," +
                            "@Gender,1,0,0,0,0,@Remark,'','',@Company,0,0,0,'',@FileAs,0,'',0,0,0,@Title,'','','','','','',0,0,'','',0,'',''," +
                            "0,0,0,0,0,'','','','','','','',0,0,0,0,'','','','','',0,'1900-01-01 00:00:00','','1900-01-01 00:00:00','','','','','','',0,@ImageFileName,0) ";
                        else
                        {

                            Query = "update Customers set employeeid=@employeeid,fname=@fname,lname=@lname,MiddleName=@MiddleName,CustomerType=@CustomerType,CameFromCustomer=@CameFromCustomer,CustomerCode=@CustomerCode," +
                            "CustomerCode2=@CustomerCode2, BirthDate=Convert(datetime,@BirthDate,103),BirthDate2=Convert(datetime,@BirthDate2,103),jobtitlePartner=@jobtitlePartner,Titel=@Titel,Safixid=@Safixid,Suffix=@Suffix," +
                            "Gender=@Gender,Remark=@Remark," +
                            "Company=@Company,FileAs=@FileAs,Title=@Title,ImageFileName=@ImageFileName " +
                            //",BankCode='',SnifNo='',AccountType='',AccountNo='',ShortComment='',Kids=0,FamlyStat=0,ForeignName='',HALIA='',Deceased=0,DeceasedYear='',BornPlace=''," +
                            //"AfterSunset1=0,AfterSunset2=0,MemberID=0,SpecialCust=0,SpouseID=0,MemberStart='',MemberEnd='',KEVAStart='',KEVAEnd='',KEVANAME='',SmallRemark=''," +
                            //"Remark2,HbirthMonthVal,HbirthMonthVal2,HbirthDayVal,HbirthDayVal2,HebDate1,HebDate2,LastUpdate,Mothername,Fathername," +
                            //"UpdateEmp,ArriveDate,IDSTR,CardIsueDate,TMPINFO,TitleSpouse,TMPINFO2,TMPINFO3,TMPINFO4,xx,xxlen,ImageFileName,IsNewsLetter) " +

                            //"Values(,,,," +
                            //",,,,,,," +
                            //",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,," +
                            //",,,,,,,,,,,,0,0,0,0,'','','','','',0,'1900-01-01 00:00:00','','1900-01-01 00:00:00','','','','','','',0,'',0";
                            " where customerid=@CustomerId";
                        }


                        // put insert query here
                        returnObj = db.InsertData(Query, ParameterDict, db.Transaction);
                        if (tempcustId != -1)
                        {
                            CustaddressParameterDict.Clear();
                            CustaddressParameterDict.Add("CustomerId", CustObj.CustomerId);
                            string DelAddressQuery = "delete from CustomerAddress where CustomerId=@CustomerId";
                            returnObj += db.InsertData(DelAddressQuery, CustaddressParameterDict, db.Transaction);
                        }
                        foreach (var CustAddress in CustObj.CustomerAddresses)
                        {
                            CustaddressParameterDict.Clear();
                            CustAddress.CustomerId = CustObj.CustomerId;
                            CustomerAddressHelper CustAddrss = new CustomerAddressHelper();
                            CustaddressParameterDict = CustAddrss.GetCustomerAddressDict(CustAddress);
                            string AddressQuery = "insert into CustomerAddress(CustomerId,StateId,CityName,CountryCode,Street,Street2,Zip," +
                            "remark,ForDelivery,MaxYearDelivery,MaxMonthlyDelivery,LastDelivery,AddToName,AddressTypeId," +
                            "MainAddress,SrteetOnly,StreetNo,Entrance,DeliveryCode) " +
                            "Values(@CustomerId,@StateId,@CityName,@CountryCode,@Street,@Street2,@Zip," +
                            "@remark,@ForDelivery,@MaxYearDelivery,@MaxMonthlyDelivery,@LastDelivery,@AddToName,@AddressTypeId," +
                            "@MainAddress,@SrteetOnly,@StreetNo,@Entrance,@DeliveryCode) ";
                            returnObj += db.InsertData(AddressQuery, CustaddressParameterDict, db.Transaction);
                        }
                        if (tempcustId != -1)
                        {
                            CustEmailsParameterDict.Clear();
                            CustEmailsParameterDict.Add("CustomerId", CustObj.CustomerId);
                            string DelEmailQuery = "delete from CustomerEmails where CustomerId=@CustomerId";
                            returnObj += db.InsertData(DelEmailQuery, CustEmailsParameterDict, db.Transaction);
                        }
                        foreach (var CustEmail in CustObj.CustomerEmails)
                        {
                            CustEmailsParameterDict.Clear();
                            CustEmail.CustomerId = CustObj.CustomerId;
                            CustomerEmailHelper CustAddrss = new CustomerEmailHelper();
                            CustEmailsParameterDict = CustAddrss.GetCustomerEmailsDict(CustEmail);
                            string EmailQuery = "insert into CustomerEmails(CustomerId,Email,EmailName,Newslettere,General,MaxYearDelivery,MaxMonthlyDelivery,LastEmail," +
                            "Priority,EmailSex,publish) " +
                            "Values(@CustomerId,@Email,@EmailName,@Newslettere,@General,@MaxYearDelivery,@MaxMonthlyDelivery,@LastEmail," +
                            "@Priority,@EmailSex,@publish) ";
                            returnObj += db.InsertData(EmailQuery, CustEmailsParameterDict, db.Transaction);
                        }
                        if (tempcustId != -1)
                        {
                            CustEmailsParameterDict.Clear();
                            CustEmailsParameterDict.Add("CustomerId", CustObj.CustomerId);
                            string DelEmailQuery = "delete from CustomerPhones where CustomerId=@CustomerId";
                            returnObj += db.InsertData(DelEmailQuery, CustEmailsParameterDict, db.Transaction);
                        }
                        foreach (var CustPhone in CustObj.CustomerPhones)
                        {
                            CustPhonesParameterDict.Clear();
                            CustPhone.CustomerId = CustObj.CustomerId;
                            CustomerPhonesHelper CustAddrss = new CustomerPhonesHelper();
                            CustPhonesParameterDict = CustAddrss.GetCustomerPhonesDict(CustPhone);
                            string PhoneQuery = "insert into CustomerPhones(CustomerId,PhoneTypeId,Prefix,Area,Phone,Comments,IsSms,publish) " +
                            "Values(@CustomerId,@PhoneTypeId,@Prefix,@Area,@Phone,@Comments,@IsSms,@publish)";

                            returnObj += db.InsertData(PhoneQuery, CustPhonesParameterDict, db.Transaction);
                        }
                        if (tempcustId != -1)
                        {
                            CustGroupsParameterDict.Clear();
                            CustGroupsParameterDict.Add("CustomerId", CustObj.CustomerId);
                            string DelGrpsQuery = "delete from CustomerGroupsGeneralSet where Customerid=@CustomerId";
                            returnObj += db.InsertData(DelGrpsQuery, CustGroupsParameterDict, db.Transaction);
                        }
                        foreach (var CustGroup in CustObj.CustomerGroups)
                        {
                            CustGroupsParameterDict.Clear();
                            CustGroup.CustomerId = CustObj.CustomerId;
                            CustomerGroupsHelper CustAddrss = new CustomerGroupsHelper();
                            CustGroupsParameterDict = CustAddrss.GetCustomerGroupsDict(CustGroup);
                            string GroupQuery = "insert into CustomerGroupsGeneralSet(Customerid,CustomerGeneralGroupId) " +
                            "Values(@Customerid,@CustomerGeneralGroupId)";
                            returnObj += db.InsertData(GroupQuery, CustGroupsParameterDict, db.Transaction);
                        }
                        db.Transaction.Commit();
                    }
                }
                else
                {
                }
            }
            // }
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public CustomersModel GetCompleteCustomerDet(int CustomerId)
        {
            CustomersModel CustObj = new CustomersModel();
            List<CustomersModel> tempCustObj = new List<CustomersModel>();
            tempCustObj = GetCustomerByCustId(CustomerId);
            if (tempCustObj.Count > 0)
            {
                CustObj = tempCustObj[0];
                CustObj.BirthDate = Convert.ToDateTime(CustObj.BirthDate).ToString("dd-MM-yyyy");
                CustAddHP.SecurityconString = SecurityconString;
                CustAddHP.LangValue = lang;
                CustObj.CustomerAddresses = CustAddHP.GetCustomerAddressByCustId(CustomerId).OrderByDescending(or=>or.MainAddress).ToList();
                CustEmailHP.SecurityconString = SecurityconString;
                CustObj.CustomerEmails = CustEmailHP.GetCustomerEmailByCustId(CustomerId);
                CustPhoneHP.SecurityconString = SecurityconString;
                CustPhoneHP.LangValue = lang;
                CustObj.CustomerPhones = CustPhoneHP.GetCustomerPhoneByCustId(CustomerId);
                CustGrpsHP.SecurityconString = SecurityconString;
                CustGrpsHP.LangValue = lang;
                CustObj.CustomerGroups = CustGrpsHP.GetCustomerGrpsByCustId(CustomerId);
                //CustWebsiteHP.SecurityconString = SecurityconString;
                //CustObj.CustomerWebsites = CustWebsiteHP.GetCustomerWebsitebyCustId(CustomerId);
                //CustServiceHP.SecurityconString = SecurityconString;
                //CustServiceHP.LangValue = lang;
                //CustObj.CustomersService = CustServiceHP.GetCustomerServicebyCustId(CustomerId, 0, 1);

            }
            return CustObj;
        }
        public CustomersModel GetCompleteCustomerDetForProfile(int CustomerId)
        {
            CustomersModel CustObj = new CustomersModel();
            List<CustomersModel> tempCustObj = new List<CustomersModel>();
            tempCustObj = GetCustomerByCustId(CustomerId);
            if (tempCustObj.Count > 0)
            {
                CustObj = tempCustObj[0];
                CustObj.BirthDate = Convert.ToDateTime(CustObj.BirthDate).ToString("dd-MM-yyyy");
                CustAddHP.SecurityconString = SecurityconString;
                CustAddHP.LangValue = lang;
                CustObj.CustomerAddresses = CustAddHP.GetCustomerAddressByCustId(CustomerId).OrderByDescending(or => or.MainAddress).ToList();
                CustEmailHP.SecurityconString = SecurityconString;
                CustObj.CustomerEmails = CustEmailHP.GetCustomerEmailByCustId(CustomerId);
                CustPhoneHP.SecurityconString = SecurityconString;
                CustPhoneHP.LangValue = lang;
                CustObj.CustomerPhones = CustPhoneHP.GetCustomerPhoneByCustId(CustomerId);
                CustGrpsHP.SecurityconString = SecurityconString;
                CustGrpsHP.LangValue = lang;
                CustObj.CustomerGroups = CustGrpsHP.GetCustomerGrpsByCustId(CustomerId);
                CustWebsiteHP.SecurityconString = SecurityconString;
                CustObj.CustomerWebsites = CustWebsiteHP.GetCustomerWebsitebyCustId(CustomerId);
                CustServiceHP.SecurityconString = SecurityconString;
                CustServiceHP.LangValue = lang;
                CustObj.CustomersService = CustServiceHP.GetCustomerServicebyCustId(CustomerId, 0, 1);
                RcptCreateHP.SecurityconString = SecurityconString;
                RcptCreateHP.lang = lang;
                CustObj.CustomersReceipts = RcptCreateHP.GetReceiptsByCustId(CustomerId);
            }
            return CustObj;
        }

        public List<CustomerCreditCardModel> GetCustomerCreditCardDet(int CustomerId, int customercreditCardid)
        {
            List<CustomerCreditCardModel> returnObj = new List<CustomerCreditCardModel>();
            string Query = "";
            int LVcreditCount = 1;
            if (LVcreditCount > 0)
            {
                Query = "Select Customerid,customercreditCardid,creditCardNum,creditCardMonth,creditCardYear,creditCardBack,creditCardName,creditCardOwnerID,ParityNum,TokenNum,digits6  from Customercreditcard Where customerid=" + CustomerId + " and customercreditCardid=" + customercreditCardid + " And Customercreditcard.deleted=0 order by customercreditCardid Desc";

            }
            else
            {
                Query = "Select Customerid,customercreditCardid,creditCardNum,creditCardMonth,creditCardYear,creditCardBack,creditCardName,creditCardOwnerID,ParityNum,TokenNum,digits6  from Customercreditcard Where customerid=" + CustomerId + " And Customercreditcard.deleted=0 order by customercreditCardid Desc";
            }
            using (DbAccess db = new DbAccess(SecurityconString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //CustTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["TypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["TypeNameEng"])));
                    CustomerCreditCardModel Obj = new CustomerCreditCardModel();
                    Obj.customerid = Convert.ToInt32(ds.Tables[0].Rows[i]["Customerid"]);
                    Obj.customercreditCardid = Convert.ToInt32(ds.Tables[0].Rows[i]["customercreditCardid"]);
                    Obj.creditCardNum = Convert.ToString(ds.Tables[0].Rows[i]["creditCardNum"]);
                    Obj.creditCardMonth = Convert.ToInt32(ds.Tables[0].Rows[i]["creditCardMonth"]);
                    Obj.creditCardYear = Convert.ToInt32(ds.Tables[0].Rows[i]["creditCardYear"]);
                    Obj.creditCardBack = Convert.ToString(ds.Tables[0].Rows[i]["creditCardBack"]);
                    Obj.creditCardName = Convert.ToString(ds.Tables[0].Rows[i]["creditCardName"]);
                    Obj.creditCardOwnerID = Convert.ToString(ds.Tables[0].Rows[i]["creditCardOwnerID"]);
                    Obj.ParityNum = Convert.ToString(ds.Tables[0].Rows[i]["ParityNum"]);
                    Obj.TokenNum = Convert.ToString(ds.Tables[0].Rows[i]["TokenNum"]);
                    Obj.digits6 = Convert.ToString(ds.Tables[0].Rows[i]["digits6"]);
                    returnObj.Add(Obj);
                }

            }
            return returnObj;
        }
        public bool UploadcustImage(string CustImage)
        {
            bool returnObj = false;
            string custImage = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustImagePath"]);
            //string OrgName = GetOrgName(OrgId);
            string[] ImageName = CustImage.Split('.');
            if(ImageName[1]!="jpg"&& ImageName[1] != "jpeg"&& ImageName[1] != "png"&& ImageName[1] != "bmp")
            {
                string Path = custImage + "\\" + CustImage;
                if (!Directory.Exists(custImage))
                {
                    Directory.CreateDirectory(custImage);
                    if (File.Exists(Path))
                    {
                        File.Delete(Path);
                    }
                    //FileStream fs1 = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
                    //StreamWriter writer = new StreamWriter(fs1);
                    //writer.Write(Source);
                    //writer.Close();
                    returnObj = true;
                    
                }
            }
            
            return returnObj;
        }
    }
}