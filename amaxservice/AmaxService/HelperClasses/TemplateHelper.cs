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
using System.Web;

namespace AmaxService.HelperClasses
{
    public class TemplateHelper
    {
        public string SecurityconString { get; set; }
        public string GetOrgName(string OrgId)
        {
            string returnObj = "";
            //try
            //{
                using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    string Query = "select OrganizationName from Organization where OrganizationId='" + OrgId + "'";
                    DataSet ds = db.GetDataSet(Query, null, false);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        returnObj = Convert.ToString(ds.Tables[0].Rows[0]["OrganizationName"]);
                    }
                }

            //}
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public string GetTemplate(int ThnksLetterId, string OrgId)
        {
            string returnObj = "";
            //try
           // {
                string MainDir = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MainDir"]);
                string Path = "";
                //string OrgName = GetOrgName(OrgId);
                string OrgName = OrgId;
                if (!Directory.Exists(MainDir))
                {
                    return null;
                }
                else {
                    if (File.Exists(MainDir + OrgName + "\\" + ThnksLetterId + ".html"))
                    {
                        Path = MainDir + OrgName + "\\" + ThnksLetterId + ".html";
                    }
                    else
                    {
                        
                        string MaintDirTmpl= HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MainDirtmpl"]);
                        if (!Directory.Exists(MaintDirTmpl))
                        {
                            return null;
                        }
                        else {
                            if (File.Exists(MaintDirTmpl + "Receipt.html"))
                            {
                                Path = MaintDirTmpl + "Receipt.html";
                            }
                            else
                            {
                                return null;
                            }

                        }
                    }
                    WebRequest req = HttpWebRequest.Create(Path);
                    req.Method = "GET";
                    using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        returnObj = reader.ReadToEnd();
                    }
                }
           // }
            //catch (Exception ex)
            //{
            //}
            return returnObj;
        }
        public bool SaveTemplate(int ThnksLetterId, string OrgId, string Source)
        {
            bool retunObj = false;
           // try
            //{
                string MainDir = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MainDir"]);
                //string OrgName = GetOrgName(OrgId);
                string OrgName = OrgId;
                string Path = MainDir + OrgName + "\\" + ThnksLetterId + ".html";
                if (!Directory.Exists(MainDir))
                {
                    return false;
                }
                else {
                    if (!Directory.Exists(MainDir + OrgName))
                    {
                        Directory.CreateDirectory(MainDir + OrgName);
                    }
                    if (File.Exists(Path))
                    {
                        File.Delete(Path);
                    }
                    
                    FileStream fs1 = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs1);
                    writer.Write(Source);
                    writer.Close();
                    retunObj = true;
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return retunObj;
        }
    }
}
