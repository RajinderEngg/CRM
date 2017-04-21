using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
    public class CustomersNoteHelper
    {
        public string SecurityconString { get; set; }
        public Dictionary<string, object> GetCustomerNotesDict(CustomerNotesModel custNotesobj)
        {
            Dictionary<string, object> CustCrdtCardDictList = new Dictionary<string, object>();
            if (custNotesobj != null)
            {
                CustCrdtCardDictList.Add("id", custNotesobj.id);
                CustCrdtCardDictList.Add("subjectid", custNotesobj.subjectid);
                CustCrdtCardDictList.Add("employeeId", custNotesobj.employeeId);
                CustCrdtCardDictList.Add("Note", custNotesobj.Note);

            }
            return CustCrdtCardDictList;
        }
        public List<CustomerNotesModel> GetCustomerNotesListFromDS(DataSet custobj)
        {
            List<CustomerNotesModel> FinalDictList = new List<CustomerNotesModel>();

            //try
            //{
            for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
            {
                CustomerNotesModel CustCrdtCardDictList = new CustomerNotesModel();
                CustCrdtCardDictList.id = Convert.ToInt32(custobj.Tables[0].Rows[i]["id"]);
                CustCrdtCardDictList.subjectid = Convert.ToInt32(custobj.Tables[0].Rows[i]["subjectid"]);
                CustCrdtCardDictList.employeeId = Convert.ToInt32(custobj.Tables[0].Rows[i]["employeeId"]);
                CustCrdtCardDictList.Note = Convert.ToString(custobj.Tables[0].Rows[i]["Note"]);
                

                FinalDictList.Add(CustCrdtCardDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
        public List<CustomerNotesModel> GetCustomerNoteList(string Query)
        {
            List<CustomerNotesModel> returnObj = new List<CustomerNotesModel>();
            

            using (DbAccess db = new DbAccess(SecurityconString))
            {
                DataSet ds = db.GetDataSet(Query, null, false);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    CustomerNotesModel mod = new CustomerNotesModel();
                    mod.employeeId = Convert.ToInt32(ds.Tables[0].Rows[0]["employeeId"]);
                    mod.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    mod.Note = Convert.ToString(ds.Tables[0].Rows[0]["Note"]);
                    mod.subjectid = Convert.ToInt32(ds.Tables[0].Rows[0]["subjectid"]);
                    returnObj.Add(mod);//.instituteName = Convert.ToString(ds.Tables[0].Rows[0]["instituteName"]);
                }
            }
            return returnObj;
        }
    }
}
