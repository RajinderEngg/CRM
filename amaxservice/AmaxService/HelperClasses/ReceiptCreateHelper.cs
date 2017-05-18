using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AmaxService.HelperClasses
{
    public class ReceiptCreateHelper
    {
        public string SecurityconString { get; set; }
        public string lang { get; set; }
        public DataBind DataB;
        public ReceiptsProductsHelper RecProdHP;
        private object serializer;

        public string LoggedEmpId { get; set; }
        public ReceiptCreateHelper()
        {
            RecProdHP = new ReceiptsProductsHelper();
            DataB = new DataBind();
        }
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


        public List<ReseiptsModel> GetReceiptListFromDS(DataSet custobj)
        {
            List<ReseiptsModel> FinalDictList = new List<ReseiptsModel>();

            // try
            // {
            for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
            {
                ReseiptsModel CustDictList = new ReseiptsModel();
                CustDictList.RecieptType = Convert.ToInt32(custobj.Tables[0].Rows[i]["RecieptType"]);
                CustDictList.RecieptNo = Convert.ToString(custobj.Tables[0].Rows[i]["RecieptNo"]);
                CustDictList.RecieptDate =Convert.ToDateTime( Convert.ToString(custobj.Tables[0].Rows[i]["RecieptDate"])).ToString("dd-MM-yyyy");
                CustDictList.CustomerId = Convert.ToInt32(custobj.Tables[0].Rows[i]["CustomerId"]);
                CustDictList.AddressId = Convert.ToInt32(custobj.Tables[0].Rows[i]["AddressId"]);
                CustDictList.RecievedCustId = Convert.ToInt32(custobj.Tables[0].Rows[i]["RecievedCustId"]);
                CustDictList.WhatFor = Convert.ToString(custobj.Tables[0].Rows[i]["WhatFor"]);
                CustDictList.CurrencyId = Convert.ToString(custobj.Tables[0].Rows[i]["CurrencyId"]);

                CustDictList.TotalInWords = Convert.ToString(custobj.Tables[0].Rows[i]["TotalInWords"]);
                CustDictList.Total = Convert.ToDecimal(custobj.Tables[0].Rows[i]["Total"]);
                CustDictList.associationId = Convert.ToInt32(custobj.Tables[0].Rows[i]["associationId"]);
                CustDictList.EmployeeId = Convert.ToInt32(custobj.Tables[0].Rows[i]["EmployeeId"]);
                CustDictList.ThanksLetter = Convert.ToBoolean(custobj.Tables[0].Rows[i]["ThanksLetter"]);
                CustDictList.ThanksLetterId = Convert.ToInt32(custobj.Tables[0].Rows[i]["ThanksLetterId"]);
                CustDictList.Credit4Digit = Convert.ToString(custobj.Tables[0].Rows[i]["Credit4Digit"]);
                CustDictList.PrinterId = Convert.ToInt32(custobj.Tables[0].Rows[i]["PrinterId"]);

                
                CustDictList.OriginalWasPrinted = Convert.ToBoolean(custobj.Tables[0].Rows[i]["OriginalWasPrinted"]);
                CustDictList.StateId = Convert.ToString(custobj.Tables[0].Rows[i]["StateId"]);
                CustDictList.CityName = Convert.ToString(custobj.Tables[0].Rows[i]["CityName"]);
                CustDictList.CountryCode = Convert.ToString(custobj.Tables[0].Rows[i]["CountryCode"]);



                CustDictList.Street = Convert.ToString(custobj.Tables[0].Rows[i]["Street"]);
                CustDictList.Street2 = Convert.ToString(custobj.Tables[0].Rows[i]["Street2"]);
                CustDictList.Zip = Convert.ToString(custobj.Tables[0].Rows[i]["Zip"]);
                CustDictList.fname = Convert.ToString(custobj.Tables[0].Rows[i]["fname"]);
                CustDictList.lname = Convert.ToString(custobj.Tables[0].Rows[i]["lname"]);
                CustDictList.Titel = Convert.ToString(custobj.Tables[0].Rows[i]["Titel"]);
                CustDictList.MiddleName = Convert.ToString(custobj.Tables[0].Rows[i]["MiddleName"]);
                CustDictList.Company = Convert.ToString(custobj.Tables[0].Rows[i]["Company"]);

                CustDictList.Safix = Convert.ToString(custobj.Tables[0].Rows[i]["Safix"]);
                CustDictList.Address_Remark = Convert.ToString(custobj.Tables[0].Rows[i]["Address_Remark"]);
                CustDictList.WhatForInThanksLet = Convert.ToString(custobj.Tables[0].Rows[i]["WhatForInThanksLet"]);
                CustDictList.TotalInLeadCurrent = Convert.ToDecimal(custobj.Tables[0].Rows[i]["TotalInLeadCurrent"]);
                CustDictList.CustomizeLine = Convert.ToString(custobj.Tables[0].Rows[i]["CustomizeLine"]);
                CustDictList.ReceiptNoKeva = Convert.ToString(custobj.Tables[0].Rows[i]["ReceiptNoKeva"]);
                CustDictList.ReceiptTypeKeva = Convert.ToInt32(custobj.Tables[0].Rows[i]["ReceiptTypeKeva"]);
                CustDictList.KeVaHistoryId = Convert.ToInt32(custobj.Tables[0].Rows[i]["KeVaHistoryId"]);


                CustDictList.digitalEmployeeId = Convert.ToInt32(custobj.Tables[0].Rows[i]["digitalEmployeeId"]);
                CustDictList.digitalfileName = Convert.ToString(custobj.Tables[0].Rows[i]["digitalfileName"]);
                CustDictList.digitalPath = Convert.ToString(custobj.Tables[0].Rows[i]["digitalPath"]);
                CustDictList.digitalDate = Convert.ToString(custobj.Tables[0].Rows[i]["digitalDate"]);
                CustDictList.RowDate =Convert.ToDateTime( Convert.ToString(custobj.Tables[0].Rows[i]["RowDate"])).ToString("dd-MM-yyyy");
                CustDictList.isCredit = Convert.ToInt32(custobj.Tables[0].Rows[i]["isCredit"]);
                FinalDictList.Add(CustDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
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
                            "@TotalInWords,(@Total*-1),@associationId,@EmployeeId,@ThanksLetter,@ThanksLetterId,@Credit4Digit,@PrinterId,@OriginalWasPrinted," +
                            "@StateId,@CityName,@CountryCode,@Street,@Street2,@Zip,@fname,@lname,@Titel,@MiddleName,@Company,@Safix,@Address_Remark,@WhatForInThanksLet,(@TotalInLeadCurrent*-1),@CustomizeLine,@ReceiptNoKeva,@KeVaHistoryId,@digitalEmployeeId,@digitalfileName,@digitalPath,Convert(datetime,@digitalDate,103),Convert(datetime,@RowDate,103),@IsCredit) ";
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
                                ReceiptsParameterDict.Add("RecieptType", CustRecpts.RecieptType);
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

                            "Values(@RecieptRoWID,@RecieptType,@RecieptNo,@ProjectId,@PayTypeId,(@Amount*-1),@USDVal,Convert(datetime,@ValueDate,103)," +
                            "@CheckNo,@BranchNo,@AccountNo,@details,@DonationTypeId,@ImageName,@AccountId,@CameFrom,@Bank," +
                            "(@AmountInLeadCurrent*-1),Convert(datetime,@ReferenceDate,103),@OldReceiptId,@Payed,@For_Invoice,@IsExport,@WasDeposit,@DepositeNo,Convert(datetime,@DepositeDate,103),@DepositeToAccountId,@DepositeRemark,@TotalDeposit,@KevaInstitute,@CreditCardType,Convert(datetime,@RowDate,103),@BankId) ";
                            returnObj += db.InsertData(AddressQuery, ReceiptsParameterDict, db.Transaction);
                        }
                        foreach (var RecptsProds in RectObj.ReceiptProducts)
                        {


                            ReceiptsParameterDict.Clear();
                            if (tempcustId != -1)
                            {
                                ReceiptsParameterDict.Clear();
                                ReceiptsParameterDict.Add("ReceiptType", RecptsProds.ReceiptType);
                                ReceiptsParameterDict.Add("ReceiptNo", RecptsProds.ReceiptNo);
                                string DelAddressQuery = "delete from [ReceiptsProducts] where ReceiptType=@ReceiptType and ReceiptNo=@ReceiptNo";
                                returnObj += db.InsertData(DelAddressQuery, ReceiptsParameterDict, db.Transaction);
                            }
                            ReceiptsParameterDict.Clear();
                            ReceiptsParameterDict = RecProdHP.GetReceiptsProductsDict(RecptsProds);
                            string AddressQuery = "insert into ReceiptsProducts([ReceiptType],[ReceiptNo],[ProductNo],[ProductName],[Price],[Qty],[Total],[RawDate]) " +
                            "Values(@ReceiptType,@ReceiptNo,@ProductNo,@ProductName,@Price,@Qty,@Total,Convert(datetime,@RawDate,103))";
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

        public string QuoteSQLstr(string s) {
            string QuoteSQLstr = s.Trim();
            if (!(string.IsNullOrEmpty(QuoteSQLstr)) && QuoteSQLstr.Length > 0)
            {
                QuoteSQLstr = QuoteSQLstr.Replace("'", "`");
                QuoteSQLstr = QuoteSQLstr.Replace("\"\"", "``");
            }
            return QuoteSQLstr;
        }

        public bool Check_Authorization(long Frmid, bool SHOWMSG = true)
        {
            //Dim LoginAuth As New ADODB.Recordset'úîéã ìäùúîù á DBO'åìà äåà éöåø àåáéé÷èéí æäéí àáì òáåø îùúîù àçø   On Error GoTo Check_Authorization_ErrorCheck_Authorization = False;
            bool Check_Authorization = false;
            DataB = new DataBind();
            DataB.SecurityConString = SecurityconString;
            string EmpId = Frmid.ToString();
            KeyPair EmployeesDet = DataB.GetEmployees().Where(r => r.Value == EmpId).FirstOrDefault();
            KeyPair LoggedEmpDet = DataB.GetEmployees().Where(r => r.Value == LoggedEmpId).FirstOrDefault();
            string  LogstrSql = "Select * from Employees Where EmpId=" + LoggedEmpId;
            string strSql = "";
            //strSql = "Select Rate from CurrenyRates Where (DATEDIFF(d, RateDate, CONVERT(datetime, '" + System.DateTime.Now.ToShortDateString() + "', 103)) = 0 And ToCurrency='" + StrLeadCurrency + "' And CurrencyId='" + CurrencyId + "') ";
            strSql = "Select * from FrmSecurityLevel Where FormId=" + Frmid;

            using (DbAccess db = new DbAccess(SecurityconString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds1 = db.GetDataSet(LogstrSql, null, false);

                DataSet ds = db.GetDataSet(strSql, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToInt32( ds.Tables[0].Rows[0]["authoType"]) >= Convert.ToInt32(ds.Tables[0].Rows[0]["levelNo"]))
                    {
                        Check_Authorization = true;
                    }

                }


                if (EmployeesDet != null)
                {  //ChecAutorForEmployee(Frmid)
                    Check_Authorization = true; //'ok! can view 
                    return false;
                }
                if (Check_Authorization == false)
                {
                    if (SHOWMSG)
                    {
                        //msgshow
                    }
                    //Then If SHOWMSG Then     MsgBox GetString(AppDictionay.No_Entry), vbCritical End IfEnd If
                }

                //Dim strSql As StringstrSql = "Select * from FrmSecurityLevel Where FormId=" & Frmid
                //objMainDB.ExecSQL strSql 'äøõ çéôåùSet LoginAuth = objMainDB.RecordsetIf objMainDB.HasRecords Then 'Username & Password correct  If objemployeeLogin.authoType >= LoginAuth("levelNo") Then Check_Authorization = True  End IfEnd IfLoginAuth.CloseSet LoginAuth = NothingIf ChecAutorForEmployee(Frmid) Then Check_Authorization = True 'ok! can view   Exit FunctionEnd IfIf Check_Authorization = False Then If SHOWMSG Then     MsgBox GetString(AppDictionay.No_Entry), vbCritical End IfEnd If   On Error GoTo 0   Exit FunctionCheck_Authorization_Error:    MsgBox "Error " & err.number & " (" & err.Description & ") in procedure Check_Authorization of Module MdlSm"End Function
            }
            return Check_Authorization;
        }

        public decimal getCurrenyXMLRates(string Path,string OnDate,string CURRENCYCODE) {

            decimal CurrentRate = 1;
            string strSql = "";
            //strSql = "Select Rate from CurrenyRates Where (DATEDIFF(d, RateDate, CONVERT(datetime, '" + System.DateTime.Now.ToShortDateString() + "', 103)) = 0 And ToCurrency='" + StrLeadCurrency + "' And CurrencyId='" + CurrencyId + "') ";
            strSql = "Select count(*) from CurrenyRates Where (DATEDIFF(d, RateDate, CONVERT(datetime, '" + OnDate + "', 103)) = 0 )";

            using (DbAccess db = new DbAccess(SecurityconString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                object rCount = db.ExecuteScalarAsString(strSql, null);
                if (Convert.ToInt32( rCount) == 0)
                {
                    string returnObj = "";
                   // CURRENCIES CurrObj=new CURRENCIES();
                     CURRENCIES Currencies = new CURRENCIES();
                    WebRequest req = HttpWebRequest.Create(Path);
                    req.Method = "GET";
                    using ( StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        returnObj = reader.ReadToEnd();
                        string[] ret = null;
                        XmlSerializer serializer = new XmlSerializer(typeof(CURRENCIES));
                        TextReader reader1 = new StringReader(returnObj);
                        ////Currencies.LAST_UPDATE = "2017-07-08";
                        //CURRENCY curr = new CURRENCY();
                        //curr.NAME = "dd";
                        //Currencies = new CURRENCY[2] ;
                        //Currencies[0]=curr;
                        //CURRENCY curr1 = new CURRENCY();
                        //curr1.NAME = "dd";
                        ////Currencies[1]=curr1;
                        Currencies = (CURRENCIES)serializer.Deserialize(reader1);
                        //XmlWriterSettings settings = new XmlWriterSettings();
                        //settings.OmitXmlDeclaration = true;
                        //using (var ms = new MemoryStream())
                        //using (var writer = XmlWriter.Create(ms,settings))
                        //{
                        //    serializer = new XmlSerializer(typeof(CURRENCY[]));
                        //    serializer.Serialize(writer, Currencies);
                        //    string str=Encoding.UTF8.GetString(ms.ToArray());
                        //}
                        reader.Close();

                    }
                    int g = 0;

                    if (Currencies !=null)
                    {
                        //// Insert all data
                        db.Transaction = db.con.BeginTransaction();
                        foreach (var currency in Currencies.CURRENCY)
                        {
                            string instquery = "insert into CurrenyRates(RateDate,CurrencyId,Rate,ToCurrency) values(Convert(datetime,'" + OnDate + "',103),'" + currency.CURRENCYCODE + "'," + currency.RATE + ",'NIS')";
                            g += db.InsertData(instquery, null, db.Transaction);
                        }
                        db.Transaction.Commit();


                        var getcurr = Currencies.CURRENCY.Where(f => f.CURRENCYCODE == CURRENCYCODE).FirstOrDefault();
                        CurrentRate = Convert.ToDecimal(getcurr.RATE);
                    }
                }
            }
            return CurrentRate;


        }
        //Sent on:ThuFrom:Shlomo Matzliach also this if needed    public double toNumber(string srcCurrency, bool RetZeroIfNegative = false) { double toNumber; if (srcCurrency == "") toNumber = 0; else { if (srcCurrency.Contains(",")) { srcCurrency = srcCurrency.Replace(",", ""); } toNumber = Convert.ToDouble(srcCurrency); if (RetZeroIfNegative) { if (toNumber < 1) { return 0; } } } return toNumber; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrencyId">From Currency-USD</param>
        /// <param name="ToCurrency">to Currency </param>
        /// <param name="ValueDate">On date</param>
        /// <returns></returns>
        public decimal CheckLeadcurrency(string CurrencyId, string ToCurrency, string ValueDate)
        {
            CurrencyModel FrmUpdateCurrecy = new CurrencyModel();
            bool CheckLeadcurrency = false;
            decimal LeadRAte = 1;



            if (ToCurrency != CurrencyId)
            {
                //'îèáò îåáéì ùåðä îîèáò ðáçø';

                string strSql = "";
                //strSql = "Select Rate from CurrenyRates Where (DATEDIFF(d, RateDate, CONVERT(datetime, '" + System.DateTime.Now.ToShortDateString() + "', 103)) = 0 And ToCurrency='" + StrLeadCurrency + "' And CurrencyId='" + CurrencyId + "') ";
                strSql = "Select Rate from CurrenyRates Where (DATEDIFF(d, RateDate, CONVERT(datetime, '" + ValueDate + "', 103)) = 0 "+
                    " And ToCurrency='" + ToCurrency + "' And CurrencyId='" + CurrencyId + "') ";

                using (DbAccess db = new DbAccess(SecurityconString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    DataSet ds = db.GetDataSet(strSql, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        LeadRAte = Convert.ToDecimal(ds.Tables[0].Rows[0]["Rate"]);
                    }
                    
                    if (LeadRAte == 0 || ds.Tables[0].Rows.Count == 0)
                    {

                      LeadRAte =  getCurrenyXMLRates("http://www.boi.org.il/currency.xml", ValueDate, CurrencyId);

                    }
                    else {
                        return LeadRAte;
                    }
                }
            }
            else
            {
                return LeadRAte;
            }
            return LeadRAte;
        }

        public List<ReseiptsModel> GetReceiptsByCustId(int CustomerId)
        {


            List<ReseiptsModel> returnObj = new List<ReseiptsModel>(); 
            
            string strSql = "";
            
            strSql = "Select * from Reciepts Where CustomerId=" + CustomerId+
                " order by Recieptdate desc";

            using (DbAccess db = new DbAccess(SecurityconString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(strSql, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnObj = GetReceiptListFromDS(ds);
                    foreach (var returnobj in returnObj)
                    {
                        //if (returnobj.RecieptType != null)
                        //{
                            strSql = "Select RecieptTypeId,RecieptName,RecieptNameEng from RecieptTypes Where RecieptTypeId=" + returnobj.RecieptType;
                            DataSet ds1 = db.GetDataSet(strSql, null, false);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (lang == "en")
                                {
                                    returnobj.ReceiptTypeName = Convert.ToString(ds1.Tables[0].Rows[0]["RecieptNameEng"]);
                                }
                                else
                                {
                                    returnobj.ReceiptTypeName = Convert.ToString(ds1.Tables[0].Rows[0]["RecieptName"]);
                                }
                            }
                        //}
                        
                    }
                }

            }
           
            return returnObj;
        }
    }
}
