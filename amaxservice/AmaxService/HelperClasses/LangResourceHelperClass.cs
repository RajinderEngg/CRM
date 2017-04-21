using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System.Data;
using System.Configuration;

namespace AmaxService.HelperClasses
{
    public class LangResourceHelperClass
    {
        public string SecurityConString { get; set; }
        public Dictionary<string, object> GetCustomerEmailsDict(LanguageResourceModel LangResobj)
        {
            Dictionary<string, object> LangResDictList = new Dictionary<string, object>();
            //try
            //{
                if (LangResobj != null)
                {
                    LangResDictList.Add("LRId", LangResobj.LRId);
                    LangResDictList.Add("FormType", LangResobj.FormType);
                    LangResDictList.Add("Lang", LangResobj.Lang);
                    LangResDictList.Add("KeyName", LangResobj.KeyName);
                    LangResDictList.Add("KeyValue", LangResobj.KeyValue);
                }
           // }
            //catch (Exception ex)
           // {
            //}
            return LangResDictList;
        }
        public List<KeyPair> GetLangResByFormtypeLang(string FormType, string Lang)
        {

            List<KeyPair> ResList = new List<KeyPair>();
            string Query = "select * from LanguageResource where FormType='" + FormType + "' and Lang='" + Lang + "'";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))   //SecurityConString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ResList.Add(new KeyPair(Convert.ToString(ds.Tables[0].Rows[i]["KeyName"]), Convert.ToString(ds.Tables[0].Rows[i]["KeyValue"])));

                }

            }
            return ResList;
        }
        public Dictionary<string, object> GetLangResDict(string FormType, string Lang)
        {
            Dictionary<string, object> LangResDict = new Dictionary<string, object>();
            if (Lang == "en")
                LangResDict.Add("APP_DIR", "ltr");
            else
                LangResDict.Add("APP_DIR", "rtl");
            List<KeyPair> LangResList = GetLangResByFormtypeLang(FormType, Lang);
            if (LangResList.Count > 0)
            {
                LangResDict.Add("APP_LANG", Lang);
                Dictionary<string, object> LangResInnerDict = new Dictionary<string, object>();
                foreach (var LangRes in LangResList)
                {
                    LangResInnerDict.Add(LangRes.Value, LangRes.Text);
                }
                LangResDict.Add(FormType, LangResInnerDict);
            }
            return LangResDict;
        }
        public string GetKeyValue(string FormType, string Lang,string KeyName)
        {
            string returnObj = "";
            string Query = "select * from LanguageResource where FormType='" + FormType + "' and Lang='" + Lang + "' and keyname='"+KeyName+"'";
            using (DbAccess db = new DbAccess(ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString))   //SecurityConString
            {
                DataSet ds = db.GetDataSet(Query, null, false);
                if (ds.Tables[0].Rows.Count>0)
                {
                    returnObj = Convert.ToString(ds.Tables[0].Rows[0]["KeyValue"]);
                }
            }
            return returnObj;
        }
    }
}
