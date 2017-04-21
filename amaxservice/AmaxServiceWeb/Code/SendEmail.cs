using AmaxDataService.DataModel;
using AmaxService.HelperClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AmaxServiceWeb.Code
{
    public class SendEmail
    {
        
        public static void SendEmailErr(LogHistoryModel LogHistObj,string conString)
        {

            string FromMail = AppConfig.FromEmail;//"info@amax.co.il";
            //string strSub = AppConfig.strSub; //"charge zehut credit";
            string strSub = "";
            if (LogHistObj.EmployeeId != null)
                strSub = "Error from DB: " +LogHistObj.OrgId+", From User: (" +LogHistObj.EmployeeId+") "+LogHistObj.fname;
            else
                strSub = "Error from Login";
            string ReplytoMail = AppConfig.ReplytoMail; //"info@amax.co.il";
            string ReplytoName = AppConfig.ReplytoName; //"Amax support";
            int Port = Convert.ToInt32(AppConfig.Port);
            string SMT, strErrorMsg;

            //string mypath = HttpContext.Current.Request.PhysicalApplicationPath;
            SMT = AppConfig.SMT; //"mail.amax.co.il";

            try
            {
                LogHistoryHelper LogHistHP = new LogHistoryHelper();

                //////////Sending Email
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(SMT, Port);
                MailMessage MyMailMessage = new System.Net.Mail.MailMessage(FromMail, FromMail, strSub, LogHistObj.Error);

                System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential("mailer@amax", "00gdcc");

                mailClient.Credentials = mailAuthentication;


                MyMailMessage.From = new MailAddress(FromMail, ReplytoName);
                //MyMailMessage.ReplyTo = new MailAddress(ReplytoMail);
                MyMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                MyMailMessage.Sender = new MailAddress(AppConfig.SenderEmail);
                mailClient.Send(MyMailMessage);
                MyMailMessage.Dispose();
                //////////Saving in Db
                StackTrace st = new StackTrace(LogHistObj.ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistHP.conString = conString;
                bool SaveObj = LogHistHP.AddErrorinLogHistory(LogHistObj);
            }
            catch (Exception ex)
            {
                LogHistoryHelper LogHistHP = new LogHistoryHelper();
                strErrorMsg = ex.Message + " in function  SendEmailErr ";
                LogHistObj.Error = strErrorMsg;
                LogHistHP.conString = conString;
                bool SaveObj = LogHistHP.AddErrorinLogHistory(LogHistObj);
                
            }
        }
        
    }
}