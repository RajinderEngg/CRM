using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaxDataService.DataModel;
using AmaxExtentions.DbAccess;
using System.Configuration;
using System.Data;
namespace AmaxService.HelperClasses
{
    public class CustomerAddressHelper
    {
        public Dictionary<string, object> GetCustomerAddressDict(CustomerAddressModel custAddressobj)
        {
            Dictionary<string,object> CustAddressDictList=new Dictionary<string,object>();
            try
            {
                if (custAddressobj != null)
                {
                    CustAddressDictList.Add("AddressId", custAddressobj.AddressId);
                    CustAddressDictList.Add("CustomerId", custAddressobj.CustomerId);
                    CustAddressDictList.Add("StateId", custAddressobj.StateId);
                    CustAddressDictList.Add("CityName", custAddressobj.CityName);
                    CustAddressDictList.Add("CountryCode", custAddressobj.CountryCode);
                    CustAddressDictList.Add("Street", custAddressobj.Street);
                    CustAddressDictList.Add("Street2", custAddressobj.Street2);
                    CustAddressDictList.Add("Zip", custAddressobj.Zip);
                    CustAddressDictList.Add("remark", custAddressobj.remark);
                    CustAddressDictList.Add("ForDelivery", custAddressobj.ForDelivery);
                    CustAddressDictList.Add("MaxYearDelivery", custAddressobj.MaxYearDelivery);
                    CustAddressDictList.Add("MaxMonthlyDelivery", custAddressobj.MaxMonthlyDelivery);
                    CustAddressDictList.Add("LastDelivery", custAddressobj.LastDelivery);
                    CustAddressDictList.Add("AddToName", custAddressobj.AddToName);
                    CustAddressDictList.Add("AddressTypeId", custAddressobj.AddressTypeId);
                    CustAddressDictList.Add("MainAddress", custAddressobj.MainAddress);

                    CustAddressDictList.Add("SrteetOnly", custAddressobj.LastDelivery);
                    CustAddressDictList.Add("StreetNo", custAddressobj.AddToName);
                    CustAddressDictList.Add("Entrance", custAddressobj.AddressTypeId);
                    CustAddressDictList.Add("DeliveryCode", custAddressobj.MainAddress);
                }
                
            }
            catch (Exception ex)
            {
            }
            return CustAddressDictList;
        }
        

    }
}
