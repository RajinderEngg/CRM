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
namespace AmaxService.HelperClasses
{
    public class CustomerHelper
    {
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
                    //CustDictList.Add("Company", custobj.Company);
                    //CustDictList.Add("Deleted", custobj.Deleted);
                    //CustDictList.Add("TempId", custobj.TempId);
                    //CustDictList.Add("tmp", custobj.tmp);
                    //CustDictList.Add("assistant", custobj.assistant);
                    //CustDictList.Add("FileAs", custobj.FileAs);
                    //CustDictList.Add("PermitionLevel", custobj.PermitionLevel);



                    //CustDictList.Add("Salutation", custobj.Salutation);
                    //CustDictList.Add("SearchHide", custobj.SearchHide);
                    //CustDictList.Add("SecurityLock", custobj.SecurityLock);
                    //CustDictList.Add("CardStatus", custobj.CardStatus);
                    //CustDictList.Add("Title", custobj.Title);
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
                    //CustDictList.Add("ImageFileName", custobj.ImageFileName);
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
        public int GetMaxCustId()
        {
            int returnObj = 0;
            try
            {
                using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
                {
                    string Query = "select max(customerid) from customers";
                    string maxId = Convert.ToString(db.ExecuteScalarAsString(Query));
                    //   if (maxId == "") maxId = 0;
                    returnObj = Convert.ToInt32(maxId) + 1;

                }
            }
            catch (Exception ex)
            {
            }
            return returnObj;
        }
        public bool IsValidCustCode(string CustCode, int CustId)
        {
            bool returnObj = true;
            try
            {
                using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
                {
                    string Query = "select * from customers where Customerid!=" + CustId + " and CustomerCode='" + CustCode + "'";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                        returnObj = false;
                }
            }
            catch (Exception ex)
            {
            }
            return returnObj;
        }
        public string GetPhoneType(int PhoneTypeId)
        {
            string returnObj = "";
            try
            {
                //using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
                //{
                //    string Query = "select * from PhoneTypes where id=" + PhoneTypeId;
                //    DataSet ds = db.GetDataSet(Query, null, false);
                //    if (ds.Tables[0].Rows.Count > 0)
                //        returnObj =Convert.ToString( ds.Tables[0].Rows[0]["contenteng"]);
                //}
            }
            catch (Exception ex)
            {
            }
            return returnObj;
        }
        public string GetCountryName(string CountryCode)
        {
            string returnObj = "";
            try
            {
                using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
                {
                    string Query = "select * from Countries where CountryShortCode='" + CountryCode + "'";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                        returnObj = Convert.ToString(ds.Tables[0].Rows[0]["CountryName"]);
                }
            }
            catch (Exception ex)
            {
            }
            return returnObj;
        }



        public int SaveCustomer(CustomersModel CustObj)
        {
            int returnObj = 0;
            try
            {
                Dictionary<string, object> CustaddressParameterDict = new Dictionary<string, object>();
                Dictionary<string, object> CustEmailsParameterDict = new Dictionary<string, object>();
                Dictionary<string, object> CustPhonesParameterDict = new Dictionary<string, object>();
                Dictionary<string, object> CustGroupsParameterDict = new Dictionary<string, object>();
                if (CustObj != null)
                {
                    //if(CustObj.CustomerId)
                    CustObj.CustomerId = GetMaxCustId();

                    bool IsValidCC = true;
                    if(string.IsNullOrEmpty(CustObj.CustomerCode)==false)
                        IsValidCustCode(CustObj.CustomerCode, CustObj.CustomerId);
                    if (IsValidCC == true)
                    {
                        Dictionary<string, object> ParameterDict = GetCustomerDict(CustObj);
                        using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))
                        {
                            db.Transaction = db.con.BeginTransaction();
                            string Query = "insert into Customers(CustomerId,employeeid,fname,lname,MiddleName,CustomerType,CameFromCustomer,CustomerCode," +
                                "CustomerCode2,BirthDate,BirthDate2,jobtitlePartner,Titel,Safixid,Suffix," +
                                "Gender,CardType,RowDate,Potentianl,RelatedCustomer,RelationType,ActiveStatus,Remark,CustPosition,SpouseName," +
                                "Company,Deleted,TempId,tmp,assistant,FileAs,PermitionLevel,Salutation,SearchHide,SecurityLock,CardStatus,Title,ID," +
                                "BankCode,SnifNo,AccountType,AccountNo,ShortComment,Kids,FamlyStat,ForeignName,HALIA,Deceased,DeceasedYear,BornPlace," +
                                "AfterSunset1,AfterSunset2,MemberID,SpecialCust,SpouseID,MemberStart,MemberEnd,KEVAStart,KEVAEnd,KEVANAME,SmallRemark," +
                                "Remark2,HbirthMonthVal,HbirthMonthVal2,HbirthDayVal,HbirthDayVal2,HebDate1,HebDate2,LastUpdate,Mothername,Fathername," +
                                "UpdateEmp,ArriveDate,IDSTR,CardIsueDate,TMPINFO,TitleSpouse,TMPINFO2,TMPINFO3,TMPINFO4,xx,xxlen,ImageFileName,IsNewsLetter) " +

                                "Values(@CustomerId,@employeeid,@fname,@lname,@MiddleName,@CustomerType,@CameFromCustomer,@CustomerCode," +
                                "@CustomerCode2,@BirthDate,@BirthDate2,@jobtitlePartner,@Titel,@Safixid,@Suffix," +
                                "@Gender,1,'',0,0,0,0,@Remark,'','','',0,0,0,'','',0,'',0,0,0,'','','','','','','',0,0,'','',0,'',''," +
                                "0,0,0,0,0,'','','','','','','',0,0,0,0,'','','','','',0,'1900-01-01 00:00:00','','1900-01-01 00:00:00','','','','','','',0,'',0) ";
                            // put insert query here
                            returnObj = db.InsertData(Query, ParameterDict, db.Transaction);
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
            }
            catch (Exception ex)
            {
            }
            return returnObj;
        }
    }
}
