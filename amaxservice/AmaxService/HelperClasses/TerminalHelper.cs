using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
//using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AmaxService.HelperClasses
{
    public class TerminalHelper
    {
        public string SecurityConString { get; set; }
        public string LangValue { get; set; }
        public TerminalDetModel GetTerminalDetFromTermNo(string TerminalNo)
        {
            TerminalDetModel returnObj = new TerminalDetModel();
            string Query = "SELECT     Accounts.AccountId, Accounts.AccountName, Accounts.AccountNameEng, institute.instituteCode, institute.instituteName" +
                            " FROM Accounts INNER JOIN " +
                            " institute ON Accounts.ASHRAY = institute.instituteCode" +
                            " where institute.instituteCode='" + TerminalNo + "'";

            using (DbAccess db = new DbAccess(SecurityConString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnObj.AccountId = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountId"]);
                    returnObj.AccountName = Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]);
                    returnObj.AccountNameEng = Convert.ToString(ds.Tables[0].Rows[0]["AccountNameEng"]);
                    returnObj.instituteCode = Convert.ToString(ds.Tables[0].Rows[0]["instituteCode"]);
                    returnObj.instituteName = Convert.ToString(ds.Tables[0].Rows[0]["instituteName"]);
                }
            }
            return returnObj;
        }
        public string IsInsertTotblLastUpdate(int CustomerId)
        {
            string returnObj = "";
            string strSql = "select top 1 * from CardCharge where Customerid=" + CustomerId + " AND DATEDIFF(d, theDate, CONVERT(datetime, '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103)) = 0 order By id desc";
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                DataSet ds = db.GetDataSet(strSql, null, false);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    returnObj = "Note that you have already made this day a customer billing amount " + ds.Tables[0].Rows[0]["mount"].ToString() + " Continue";
                    //InsertTotblLastUpdate objCustomer.Customerid, objemployeeLogin.EmployeeId, "חיוב באשראי פעם נוספת באותו יום למרות שקיבל התראה בסכום של " & txtSumToBill.Text

                }
            }
            return returnObj;
        }
        public int InsertTotblLastUpdate(int CustomerId, int EmployeeId, decimal SumtoBill)
        {
            int returnObj = 0;
            string strSql = "insert into tblLastUpdate(CustomerId,Employeeid,details)" +
                " values(" + CustomerId + "," + EmployeeId + ",'Credit charge again on the same day despite the warning received an amount of " + SumtoBill + "')";
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                db.Transaction = db.con.BeginTransaction();
                returnObj = db.InsertData(strSql, null, db.Transaction);
                if (returnObj > 0)
                {
                    db.Transaction.Commit();
                }

            }
            return returnObj;
        }
        public string MTS_Ping(string TerminalNum, string Password)
        {


            string retSt = null;
            object Client = null;
            object answer = null;
            bool arkomReal = true;
            TerminalNum = TerminalNum.Trim();
            Password = Password.Trim();
            if (arkomReal == true)
            {
                ClientArkom1.MTS_WebServiceSoapClient CC_Trans = new ClientArkom1.MTS_WebServiceSoapClient();
                retSt = Convert.ToString(CC_Trans.MTS_Ping(TerminalNum, Password));
            }
            else {
                ClientAkromSecure.MTS_WebServiceSoapClient CC_Trans = new ClientAkromSecure.MTS_WebServiceSoapClient();
                retSt = Convert.ToString(CC_Trans.MTS_Ping(TerminalNum, Password));

            }
            return retSt;
        }
        public string getPrint(string DealNumber, string Terminal, string GotothisURL)
        {
            string returnObj = "";
            string txtXml = "DealNumber=" + DealNumber;
            txtXml += "&DisplayData=3&Termianl=" + Terminal;
            txtXml += "&DefPrint=false";
            WebRequest req = HttpWebRequest.Create(GotothisURL + "?" + txtXml);
            req.Method = "GET";

            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                returnObj = reader.ReadToEnd();
                returnObj = returnObj.Replace("<body>", "<body dir='rtl'><div id='PrintDiv'>");
                returnObj = returnObj.Replace("</div>\r\n        \r\n    </form>", "</div></div><div><button type=\"button\" value=\"Print\" id=\"PrintButton\">Print</button></div></form>");
                returnObj = returnObj.Replace("</body>", "<script src='https://code.jquery.com/jquery-3.1.1.min.js'></script><script>$(\"#PrintButton\").click(function() {$(\"#PrintButton\").hide();window.print();$(\"#PrintButton\").show()});</script></body>");
            }
            return returnObj;
        }
        public string ChargeAshrait(ChargeAshrayModel Obj, string GotothisURL)
        {
            string returnObj = "";
            string txtXml = "";
            int CredType = 1;
            object Client;
            string otokef, IsRefund, oemail, odescripion;
            string retSt;
            decimal ocurrency1;
            string TransactionID, CardExpiry;
            string TransSum, TransPoints, Last4Digits;
            string CVV2, id, TransCurrency;
            string ISO_Currency, CreditType, ApprovalCode;
            string FirstPayment, FixedPayment;
            string NumOfFixedPayments, TransRef, J_Prm;
            string Z_Prm, Q_Prm, R_Prm;
            string answer, MerchantNote, ClientNote;
            //GotothisURL = "https://secure.cardcom.co.il/BillGoldPost.aspx";
            //GetConnection = CreateObject("MSXML2.ServerXMLHTTP");
            txtXml = "TerminalNumber=" + Obj.oTerminalNumber;


            txtXml = txtXml + "&Sum=" + Obj.oSumToBill;


            if (Obj.UseToken)
                txtXml = txtXml + "&token=" + Obj.oCardNumber;
            else
                txtXml = txtXml + "&CardNumber=" + Obj.oCardNumber;





            txtXml = txtXml + "&cardvalidityyear=" + Obj.oCardValidityYear;

            txtXml = txtXml + "&cardvaliditymonth=" + Obj.oCardValidityMonth;

            txtXml = txtXml + "&identitynumber=" + Obj.oCardOwnerId;

            txtXml = txtXml + "&Currency=" + Obj.oCurrency;

            if (string.IsNullOrEmpty(Obj.oapprovalnumber) == false)
                txtXml = txtXml + "&approvalnumber=" + Obj.oapprovalnumber;


            if (string.IsNullOrEmpty(Obj.DealCode) == false)
                txtXml = txtXml + "&DealCode=" + Obj.DealCode;



            if (Convert.ToInt32(Obj.CreditType) == 0)
            {
                txtXml = txtXml + "&numofpayments=" + Obj.oNumOfPayments;
                txtXml = txtXml + "&credittype=1";
            }
            else if (Convert.ToInt32(Obj.CreditType) == 1)
            {
                txtXml = txtXml + "&numofpayments=" + Obj.oNumOfPayments;
                txtXml = txtXml + "&credittype=8";
                txtXml = txtXml + "&constpatment=" + Obj.oconstpatment;
                if (Obj.ofirstpaymentsum != "")
                {
                    txtXml = txtXml + "&firstpaymentsum=" + Obj.ofirstpaymentsum;
                }
            }
            else if (Convert.ToInt32(Obj.CreditType) == 2)
            {
                txtXml = txtXml + "&numofpayments=" + Obj.oNumOfPayments;
                txtXml = txtXml + "&credittype=6";
            }
            if (Obj.CVV.Length >= 3)
            {
                txtXml = txtXml + "&cvv=" + Obj.CVV.Trim();
            }
            if (Convert.ToInt32(Obj.odealtype) != 1)
            {
                txtXml = txtXml + "&userpassword=" + Obj.ouserpassword;
                txtXml = txtXml + "&username=" + Obj.ousername;
                txtXml = txtXml + "&dealtype=" + Obj.odealtype;
            }
            else {
                txtXml = txtXml + "&username=" + Obj.ousername;
                txtXml = txtXml + "&dealtype=1";
                txtXml = txtXml + "&jparameter=0";
            }
            if (Obj.CustomerName != "")
            {
                txtXml = txtXml + "&custom_field_01=" + HttpContext.Current.Server.UrlEncode(Obj.CustomerName);//  UrlEncode2(Obj.CustomerName) ' Lcid_Supported);
            }
            //GetConnection.Open("GET", GotothisURL & "?" & txtXml, False);


            //GetConnection.Send("");



            //string ResponsePage;
            //ResponsePage = GetConnection.ResponseText;
            //ChargeAshrait = ResponsePage;
            //GetConnection = null;
            WebRequest req = HttpWebRequest.Create(GotothisURL + "?" + txtXml);
            req.Method = "GET";

            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                returnObj = reader.ReadToEnd();
            }
            return returnObj;
        }
        public int InsertToCardCharge(int CustomerId, int EmployeeId, double SumToBill)
        {
            int returnObj = 0;
            string strSql = "insert into CardCharge(CustomerId,Employeeid,Mount)" +
                " values(" + CustomerId + "," + EmployeeId + "," + SumToBill + ")";
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                db.Transaction = db.con.BeginTransaction();
                returnObj = db.InsertData(strSql, null, db.Transaction);
                if (returnObj > 0)
                {
                    db.Transaction.Commit();
                }
            }
            return returnObj;
        }
        public int InsertTotblLastUpdate(int CustomerId, int EmployeeId, string details)
        {
            int returnObj = 0;
            string strSql = "insert into tblLastUpdate(CustomerId,Employeeid,details,sync)" +
                " values(" + CustomerId + "," + EmployeeId + ",'" + details + "',1)";
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                db.Transaction = db.con.BeginTransaction();
                returnObj = db.InsertData(strSql, null, db.Transaction);
                if (returnObj > 0)
                {
                    db.Transaction.Commit();
                }
            }
            return returnObj;
        }
        public int UpdateOwnerId(int CustomerId, string OwnerId)
        {
            int returnObj = 0;
            string strSql = "update Customercreditcard set creditCardOwnerID= '" + OwnerId + "' " +
                "   Where customerid=" + CustomerId;
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                db.Transaction = db.con.BeginTransaction();
                returnObj = db.InsertData(strSql, null, db.Transaction);
                strSql = "update customers set CustomerCode= '" + OwnerId + "' " +
                "   Where customerid=" + CustomerId;
                returnObj += db.InsertData(strSql, null, db.Transaction);
                if (returnObj > 0)
                {
                    db.Transaction.Commit();
                }
            }
            return returnObj;
        }
        public bool InsertToCustomerService(int CustomerId,int EmployeeId, string otxt,
int oServiceTypeId, bool LogEmailDate = false,
bool logMailDate = false, string EmailAddress = "",
 int AddressId = 0, int DoneiT = 1)
        {
            bool returnObj = false;
            int Count;
            string strSql = "INSERT INTO CustomerService (EmployId, ";
            strSql = strSql + "ServiceTypeId , ";
            strSql = strSql + "StartTime, ";
            strSql = strSql + "StartHour  , ";
            strSql = strSql + "StartMinute , ";
            strSql = strSql + "Details , ";
            strSql = strSql + "MemoDate , ";
            strSql = strSql + "MemoMinutes , ";
            strSql = strSql + "FileName , ";
                    strSql = strSql + "customerid , ";
                    strSql = strSql + "EmployeeMemo, ";
                    strSql = strSql + "DoneiT, ";
                    strSql = strSql + "Employeehandle , ";
                    strSql = strSql + "DoneDate) ";
                    strSql = strSql + " VALUES (";
                    strSql = strSql + EmployeeId + ", ";
                    strSql = strSql + oServiceTypeId + ", ";
                    strSql = strSql + "Convert(datetime,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "',103), ";
                    strSql = strSql + "'" + System.DateTime.Now.Hour + "', ";
                    strSql = strSql + "'" + System.DateTime.Now.Minute + "', ";
                    if (AddressId == 0) {
                strSql = strSql + "'" + otxt + " " + EmailAddress +EmployeeId+ System.DateTime.Now + "', ";
                    }
            else {
                strSql = strSql + "'" + otxt + " (" + AddressId + ")"  + EmployeeId + System.DateTime.Now + "', ";
                    }
            strSql = strSql + "Convert(datetime,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "',103), ";
                    strSql = strSql + 0 + ", ";
                    strSql = strSql + "'" + "" + "', ";
                    strSql = strSql + CustomerId + ", ";
                    strSql = strSql + 1 + ", ";
                    strSql = strSql + DoneiT + ", ";
                    strSql = strSql + EmployeeId + ", ";
                    strSql = strSql + "Convert(datetime,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "',103)) ";
                    ;
            using (DbAccess db = new DbAccess(SecurityConString))   //ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
            {
                db.Transaction = db.con.BeginTransaction();
                Count = db.InsertData(strSql, null, db.Transaction);


                if (Count > 0) {

                    if (LogEmailDate && EmailAddress != "") {
                       string strSqlNew = "Update CustomerEmails set LastEmail=Convert(datetime,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "',103) Where Customerid =" + CustomerId + " And Email='" + EmailAddress + "'";
                        Count += db.InsertData(strSqlNew, null, db.Transaction);


                        otxt = otxt.Length>99?otxt.Substring(0,99):otxt;
                        
                        strSqlNew = "Insert into CustomerEmailSents (Email,SentDate,Remark) values ('" + EmailAddress + "',Convert(datetime,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "',103),'" + otxt + "')";
                           Count += db.InsertData(strSqlNew, null, db.Transaction);

                    }



                    if (logMailDate && AddressId != 0) {

                        strSql = "Update CustomerAddress set LastDelivery=Convert(datetime,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "',103) Where AddressId =" + AddressId;
                        Count += db.InsertData(strSql, null, db.Transaction);

                        otxt = otxt.Length > 99 ? otxt.Substring(0, 99) : otxt;
                        strSql = "Insert into CustomerMailSents (AddressId,SentDate,Remark) values (" + AddressId + ",Convert(datetime,'" + System.DateTime.Now.ToString("dd/MM/yyyy") + "',103),'" + otxt + "')";
                        Count += db.InsertData(strSql, null, db.Transaction);


                    }
                    db.Transaction.Commit();
                    returnObj = true;
                }
            }
            return returnObj;
        }
    }
}
