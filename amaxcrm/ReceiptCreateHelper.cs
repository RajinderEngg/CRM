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
    public class ReceiptCreateHelper
    {
        public string SecurityconString { get; set; }
        public string lang { get; set; }
        public Dictionary<string, object> GetReceiptDict(ReseiptsModel custobj)
        {

            Dictionary<string, object> ReceiptDictList = new Dictionary<string, object>();
            try
            {
                if (custobj != null)
                {
                    ReceiptDictList.Add("RecieptType", custobj.RecieptType);
                    ReceiptDictList.Add("RecieptNo", custobj.RecieptNo);
                    ReceiptDictList.Add("RecieptDate", custobj.RecieptDate);
                    ReceiptDictList.Add("CustomerId", custobj.CustomerId);
                    ReceiptDictList.Add("AddressId", custobj.AddressId);
                    ReceiptDictList.Add("RecievedCustId", custobj.RecievedCustId);
                    ReceiptDictList.Add("WhatFor", custobj.WhatFor);
                    ReceiptDictList.Add("CurrencyId", custobj.CurrencyId);

                    ReceiptDictList.Add("TotalInWords", custobj.TotalInWords);
                    ReceiptDictList.Add("Total", custobj.Total);
                    ReceiptDictList.Add("associationId", custobj.associationId);
                    ReceiptDictList.Add("EmployeeId", custobj.EmployeeId);
                    ReceiptDictList.Add("ThanksLetter", custobj.ThanksLetter);
                    ReceiptDictList.Add("ThanksLetterId", custobj.ThanksLetterId);
                    ReceiptDictList.Add("Credit4Digit", custobj.Credit4Digit);
                    ReceiptDictList.Add("PrinterId", custobj.PrinterId);


                    ReceiptDictList.Add("OriginalWasPrinted", custobj.OriginalWasPrinted);
                    ReceiptDictList.Add("StateId", custobj.StateId);
                    ReceiptDictList.Add("CityName", custobj.CityName);
                    ReceiptDictList.Add("CountryCode", custobj.CountryCode);
                    ReceiptDictList.Add("Street", custobj.Street);
                    ReceiptDictList.Add("Street2", custobj.Street2);
                    ReceiptDictList.Add("Zip", custobj.Zip);
                    ReceiptDictList.Add("fname", custobj.fname);

                    ReceiptDictList.Add("lname", custobj.lname);
                    ReceiptDictList.Add("Titel", custobj.Titel);
                    ReceiptDictList.Add("MiddleName", custobj.MiddleName);
                    ReceiptDictList.Add("Company", custobj.Company);
                    ReceiptDictList.Add("Safix", custobj.Safix);
                    ReceiptDictList.Add("Address_Remark", custobj.Address_Remark);
                    ReceiptDictList.Add("WhatForInThanksLet", custobj.WhatForInThanksLet);
                    ReceiptDictList.Add("TotalInLeadCurrent", custobj.TotalInLeadCurrent);



                    ReceiptDictList.Add("CustomizeLine", custobj.CustomizeLine);
                    ReceiptDictList.Add("ReceiptNoKeva", custobj.ReceiptNoKeva);
                    ReceiptDictList.Add("ReceiptTypeKeva", custobj.ReceiptTypeKeva);
                    ReceiptDictList.Add("KeVaHistoryId", custobj.KeVaHistoryId);
                    ReceiptDictList.Add("digitalEmployeeId", custobj.digitalEmployeeId);
                    ReceiptDictList.Add("digitalfileName", custobj.digitalfileName);
                    ReceiptDictList.Add("digitalPath", custobj.digitalPath);
                    ReceiptDictList.Add("digitalDate", custobj.digitalDate);

                    ReceiptDictList.Add("RowDate", custobj.RowDate);
                    ReceiptDictList.Add("isCredit", custobj.isCredit);
                    
                }
            }
            catch (Exception ex)
            {
            }
            return ReceiptDictList;
        }
        public Dictionary<string, object> GetReceiptLinesDict(ReceiptLineModel custobj)
        {

            Dictionary<string, object> ReceiptLnDictList = new Dictionary<string, object>();
            try
            {
                if (custobj != null)
                {                         

                    ReceiptLnDictList.Add("RecieptRoWID", custobj.RecieptRoWID);
                    ReceiptLnDictList.Add("RecieptType", custobj.RecieptType);
                    ReceiptLnDictList.Add("RecieptNo", custobj.RecieptNo);
                    ReceiptLnDictList.Add("ProjectId", custobj.ProjectId);
                    ReceiptLnDictList.Add("PayTypeId", custobj.PayTypeId);
                    ReceiptLnDictList.Add("Amount", custobj.Amount);
                    ReceiptLnDictList.Add("USDVal", custobj.USDVal);
                    //ReceiptLnDictList.Add("TotalInWords", custobj.TotalInWords);

                    ReceiptLnDictList.Add("ValueDate", custobj.ValueDate);
                    ReceiptLnDictList.Add("CheckNo", custobj.CheckNo);
                    ReceiptLnDictList.Add("BranchNo", custobj.BranchNo);
                    ReceiptLnDictList.Add("AccountNo", custobj.AccountNo);
                    ReceiptLnDictList.Add("details", custobj.details);
                    ReceiptLnDictList.Add("DonationTypeId", custobj.DonationTypeId);
                    ReceiptLnDictList.Add("ImageName", custobj.ImageName);
                    ReceiptLnDictList.Add("AccountId", custobj.AccountId);


                    ReceiptLnDictList.Add("CameFrom", custobj.CameFrom);
                    ReceiptLnDictList.Add("Bank", custobj.Bank);
                    ReceiptLnDictList.Add("AmountInLeadCurrent", custobj.AmountInLeadCurrent);
                    ReceiptLnDictList.Add("ReferenceDate", custobj.ReferenceDate);
                    ReceiptLnDictList.Add("OldReceiptId", custobj.OldReceiptId);
                    ReceiptLnDictList.Add("Payed", custobj.Payed);
                    ReceiptLnDictList.Add("For_Invoice", custobj.For_Invoice);
                    ReceiptLnDictList.Add("IsExport", custobj.IsExport);

                    ReceiptLnDictList.Add("WasDeposit", custobj.WasDeposit);
                    ReceiptLnDictList.Add("DepositeNo", custobj.DepositeNo);
                    ReceiptLnDictList.Add("DepositeDate", custobj.DepositeDate);
                    ReceiptLnDictList.Add("DepositeToAccountId", custobj.DepositeToAccountId);
                    ReceiptLnDictList.Add("DepositeRemark", custobj.DepositeRemark);
                    //ReceiptLnDictList.Add("WhatForInThanksLet", custobj.WhatForInThanksLet);
                    //ReceiptLnDictList.Add("TotalInLeadCurrent", custobj.TotalInLeadCurrent);



                    ReceiptLnDictList.Add("TotalDeposit", custobj.TotalDeposit);
                    ReceiptLnDictList.Add("KevaInstitute", custobj.KevaInstitute);
                    //ReceiptLnDictList.Add("ReceiptNoKeva", custobj.ReceiptNoKeva);
                    ReceiptLnDictList.Add("CreditCardType", custobj.CreditCardType);
                   // ReceiptLnDictList.Add("KeVaHistoryId", custobj.KeVaHistoryId);
                    ReceiptLnDictList.Add("RowDate", custobj.RowDate);
                    ReceiptLnDictList.Add("BankId", custobj.BankId);
                }
            }
            catch (Exception ex)
            {
            }
            return ReceiptLnDictList;
        }


        public string IsValidCustomerReceipt(int CustId,int ReceiptId)
        {
            string returnObj = "";
            string Query = "select * from Customers where CustomerId="+ CustId;
            using (DbAccess db = new DbAccess(SecurityconString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count==0)
                {
                    returnObj += "\nPlease enter valid customerid";
                }
                Query = "select * from RecieptTypes where RecieptTypeId=" + ReceiptId;
                DataSet ds1 = db.GetDataSet(Query, null, false);
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    returnObj += "\nPlease enter valid receiptid";
                }
            }
            return returnObj;
        }
        public CurrencyModel CheckLeadcurrency(string CurrencyId,string StrLeadCurrency,DateTime ValueDate)
        {
            CurrencyModel FrmUpdateCurrecy = new CurrencyModel();
            bool CheckLeadcurrency = false;
            int LeadRAte = 1;



            if (StrLeadCurrency != CurrencyId) {
                //'îèáò îåáéì ùåðä îîèáò ðáçø';

                string strSql = "";
                //strSql = "Select Rate from CurrenyRates Where (DATEDIFF(d, RateDate, CONVERT(datetime, '" + System.DateTime.Now.ToShortDateString() + "', 103)) = 0 And ToCurrency='" + StrLeadCurrency + "' And CurrencyId='" + CurrencyId + "') ";
                strSql = "Select Rate from CurrenyRates Where (DATEDIFF(d, RateDate, CONVERT(datetime, '" + ValueDate.ToShortDateString() + "', 103)) = 0 And ToCurrency='" + StrLeadCurrency + "' And CurrencyId='" + CurrencyId + "') ";

                using (DbAccess db = new DbAccess(SecurityconString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    DataSet ds = db.GetDataSet(strSql, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        LeadRAte = Convert.ToInt32(ds.Tables[0].Rows[0]["Rate"]);
                    }





                    if (LeadRAte == 0 || ds.Tables[0].Rows.Count == 0) {


                        FrmUpdateCurrecy.CurrencyId = StrLeadCurrency;
                        FrmUpdateCurrecy.CurrencyLead = CurrencyId;
                        if (Check_Authorization(FormNameId.FrmUpdateCurrecy_Enm, true) == false) {
                            return null;
                        }
                        FrmUpdateCurrecy.CurrencySaveDate = ValueDate;
                        //FrmUpdateCurrecy.Show vbModal



                }
                    else {
                        return FrmUpdateCurrecy;
                    }
                }
            }
            else
            {
                return FrmUpdateCurrecy;
            }
            return FrmUpdateCurrecy;
        }
        public int SaveReceipt(ReseiptsModel RectObj)
        {
            int returnObj = 0;
            // try
            //{
            Dictionary<string, object> ReceiptsParameterDict = new Dictionary<string, object>();
            if (ReceiptsParameterDict != null)
            {
                //if(CustObj.CustomerId)
                int tempcustId = -1;
                string constring = SecurityconString;
                //if (RectObj. < 0)
                //{

                //    CustObj.CustomerId = GetMaxCustId(constring);
                //}
                //else
                //{
                //    tempcustId = CustObj.CustomerId;
                //}
                bool IsValidCC = true;
                //if (string.IsNullOrEmpty(CustObj.CustomerCode) == false)
                //    IsValidCustCode(CustObj.CustomerCode, CustObj.CustomerId, constring);
                if (IsValidCC == true)
                {
                    Dictionary<string, object> ParameterDict = GetReceiptDict(RectObj);
                    using (DbAccess db = new DbAccess(SecurityconString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                    {
                        db.Transaction = db.con.BeginTransaction();
                        string Query = "";
                        if (tempcustId == -1)
                            Query = "insert into Reciepts(RecieptType,RecieptNo,RecieptDate,CustomerId,AddressId,RecievedCustId,WhatFor,CurrencyId," +
                            "TotalInWords,Total,associationId,EmployeeId,ThanksLetter,ThanksLetterId,Credit4Digit," +
                            "PrinterId,OriginalWasPrinted,StateId,CityName,CountryCode,Street,Street2,Zip,fname," +
                            "lname,Titel,MiddleName,Company,Safix,Address_Remark,WhatForInThanksLet,TotalInLeadCurrent,CustomizeLine,ReceiptNoKeva,KeVaHistoryId,digitalEmployeeId,digitalfileName," +
                            "digitalPath,digitalDate,RowDate,isCredit) "+

                            "Values(@RecieptType,@RecieptNo,Convert(datetime,@RecieptDate,103),@CustomerId,@AddressId,@RecievedCustId,@WhatFor,@CurrencyId," +
                            "@TotalInWords,@Total,@associationId,@EmployeeId,@ThanksLetter,@ThanksLetterId,@Credit4Digit,@PrinterId,@OriginalWasPrinted," +
                            "@StateId,@CityName,@CountryCode,@Street,@Street2,@Zip,@fname,@lname,@Titel,@MiddleName,@Company,@Safix,@Address_Remark,@WhatForInThanksLet,@TotalInLeadCurrent,@CustomizeLine,@ReceiptNoKeva,@KeVaHistoryId,@digitalEmployeeId,@digitalfileName,@digitalPath,Convert(datetime,@digitalDate,103),Convert(datetime,@RowDate,103),@IsCredit) ";
                        else
                        {

                            Query = "update Reciepts set RecieptType=@RecieptType,RecieptNo=@RecieptNo,RecieptDate=Convert(datetime,@RecieptDate,103),CustomerId=@CustomerId,AddressId=@AddressId,RecievedCustId=@RecievedCustId,WhatFor=@WhatFor," +
                            "CurrencyId=@CurrencyId, TotalInWords=@TotalInWords,Total=@Total,associationId=@associationId,EmployeeId=@EmployeeId,ThanksLetter=@ThanksLetter,ThanksLetterId=@ThanksLetterId," +
                            "Credit4Digit=@Credit4Digit,PrinterId=@PrinterId," +
                            "OriginalWasPrinted=@OriginalWasPrinted,StateId=@StateId,CityName=@CityName" +
                            //",CountryCode=@CountryCode,Street1=@Street1,Street2=@Street2,Zip=@Zip,fname=@fname,lname=@lname,Titel=@Titel,MiddleName=@MiddleName,Company=@Company,Safix=@Safix,Address_Remark=@Address_Remark,WhatForInThanksLet=@WhatForInThanksLet," +
                            //"TotalInLeadCurrent=@TotalInLeadCurrent,CustomizeLine=@CustomizeLine,ReceiptNoKeva=@ReceiptNoKeva,KeVaHistoryId=@KeVaHistoryId,digitalEmployeeId=@digitalEmployeeId,digitalfileName=@digitalfileName,digitalPath=@digitalPath,digitalDate=@digitalDate,RowDate=@RowDate,IsCredit=@IsCredit
                            " where RecieptType=@RecieptType and RecieptNo=@RecieptNo";
                        }


                        // put insert query here
                        returnObj = db.InsertData(Query, ParameterDict, db.Transaction);
                        
                        foreach (var CustRecpts in RectObj.ReceiptLines)
                        {


                            ReceiptsParameterDict.Clear();
                            if (tempcustId != -1)
                            {

                                ReceiptsParameterDict.Clear();
                                ReceiptsParameterDict.Add("RecieptRoWID", CustRecpts.RecieptRoWID);
                                ReceiptsParameterDict.Add("RecieptType", CustRecpts.RecieptNo);
                                ReceiptsParameterDict.Add("RecieptNo", CustRecpts.RecieptNo);
                                string DelAddressQuery = "delete from RecieptLine where RecieptRoWID=@RecieptRoWID and RecieptType=@RecieptType and RecieptNo=@RecieptNo";
                                returnObj += db.InsertData(DelAddressQuery, ReceiptsParameterDict, db.Transaction);
                            }
                            ReceiptsParameterDict.Clear();
                            ReceiptsParameterDict = GetReceiptLinesDict(CustRecpts);
                            string AddressQuery = "insert into RecieptLine(RecieptRoWID,RecieptType,RecieptNo,ProjectId,PayTypeId,Amount,USDVal,ValueDate," +
                            "CheckNo,BranchNo,AccountNo,details,DonationTypeId,ImageName,AccountId," +
                            "CameFrom,Bank,AmountInLeadCurrent,ReferenceDate,OldReceiptId,Payed,For_Invoice,IsExport,WasDeposit," +
                            "DepositeNo,DepositeDate,DepositeToAccountId,DepositeRemark,TotalDeposit,KevaInstitute,CreditCardType,RowDate,BankId) " +

                            "Values(@RecieptRoWID,@RecieptType,@RecieptNo,@ProjectId,@PayTypeId,@Amount,@USDVal,Convert(datetime,@ValueDate,103)," +
                            "@CheckNo,@BranchNo,@AccountNo,@details,@DonationTypeId,@ImageName,@AccountId,@CameFrom,@Bank," +
                            "@AmountInLeadCurrent,Convert(datetime,@ReferenceDate,103),@OldReceiptId,@Payed,@For_Invoice,@IsExport,@WasDeposit,@DepositeNo,Convert(datetime,@DepositeDate,103),@DepositeToAccountId,@DepositeRemark,@TotalDeposit,@KevaInstitute,@CreditCardType,Convert(datetime,@RowDate,103),@BankId) ";
                            returnObj += db.InsertData(AddressQuery, ReceiptsParameterDict, db.Transaction);
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

    }
}
