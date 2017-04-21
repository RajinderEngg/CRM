using AmaxDataService.DataModel;
using AmaxService.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmaxServiceWeb.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        CustomerHelper CustHP;
        public CustomerController()
        {
            CustHP = new CustomerHelper();
        }

        [HttpGet]
        public ResponseData GetPhonetype(int PhoneTypeId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {

            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
            }
            return returnObj;
        }
        [HttpGet]
        public ResponseData GetCountryName(string Countrycode)
        {
            ResponseData returnObj = new ResponseData();
            try
            {

            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
            }
            return returnObj;
        }
        //[ActionName("Save")]
        [HttpPost]
        public ResponseData Save(CustomersModel CustObj)
        {
            ResponseData returnObj = new ResponseData();
            try {
                //if (ModelState.IsValid) {
                if (string.IsNullOrEmpty(CustObj.CustomerCode) == true)
                    CustObj.CustomerCode = "";
                if (string.IsNullOrEmpty(CustObj.CustomerCode2) == true)
                    CustObj.CustomerCode2 = "";
                if (string.IsNullOrEmpty(CustObj.BirthDate) == true)
                    CustObj.BirthDate = "1900-01-01 00:00:00";
                if (string.IsNullOrEmpty(CustObj.BirthDate2) == true)
                    CustObj.BirthDate2 = "1900-01-01 00:00:00";
                if (string.IsNullOrEmpty(CustObj.jobtitlePartner) == true)
                    CustObj.jobtitlePartner = "";
                if (string.IsNullOrEmpty(CustObj.Suffix) == true)
                    CustObj.Suffix = "";
                if (string.IsNullOrEmpty(CustObj.Remark) == true)
                    CustObj.Remark = "";
                foreach (var address in CustObj.CustomerAddresses)
                {
                    if (string.IsNullOrEmpty(address.remark) == true)
                        address.remark = "";
                    if (string.IsNullOrEmpty(address.LastDelivery) == true)
                        address.LastDelivery = "";
                    if (string.IsNullOrEmpty(address.AddToName) == true)
                        address.AddToName = "";

                    if (string.IsNullOrEmpty(address.SrteetOnly) == true)
                        address.SrteetOnly = "";
                    if (string.IsNullOrEmpty(address.StreetNo) == true)
                        address.StreetNo = "";
                    if (string.IsNullOrEmpty(address.Entrance) == true)
                        address.Entrance = "";
                    if (string.IsNullOrEmpty(address.DeliveryCode) == true)
                        address.DeliveryCode = "";
                }
                foreach (var emails in CustObj.CustomerEmails)
                {
                    if (string.IsNullOrEmpty(emails.EmailName) == true)
                        emails.EmailName = "";
                    if (string.IsNullOrEmpty(emails.Email) == true)
                        emails.Email = "";
                    if (string.IsNullOrEmpty(emails.LastEmail) == true)
                        emails.LastEmail = "";
                }
                foreach (var phones in CustObj.CustomerPhones)
                {
                    if (string.IsNullOrEmpty(phones.Area) == true)
                        phones.Area = "";
                    if (string.IsNullOrEmpty(phones.Prefix) == true)
                        phones.Prefix = "";
                    if (string.IsNullOrEmpty(phones.Phone) == true)
                        phones.Phone = "";
                    if (string.IsNullOrEmpty(phones.Comments) == true)
                        phones.Comments = "";
                }
                int g = CustHP.SaveCustomer(CustObj);
                    if (g > 0) {
                        returnObj.IsError = false;
                        returnObj.ErrMsg = "Successfully Saved";
                    }
                //}
                //else {
                //    returnObj.IsError = true;
                //    returnObj.ErrMsg = "Page validations are not done.";
                //}
            } catch (Exception ex) {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
            }
            return returnObj;
        }
    }
}