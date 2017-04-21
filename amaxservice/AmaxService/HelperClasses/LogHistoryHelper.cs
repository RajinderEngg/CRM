using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
    public class LogHistoryHelper
    {
        public string conString { get; set; }
        public bool AddErrorinLogHistory(LogHistoryModel LogHistoryObj)
        {
            
            bool returnObj = false;
            try {
                using (DbAccess db = new DbAccess(conString))//ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString
                {
                    db.Transaction = db.con.BeginTransaction();
                    string Query = "insert into tblLogHistory(EmployeeId,ExeptionType,Error,ExcLine,ExcPlace,OnDate,Action,XmlData," +
                                    "FromPage,FullDescription,APIVersion) " +

                                    "Values(" + LogHistoryObj.EmployeeId + ",'" + LogHistoryObj.ExeptionType + "','" + LogHistoryObj.Error + "'," + LogHistoryObj.ExcLine + "," + LogHistoryObj.ExcPlace + ",'" + LogHistoryObj.OnDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + LogHistoryObj.Action + "','" + LogHistoryObj.XmlData + "'," +

                                    "'" + LogHistoryObj.FromPage + "','" + LogHistoryObj.FullDescription + "','" + LogHistoryObj.APIVersion + "') ";
                    int i = db.InsertData(Query, null, db.Transaction);
                    if (i > 0)
                    {
                        db.Transaction.Commit();
                        returnObj = true;
                    }
                }
            }
            catch(Exception ex)
            {
                returnObj = false;
            }
            return returnObj;
        }
    }
}
