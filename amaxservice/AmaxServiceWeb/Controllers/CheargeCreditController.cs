using AmaxDataService.DataModel;
using AmaxService.HelperClasses;
using AmaxServiceWeb.Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmaxServiceWeb.Controllers
{
    public class CheargeCreditController : ApiController
    {
        // GET api/<controller>
        public CustomerHelper CustHP;
        public TerminalHelper TermHP;
        public LogHistoryHelper LogHistHP;
        public CheargeCreditController()
        {
            CustHP = new CustomerHelper();
            TermHP = new TerminalHelper();
            LogHistHP = new LogHistoryHelper();
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        [Security]
        public ResponseData GetTerminalDetByTermNo(string TerminalNo)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                TermHP.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                returnObj.Data = TermHP.GetTerminalDetFromTermNo(TerminalNo);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetTerminalDetByTermNo", ex.ToString(), AppConfig.APIVersion, "CheargeCredit Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["OrgId"])))
                //{
                //    LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //}

                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["fname"])))
                //{
                //    LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //}
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "CheckCustOfSameNameComp";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "CheargeCredit Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData IsInsertTotblLastUpdate(int CustomerId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                TermHP.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                returnObj.Data = TermHP.IsInsertTotblLastUpdate(CustomerId);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "IsInsertTotblLastUpdate", ex.ToString(), AppConfig.APIVersion, "CheargeCredit Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["OrgId"])))
                //{
                //    LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //}

                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["fname"])))
                //{
                //    LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //}
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "IsInsertTotblLastUpdate";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "CheargeCredit Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData UpdateOwnerId(int CustomerId, string OwnerId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                TermHP.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                returnObj.Data = TermHP.UpdateOwnerId(CustomerId, OwnerId);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "UpdateOwnerId", ex.ToString(), AppConfig.APIVersion, "CheargeCredit Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["OrgId"])))
                //{
                //    LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //}

                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["fname"])))
                //{
                //    LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //}
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "UpdateOwnerId";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "CheargeCredit Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData InsertTotblLastUpdate(int CustomerId, decimal SumtoBill)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                TermHP.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                int EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                returnObj.Data = TermHP.InsertTotblLastUpdate(CustomerId, EmployeeId, SumtoBill);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "InsertTotblLastUpdate", ex.ToString(), AppConfig.APIVersion, "CheargeCredit Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["OrgId"])))
                //{
                //    LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //}

                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["fname"])))
                //{
                //    LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //}
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "InsertTotblLastUpdate";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "CheargeCredit Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
        [HttpPost]
        [Security]
        public ResponseData GetChargeAshrait(ChargeAshrayModel Obj)
        {
            ResponseData returnObj = new ResponseData();
            string anser;
            string strSql;
            try
            {
                TermHP.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                string OrganizationName= ControllerContext.RouteData.Values["OrgId"].ToString();
                int EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                if (Obj.CompanySlika == "1")
                {
                }
                else
                {
                    anser = TermHP.MTS_Ping(Obj.oTerminalNumber, Obj.ouserpassword);
                    //SaveSetting App.EXEName, "Settings", "Textpass", TextPass.Text
                    if (anser != "0") {
                        returnObj.Data = null;
                        returnObj.IsError = true;
                        returnObj.ErrMsg = "Wrong username or password or the connection fails";
                        return returnObj;
                    }
                }
                //if (string.IsNullOrEmpty(Obj.ChargeType)==false&& Obj.ChargeType != "0")
                //{
                    if ((Obj.oNumOfPayments.Length) < 1)
                    {
                        returnObj.Data = null;
                        returnObj.IsError = true;
                        returnObj.ErrMsg = "Unknown tax payments";
                        return returnObj;
                    }
                int intcheck;
                if (!int.TryParse(Obj.oNumOfPayments, out intcheck))
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Payments must be in numerics";
                    return returnObj;
                }
                    //If Not IsNumeric(txtPayments.Text) Then
                    //       MsgBox GetString(AppDictionay.Value_must_be_NUMERIC)
                    //       txtPayments.Text = ""
                    //       txtPayments.SetFocus
                    //       Exit Sub
                    //End If
                    if (Convert.ToInt32(Obj.oNumOfPayments) < 1)
                    {
                        returnObj.Data = null;
                        returnObj.IsError = true;
                        returnObj.ErrMsg = "Tax payments of less than 1";
                        return returnObj;
                    }
                    if (Convert.ToInt32(Obj.CreditType) == 1)
                    {
                        if (Convert.ToInt32(Obj.oNumOfPayments) == 1)
                        {
                            returnObj.Data = null;
                            returnObj.IsError = true;
                            returnObj.ErrMsg = "Tax payments on credit payments greater than 1";
                            return returnObj;
                        }
                    }
                    if (Convert.ToInt32(Obj.CreditType) == 2)
                    {
                        if (Convert.ToInt32(Obj.oNumOfPayments) < 2)
                        {
                            returnObj.Data = null;
                            returnObj.IsError = true;
                            returnObj.ErrMsg = "Tax Credit payments not determined";
                            return returnObj;
                        }
                    }
                //}
                if ((Obj.oTerminalNumber.Length) < 3) {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Not registered Tax Credit";
                    return returnObj;
                }
                if (string.IsNullOrEmpty(Obj.CustomerName) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Not registered customer name";
                    return returnObj;
                }
                if (string.IsNullOrEmpty(Obj.oSumToBill) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Amount is required";
                    return returnObj;
                }
                double abc;
                if (!double.TryParse(Obj.oSumToBill,out abc))
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Amount must be in numeric";
                    return returnObj;
                }
                    //   If Not IsNumeric(txtSumToBill.Text) Then
                    //       MsgBox GetString(AppDictionay.Value_must_be_NUMERIC)
                    //       txtSumToBill.Text = ""
                    //       txtSumToBill.SetFocus
                    //       Exit Sub
                    //End If
                    if (Convert.ToDecimal(Obj.oSumToBill) < 1)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "The amount is less than 1";
                    return returnObj;
                }
                /////This code is writen in client side//////////////
                //strSql = "select top 1 * from CardCharge where Customerid=" + Obj.Customerid + " AND DATEDIFF(d, theDate, CONVERT(datetime, '" + DateTime.Now.ToShortDateString() + "', 103)) = 0 order By id desc";
                //objMainDB.ExecSQL(strSql)
                //If objMainDB.HasRecords Then
                //  If MsgBox(" שים לב כבר ביצעת היום חיוב ללקוח זה  בסכום " + Str(objMainDB.Recordset("mount")) + " האם להמשיך", vbYesNo) = vbNo Then Exit Sub
                //  InsertTotblLastUpdate objCustomer.Customerid, objemployeeLogin.EmployeeId, "חיוב באשראי פעם נוספת באותו יום למרות שקיבל התראה בסכום של " & txtSumToBill.Text

                //End If
                if (string.IsNullOrEmpty(Obj.oCardOwnerId) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Not registered ID";
                    return returnObj;
                }
                if (string.IsNullOrEmpty(Obj.oTerminalNumber) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Taxes were not terminal";
                    return returnObj;
                }
                if (string.IsNullOrEmpty(Obj.ChargeType) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Not selected Transaction Type";
                    return returnObj;
                }
                if (string.IsNullOrEmpty(Obj.oCurrency) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Unknown Currency";
                    return returnObj;
                }
                if (string.IsNullOrEmpty(Obj.oCardValidityYear) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Unknown attacker year";
                    return returnObj;
                }
                if (string.IsNullOrEmpty(Obj.oCardValidityMonth) == true)
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "Unknown effect in";
                    return returnObj;
                }
                if (Obj.ChargeType == "0")
                {
                    Obj.odealtype = "1";
                }
                else if (Obj.ChargeType == "1")
                {
                    Obj.odealtype = "51";
                }
                else if (Obj.ChargeType == "2")
                {
                    Obj.odealtype = "52";
                }
                if (Obj.CompanySlika == "1")
                {
                    if (Obj.odealtype != "51")
                    {
                        if (string.IsNullOrEmpty(Obj.ouserpassword) == false)
                        {
                            returnObj.Data = null;
                            returnObj.IsError = true;
                            returnObj.ErrMsg = "Password only type of credit transaction";
                            return returnObj;
                        }
                    }
                }
                if (Convert.ToInt32(Obj.ChargeType) != 0)
                {
                    if (string.IsNullOrEmpty(Obj.ousername) == true || string.IsNullOrEmpty(Obj.ouserpassword) == true)
                    {
                        returnObj.Data = null;
                        returnObj.IsError = true;
                        returnObj.ErrMsg = "This type of transaction you must fill out name and password";
                        return returnObj;
                    }
                }
                /////This is written in client side because it is confirm message////
                //if (Obj.odealtype == "1")
                //{
                //    returnObj.Data = null;
                //    returnObj.IsError = true;
                //    returnObj.ErrMsg = "Note that you requested charge the customer, will continue?";
                //    return returnObj;
                //}
                //           If isfindcreditCardOwnerID = False Then
                //If MsgBox(" תרצה לעדכן תעודת זהות בכרטיס לקוח ? ", vbYesNo) = vbYes Then


                // objMainDB.ExecSQL("update Customercreditcard set creditCardOwnerID= '" & txtCardOwnerId.Text & "'    Where customerid=" & objCustomer.Customerid)
                // objMainDB.ExecSQL("update customers set CustomerCode= '" & txtCardOwnerId.Text & "'    Where customerid=" & objCustomer.Customerid)
                //End If
                // End If
                string GotothisURL = AppConfig.CreditUrl == "" ? "https://secure.cardcom.co.il/BillGoldPost.aspx" : AppConfig.CreditUrl;
                string oresponse = TermHP.ChargeAshrait(Obj,GotothisURL);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
                if (Obj.CompanySlika == "1")
                {
                    oresponse = oresponse.Trim();
                    //string[] Res = oresponse.Split(';');
                    if (oresponse!=""&& oresponse.Split(';')[0] == "0")
                    {
                        string[] Res = oresponse.Split(';');

                        Obj.DealNumber = Res[1];
                        //frmPrintCredit.DealNumber = Trim(Res(1))
                        //frmPrintCredit.Termianl = txtTerminalNumber.Text
                        //frmPrintCredit.Show vbModal


                    


                        if(Obj.ChargeType == "1")
                        {
                            if( OrganizationName.ToLower() == "metzoken" ){
                                TermHP.InsertToCustomerService(Obj.CustomerId, EmployeeId, "Please credit invoice at Card Get up to the amount "+Obj.oSumToBill, 1000001,false,true,"",0,0);
                            //  InsertToCustomerService objCustomer.Customerid, " יש לבצע חשבונית זיכוי בקארד קום לסכום" + txtSumToBill.Text, 1000001, False, True, , 0, 0
                            }
                        }

                        TermHP.InsertToCardCharge(Obj.CustomerId,EmployeeId,Convert.ToDouble(Obj.oSumToBill));
                        //InsertToCardCharge objCustomer.Customerid, objemployeeLogin.EmployeeId, CDbl(txtSumToBill.Text)
                        //                glbStrCreditCardSum = txtSumToBill.Text
                        //                glbStrCreditCardNum = Right(StrtxtCardNumber, 4)
                        TermHP.InsertTotblLastUpdate(Obj.CustomerId, EmployeeId, "Amount: " + Obj.oSumToBill + " Card: " + Obj.oTerminalNumber + " debit authorization was successful No. " + Res[1]);
                        //                InsertTotblLastUpdate objCustomer.Customerid, objemployeeLogin.EmployeeId, "מסוף: " & txtTerminalNumber.Text & " סכום: " & txtSumToBill.Text & " כרטיס: " & Right(StrtxtCardNumber, 6) & " " & "החיוב בוצע בהצלחה מס אישור " & Trim(Res(1))


                        //                Call frmCustomers.checkCustloadingStatus 'get customer details
                        //                Call frmCustomers.StripSelect(3)
                        //                Call frmCustomers.NEWRECEIPT
                        returnObj.Data = Obj;
                        returnObj.IsError = false;
                        returnObj.ErrMsg = "Billing was successful with Deal No: " + Res[1];
                        return returnObj;

                    }
                    else {
                        if (oresponse.Trim() == "")
                        {
                            //TextLog = TextLog & "תקלה לא מזוהה יתכן שהכרטיס נסלק, אנא בדוק מול קארדקום" + vbCrLf + oresponse
                            string cardno = "";
                            if (Obj.oCardNumber.Length > 6)
                            {
                                cardno = Obj.oCardNumber.Substring(Obj.oCardNumber.Length - 6);
                            }
                            else
                            {
                                cardno = Obj.oCardNumber;
                            }
                            TermHP.InsertTotblLastUpdate(Obj.CustomerId, EmployeeId, "Terminal: "+Obj.oTerminalNumber+" Amount: "+Obj.oSumToBill+" Card: "+ cardno + "unidentified malfunction may you remove the card, please check with Kardkom " + oresponse);
                            //InsertTotblLastUpdate objCustomer.Customerid, objemployeeLogin.EmployeeId, "מסוף: " & txtTerminalNumber.Text & " סכום: " & txtSumToBill.Text & " כרטיס: " & Right(StrtxtCardNumber, 6) & " " & "תקלה לא מזוהה יתכן שהכרטיס נסלק, אנא בדוק מול קארדקום " + oresponse
                            //MsgBox TextLog, vbCritical
                            returnObj.IsError = true;
                            returnObj.ErrMsg = "An unidentified malfunction may you remove the card, please check with Kardkom";
                        }
                        else {
                            string cardno = "";
                            if (Obj.oCardNumber.Length > 6)
                            {
                                cardno = Obj.oCardNumber.Substring(Obj.oCardNumber.Length - 6);
                            }
                            else
                            {
                                cardno = Obj.oCardNumber;
                            }
                            //TextLog = TextLog & "תקלה בשידור כרטיס של לקוח: " + vbCrLf + oresponse
                            TermHP.InsertTotblLastUpdate(Obj.CustomerId, EmployeeId, "Terminal: "+Obj.oTerminalNumber+" Amount: " + Obj.oSumToBill + " Card: " + cardno + " Charged unidentified malfunction may you remove the card, please check with Kardkom " + oresponse);
                            //            InsertTotblLastUpdate objCustomer.Customerid, objemployeeLogin.EmployeeId, "מסוף: " & txtTerminalNumber.Text & " סכום: " & txtSumToBill.Text & " כרטיס: " & Right(StrtxtCardNumber, 6) & " " & "תקלה בשידור כרטיס של לקוח: " + oresponse
                            returnObj.IsError = true;
                            returnObj.ErrMsg = "LIVE malfunction customer card: "+oresponse;
                            //            MsgBox TextLog, vbCritical

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetChargeAshrait", ex.ToString(), AppConfig.APIVersion, "CheargeCredit Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["OrgId"])))
                //{
                //    LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //}

                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["fname"])))
                //{
                //    LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //}
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetChargeAshrait";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "CheargeCredit Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData getPrint(string DealNumber, string TerminalNo)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                TermHP.SecurityConString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                string GotoUrl = AppConfig.CreditPrintUrl;
                returnObj.Data = TermHP.getPrint(DealNumber, TerminalNo,GotoUrl);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "getPrint", ex.ToString(), AppConfig.APIVersion, "CheargeCredit Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["OrgId"])))
                //{
                //    LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //}

                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["fname"])))
                //{
                //    LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //}
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "UpdateOwnerId";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "CheargeCredit Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
    }
}