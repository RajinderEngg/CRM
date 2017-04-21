using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
namespace AmaxService.HelperClasses
{
    class CustomerPhonesHelper
    {
        public Dictionary<string, object> GetCustomerPhonesDict(CustomerPhonesModel custPhonesobj)
        {
            Dictionary<string, object> CustAddressDictList = new Dictionary<string, object>();
            if (custPhonesobj != null)
            {
                CustAddressDictList.Add("CustomerId", custPhonesobj.CustomerId);
                CustAddressDictList.Add("Id", custPhonesobj.Id);
                CustAddressDictList.Add("PhoneTypeId", custPhonesobj.PhoneTypeId);
                CustAddressDictList.Add("Prefix", custPhonesobj.Prefix);
                CustAddressDictList.Add("Area", custPhonesobj.Area);
                CustAddressDictList.Add("Phone", custPhonesobj.Phone);
                CustAddressDictList.Add("Comments", custPhonesobj.Comments);
                CustAddressDictList.Add("IsSms", custPhonesobj.IsSms);
                CustAddressDictList.Add("publish", custPhonesobj.publish);
            }
            return CustAddressDictList;
        }
    }
}
