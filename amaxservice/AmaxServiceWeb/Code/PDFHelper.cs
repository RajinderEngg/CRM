using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AmaxServiceWeb.Code
{
    public class PDFHelper
    {
        public string SecurityConString { get; set; }
        public string LangValue { get; set; }
        public string Empid { get; set; }
        DbAccess db;
        public PDFHelper()
        {

        }
        public string PrintPdf(int prpCustomerId,string txtEmail, string OrganizationId, int ThankLetterId, string prpRecieptCode, string prpRecieptType,bool forprint,string StrLeadCurrency)
        {
            db = new DbAccess(SecurityConString);
            DbAccess db1=new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString());
            DbAccess dbSE = new DbAccess(ConfigurationManager.ConnectionStrings["SendEmail"].ConnectionString.ToString());
            //forprint = false;
            //StrLeadCurrency = "NIS";
            long index = 0;
            long ocustomerid = 0;
            // ADODB.Recordset tmpRs = default(ADODB.Recordset);
            string strSql = null;
            //ADODB.Recordset rsPays = default(ADODB.Recordset);
            double od = 0;
            string strhtml = null;
            //ADODB.Recordset rst = default(ADODB.Recordset);
            string tmpHTML = null;
            string tmp = null;
            string osub = null;
            string s_output = null;
            string oemail = null;
            string TextLog = null;
            string FromMail = null;
            string ReplytoName = null;
            long oCustomerIdtmp = 0;
            long I = 0;
            double orgid = 0;
            long oSendID = 0;
            long oid = 0;
            string Res = null;
            string CompanyTitle = null;
            string Src = null;
            string href = null;
            int rtl_lang = 0;
            long ThanksLetterId = 0;
            string th1 = null;
            string th2 = null;
            string th3 = null;
            string th4 = null;
            string th5 = null;
            string th6 = null;
            string th7 = null;
            string th8 = null;
            string th9 = null;
            string oguid = null;
            string link1 = null;
            string MailBody = null;
            string MailSubj = null;
            string CompanyTitleEn = null;
            int bb = 0;
            double ColsForSendEmail = 0;
            string ReceiptName = null;
            string checkIsAacount = null;
            var Client = new PdfClient.PrintPdfReceiptSoapClient();
            string retSt = null;
            string Address = "";
            string OrganizationName = "";
            string orgname = "";
            DataTable dTable = new DataTable();
            //Guid gid1 =Guid.NewGuid();
           // oguid = gid1.ToString();
            if (forprint == true) {
                //If ComboThanksLetter.ListIndex = -1 Then
                // MsgBox GetString(AppDictionay.Select_) & " " & GetString(AppDictionay.Template)
                // ComboThanksLetter.SetFocus
                // Exit Sub
                //End If
                OrganizationName = OrganizationId;//db1.ExecuteScalarAsString("select OrganizationName from Organization where OrganizationId='" + OrganizationId+"'",null).ToString();
                orgname = db1.ExecuteScalarAsString("select OrganizationName from Organization where OrganizationId='" + OrganizationId + "'", null).ToString();
                dTable = db.GetDataTable(" select * from RecieptThanksLetters where ThanksLetterId = " + ThankLetterId + " ", null, false);
                if (dTable.Rows.Count > 0)
                {
                    MailBody = Convert.ToString(dTable.Rows[0]["MailBody"]);
                    MailSubj = Convert.ToString(dTable.Rows[0]["MailSubj"]);
                    rtl_lang = Convert.ToInt32(dTable.Rows[0]["isrtl"]);
                    ThanksLetterId = Convert.ToInt32(dTable.Rows[0]["ThanksLetterId"]);

                }
                else {
                    db.Dispose();
                    return "תבנית חסרה";
                    //Exit Sub
                }
                retSt = Client.IsFileExists(OrganizationName, ThanksLetterId.ToString() + ".html");
                if (retSt != "YES")
                {
                    return "התבנית חסרה בשרת";
                    //Exit Sub
                }

            }


            txtEmail = "2576091@gmail.com";

            if (ThanksLetterId == -1) { //Then
                                        //MsgBox GetString(AppDictionay.Select_) & " " & GetString(AppDictionay.Template)

                //ComboThanksLetter.SetFocus

                //Exit Sub
                return "Please select Print Template";
            }
            if (forprint == false) { //Then 'áãé÷ä áîöá ùìéçä ìàéîééì
                if(txtEmail.Length == 0)
                {

                    return "EmailId not find";
                }
                //Then MsgBox "ìà ðøùí àéîééì ": txtEmail.SetFocus: Exit Sub
                //bb = IsValidEmail(txtEmail);
                //if (bb == 0) { //Then
                //    //If MsgBox("äàéîééì àéðå çå÷é äàí ìäîùéê áëì æàú ?", vbYesNo) = vbNo Then Exit Sub
                //}


                //If Len(oarray(0)) < 3 Then
                //  If MsgBox("úøöä ìäëðéñ àéîééì æä ìëøèéñ äì÷åç ?", vbYesNo) = vbYes Then

                //      strSql = "insert into CustomerEmails   (CustomerId,Email, EmailName, Newslettere,  publish) values (" & objCustomer.Customerid & " ,'" & Trim(txtEmail.Text) & "','" & QuoteSQLstr(objCustomer.FileAs) & "',0,1)"


                //       If objMainDB.ExecSQL(strSql) = False Then Exit Sub
                //  End If

                //if (Strings.Len(oarray(0)) < 3)
                //{
                //    if (Interaction.MsgBox("úøöä ìäëðéñ àéîééì æä ìëøèéñ äì÷åç ?", Constants.vbYesNo) == Constants.vbYes)
                //    {

                //        strSql = "insert into CustomerEmails   (CustomerId,Email, EmailName, Newslettere,  publish) values (" + objCustomer.Customerid + " ,'" + Strings.Trim(txtEmail.Text) + "','" + QuoteSQLstr(objCustomer.FileAs) + "',0,1)";

                //        if (objMainDB.ExecSQL(strSql) == false)
                //            return;
                //    }
                //}

            }
            if (forprint == false) {//Then

                orgname = db1.ExecuteScalarAsString("select OrganizationName from Organization where OrganizationId='" + OrganizationId + "'", null).ToString();
                DataTable dTable1 = dbSE.GetDataTable(" select top 1 * from Organization where OrganizationName like '%"+ orgname + "%' and OrgCanSendReciept=1",null);
                if (dTable1.Rows.Count > 0)
                {
                    ReplytoName = Convert.ToString(dTable1.Rows[0]["CompanyTitle"]);
                    orgid = Convert.ToDouble(dTable1.Rows[0]["OrganizationId"]);// objSendEmailDB.Recordset("OrganizationId")
                    FromMail = Convert.ToString(dTable1.Rows[0]["MailFrom"]);// objSendEmailDB.Recordset("MailFrom")
                    CompanyTitle = Convert.ToString(dTable1.Rows[0]["CompanyTitle"]);// objSendEmailDB.Recordset("CompanyTitle")
                    Src = Convert.ToString(dTable1.Rows[0]["src"]);// objSendEmailDB.Recordset("src")
                    href = Convert.ToString(dTable1.Rows[0]["href"]);// objSendEmailDB.Recordset("href")
                    CompanyTitleEn = Convert.ToString(dTable1.Rows[0]["CompanyTitleEn"]);// objSendEmailDB.Recordset("CompanyTitleEn")
                    ColsForSendEmail = Convert.ToDouble(dTable1.Rows[0]["ColsForSendEmail"]);// objSendEmailDB.Recordset("ColsForSendEmail")
                }
                else {
                    db1.Dispose();
                    dbSE.Dispose();
                    return "äàøâåï ìà ðøùí ìäãôñä àå ùìéçú ÷áìåú";
         //Exit Sub
        }


            }
            else {
                orgname = db1.ExecuteScalarAsString("select OrganizationName from Organization where OrganizationId='" + OrganizationId + "'", null).ToString();
                DataTable dTable1 = dbSE.GetDataTable(" select top 1 * from Organization where OrganizationName like '%" + orgname + "%'", null);
                if (dTable1.Rows.Count > 0)
                {
                    ReplytoName = Convert.ToString(dTable.Rows[0]["CompanyTitle"]);
                    orgid = Convert.ToDouble(dTable.Rows[0]["OrganizationId"]);// objSendEmailDB.Recordset("OrganizationId")
                    FromMail = Convert.ToString(dTable.Rows[0]["MailFrom"]);// objSendEmailDB.Recordset("MailFrom")
                    CompanyTitle = Convert.ToString(dTable.Rows[0]["CompanyTitle"]);// objSendEmailDB.Recordset("CompanyTitle")
                    Src = Convert.ToString(dTable.Rows[0]["src"]);// objSendEmailDB.Recordset("src")
                    href = Convert.ToString(dTable.Rows[0]["href"]);// objSendEmailDB.Recordset("href")
                    CompanyTitleEn = Convert.ToString(dTable.Rows[0]["CompanyTitleEn"]);// objSendEmailDB.Recordset("CompanyTitleEn")
                    ColsForSendEmail = Convert.ToDouble(dTable.Rows[0]["ColsForSendEmail"]);// objSendEmailDB.Recordset("ColsForSendEmail")
                }
                else {
                    db1.Dispose(); 
                    return "äàøâåï ìà ðøùí ìäãôñä àå ùìéçú ÷áìåú";
                    //Exit Sub
                }
                //bb = objSendEmailDB.ExecSQL(" select top 1 * from Organization where OrganizationName like '%" & Trim(OrganizationNameFull) & "%' ");
            }

            dTable = db.GetDataTable(" select * from RecieptThanksLetters where ThanksLetterId = " + ThankLetterId + " ", null, false);
            if (dTable.Rows.Count > 0)
            {
                MailBody = Convert.ToString(dTable.Rows[0]["MailBody"]);
                MailSubj = Convert.ToString(dTable.Rows[0]["MailSubj"]);
                rtl_lang = Convert.ToInt32(dTable.Rows[0]["isrtl"]);
                ThanksLetterId = Convert.ToInt32(dTable.Rows[0]["ThanksLetterId"]);

            }
            else {
                db.Dispose();
                return "תבנית חסרה";
                //Exit Sub
            }


            if (rtl_lang == 1)
            {
                th1 = "תאריך ערך";
                th2 = "פרוייקט";
                th3 = "סוג תשלום";
                th4 = "סכום";
                th5 = "מס צק";
                th6 = "סניף";
                th7 = "מס חשבון";
                th8 = "בנק";
                th9 = "מטרה";
            }
            else {
                th1 = "ValueDate";
                th2 = "Project";
                th3 = "payment Type";
                th4 = "Amount";
                th5 = "CheckNo";
                th6 = "Branch";
                th7 = "AccountNo";
                th8 = "Bank";
                th9 = "DonationType";
            }


            if (OrganizationName.ToUpper() == "CHABADEDUMIM")
            {
                osub = " קבלה מס " + prpRecieptCode + " ";// + TxtReciept(9).Text;
            }
            else {
                //osub = Strings.Left(QuoteSQLstr(MailSubj), 50);
                if (MailSubj.Length > 50)
                {
                    osub = MailSubj.Substring(0, 50);
                }
                else {

                    osub = MailSubj;
                }
            }
            //isSend,
            string strSqlH = " SELECT distinct top 500000  ProjectName,JI, RecieptRoWID,For_Invoice,OriginalWasPrinted,payed,ActiveStatus," +
                  "CustomerId,FileAs,RecieptName,RecieptNo,RecieptDate,WhatFor,RecieptType,RecieptsEmployeeId,AmountInLeadCurrent as Total," +
                  " LeadCurrency as CurrencyId,Total as Total1,CurrencyId as CurrencyId1,ValueDate,CheckNo,BranchNo,AccountNo,DonationTypeOther as DonationType," +
                  "AccountName,PaymentTypeOther as PaymentType,HASHAVSHEVETPR,HASHAVSHEVETAC,HASHKUPA,HashHeshZchut,Amount,IsExport,Camefrom,details,creditcardtype," +
                  "TotalInLeadCurrent,Street,Street2,Zip From ND_Receipts_View where RecieptNo ='" +
                  prpRecieptCode + "'  And RecieptType = '" + prpRecieptType + "' order by  RecieptDate";

            dTable = db.GetDataTable(strSqlH, null);
            if (dTable.Rows.Count > 0)
            {
                //   DataRow rsPays = dTable.Rows[0];
                Address = Convert.ToString(dTable.Rows[0]["Street"])+" " + Convert.ToString(dTable.Rows[0]["Street2"]) +" " + Convert.ToString(dTable.Rows[0]["Zip"]); ;
                checkIsAacount = checkIsAacount + Convert.ToString(dTable.Rows[0]["BranchNo"]) + Convert.ToString(dTable.Rows[0]["AccountNo"]) + Convert.ToString(dTable.Rows[0]["CheckNo"]);
            }
            else {
                db.Dispose();
                return "עליך לשמור את הקבלה  ";
            }



            ocustomerid = prpCustomerId;



            ////////////Send tablecode/////////////////


            if (forprint == true)
            {
                strSql = "INSERT INTO Send  (OrganizationId, Subject, BodyContent,   isTry,FromMail,ReplytoMail,ReplytoName,employeeid,isOpen)";
                strSql = strSql + " SELECT    " + orgid + ", '" + osub + "', '" + QuoteSQLstr(MailBody) + "',   0 ,'" + FromMail + "', '" + FromMail + "', '" + ReplytoName + "', " + Empid + ",1";
            }
            else {
                strSql = "INSERT INTO Send  (OrganizationId, Subject, BodyContent,   isTry,FromMail,ReplytoMail,ReplytoName,employeeid)";
                strSql = strSql + " SELECT    " + orgid + ", '" + osub + "', '" + QuoteSQLstr(MailBody) + "',   0 ,'" + FromMail + "', '" + FromMail + "', '" + ReplytoName + "', " + Empid + "";
            }
            int g= dbSE.InsertData(strSql, null);
            if (g == 0)
            {
                db.Dispose();
                return "There is problem in inserting data in send table";
            }
            DataTable dTablesend = dbSE.GetDataTable(" select top 1 SendID from Send order by SendID desc", null);
            if (dTablesend.Rows.Count == 0)
            {
                return "No record found from Send table";
            }
           


            I = 1;
            s_output = "";


            if (checkIsAacount.Length > 4)
            {
                if (rtl_lang == 1)
                {
                    if (ColsForSendEmail == 3)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th3 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th5 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th7 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th6 + "</th><th class=\"payrow_header\" style=\"width:280px;text-align:right\" > " + th9 + "</th><th  class=\"payrow_header\" align=\"center\"  > " + th4 + "</th></tr>";
                    }
                    else if (ColsForSendEmail == 2)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th3 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th5 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th7 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th6 + "</th><th class=\"payrow_header\" style=\"width:280px;text-align:right\" > " + th9 + "</th><th  class=\"payrow_header\" align=\"center\"  > " + th4 + "</th></tr>";
                    }
                    else if (ColsForSendEmail == 1)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th3 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th5 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th7 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th6 + "</th><th  class=\"payrow_header\" align=\"center\"  > " + th4 + "</th></tr>";
                    }
                }
                else {
                    if (ColsForSendEmail == 3)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th3 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th5 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th7 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th6 + "</th><th class=\"payrow_header\" style=\"width:280px;text-align:left\" > " + th9 + "</th><th  class=\"payrow_header\" align=\"center\"  > " + th4 + "</th></tr>";
                    }
                    else if (ColsForSendEmail == 2)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th3 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th5 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th7 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th6 + "</th><th class=\"payrow_header\" style=\"width:280px;text-align:left\" > " + th9 + "</th><th  class=\"payrow_header\" align=\"center\"  > " + th4 + "</th></tr>";
                    }
                    else if (ColsForSendEmail == 1)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th3 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th5 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th7 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th6 + "</th><th  class=\"payrow_header\" align=\"center\"  > " + th4 + "</th></tr>";
                    }
                }

            }
            else {
                if (rtl_lang == 1)
                {
                    if (ColsForSendEmail == 3)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th2 + "</th><th class=\"payrow_header\" style=\"width:80px;text-align:center\" > " + th3 + "</th><th class=\"payrow_header\" align=\"center\" > " + th4 + "</th align=\"center\" ></tr>";
                    }
                    else if (ColsForSendEmail == 2)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:280px;text-align:center\"> " + th9 + "</th><th class=\"payrow_header\" style=\"width:80px;text-align:center\" > " + th3 + "</th><th class=\"payrow_header\" align=\"center\" > " + th4 + "</th align=\"center\" ></tr>";
                    }
                    else if (ColsForSendEmail == 1)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\" > " + th3 + "</th><th class=\"payrow_header\" align=\"center\" > " + th4 + "</th align=\"center\" ></tr>";
                    }
                }
                else {
                    if (ColsForSendEmail == 3)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th2 + "</th><th class=\"payrow_header\" style=\"width:80px;text-align:center\" > " + th3 + "</th><th class=\"payrow_header\" align=\"center\" > " + th4 + "</th align=\"center\" ></tr>";
                    }
                    else if (ColsForSendEmail == 2)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:280px;text-align:center\"> " + th9 + "</th><th class=\"payrow_header\" style=\"width:80px;text-align:center\" > " + th3 + "</th><th class=\"payrow_header\" align=\"center\" > " + th4 + "</th align=\"center\" ></tr>";
                    }
                    else if (ColsForSendEmail == 1)
                    {
                        s_output = "<tr><th class=\"payrow_header\" style=\"width:80px;text-align:center\"> " + th1 + " </th><th class=\"payrow_header\" style=\"width:80px;text-align:center\" > " + th3 + "</th><th class=\"payrow_header\" align=\"center\" > " + th4 + "</th align=\"center\" ></tr>";
                    }
                }


            }

            od = 0;

            foreach (DataRow rseachPays in dTable.Rows)
            {


                if (checkIsAacount.Length > 4)
                {
                    s_output = s_output + "<tr>";
                    s_output = s_output + "<td class=\"payrow_line\"  align=center > " + rseachPays["PaymentType"] + "</td>";
                    s_output = s_output + "<td class=\"payrow_line\" align=center>" + Convert.ToString(rseachPays["ValueDate"]) + "</td>";
                    s_output = s_output + "<td class=\"payrow_line\" align=center > " + rseachPays["CheckNo"] + "</td>";
                    s_output = s_output + "<td class=\"payrow_line\" align=center > " + rseachPays["AccountNo"] + "</td>";
                    s_output = s_output + "<td class=\"payrow_line\" align=center > " + rseachPays["BranchNo"] + "</td>";


                    if (ColsForSendEmail != 1)
                    {
                        if (rtl_lang == 1)
                        {
                            s_output = s_output + "<td class=\"payrow_line\" align=right >" + rseachPays["DonationType"] + "</td>";
                        }
                        else {
                            s_output = s_output + "<td class=\"payrow_line\" align=left >" + rseachPays["DonationType"] + "</td>";
                        }
                    }
                    
                    s_output = s_output + "<td class=\"payrow_line\" align=center >" + rseachPays["total"] + "</td>";
                    s_output = s_output + "</tr>";
                }
                else {
                    s_output = s_output + "<tr>";
                    s_output = s_output + "<td class=\"payrow_line\" align=center>" + Convert.ToString(rseachPays["ValueDate"]) + "</td>";

                    if (rtl_lang == 1)
                    {
                        if (ColsForSendEmail == 3)
                        {
                            s_output = s_output + "<td class=\"payrow_line\" align=center > " + rseachPays["ProjectName"] + "</td>";
                        }
                        else if (ColsForSendEmail == 2)
                        {
                            s_output = s_output + "<td class=\"payrow_line\" align=center > " + rseachPays["DonationType"] + "</td>";
                        }
                        else if (ColsForSendEmail == 1)
                        {
                            //ללא העמודה
                        }
                    }
                    else {
                        if (ColsForSendEmail == 3)
                        {
                            s_output = s_output + "<td class=\"payrow_line\" align=center > " + rseachPays["ProjectName"] + "</td>";
                        }
                        else if (ColsForSendEmail == 2)
                        {
                            s_output = s_output + "<td class=\"payrow_line\" align=center > " + rseachPays["DonationType"] + "</td>";
                        }
                        else if (ColsForSendEmail == 1)
                        {
                            //ללא העמודה
                        }
                    }

                    s_output = s_output + "<td class=\"payrow_line\" align=center >" + rseachPays["PaymentType"] + "</td>";
                    s_output = s_output + "<td class=\"payrow_line\" align=center >" + rseachPays["total"] + "</td>";
                    s_output = s_output + "</tr>";

                }
                
                od = od + Convert.ToInt32(rseachPays["total"]);

                //rsPays.MoveNext
            }
            DataRow rsPays = null;
            if (dTable.Rows.Count >= 1)
            {
                rsPays = dTable.Rows[dTable.Rows.Count - 1];// rsPays.MovePrevious
            }
            //if (dTable.Rows.Count == 1)
            //{
            //    rsPays = dTable.Rows[dTable.Rows.Count - 1];// rsPays.MovePrevious
            //}
            //'שורת סיכום
            int ocolspan = 3;
            //'If ColsForSendEmail = 1 Then ocolspan = 4



            //        'שורת סיכום
            if (rsPays != null)
            {
                if (rtl_lang == 1)
                {
                    if (checkIsAacount.Length > 4)
                    {
                        s_output = s_output + "<tr><td class=\"payrow_line_foot\" colspan=5 align=left></td><td class=\"payrow_line_foot\" colspan=2 align=left>סה``כ בש``ח: " + rsPays["TotalInLeadCurrent"] + "</td></tr>";
                    }
                    else {
                        if (ColsForSendEmail == 1)
                        {
                            s_output = s_output + "<tr><td class=\"payrow_line_foot\" align=center>סה``כ בש``ח: " + rsPays["TotalInLeadCurrent"] + "</td><td class=\"payrow_line_foot\"   align=left></td><td></td></tr>";

                        }
                        else {
                            s_output = s_output + "<tr><td class=\"payrow_line_foot\"  colspan=" + ocolspan + " align=left></td><td class=\"payrow_line_foot\" align=center>סה``כ בש``ח: " + rsPays["TotalInLeadCurrent"] + "</td></tr>";
                        }
                    }
                }
                else {
                    if (checkIsAacount.Length > 4)
                    {
                        s_output = s_output + "<tr><td class=\"payrow_line_foot\" colspan=5 align=left></td><td class=\"payrow_line_foot\" colspan=2 align=right>Total in Nis: " + rsPays["TotalInLeadCurrent"] + "</td></tr>";
                    }
                    else {

                        s_output = s_output + "<tr><td class=\"payrow_line_foot\" colspan=" + ocolspan + " align=left></td><td class=\"payrow_line_foot\" align=center>Total in Nis: " + rsPays["TotalInLeadCurrent"] + "</td></tr>";
                    }
                }
            }
            String yeard = DateTime.Now.Year.ToString();
            String dateon = DateTime.Now.ToShortDateString();

            ////////////////////Old Code/////////////////////////////////
            //if (rsPays != null)
            //{
            //bb = db.InsertData(" insert into RectmpPdf (FileAs,Customerid,Address,pays,WhatFor,Original,Rec_year,RecieptDate,RecieptNo,CurrencyId) " +
            //    "select '" + QuoteSQLstr(Convert.ToString(rsPays["FileAs"])).Substring(0, 50) + "'," + prpCustomerId + ",'" + Address + "','" +
            //    QuoteSQLstr(s_output) + "','" + QuoteSQLstr(Convert.ToString(rsPays["WhatFor"])).Substring(0, 50) + "'," +
            //    (Convert.ToBoolean(rsPays["OriginalWasPrinted"]) == true ? 1 : 0)
            //    + ",'" + yeard + "','" + dateon + "','" +
            //    prpRecieptCode.Trim() + "','" + rsPays["CurrencyId"] + "'", null);

            ////bb = db.InsertData(" insert into RectmpPdf (FileAs,Customerid,Address,pays,WhatFor,Original,Rec_year,RecieptDate,RecieptNo,CurrencyId) " +
            ////    "select '" + Left(QuoteSQLstr(rsPays["FileAs"]), 50) + "'," + prpCustomerId + ",'" + QuoteSQLstr(LabelAdd(1).Caption) + "','" +
            ////    s_output + "','" + Convert.to(rsPays["WhatFor"]), 50) + "'," + (Convert.ToBoolean(rsPays["OriginalWasPrinted"]) == true ? 1 : 0)
            ////    + ",'" + Right(Format(Trim(TxtReciept(2).Text), DateFormat), 4) + "','" + Format(Trim(TxtReciept(2).Text), DateFormat) + "','" +
            ////    Trim(prpRecieptCode) + "','" + rsPays["CurrencyId"] + "'");

            //    if (bb == 0)
            //    {
            //        //Screen.MousePointer = vbDefault
            //        //MsgBox GetString(AppDictionay.Error_execute_query);
            //        db.Dispose();
            //        return "Error while inserting RectmpPdf ";
            //    }
            //}

            //string Ids = Convert.ToString(db.ExecuteScalarAsString("select top 1 ID from RectmpPdf order by id desc"));

            //if (Ids == "")
            //{
            //    db.Dispose();
            //    return "Error in executing sql (select top 1 ID from RectmpPdf order by id desc) ";
            //}



            //If bb = False Then
            // Screen.MousePointer = vbDefault
            //MsgBox GetString(AppDictionay.Error_execute_query)
            //Exit Sub
            //End If
            ///////////////////////////////////////////////////////////
            ////////////////////New Code///////////////////////////////


            //'    oemail = "2576091@gmail.com"

            ////////////////SendEmails is not created in db////////////////////////
            string emailName = "";
            Guid gid = Guid.NewGuid();
            oguid = gid.ToString();
            if (rsPays != null)
            {
                if (forprint == true)
                {
                    oemail = "2576091@gmail.com";
                    string Whatfor = "";
                    if (QuoteSQLstr(Convert.ToString(rsPays["WhatFor"])).Length > 50)
                        Whatfor = QuoteSQLstr(Convert.ToString(rsPays["WhatFor"])).Substring(0, 50);
                    else
                        Whatfor = QuoteSQLstr(Convert.ToString(rsPays["WhatFor"]));
                    string fa = "";
                    if (QuoteSQLstr(Convert.ToString(rsPays["WhatFor"])).Length > 50)
                    {
                        fa = QuoteSQLstr(Convert.ToString(rsPays["FileAs"])).Substring(0, 50);
                    }
                    else
                    {
                        fa = QuoteSQLstr(Convert.ToString(rsPays["FileAs"]));
                    }
                    bb = dbSE.InsertData(" insert into SendEmails (SendID,email,FileAs,EmailName,Customerid,Address,pays,WhatFor,Original,Rec_year,RecieptDate,RecieptNo,CurrencyId,isSending,RecieptType,ThanksLetterId,GUID)select " + oSendID + ",'" + oemail + "','" + fa + "','" + emailName + "'," + prpCustomerId + ",'" + Address + "','" + QuoteSQLstr(s_output) + "','" + Whatfor + "'," + (Convert.ToBoolean(rsPays["OriginalWasPrinted"]) == true ? 1 : 0) + ",'" + yeard + "','" + dateon + "','" + prpRecieptCode.Trim() + "','" + Convert.ToString(rsPays["CurrencyId"]) + "',1,'" + (prpRecieptType.Trim()) + "'," + Convert.ToString(ThanksLetterId) + ",'"+ oguid.ToString().Trim() + "'", null);
                }
                else {
                    oemail = "2576091@gmail.com";// txtEmail.Text;
                    string Whatfor = "";
                    if (QuoteSQLstr(Convert.ToString(rsPays["WhatFor"])).Length > 50)
                        Whatfor = QuoteSQLstr(Convert.ToString(rsPays["WhatFor"])).Substring(0, 50);
                    else
                        Whatfor = QuoteSQLstr(Convert.ToString(rsPays["WhatFor"]));
                    string fa = "";
                    if (QuoteSQLstr(Convert.ToString(rsPays["WhatFor"])).Length > 50)
                    {
                        fa = QuoteSQLstr(Convert.ToString(rsPays["FileAs"])).Substring(0, 50);
                    }
                    else
                    {
                        fa = QuoteSQLstr(Convert.ToString(rsPays["FileAs"]));
                    }
                    bb = dbSE.InsertData("insert into SendEmails (SendID,email,FileAs,EmailName,Customerid,Address,pays,WhatFor,Original,Rec_year,RecieptDate,RecieptNo,CurrencyId,RecieptType,ThanksLetterId,GUID)select " + oSendID + ",'" + oemail + "','" + fa + "','" + emailName + "'," + prpCustomerId + ",'" + Address + "','" + QuoteSQLstr(s_output) + "','" + Whatfor + "'," + (Convert.ToBoolean(rsPays["OriginalWasPrinted"]) == true ? 1 : 0) + ",'" + yeard + "','" + dateon + "','" + prpRecieptCode.Trim() + "','" + Convert.ToString(rsPays["CurrencyId"]) + "','" + (prpRecieptType.Trim()) + "'," + Convert.ToString(ThanksLetterId) + ",'"+ oguid.ToString().Trim() + "'", null);//QuoteSQLstr(LabelAdd(0).Caption)=emailName
                }
                if (bb == 0)
                {// Then
                 //'   objSendEmailDB.SetAbort
                 //      Screen.MousePointer = vbDefault
                 //     MsgBox GetString(AppDictionay.Error_execute_query)
                 //     Exit Sub
                    db.Dispose();
                    return "Error in inserting data to SendEmails ";
                }
            }
            /////////////////////////////////////////////
            link1 = "";


            ////////////////SendEmails is not created in db////////////////////////
            


            string Ids = Convert.ToString(dbSE.ExecuteScalarAsString(" select top 1 ID from SendEmails order by id desc"));

            if (Ids == "")
            {// Then
             //'      objSendEmailDB.SetAbort
             //Screen.MousePointer = vbDefault
             //MsgBox GetString(AppDictionay.Error_execute_query)
             //Exit Sub
                db.Dispose();
                return "Error in executing sql (select top 1 ID from SendEmails order by id desc) ";

            }




            oid = Convert.ToInt32(Ids);
            /////////////////////////////////////////////////////////////////


            //Dim oSOAP As Object
            //'Set oSOAP = Server.CreateObject("MSSOAP.SoapClient30")
            //'oSOAP.ClientProperty("ServerHTTPRequest") = True
            //'oSOAP.mssoapinit ("https://secure.amax.co.il/PrintPdfReceipt.asmx?wsdl")
            //'result = oSOAP.Retrieve()
            //'Set oSOAP = Nothing
            //Client.ClientProperty("ServerHTTPRequest") = True

            //oguid = "1";
            
            if (forprint == true)
            {
                link1 = "http://createpays.amax.co.il/CreatesessionReceipt.aspx?oid=" + oguid.ToString().Trim() + "&orgid=" + orgid + "&orgname=" + OrganizationId + "&RecieptType=" + ThanksLetterId.ToString().Trim() + "&Currency=" + StrLeadCurrency + "&forprint=1";
            }
            else {
                link1 = "http://createpays.amax.co.il/CreatesessionReceipt.aspx?oid=" + oguid.ToString().Trim() + "&orgid=" + orgid + "&orgname=" + OrganizationId + "&RecieptType=" + ThanksLetterId.ToString().Trim() + "&Currency=" + StrLeadCurrency + "";
            }

            WebRequest req = HttpWebRequest.Create(link1);
            req.Method = "GET";
            string returnObj;
            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                //retSt = "Link:"+ reader.ReadToEnd();
            }

            retSt = "Link:"+link1;
            ////////////////SendEmails is not created in db////////////////////////
            //bb = db.InsertData(" update SendEmails set link1= '" + link1 + "',subject= '" + Res + "',GUID = '" + oguid + "'  where id=" + oid + "",null);


            //if (bb == 0) {// Then
            //'  objSendEmailDB.SetAbort
            //                                               Screen.MousePointer = vbDefault
            //                                              MsgBox GetString(AppDictionay.Error_execute_query)
            //                                              Exit Sub

            //                                              }

            long X = 0;
            //Dim ourl As String
            //our l = encStr("http://editor.amax.co.il/Createsession.aspx?OrganizationId=" & Trim(OrganizationName) & "&tplId=" & Trim(Str(ComboThanksLetter.ItemData(ComboThanksLetter.ListIndex))) & "&rectitle=www", "1")
            //our l = DecStr(ourl, "1")

            if (forprint == false) { //Then
                strSql = "Update Reciepts set isSend=1 where RecieptNo='" + prpRecieptCode.Trim() + "' and RecieptType=" + prpRecieptType;
                db.InsertData(strSql, null);
                //'  objSendEmailDB.SetComplete
                //Screen.MousePointer = vbDefault
                db.Dispose();
                //return "ä÷áìä ðùìçä    ";
                    }
            else {
                //'   objSendEmailDB.SetComplete
                //  Screen.MousePointer = vbDefault
                //'  X = ShellExecute(0, "open", link1, 0, 0, 1)

               

                //X = System.Diagnostics.Process.st(0, "open", link1, 0, 0, 1);
                //' X = ShellExecute(0, "open", "Iexplore.exe", link1, "", 1)

                //' X = ShellExecute(0, "open", "chrome.exe", link1, "", 1)
            }





            // retSt = Client.PrintPdf(ThanksLetterId.ToString(), Convert.ToString(oid), OrganizationName, "con");
            //if (retSt.ToUpper().Substring(0, 2) == "OK")
            //{
            //    db.Dispose();
            //    return " ההודעה לא נשלחה שגיאה " + retSt;
            //}
            db.Dispose();
            db1.Dispose();
            dbSE.Dispose();
            return retSt;
        }

        public string QuoteSQLstr(string s)
        {
            string QuoteSQLstr = s.Trim();
            if (!(string.IsNullOrEmpty(QuoteSQLstr)) && QuoteSQLstr.Length > 0)
            {
                QuoteSQLstr = QuoteSQLstr.Replace("'", "`");
                QuoteSQLstr = QuoteSQLstr.Replace("\"\"", "``");
            }
            return QuoteSQLstr;
        }
    }
}
