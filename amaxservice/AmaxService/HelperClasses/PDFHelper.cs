using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
  public  class PDFHelper
    {
        public string SecurityConString { get; set; }
        public string LangValue { get; set; }
        DbAccess db;
        public PDFHelper()
        {
            db = new DbAccess(SecurityConString);
        }
        private string PrintPdf(int prpCustomerId, string txtEmail, string OrganizationName, int ThankLetterId, string prpRecieptCode, string prpRecieptType)
        {
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
            long orgid = 0;
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
            long ColsForSendEmail = 0;
            string ReceiptName = null;
            string checkIsAacount = null;
            var Client = new PdfClient.PrintPdfReceiptSoapClient();
            string retSt = null;


            //If ComboThanksLetter.ListIndex = -1 Then
            // MsgBox GetString(AppDictionay.Select_) & " " & GetString(AppDictionay.Template)
            // ComboThanksLetter.SetFocus
            // Exit Sub
            //End If


            DataTable dTable = db.GetDataTable(" select   * from RecieptThanksLetters where ThanksLetterId = " + ThankLetterId + " ", null, false);
            if (dTable.Rows.Count > 0)
            {
                MailBody = Convert.ToString(dTable.Rows[0]["MailBody"]);
                MailSubj = Convert.ToString(dTable.Rows[0]["MailSubj"]);
                rtl_lang = Convert.ToInt32(dTable.Rows[0]["isrtl"]);
                ThanksLetterId = Convert.ToInt32(dTable.Rows[0]["ThanksLetterId"]);

            }
            {
                return "תבנית חסרה";
                //Exit Sub
            }
            retSt = Client.IsFileExists(OrganizationName, ThanksLetterId.ToString() + ".html");
            if (retSt != "YES")
            {
                return "התבנית חסרה בשרת";
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
                osub = MailSubj.Substring(0, 50);
            }

            string strSqlH = " SELECT distinct top 500000  ProjectName,JI, RecieptRoWID,For_Invoice,OriginalWasPrinted,payed,ActiveStatus," +
                  "CustomerId,FileAs,RecieptName,RecieptNo,RecieptDate,WhatFor,RecieptType,RecieptsEmployeeId,AmountInLeadCurrent as Total," +
                  " LeadCurrency as CurrencyId,Total as Total1,CurrencyId as CurrencyId1,ValueDate,CheckNo,BranchNo,AccountNo,DonationTypeOther as DonationType," +
                  "AccountName,PaymentTypeOther as PaymentType,HASHAVSHEVETPR,HASHAVSHEVETAC,HASHKUPA,HashHeshZchut,Amount,IsExport,Camefrom,details,creditcardtype," +
                  "TotalInLeadCurrent,isSend From ND_Receipts_View where RecieptNo ='" +
                  prpRecieptCode + "'  And RecieptType = " + prpRecieptType + " order by  RecieptDate";

            dTable = db.GetDataTable(strSqlH, null);
            if (dTable.Rows.Count > 0)
            {
             //   DataRow rsPays = dTable.Rows[0];
                checkIsAacount = checkIsAacount + Convert.ToString(dTable.Rows[0]["BranchNo"]) + Convert.ToString(dTable.Rows[0]["AccountNo"]) + Convert.ToString(dTable.Rows[0]["CheckNo"]);
            }
            else {
                return "עליך לשמור את הקבלה  ";
            }



            ocustomerid = prpCustomerId;



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
            DataRow rsPays;
            if (dTable.Rows.Count > 0)
            {
                rsPays = dTable.Rows[dTable.Rows.Count - 2];// rsPays.MovePrevious
            }
            //'שורת סיכום
            int ocolspan = 3;
            //'If ColsForSendEmail = 1 Then ocolspan = 4



            //        'שורת סיכום
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

            //bb = db.InsertData(" insert into RectmpPdf (FileAs,Customerid,Address,pays,WhatFor,Original,Rec_year,RecieptDate,RecieptNo,CurrencyId) " +
            //    "select '" + Left(QuoteSQLstr(rsPays["FileAs"]), 50) + "'," + prpCustomerId + ",'" + QuoteSQLstr(LabelAdd(1).Caption) + "','" +
            //    QuoteSQLstr(s_output) + "','" + Left(QuoteSQLstr(rsPays["WhatFor"]), 50) + "'," + (Convert.ToBoolean(rsPays["OriginalWasPrinted"]) == true ? 1 : 0)
            //    + ",'" + Right(Format(Trim(TxtReciept(2).Text), DateFormat), 4) + "','" + Format(Trim(TxtReciept(2).Text), DateFormat) + "','" +
            //    Trim(prpRecieptCode) + "','" + rsPays["CurrencyId"] + "'");

            //bb = db.InsertData(" insert into RectmpPdf (FileAs,Customerid,Address,pays,WhatFor,Original,Rec_year,RecieptDate,RecieptNo,CurrencyId) " +
            //    "select '" + Left(QuoteSQLstr(rsPays["FileAs"]), 50) + "'," + prpCustomerId + ",'" + QuoteSQLstr(LabelAdd(1).Caption) + "','" +
            //    s_output + "','" + Convert.to(rsPays["WhatFor"]), 50) + "'," + (Convert.ToBoolean(rsPays["OriginalWasPrinted"]) == true ? 1 : 0)
            //    + ",'" + Right(Format(Trim(TxtReciept(2).Text), DateFormat), 4) + "','" + Format(Trim(TxtReciept(2).Text), DateFormat) + "','" +
            //    Trim(prpRecieptCode) + "','" + rsPays["CurrencyId"] + "'");

            //If bb = False Then
            //  Screen.MousePointer = vbDefault
            // MsgBox GetString(AppDictionay.Error_execute_query)
            //Exit Sub
            //End If


            string Ids = Convert.ToString(db.ExecuteScalarAsString("select top 1 ID from RectmpPdf order by id desc"));

            if (Ids == "")
            {
                return "Error in executing sql (select top 1 ID from RectmpPdf order by id desc) ";
            }
            //If bb = False Then
            // Screen.MousePointer = vbDefault
            //MsgBox GetString(AppDictionay.Error_execute_query)
            //Exit Sub
            //End If

            oid = Convert.ToInt32(Ids);
            //Dim oSOAP As Object
            //'Set oSOAP = Server.CreateObject("MSSOAP.SoapClient30")
            //'oSOAP.ClientProperty("ServerHTTPRequest") = True
            //'oSOAP.mssoapinit ("https://secure.amax.co.il/PrintPdfReceipt.asmx?wsdl")
            //'result = oSOAP.Retrieve()
            //'Set oSOAP = Nothing
            //Client.ClientProperty("ServerHTTPRequest") = True
            retSt = Client.PrintPdf(ThanksLetterId.ToString(), Convert.ToString(oid), OrganizationName, "con");
            if (retSt.ToUpper().Substring(0, 2) == "OK")
            {
                return " ההודעה לא נשלחה שגיאה " + retSt;


            }

        }


    }
}
