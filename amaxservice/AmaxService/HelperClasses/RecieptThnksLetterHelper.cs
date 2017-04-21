using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Web;

namespace AmaxService.HelperClasses
{
    public class RecieptThnksLetterHelper
    {
        public string SecurityconString { get; set; }
        public string LangValue { get; set; }
        public Dictionary<string, object> GetRecieptThnksLetterDict(RecieptThnksLetterModel RcptThnksLtrsobj)
        {
            Dictionary<string, object> RcptThnksLtrsDictList = new Dictionary<string, object>();
            //try
            //{
                if (RcptThnksLtrsobj != null)
                {
                    RcptThnksLtrsDictList.Add("ThanksLetterId", RcptThnksLtrsobj.ThanksLetterId);
                    RcptThnksLtrsDictList.Add("ThanksLetterName", RcptThnksLtrsobj.ThanksLetterName);
                    RcptThnksLtrsDictList.Add("ThanksLetterNameEng", RcptThnksLtrsobj.ThanksLetterNameEng);
                    RcptThnksLtrsDictList.Add("ThanksLetterfileName", RcptThnksLtrsobj.ThanksLetterfileName);
                    RcptThnksLtrsDictList.Add("ReceiptId", RcptThnksLtrsobj.ReceiptId);
                    RcptThnksLtrsDictList.Add("MailBody", RcptThnksLtrsobj.MailBody);
                    RcptThnksLtrsDictList.Add("MailSubj", RcptThnksLtrsobj.MailSubj);
                    RcptThnksLtrsDictList.Add("IsRtl", RcptThnksLtrsobj.IsRtl);
                    RcptThnksLtrsDictList.Add("langNum", RcptThnksLtrsobj.langNum);
                    
                }
            //}
           // catch (Exception ex)
           // {
            //}
            return RcptThnksLtrsDictList;
        }
        public List<RecieptThnksLetterModel> GetRcptThnksLtrsListFromDS(DataSet custobj)
        {
            List<RecieptThnksLetterModel> FinalDictList = new List<RecieptThnksLetterModel>();

            //try
            //{
                for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
                {
                    RecieptThnksLetterModel CustDictList = new RecieptThnksLetterModel();
                    CustDictList.ThanksLetterId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ThanksLetterId"]);
                    CustDictList.ThanksLetterName = Convert.ToString(custobj.Tables[0].Rows[i]["ThanksLetterName"]);
                    CustDictList.ThanksLetterNameEng = Convert.ToString(custobj.Tables[0].Rows[i]["ThanksLetterNameEng"]);
                    CustDictList.ThanksLetterfileName = Convert.ToString(custobj.Tables[0].Rows[i]["ThanksLetterfileName"]);
                    CustDictList.ReceiptId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ReceiptId"]);
                    CustDictList.MailBody = Convert.ToString(custobj.Tables[0].Rows[i]["MailBody"]);
                    CustDictList.MailSubj = Convert.ToString(custobj.Tables[0].Rows[i]["MailSubj"]);
                    CustDictList.IsRtl = Convert.ToBoolean(custobj.Tables[0].Rows[i]["IsRtl"]);
                    CustDictList.langNum = Convert.ToInt32(custobj.Tables[0].Rows[i]["langNum"]);
                    FinalDictList.Add(CustDictList);
                }
            //}
            //catch (Exception ex)
           // {
           // }
            return FinalDictList;
        }
        public List<RecieptThnksLetterModel> GetRecieptThnksLetters()
        {
            List<RecieptThnksLetterModel> returnObj = new List<RecieptThnksLetterModel>();
            //try
            //{
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from RecieptThanksLetters where 1=1";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        returnObj = GetRcptThnksLtrsListFromDS(ds);
                        if (LangValue == "en")
                        {
                            foreach (var robj in returnObj)
                            {
                                robj.RecieptThnksLetterText = robj.ThanksLetterNameEng;
                            }
                        }
                        else if (LangValue == "he")
                        {
                            foreach (var robj in returnObj)
                            {
                                robj.RecieptThnksLetterText = robj.ThanksLetterName;
                            }
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        
        public RecieptThnksLetterModel GetRecieptThnksLetter(int ThnksLetterId)
        {
            RecieptThnksLetterModel returnObj = new RecieptThnksLetterModel();
            //try
            //{
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from RecieptThanksLetters where 1=1 and ThanksLetterId="+ThnksLetterId;
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        returnObj = GetRcptThnksLtrsListFromDS(ds).FirstOrDefault();
                        


                    }
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }

        public List<RecieptThnksLetterModel> GetRecieptThnksLettersByRcptTypId(int RecieptTypeId)
        {
            List<RecieptThnksLetterModel> returnObj = new List<RecieptThnksLetterModel>();
            //try
            //{
                using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select * from RecieptThanksLetters where 1=1 and ReceiptId="+RecieptTypeId;
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        returnObj = GetRcptThnksLtrsListFromDS(ds);
                        if (LangValue == "en")
                        {
                            foreach (var robj in returnObj)
                            {
                                robj.RecieptThnksLetterText = robj.ThanksLetterNameEng;
                            }
                        }
                        else if (LangValue == "he")
                        {
                            foreach (var robj in returnObj)
                            {
                                robj.RecieptThnksLetterText = robj.ThanksLetterName;
                            }
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }

        public int GetMaxThnksLetterId(string ConString)
        {
            int returnObj = 0;
            //try
            //{
                using (DbAccess db = new DbAccess(ConString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select max(ThanksLetterId) from RecieptThanksLetters";
                    string maxId = Convert.ToString(db.ExecuteScalarAsString(Query));
                    //   if (maxId == "") maxId = 0;
                    returnObj = Convert.ToInt32(maxId) + 1;

                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public int SaveReceiptThnksLetter(RecieptThnksLetterModel RcptThnksLtrObj)
        {
            int returnObj = 0;
            //try
            //{
                if (RcptThnksLtrObj != null)
                {
                    //if(CustObj.CustomerId)
                    int tempthnksltrId = 0;
                    string constring = SecurityconString;
                    if (RcptThnksLtrObj.ThanksLetterId == 0)
                    {

                        RcptThnksLtrObj.ThanksLetterId = GetMaxThnksLetterId(constring);
                    }
                    else
                    {
                        tempthnksltrId = RcptThnksLtrObj.ThanksLetterId;
                    }
                    //bool IsValidCC = true;
                    //if (string.IsNullOrEmpty(CustObj.CustomerCode) == false)
                    //    IsValidCustCode(CustObj.CustomerCode, CustObj.CustomerId, constring);
                    //if (IsValidCC == true)
                    //{
                    Dictionary<string, object> ParameterDict = GetRecieptThnksLetterDict(RcptThnksLtrObj); 
                        using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                        {
                            db.Transaction = db.con.BeginTransaction();
                            string Query = "";
                            if (tempthnksltrId == 0)
                                Query = "insert into RecieptThanksLetters(ThanksLetterId,ThanksLetterName,ThanksLetterNameEng,ThanksLetterfileName,ReceiptId,MailBody,MailSubj,IsRtl,langNum) " +
                                "Values(@ThanksLetterId,@ThanksLetterName,@ThanksLetterNameEng,'',@ReceiptId,@MailBody,@MailSubj,0,0) ";
                            else
                            {

                                Query = "update RecieptThanksLetters set ThanksLetterName=@ThanksLetterName,ThanksLetterNameEng=@ThanksLetterNameEng,ReceiptId=@ReceiptId,MailBody=@MailBody,MailSubj=@MailSubj"+
                                " where ThanksLetterId=@ThanksLetterId";
                            }


                            // put insert query here
                            returnObj = db.InsertData(Query, ParameterDict, db.Transaction);
                            db.Transaction.Commit();
                        }
                    //}
                    //else
                    //{
                    //}
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }

        public int DeleteReceiptThnksLetter(int ThnksLetterId)
        {
            int returnObj = 0;
            //try
            //{
                    using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                    {
                        db.Transaction = db.con.BeginTransaction();
                        string Query = "delete from RecieptThanksLetters where ThanksLetterId="+ ThnksLetterId;
                        returnObj = db.InsertData(Query, null, db.Transaction);
                        db.Transaction.Commit();
                    }
            //}
           // catch (Exception ex)
            //{
            //}
            return returnObj;
        }


    }
}
