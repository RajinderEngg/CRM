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
    public class RecieptTypeHelper
    {
        public string SecurityconString { get; set; }
        public string LangValue { get; set; }
        public Dictionary<string, object> GetRecieptTypeDict(RecieptTypeModel RcptTypeobj)
        {
            Dictionary<string, object> RcptTypeDictList = new Dictionary<string, object>();
            try
            {
                if (RcptTypeobj != null)
                {
                    RcptTypeDictList.Add("RecieptTypeId", RcptTypeobj.RecieptTypeId);
                    RcptTypeDictList.Add("RecieptName", RcptTypeobj.RecieptName);
                    RcptTypeDictList.Add("RecieptNameEng", RcptTypeobj.RecieptNameEng);
                    RcptTypeDictList.Add("StartFromNum", RcptTypeobj.StartFromNum);
                    RcptTypeDictList.Add("CurrencyId", RcptTypeobj.CurrencyId);
                    RcptTypeDictList.Add("ForCanclation", RcptTypeobj.ForCanclation);
                    RcptTypeDictList.Add("TopTitleBig", RcptTypeobj.TopTitleBig);
                    RcptTypeDictList.Add("TopTitleSmall", RcptTypeobj.TopTitleSmall);
                    RcptTypeDictList.Add("ButtomTitleBig", RcptTypeobj.ButtomTitleBig);
                    RcptTypeDictList.Add("ButtomTitleSmall", RcptTypeobj.ButtomTitleSmall);
                    RcptTypeDictList.Add("SignitureImage", RcptTypeobj.SignitureImage);
                    RcptTypeDictList.Add("ThanksLetterId", RcptTypeobj.ThanksLetterId);
                    RcptTypeDictList.Add("UseAsCreditReceipt", RcptTypeobj.UseAsCreditReceipt);
                    RcptTypeDictList.Add("DatePrintFormat", RcptTypeobj.DatePrintFormat);
                    RcptTypeDictList.Add("Preview", RcptTypeobj.Preview);
                    RcptTypeDictList.Add("ReceiptCancelID", RcptTypeobj.ReceiptCancelID);

                    RcptTypeDictList.Add("DonationReceipt", RcptTypeobj.DonationReceipt);
                    RcptTypeDictList.Add("NotForTaxReport", RcptTypeobj.NotForTaxReport);
                    RcptTypeDictList.Add("SecurityLevel", RcptTypeobj.SecurityLevel);
                    RcptTypeDictList.Add("FORKEVA", RcptTypeobj.FORKEVA);

                    RcptTypeDictList.Add("IsSupport", RcptTypeobj.IsSupport);
                    RcptTypeDictList.Add("Num2WordLng", RcptTypeobj.Num2WordLng);

                    RcptTypeDictList.Add("ToRecordPaysForSupp", RcptTypeobj.ToRecordPaysForSupp);
                    RcptTypeDictList.Add("HandReciept", RcptTypeobj.HandReciept);
                    RcptTypeDictList.Add("RecieptForInvoice", RcptTypeobj.RecieptForInvoice);
                    RcptTypeDictList.Add("HideIt", RcptTypeobj.HideIt);
                }
            }
            catch (Exception ex)
            {
            }
            return RcptTypeDictList;
        }
        public List<RecieptTypeModel> GetRcptTypeListFromDS(DataSet custobj)
        {
            List<RecieptTypeModel> FinalDictList = new List<RecieptTypeModel>();

            try
            {
                for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
                {
                    RecieptTypeModel CustDictList = new RecieptTypeModel();
                    CustDictList.RecieptTypeId = Convert.ToInt32(custobj.Tables[0].Rows[i]["RecieptTypeId"]);
                    CustDictList.RecieptName = Convert.ToString(custobj.Tables[0].Rows[i]["RecieptName"]);
                    CustDictList.RecieptNameEng = Convert.ToString(custobj.Tables[0].Rows[i]["RecieptNameEng"]);
                    CustDictList.StartFromNum = Convert.ToInt32(custobj.Tables[0].Rows[i]["StartFromNum"]);
                    CustDictList.CurrencyId = Convert.ToString(custobj.Tables[0].Rows[i]["CurrencyId"]);
                    CustDictList.ForCanclation = Convert.ToBoolean(custobj.Tables[0].Rows[i]["ForCanclation"]);
                    CustDictList.TopTitleBig = Convert.ToString(custobj.Tables[0].Rows[i]["TopTitleBig"]);
                    CustDictList.TopTitleSmall = Convert.ToString(custobj.Tables[0].Rows[i]["TopTitleSmall"]);

                    CustDictList.ButtomTitleBig = Convert.ToString(custobj.Tables[0].Rows[i]["ButtomTitleBig"]);
                    CustDictList.ButtomTitleSmall = Convert.ToString(custobj.Tables[0].Rows[i]["ButtomTitleSmall"]);
                    CustDictList.SignitureImage = Convert.ToString(custobj.Tables[0].Rows[i]["SignitureImage"]);
                    CustDictList.ThanksLetterId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ThanksLetterId"]);
                    CustDictList.UseAsCreditReceipt = Convert.ToBoolean(custobj.Tables[0].Rows[i]["UseAsCreditReceipt"]);
                    CustDictList.DatePrintFormat = Convert.ToString(custobj.Tables[0].Rows[i]["DatePrintFormat"]);
                    CustDictList.Preview = Convert.ToBoolean(custobj.Tables[0].Rows[i]["Preview"]);
                    CustDictList.ReceiptCancelID = Convert.ToInt32(custobj.Tables[0].Rows[i]["ReceiptCancelID"]);



                    CustDictList.DonationReceipt = Convert.ToBoolean(custobj.Tables[0].Rows[i]["DonationReceipt"]);
                    CustDictList.NotForTaxReport = Convert.ToBoolean(custobj.Tables[0].Rows[i]["NotForTaxReport"]);

                    CustDictList.SecurityLevel = Convert.ToInt32(custobj.Tables[0].Rows[i]["SecurityLevel"]);
                    CustDictList.FORKEVA = Convert.ToBoolean(custobj.Tables[0].Rows[i]["FORKEVA"]);
                    CustDictList.IsSupport = Convert.ToBoolean(custobj.Tables[0].Rows[i]["IsSupport"]);
                    CustDictList.Num2WordLng = Convert.ToInt32(custobj.Tables[0].Rows[i]["Num2WordLng"]);
                    CustDictList.ToRecordPaysForSupp = Convert.ToInt32(custobj.Tables[0].Rows[i]["ToRecordPaysForSupp"]);
                    CustDictList.HandReciept = Convert.ToBoolean(custobj.Tables[0].Rows[i]["HandReciept"]);
                    CustDictList.RecieptForInvoice = Convert.ToBoolean(custobj.Tables[0].Rows[i]["RecieptForInvoice"]);
                    CustDictList.HideIt = Convert.ToInt32(custobj.Tables[0].Rows[i]["HideIt"]);
                    FinalDictList.Add(CustDictList);
                }
            }
            catch (Exception ex)
            {
            }
            return FinalDictList;
        }
        
        public List<RecieptTypeModel> GetRecieptTypes()
        {
            List<RecieptTypeModel> returnObj = new List<RecieptTypeModel>();
            try
            {
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from RecieptTypes where 1=1";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        returnObj = GetRcptTypeListFromDS(ds);
                        if (LangValue == "en")
                        {
                            foreach (var robj in returnObj)
                            {
                                robj.RecieptTypeText = robj.RecieptNameEng;
                            }
                        }
                        else if (LangValue == "he")
                        {
                            foreach (var robj in returnObj)
                            {
                                robj.RecieptTypeText = robj.RecieptName;
                            }
                        }
                    }
                        
                }
            }
            catch (Exception ex)
            {
            }
            return returnObj;
        }
        public string GetouthType(DbAccess db , int EmployeeId)
        {
            string SecurityLevel = Convert.ToString(db.ExecuteScalar("Select authoType from Employees where EmployeeId="+EmployeeId, null, false));
            return SecurityLevel;
        }
        public List<RecieptTypeModel> GetReceipts(int EmployeeId, int RecModes)
        {
            List<RecieptTypeModel> returnObj = new List<RecieptTypeModel>();
            using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                string Query = "";
                int Securitylevel = Convert.ToInt32(GetouthType(db, EmployeeId));

                bool isUseTmiha = Convert.ToBoolean(db.ExecuteScalar("Select IsSupport from ApplicationInfo", null, false));
                if (LangValue == "en")
                {
                    if (isUseTmiha)
                    {
                        // 1 for Donation 2 for regular 3 for Credit and 4 for Record
                        
                        Query = "SELECT     RecieptTypeId, RecieptNameEng   + ' (' + CurrencyId + ')' as RecieptName  From RecieptTypeS   "+
                            "Where  RecieptTypeId in ( SELECT     RecieptTypeId FROM         RecieptTypes WHERE     (RecieptTypes.IsSupport = 0) "+
                            " UNION ALL SELECT     RecieptTypeId FROM   ReceiptEmpSupport Where (ReceiptEmpSupport.EmployeeId = " + EmployeeId + ")) "+
                            " and RecieptTypes.SecurityLevel<= " + Securitylevel + " and HideIt=0  ";
                        
                    }
                    else {
                        Query = "SELECT  RecieptTypeId, RecieptNameEng   + ' (' + CurrencyId + ')' as RecieptName  From RecieptTypeS    Where HideIt=0 ";
                    }
                }
                else {
                    if (isUseTmiha)
                    {
                        Query = "SELECT RecieptTypeId, RecieptName    + ' (' + CurrencyId + ')' as RecieptName From RecieptTypes   Where RecieptTypeId in ( SELECT     RecieptTypeId FROM         RecieptTypes WHERE     (RecieptTypes.IsSupport = 0)"+
                            " UNION ALL SELECT     RecieptTypeId FROM         ReceiptEmpSupport Where (ReceiptEmpSupport.EmployeeId = " + EmployeeId + ")) "+
                            " and RecieptTypes.SecurityLevel<= " + Securitylevel + " and HideIt=0  ";
                    }
                    else {
                        Query = "SELECT RecieptTypeId, RecieptName    + ' (' + CurrencyId + ')' as RecieptName From RecieptTypes "+
                            " Where HideIt=0  ";
                        // RecieptTypes.SecurityLevel<= " + Securitylevel + " and 
                    }
                }
                if (RecModes == 1) Query += " and DonationReceipt = 1 and UseAsCreditReceipt = 0 and ForCanclation = 0 ";
                if (RecModes == 2) Query += " and DonationReceipt = 0 and UseAsCreditReceipt = 0 and ForCanclation = 0 ";
                if (RecModes == 4) Query += " and UseAsCreditReceipt =1 ";
                if (RecModes == 3) Query += "  and ForCanclation = 1  and UseAsCreditReceipt = 0";

                Query += " ORDER BY RecieptNameEng";
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        RecieptTypeModel CustDictList = new RecieptTypeModel();
                        CustDictList.RecieptTypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["RecieptTypeId"]);
                        CustDictList.RecieptName = Convert.ToString(ds.Tables[0].Rows[i]["RecieptName"]);
                        returnObj.Add(CustDictList);
                    }
                }
            }

            return returnObj;
        }

    }
}
