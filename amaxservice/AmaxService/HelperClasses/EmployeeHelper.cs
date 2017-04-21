using AmaxDataService.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
    public class EmployeeHelper
    {
        public Dictionary<string, object> GetEmployeesDict(EmployeeModel custPhonesobj)
        {
            Dictionary<string, object> CustAddressDictList = new Dictionary<string, object>();
            if (custPhonesobj != null)
            {
                CustAddressDictList.Add("employeeid", custPhonesobj.employeeid);
                CustAddressDictList.Add("eType", custPhonesobj.eType);
                CustAddressDictList.Add("sysuser", custPhonesobj.sysuser);
                CustAddressDictList.Add("password", custPhonesobj.password);
                CustAddressDictList.Add("fname", custPhonesobj.fname);
                CustAddressDictList.Add("lname", custPhonesobj.lname);
                CustAddressDictList.Add("authoType", custPhonesobj.authoType);
            }
            return CustAddressDictList;
        }
        public List<EmployeeModel> GetEmployeeListFromDS(DataSet custobj)
        {
            List<EmployeeModel> FinalDictList = new List<EmployeeModel>();

            // try
            // {
            for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
            {
                EmployeeModel CustAddressDictList = new EmployeeModel();
                CustAddressDictList.employeeid = Convert.ToInt32(custobj.Tables[0].Rows[i]["employeeid"]);
                CustAddressDictList.eType = Convert.ToInt32(custobj.Tables[0].Rows[i]["eType"]);
                CustAddressDictList.sysuser = Convert.ToBoolean(custobj.Tables[0].Rows[i]["sysuser"]);
                CustAddressDictList.password = Convert.ToString(custobj.Tables[0].Rows[i]["password"]);
                CustAddressDictList.fname = Convert.ToString(custobj.Tables[0].Rows[i]["fname"]);
                CustAddressDictList.lname = Convert.ToString(custobj.Tables[0].Rows[i]["lname"]);
                CustAddressDictList.authoType = Convert.ToInt32(custobj.Tables[0].Rows[i]["authoType"]);
                FinalDictList.Add(CustAddressDictList);
            }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
    }
}
