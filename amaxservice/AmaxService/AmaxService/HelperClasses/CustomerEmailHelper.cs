using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
namespace AmaxService.HelperClasses
{
    public class CustomerEmailHelper
    {
        public Dictionary<string, object> GetCustomerEmailsDict(CustomerEmailsModel custEmailsobj)
        {
            Dictionary<string, object> CustAddressDictList = new Dictionary<string, object>();
            try
            {
                if (custEmailsobj != null)
                {
                    CustAddressDictList.Add("CustomerId", custEmailsobj.CustomerId);
                    CustAddressDictList.Add("Email", custEmailsobj.Email);
                    CustAddressDictList.Add("EmailName", custEmailsobj.EmailName);
                    CustAddressDictList.Add("Newslettere", custEmailsobj.Newslettere);
                    CustAddressDictList.Add("General", custEmailsobj.General);
                    CustAddressDictList.Add("MaxYearDelivery", custEmailsobj.MaxYearDelivery);
                    CustAddressDictList.Add("MaxMonthlyDelivery", custEmailsobj.MaxMonthlyDelivery);
                    CustAddressDictList.Add("LastEmail", custEmailsobj.LastEmail);
                    CustAddressDictList.Add("tempid", custEmailsobj.tempid);
                    CustAddressDictList.Add("Priority", custEmailsobj.Priority);
                    CustAddressDictList.Add("EmailSex", custEmailsobj.EmailSex);
                    CustAddressDictList.Add("publish", custEmailsobj.publish);
                }
            }
            catch (Exception ex)
            {
            }
            return CustAddressDictList;
        }
    }
}
