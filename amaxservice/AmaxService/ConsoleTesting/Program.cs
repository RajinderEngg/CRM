using AmaxService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ConsoleTesting
{
    class Program
    {
        public static void Main(string[] args) {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("localhost");

            mail.From = new MailAddress("test2@localhost");
            mail.To.Add("nitesh_shaw@hotmail.com");
            mail.Subject = "Test Mail";
            mail.Body = "This is for testing SMTP mail from GMAIL";

            //SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("test2@localhost", "test2");
            //SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        static void _Main(string[] args)
        {
            XElement xmlSendSMS = new XElement("SMS",
                     new XElement("USERNAME", new XCData("TestUsername")),
                              new XElement("PASSWORD", new XCData("TestPassword")),
                              new XElement("SENDER_PREFIX", "TestPrefix"),
                              new XElement("SENDER_SUFFIX", new XCData("Test Suffix")),
                              new XElement("MSGLNG", "HEB"),
                              new XElement("MSG", new XCData("Test Message"))
                              );

            xmlSendSMS.Add(
                new XElement("MOBILE_LIST",
                    new XElement("MOBILE_NUMBER", 7686925390)
                )
            );
            xmlSendSMS.Add(
                new XElement("UNIODE", "Fasle"),
                new XElement("USE_PERSONAL", "False")
            );


            Console.WriteLine(xmlSendSMS.ToString());
            Console.ReadLine();

        }
        static void __Main(string[] args)
        {
            //var x = XElement.Parse(File.ReadAllText("SQL.xml"));
            //Console.WriteLine();
            //xqlExecutor exec = new xqlExecutor(x, @"Server=USER-PC\SQLEXPRESS2005;Database=JaffaNet_2005;User Id=sa;Password=MoonShine");
            //exec.ExecuteXQL();

            //Console.WriteLine(exec.ErrorMessage);
            ////Console.ReadLine();


            //try
            //{
            //    SqlConnection con = new SqlConnection("Persist Security Info=True;User ID=sa;Password=00gdcc;Initial Catalog=Takeit-soft;Data Source=213.8.108.107,1499;Min Pool Size=20;Max Pool Size=100; Connection Timeout=30;");
            //    con.Open();

            //    Console.WriteLine("Connection Opened succesfullt");
            //    con.Close();
            //    Console.WriteLine("Connection Closed succesfullt");

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Console.ReadLine();
        }
    }
}
