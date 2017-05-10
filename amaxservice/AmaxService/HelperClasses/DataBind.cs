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
        public string SecurityConString { get; set; }
        public string LangValue { get; set; }
        public List<KeyPair> AutoCompleteSrch()
        {
            List<KeyPair> CustSearchList = new List<KeyPair>();
            string Query = "select customers.customerid as CustomerId,fname,lname,CustomerCode,CustomerCode2,SpouseName,Company,FileAs from Customers";
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["CustomerId"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CustomerId"]), Convert.ToString(ds.Tables[0].Rows[i]["CustomerId"])));
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["fname"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["fname"]), Convert.ToString(ds.Tables[0].Rows[i]["fname"])));
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["lname"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["lname"]), Convert.ToString(ds.Tables[0].Rows[i]["lname"])));
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["CustomerCode"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CustomerCode"]), Convert.ToString(ds.Tables[0].Rows[i]["CustomerCode"])));
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["CustomerCode2"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CustomerCode2"]), Convert.ToString(ds.Tables[0].Rows[i]["CustomerCode2"])));
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["SpouseName"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["SpouseName"]), Convert.ToString(ds.Tables[0].Rows[i]["SpouseName"])));
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Company"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["Company"]), Convert.ToString(ds.Tables[0].Rows[i]["Company"])));
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["FileAs"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["FileAs"]), Convert.ToString(ds.Tables[0].Rows[i]["FileAs"])));
                    }
                }
                Query = "select Phone from CustomerPhones";
                DataSet ds1 = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[i]["Phone"])) == false)
                    {
                        CustSearchList.Add(new KeyPair(Convert.ToString(ds1.Tables[0].Rows[i]["Phone"]), Convert.ToString(ds1.Tables[0].Rows[i]["Phone"])));
                    }
                }

            }
            return CustSearchList.Distinct().ToList();
        }
        public List<KeyPair> GetCustType()
        {

            List<KeyPair> CustTypeList = new List<KeyPair>();
            string Query = "select * from CustomerType";
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //CustTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["TypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["TypeNameEng"])));
                    if (LangValue == "en")
                    {
                        CustTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["TypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["TypeNameEng"])));
                    }
                    else if (LangValue == "he")
                    {
                        CustTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["TypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["TypeNameHeb"])));
                    }
                }

            }
            return CustTypeList;
        }
        public List<KeyPair> GetEmployees()
        {
            List<KeyPair> EmployeeList = new List<KeyPair>();
            string Query = "select * from Employees";
            using (DbAccess db = new DbAccess(SecurityConString)) //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["employeeid"]), Convert.ToString(ds.Tables[0].Rows[i]["fname"]) + " " + Convert.ToString(ds.Tables[0].Rows[i]["lname"])));
                }
            }
            return EmployeeList;
        }
        public List<KeyPair> GetCustTitle()
        {
            List<KeyPair> EmployeeList = new List<KeyPair>();
            string Query = "select * from CustomerTitle";
            using (DbAccess db = new DbAccess(SecurityConString)) //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["TitleId"]), Convert.ToString(ds.Tables[0].Rows[i]["TitleEng"])));
                    if (LangValue == "he")
                        EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["TitleId"]), Convert.ToString(ds.Tables[0].Rows[i]["TitleHeb"])));
                    
                }
            }
            return EmployeeList;
        }


        public List<KeyPair> GetThnksLetters(int ReceiptId)
        {
            List<KeyPair> EmployeeList = new List<KeyPair>();
            string Query = "select * from RecieptThanksLetters where ReceiptId="+ReceiptId;
            using (DbAccess db = new DbAccess(SecurityConString)) //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if(LangValue=="en")
                    EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ThanksLetterId"]), Convert.ToString(ds.Tables[0].Rows[i]["ThanksLetterNameEng"])));
                    if (LangValue == "he")
                        EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ThanksLetterId"]), Convert.ToString(ds.Tables[0].Rows[i]["ThanksLetterName"])));
                }
            }
            return EmployeeList;
        }
        public List<KeyPair> Getassociation()
        {
            List<KeyPair> EmployeeList = new List<KeyPair>();
            string Query = "select * from Associations";
            using (DbAccess db = new DbAccess(SecurityConString)) //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                if(ds.Tables[0].Rows.Count>0)
                {
                    if (LangValue == "en")
                        EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[0]["associationId"]), Convert.ToString(ds.Tables[0].Rows[0]["associationNameEng"])));
                    if (LangValue == "he")
                        EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[0]["associationId"]), Convert.ToString(ds.Tables[0].Rows[0]["associationName"])));
                }
            }
            return EmployeeList;
        }
        public List<KeyPair> GetPrinter()
        {
            List<KeyPair> EmployeeList = new List<KeyPair>();
            string Query = "select * from Printers";
            using (DbAccess db = new DbAccess(SecurityConString)) //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (LangValue == "en")
                        EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[0]["PrinterId"]), Convert.ToString(ds.Tables[0].Rows[0]["PrinterNameEng"])));
                    if (LangValue == "he")
                        EmployeeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[0]["PrinterId"]), Convert.ToString(ds.Tables[0].Rows[0]["PrinterName"])));
                }
            }
            return EmployeeList;
        }
        public List<KeyPair> GetCustSources()
        {
            List<KeyPair> ClientCameFromList = new List<KeyPair>();
            string Query = "select * from ClientCameFrom";
            using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
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
            using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        SafixList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["SafixId"]), Convert.ToString(ds.Tables[0].Rows[i]["SafixEng"])));
                    else if (LangValue == "he")
                        SafixList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["SafixId"]), Convert.ToString(ds.Tables[0].Rows[i]["Safix"])));
                }
            }
            return SafixList;
        }
        public List<KeyPair> GetPhoneTypes()
        {
            List<KeyPair> PhtypeList = new List<KeyPair>();
            string Query = "select * from PhoneTypes";
            using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //PhTypeObj.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                    //PhTypeObj.contenteng = Convert.ToString(ds.Tables[0].Rows[i]["contenteng"]);
                    //PhTypeObj.contentHeb = Convert.ToString(ds.Tables[0].Rows[i]["contentHeb"]);
                    //PhTypeObj.CellPhone = Convert.ToInt32(ds.Tables[0].Rows[i]["CellPhone"]);
                    if (LangValue == "en")
                        PhtypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["id"]), Convert.ToString(ds.Tables[0].Rows[i]["contenteng"])));
                    else if (LangValue == "he")
                        PhtypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["id"]), Convert.ToString(ds.Tables[0].Rows[i]["contentHeb"])));
                }
            }
            return PhtypeList;
        }
        public List<KeyPair> GetAddressType()
        {
            List<KeyPair> AddressTypeList = new List<KeyPair>();
            string Query = "select * from CustomerAddressType";
            using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        AddressTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["AddressTypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["AddressTypeNameEng"])));
                    else if (LangValue == "he")
                        AddressTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["AddressTypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["AddressTypeName"])));
                }
            }
            return AddressTypeList;
        }
        public List<KeyPair> GetGroups()
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            string Query = "select * from CustomerGroupsGeneral";
            using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["GroupId"]), Convert.ToString(ds.Tables[0].Rows[i]["GroupNameEng"])));
                    else
                        GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["GroupId"]), Convert.ToString(ds.Tables[0].Rows[i]["GroupName"])));
                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetStates(string countryName = null)
        {
            List<KeyPair> GroupsList = new List<KeyPair>();

            string Query = "select * from States where 1=1";
            if (string.IsNullOrEmpty(countryName) == false)
                Query += " and countryName='" + countryName + "'";
            using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["StateShort"]), Convert.ToString(ds.Tables[0].Rows[i]["StateName"])));

                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetCities(string CountryCode = null, string StateName = null)
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            string Query = "select * from Cities where 1=1 ";
            if (string.IsNullOrEmpty(CountryCode) == false)
                Query += " and countrycode='" + CountryCode + "'";
            if (string.IsNullOrEmpty(StateName) == false)
                Query += " and StateName='" + StateName + "'";
            using (DbAccess db = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CityName"]), Convert.ToString(ds.Tables[0].Rows[i]["CityName"])));
                }
            }
            return GroupsList;
        }
        public List<GroupTree> GetGroupDataSet(bool IsShowAll)
        {
            List<GroupTree> GroupsList = new List<GroupTree>();
            DataTable dt;
            using (DbAccess da = new DbAccess(SecurityConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "SELECT GroupId, GroupName, GroupNameEng, GroupParenCategory FROM CustomerGroupsGeneral WHERE ( IsSupport = 0 and Ishide = 0 and SecurityLevel <= 10)";
                if (IsShowAll == false)
                    Query += " and quick=1 ";
                Query += " ORDER BY GroupName";
                dt = da.GetDataTable(Query, null);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if(LangValue=="en")
                    GroupsList.Add(new GroupTree()
                    {
                        GroupId = Convert.ToInt32(dt.Rows[i]["GroupId"]),
                        GroupName = Convert.ToString(dt.Rows[i]["GroupName"])
                        ,
                        GroupNameEng = Convert.ToString(dt.Rows[i]["GroupNameEng"]),
                        GroupParenCategory = Convert.ToInt32(dt.Rows[i]["GroupParenCategory"])
                    });
                    //else if(LangValue=="he")
                    //    GroupsList.Add(new GroupTree()
                    //    {
                    //        GroupId = Convert.ToInt32(dt.Rows[i]["GroupId"]),
                    //        GroupName = Convert.ToString(dt.Rows[i]["GroupName"])
                    //    ,
                    //        GroupNameEng = Convert.ToString(dt.Rows[i]["GroupNameEng"]),
                    //        GroupParenCategory = Convert.ToInt32(dt.Rows[i]["GroupParenCategory"])
                    //    });
                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetCountries()
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            string Query = "select * from Countries";
            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //if (LangValue == "en")
                        GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CountryShortCode"]), Convert.ToString(ds.Tables[0].Rows[i]["CountryName"])));
                    //else if (LangValue == "he")
                    //    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CountryShortCode"]), Convert.ToString(ds.Tables[0].Rows[i]["CountryNameHeb"])));
                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetCustomerNotes()
        {
            List<KeyPair> GroupsList = new List<KeyPair>();
            string Query = "select id,note from tblCustomNotesItems where subjectid=0 order by note";
            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //if (LangValue == "en")
                    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["id"]), Convert.ToString(ds.Tables[0].Rows[i]["note"])));
                    //else if (LangValue == "he")
                    //    GroupsList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CountryShortCode"]), Convert.ToString(ds.Tables[0].Rows[i]["CountryNameHeb"])));
                }
            }
            return GroupsList;
        }
        public List<KeyPair> GetReceiptTypes()
        {
            List<KeyPair> RcptTypeList = new List<KeyPair>();
            string Query = "select * from RecieptTypes";
            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RcptTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["RecieptTypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["RecieptNameEng"]) + " | " + Convert.ToString(ds.Tables[0].Rows[i]["RecieptName"])));
                }
            }
            return RcptTypeList;
        }
        public List<KeyPair> GetTerminalList()
        {
            List<KeyPair> TermList = new List<KeyPair>();
            string Query = "SELECT     Accounts.AccountId, Accounts.AccountName, Accounts.AccountNameEng, institute.instituteCode, institute.instituteName" +
                            " FROM Accounts INNER JOIN " +
                            " institute ON Accounts.ASHRAY = institute.instituteCode";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        TermList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["instituteCode"]), Convert.ToString(ds.Tables[0].Rows[i]["AccountNameEng"]) + " | " + Convert.ToString(ds.Tables[0].Rows[i]["instituteCode"])));
                    else if (LangValue == "he")
                        TermList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["instituteCode"]), Convert.ToString(ds.Tables[0].Rows[i]["AccountName"]) + " | " + Convert.ToString(ds.Tables[0].Rows[i]["instituteCode"])));
                }
            }
            return TermList;
        }
        public List<KeyPair> GetCurrencyList()
        {
            List<KeyPair> CurrencyList = new List<KeyPair>();
            CurrencyList.Add(new KeyPair("Nis", "NIS"));
            //CurrencyList.Add(new KeyPair("SHEKEL", "SHEKEL"));
            CurrencyList.Add(new KeyPair("Doller", "DOLLER"));
            //CurrencyList.Add(new KeyPair("POUND", "POUND"));
            return CurrencyList;
        }
        public List<KeyPair> GetCurrencyListFromDb()
        {
            List<KeyPair> CurrencyList = new List<KeyPair>();
            string Query = "SELECT * FROM currencies ";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //if (LangValue == "en")
                        CurrencyList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["CurrencyId"]), Convert.ToString(ds.Tables[0].Rows[i]["CurrencyDetailEng"])));
                    //else if (LangValue == "he")
                    //    CurrencyList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["PayTypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["PaymentTypeOther"])));
                }
            }
            return CurrencyList;
        }
        public List<KeyPair> GetBanks()
        {
            List<KeyPair> CurrencyList = new List<KeyPair>();
            string Query = "SELECT * FROM banks ";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                    CurrencyList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["BankNameEng"]), Convert.ToString(ds.Tables[0].Rows[i]["BankNameEng"])));
                    else if (LangValue == "he")
                        CurrencyList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["BankName"]), Convert.ToString(ds.Tables[0].Rows[i]["BankName"])));
                }
            }
            return CurrencyList;
        }

        public List<KeyPair> GetCreditTypeList()
        {
            List<KeyPair> CredittypeList = new List<KeyPair>();
            CredittypeList.Add(new KeyPair("0", "Regular"));
            CredittypeList.Add(new KeyPair("1", "num Of Payments"));
            CredittypeList.Add(new KeyPair("2", "Credit"));
            return CredittypeList;
        }

        public List<KeyPair> GetChargeTypeList()
        {
            List<KeyPair> ChargetypeList = new List<KeyPair>();
            ChargetypeList.Add(new KeyPair("0", "Charge"));
            ChargetypeList.Add(new KeyPair("1", "Credit"));
            ChargetypeList.Add(new KeyPair("2", "Cancel"));
            return ChargetypeList;
        }

        public List<KeyPair> GetYears(int CurrYear)
        {
            List<KeyPair> YrList = new List<KeyPair>();
            for (int i = 0; i < 10; i++)
            {
                YrList.Add(new KeyPair((CurrYear+i).ToString(), (CurrYear + i).ToString()));
            }
            return YrList;
        }
        public List<KeyPair> GetMonths()
        {
            List<KeyPair> MnthList = new List<KeyPair>();
            MnthList.Add(new KeyPair("1", "Jan"));
            MnthList.Add(new KeyPair("2", "Feb"));
            MnthList.Add(new KeyPair("3", "Mar"));
            

            MnthList.Add(new KeyPair("4", "April"));
            MnthList.Add(new KeyPair("5", "May"));
            MnthList.Add(new KeyPair("6", "June"));
            MnthList.Add(new KeyPair("7", "July"));

            MnthList.Add(new KeyPair("8", "Aug"));
            MnthList.Add(new KeyPair("9", "Sept"));
            MnthList.Add(new KeyPair("10", "Oct"));
            MnthList.Add(new KeyPair("11", "Nov"));
            MnthList.Add(new KeyPair("12", "Dec"));
            return MnthList;
        }
        public KeyPair GetPhoneTypeDet(int PhoneTypeId)
        {
            KeyPair PhoneTypeObj = new KeyPair("","");
            string Query = "SELECT     Id,CellPhone FROM PhoneTypes " +
                            " where Id="+PhoneTypeId;

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                if ( ds.Tables[0].Rows.Count>0)
                {

                    PhoneTypeObj.Value = Convert.ToString(ds.Tables[0].Rows[0]["Id"]);
                    PhoneTypeObj.Text= Convert.ToString(ds.Tables[0].Rows[0]["CellPhone"]);
                }
            }
            return PhoneTypeObj;
        }
        public List<KeyPair> GetReceiptModes()
        {
            
           List<KeyPair> ReceiptModesList = new List<KeyPair>();
            if (LangValue == "he")
            {
                ReceiptModesList.Add(new KeyPair("1", "קבלה של תרומה"));
                ReceiptModesList.Add(new KeyPair("2", "קבלה רגילה"));
                ReceiptModesList.Add(new KeyPair("3", "קבלת שיא"));
                ReceiptModesList.Add(new KeyPair("4", "קבלת אשראי"));
            }
            else if (LangValue == "en")
            {
                ReceiptModesList.Add(new KeyPair("1", "Donation Receipt"));
                ReceiptModesList.Add(new KeyPair("2", "Regular Receipt"));
                ReceiptModesList.Add(new KeyPair("3", "Record Receipt"));
                ReceiptModesList.Add(new KeyPair("4", "Credit Receipt"));

            }
            return ReceiptModesList;
        }
        public List<KeyPair> GetPayType()
        {
            List<KeyPair> PayTypeList = new List<KeyPair>();
            string Query = "SELECT     PayTypeId,PaymentTypeOther,PaymentTypeEng FROM PaymentTypes ";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        PayTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["PayTypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["PaymentTypeEng"]) ));
                    else if (LangValue == "he")
                        PayTypeList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["PayTypeId"]), Convert.ToString(ds.Tables[0].Rows[i]["PaymentTypeOther"])));
                }
            }
            return PayTypeList;
        }
        public List<KeyPair> GetAccounts()
        {
            List<KeyPair> AccountList = new List<KeyPair>();
            string Query = "SELECT     AccountId,AccountName,AccountNameEng FROM Accounts ";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        AccountList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["AccountId"]), Convert.ToString(ds.Tables[0].Rows[i]["AccountNameEng"])));
                    else if (LangValue == "he")
                        AccountList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["AccountId"]), Convert.ToString(ds.Tables[0].Rows[i]["AccountName"])));
                }
            }
            return AccountList;
        }
        public List<KeyPair> GetGoals()
        {
            List<KeyPair> GoalList = new List<KeyPair>();
            string Query = "SELECT     DonationTypeId,DonationTypeOther,DonationTypeEng FROM DonationTypes ";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        GoalList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["DonationTypeId"]),  Convert.ToString(ds.Tables[0].Rows[i]["DonationTypeEng"])));
                    else if (LangValue == "he")
                        GoalList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["DonationTypeId"]),  Convert.ToString(ds.Tables[0].Rows[i]["DonationTypeOther"])));
                }
            }
            return GoalList;
        }
        public List<KeyPair> GetProjectCats()
        {
            List<KeyPair> ProjectCatList = new List<KeyPair>();
            string Query = "SELECT     ProjectCategoryId,CategoryName,CategoryNameEng FROM ProjectsCategories ";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        ProjectCatList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ProjectCategoryId"]), Convert.ToString(ds.Tables[0].Rows[i]["CategoryNameEng"])));
                    else if (LangValue == "he")
                        ProjectCatList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ProjectCategoryId"]), Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"])));
                }
            }
            return ProjectCatList;
        }
        public List<KeyPair> GetProjects(int CategoryId)
        {
            List<KeyPair> ProjectList = new List<KeyPair>();
            string Query = "SELECT     ProjectId,ProjectName,ProjectNameEng FROM Projects ";
            if (CategoryId != -1) { Query += " where ProjectCategoryId=" + CategoryId; }
                

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        ProjectList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ProjectId"]), Convert.ToString(ds.Tables[0].Rows[i]["ProjectNameEng"])));
                    else if (LangValue == "he")
                        ProjectList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ProjectId"]), Convert.ToString(ds.Tables[0].Rows[i]["ProjectName"])));
                }
            }
            return ProjectList;
        }
        public KeyPair GetReceiptDetail()
        {
            KeyPair RectDet = new KeyPair("","");
            string Query = "SELECT max(Convert(int,RecieptNo)) as ReceiptNo FROM reciepts ";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                if ( ds.Tables[0].Rows.Count>0)
                {

                    RectDet.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    int receiptno = 0;
                    if (string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ReceiptNo"])) == false)
                    {
                        receiptno = Convert.ToInt32(Convert.ToString(ds.Tables[0].Rows[0]["ReceiptNo"]));
                    }
                    receiptno++;
                    RectDet.Value = receiptno.ToString();
                    //if (LangValue == "en")
                    //    RectDet.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["BankId"]), Convert.ToString(ds.Tables[0].Rows[i]["BankNameEng"])));
                    //else if (LangValue == "he")
                    //    RectDet.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["BankId"]), Convert.ToString(ds.Tables[0].Rows[i]["BankName"])));
                }
            }
            return RectDet;
        }

        public List<KeyPair> GetProductCats()
        {
            List<KeyPair> ProjectList = new List<KeyPair>();
            string Query = "SELECT     ProdCatId,CategoryName,CategoryNameEng FROM ProdactsCategories where Deleted!=1 ";



            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (LangValue == "en")
                        ProjectList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ProdCatId"]), Convert.ToString(ds.Tables[0].Rows[i]["CategoryNameEng"])));
                    else if (LangValue == "he")
                        ProjectList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["ProdCatId"]), Convert.ToString(ds.Tables[0].Rows[i]["CategoryName"])));
                }
            }
            return ProjectList;
        }
    }
}
