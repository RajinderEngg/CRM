using AmaxDataService.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaxService.HelperClasses
{
    public class CreditCardHelper
    {
        public string SecurityconString { get; set; }
        public Dictionary<string, object> GetCustomerCreditCardDict(CustomerCreditCardModel custCrdtCardobj)
        {
            Dictionary<string, object> CustCrdtCardDictList = new Dictionary<string, object>();
            if (custCrdtCardobj != null)
            {
                CustCrdtCardDictList.Add("customerid", custCrdtCardobj.customerid);
                CustCrdtCardDictList.Add("customercreditCardid", custCrdtCardobj.customercreditCardid);
                CustCrdtCardDictList.Add("creditCardid", custCrdtCardobj.creditCardid);
                CustCrdtCardDictList.Add("creditCardNum", custCrdtCardobj.creditCardNum);
                CustCrdtCardDictList.Add("creditCardName", custCrdtCardobj.creditCardName);
                CustCrdtCardDictList.Add("creditCardMonth", custCrdtCardobj.creditCardMonth);
                CustCrdtCardDictList.Add("creditCardYear", custCrdtCardobj.creditCardYear);
                CustCrdtCardDictList.Add("creditCardBack", custCrdtCardobj.creditCardBack);
                CustCrdtCardDictList.Add("creditCardOwnerID", custCrdtCardobj.creditCardOwnerID);

                CustCrdtCardDictList.Add("RowDate", custCrdtCardobj.RowDate);
                CustCrdtCardDictList.Add("Deleted", custCrdtCardobj.Deleted);
                CustCrdtCardDictList.Add("TokenNum", custCrdtCardobj.TokenNum);
                CustCrdtCardDictList.Add("TerminalNumber", custCrdtCardobj.TerminalNumber);
                CustCrdtCardDictList.Add("ParityNum", custCrdtCardobj.ParityNum);
                CustCrdtCardDictList.Add("creditApproveNo", custCrdtCardobj.creditApproveNo);
                CustCrdtCardDictList.Add("digits6", custCrdtCardobj.digits6);
                CustCrdtCardDictList.Add("creditCardTypeName", custCrdtCardobj.creditCardTypeName);
               
            }
            return CustCrdtCardDictList;
        }
        public List<CustomerCreditCardModel> GetCustomerCreditCardListFromDS(DataSet custobj)
        {
            List<CustomerCreditCardModel> FinalDictList = new List<CustomerCreditCardModel>();

            //try
            //{
                for (int i = 0; i < custobj.Tables[0].Rows.Count; i++)
                {
                    CustomerCreditCardModel CustCrdtCardDictList = new CustomerCreditCardModel();
                    CustCrdtCardDictList.customerid = Convert.ToInt32(custobj.Tables[0].Rows[i]["customerid"]);
                    CustCrdtCardDictList.customercreditCardid = Convert.ToInt32(custobj.Tables[0].Rows[i]["customercreditCardid"]);
                    CustCrdtCardDictList.creditCardid = Convert.ToInt32(custobj.Tables[0].Rows[i]["creditCardid"]);
                    CustCrdtCardDictList.creditCardNum = Convert.ToString(custobj.Tables[0].Rows[i]["creditCardNum"]);
                    CustCrdtCardDictList.creditCardName = Convert.ToString(custobj.Tables[0].Rows[i]["creditCardName"]);
                    CustCrdtCardDictList.creditCardMonth = Convert.ToInt32(custobj.Tables[0].Rows[i]["creditCardMonth"]);
                    CustCrdtCardDictList.creditCardYear = Convert.ToInt32(custobj.Tables[0].Rows[i]["creditCardYear"]);
                    CustCrdtCardDictList.creditCardBack = Convert.ToString(custobj.Tables[0].Rows[i]["creditCardBack"]);
                    CustCrdtCardDictList.creditCardOwnerID = Convert.ToString(custobj.Tables[0].Rows[i]["creditCardOwnerID"]);

                    CustCrdtCardDictList.RowDate = Convert.ToString(custobj.Tables[0].Rows[i]["RowDate"]);
                    CustCrdtCardDictList.Deleted = Convert.ToBoolean(custobj.Tables[0].Rows[i]["Deleted"]);
                    CustCrdtCardDictList.TokenNum = Convert.ToString(custobj.Tables[0].Rows[i]["TokenNum"]);
                    CustCrdtCardDictList.TerminalNumber = Convert.ToString(custobj.Tables[0].Rows[i]["TerminalNumber"]);
                    CustCrdtCardDictList.ParityNum = Convert.ToString(custobj.Tables[0].Rows[i]["ParityNum"]);
                    CustCrdtCardDictList.creditApproveNo = Convert.ToString(custobj.Tables[0].Rows[i]["creditApproveNo"]);
                    CustCrdtCardDictList.digits6 = Convert.ToString(custobj.Tables[0].Rows[i]["digits6"]);
                    CustCrdtCardDictList.creditCardTypeName = Convert.ToString(custobj.Tables[0].Rows[i]["creditCardTypeName"]);

                    FinalDictList.Add(CustCrdtCardDictList);
                }
            //}
            //catch (Exception ex)
            //{
            //}
            return FinalDictList;
        }
    }
}
