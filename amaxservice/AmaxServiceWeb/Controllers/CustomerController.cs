﻿using AmaxDataService.DataModel;
using AmaxService.HelperClasses;
using AmaxServiceWeb.Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace AmaxServiceWeb.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        public CustomerHelper CustHP;
        public CustomerAddressHelper CustAddHP;
        public CustomerEmailHelper CustEmailHP;
        public CustomerPhonesHelper CustPhoneHP;
        public CustomerGroupsHelper CustGrpsHP;
        public LangResourceHelperClass LangResHP;
        public LogHistoryHelper LogHistHP;
        public CustomerServiceHelper CustSerHP;
        public CustomerController()
        {
            CustHP = new CustomerHelper();
            CustAddHP = new CustomerAddressHelper();
            CustEmailHP = new CustomerEmailHelper();
            CustPhoneHP = new CustomerPhonesHelper();
            CustGrpsHP = new CustomerGroupsHelper();
            LangResHP = new LangResourceHelperClass();
            LogHistHP = new LogHistoryHelper();
            CustSerHP = new CustomerServiceHelper();
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
                //SendEmail.SendEmailErr(ex.Message);
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
        [Security]
        public ResponseData SaveCustomerServices(CustomerServiceModel CustObj)
        {
            ResponseData returnObj = new ResponseData();
            CustSerHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
            try
            {
                string[] Times = new string[4];
                if (!(string.IsNullOrEmpty(CustObj.StartTime)))
                {
                    Times = CustObj.StartTime.Trim().Split('-');
                }
                int StartHour = Convert.ToInt32(CustObj.StartHour);
                int StartMinute = Convert.ToInt32(CustObj.StartMinute);
                DateTime Memodate = new DateTime(Convert.ToInt32(Times[2]), Convert.ToInt32(Times[1]), Convert.ToInt32(Times[0]), StartHour, StartMinute, 00);// DateTime.ParseExact(txtDate.Text.Trim(), "MM/dd/yyyy", null);
                                                                                                                                                     //startTime = Memodate;
                DateTime startTime = CustSerHP.getClientTime(Memodate, CustObj.TimeZone, false);
                StartHour = startTime.Hour;
                StartMinute = startTime.Minute;
                startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 00, 00, 00);
                Memodate = CustSerHP.getClientTime(Memodate, CustObj.TimeZone, false);
                CustObj.MemoDate = Memodate.AddMinutes(-Convert.ToInt32(CustObj.MemoMinutes));
                int g = CustSerHP.AddEditCustomerService(CustObj);
                if (g > 0)
                {
                    returnObj.Data = CustObj;
                    returnObj.IsError = false;
                    returnObj.ErrMsg = "Data saved successfully";
                }
                else
                {
                    returnObj.Data = null;
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "There is some problem in saving data";
                }
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "SaveCustServices", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId= ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname= ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "SaveFileAs";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }

        [HttpPost]
        [Security]
        public ResponseData SaveFileAs(int CustomerId, string FileAs,string lang)
        {
            ResponseData returnObj = new ResponseData();
            CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
            try
            {
                int g = CustHP.SaveFileAs(CustomerId,FileAs);
                if (g > 0)
                {
                    returnObj.IsError = false;

                    string KeyName = "APP_UPDATEFILEAS_MSG";
                    string KeyValue = LangResHP.GetKeyValue("CUSTOMER_MASTER", lang, KeyName);
                    if (KeyValue != "")
                    {
                        returnObj.ErrMsg = KeyValue;
                    }
                    else
                    {
                        if (lang == "en")
                        {
                                returnObj.ErrMsg = "Successfully Updated File As";
                            
                        }
                        if (lang == "he")
                        {
                                returnObj.ErrMsg = "עודכן בהצלחה הקובץ כפי";
                            
                        }
                    }
                    
                }
                else {
                    returnObj.IsError = true;
                    string KeyValue = LangResHP.GetKeyValue("CUSTOMER_MASTER", lang, "APP_SAVEFILEAS_ERRMSG");
                    if (KeyValue != "")
                    {
                        returnObj.ErrMsg = KeyValue;
                    }
                    else
                    {
                        if (lang == "en")
                        {
                            returnObj.ErrMsg = "There is problem in saving";

                        }
                        if (lang == "he")
                        {
                            returnObj.ErrMsg = "יש בעיה בחיסכון";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "SaveFileAs", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId= ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname= ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "SaveFileAs";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
        [HttpPost]
        [Security]
        public ResponseData Save(CustomersModel CustObj,string lang)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            
            ResponseData returnObj = new ResponseData();
            //foreach (var grpObj in CustObj.CustomerGroups)
            //{
            //    if (grpObj.CustomerGeneralGroupId == 0 && CustObj.CustomerGroups.Count > 1)
            //    {
            //        returnObj.Data = null;
            //        returnObj.IsError = true;
            //        if (lang == "en")
            //        {
            //            returnObj.ErrMsg = "You can not select the Primary group together with another group";
            //        }
            //        else if(lang=="he")
            //        {
            //            returnObj.ErrMsg = "לא ניתן לבחור את הקבוצה הראשית יחד עם קבוצה אחרת";
            //        }
            //        return returnObj;
            //    }
            //}
            
            

            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                CustHP.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                int tempcustid = CustObj.CustomerId;
                if (string.IsNullOrEmpty(CustObj.MiddleName) == true)
                    CustObj.MiddleName = "";
                if (string.IsNullOrEmpty(CustObj.CustomerCode) == true)
                    CustObj.CustomerCode = "";
                if (string.IsNullOrEmpty(CustObj.CustomerCode2) == true)
                    CustObj.CustomerCode2 = "";
                if (string.IsNullOrEmpty(CustObj.BirthDate) == true)
                    CustObj.BirthDate = "01-01-1900";
                if (string.IsNullOrEmpty(CustObj.BirthDate2) == true)
                    CustObj.BirthDate2 = "01-01-1900";
                if (string.IsNullOrEmpty(CustObj.jobtitlePartner) == true)
                    CustObj.jobtitlePartner = "";
                if (string.IsNullOrEmpty(CustObj.Suffix) == true)
                    CustObj.Suffix = "";
                if (string.IsNullOrEmpty(CustObj.Remark) == true)
                    CustObj.Remark = "";
                if (string.IsNullOrEmpty(CustObj.Company) == true)
                    CustObj.Company = "";
                if (string.IsNullOrEmpty(CustObj.FileAs) == true)
                {
                    if (CustObj.Company == "")
                    {
                        CustObj.FileAs = CustObj.lname + " " + CustObj.fname;
                    }
                    else if (CustObj.lname.Length == 0 && CustObj.fname.Length == 0) { CustObj.FileAs = CustObj.Company; }
                    else
                        CustObj.FileAs = "(" + CustObj.Company + ") " + CustObj.lname + " " + CustObj.fname; //+ " " & m_strSpouse
                }
                if(string.IsNullOrEmpty(CustObj.Title)==true)
                {
                    CustObj.Title = "";
                }


                if (CustObj.CustomerEmails == null)
                {
                    CustObj.CustomerEmails = new List<CustomerEmailsModel>();
                }
                else
                {
                    List<CustomerEmailsModel> EmailList = new List<CustomerEmailsModel>();
                    foreach (var emails in CustObj.CustomerEmails)
                    {

                        if (string.IsNullOrEmpty(emails.Email) == false)
                        {
                            EmailList.Add(emails);
                        }
                    }
                    CustObj.CustomerEmails = EmailList;
                }
                if (CustObj.CustomerAddresses == null)
                {
                    CustObj.CustomerAddresses = new List<CustomerAddressModel>();
                }
                else
                {
                    List<CustomerAddressModel> AddressList = new List<CustomerAddressModel>();
                    foreach (var addresses in CustObj.CustomerAddresses)
                    {
                        if (string.IsNullOrEmpty(addresses.CityName) == false|| string.IsNullOrEmpty(addresses.Street) == false)
                        {
                            AddressList.Add(addresses);
                        }
                    }
                    CustObj.CustomerAddresses = AddressList;
                }

                if (CustObj.CustomerPhones == null)
                {
                    CustObj.CustomerPhones = new List<CustomerPhonesModel>();
                }
                else
                {
                   List< CustomerPhonesModel> PhList = new List<CustomerPhonesModel>();
                    foreach (var phones in CustObj.CustomerPhones)
                    {
                        
                        if (string.IsNullOrEmpty(phones.Phone) == false)
                        {
                            PhList.Add(phones);
                        }
                    }
                    CustObj.CustomerPhones = PhList;
                }
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
                    if (string.IsNullOrEmpty(address.StateId) == true)
                        address.StateId = "";
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
                //if (ModelState.IsValid) {
                int g = CustHP.SaveCustomer(CustObj);
                if (g > 0)
                {
                    returnObj.Data = CustObj;
                    returnObj.IsError = false;
                    string KeyName = "";
                    if (tempcustid == -1)
                    {
                        KeyName = "APP_SAVE_MSG";
                    }
                    else
                    {
                        KeyName = "APP_UPDATE_MSG";
                    }
                    string KeyValue = LangResHP.GetKeyValue("CUSTOMER_MASTER", lang, KeyName);
                    if (KeyValue != "")
                    {
                        returnObj.ErrMsg = KeyValue;
                    }
                    else
                    {
                        if (lang == "en")
                        {
                            if (tempcustid == -1)
                            {
                                returnObj.ErrMsg = "Successfully Saved";
                            }
                            else
                            {
                                returnObj.ErrMsg = "Successfully Updated";
                            }
                        }
                        if (lang == "he")
                        {
                            if (tempcustid == -1)
                            {
                                returnObj.ErrMsg = "נשמר בהצלחה";
                            }
                            else
                            {
                                returnObj.ErrMsg = "עודכן בהצלחה";
                            }
                        }
                    }
                }
                else {
                    returnObj.IsError = true;
                    string KeyValue = LangResHP.GetKeyValue("CUSTOMER_MASTER", lang, "APP_SAVE_ERRMSG");
                    if (KeyValue != "")
                    {
                        returnObj.ErrMsg = KeyValue;
                    }
                    else
                    {
                        if (lang == "en")
                        {
                                returnObj.ErrMsg = "There is problem in saving";
                            
                        }
                        if (lang == "he")
                        {
                                returnObj.ErrMsg = "יש בעיה בחיסכון";
                            
                        }
                    }
                }
                //}
                //else {
                //    returnObj.IsError = true;
                //    returnObj.ErrMsg = "Page validations are not done.";
                //}
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "Save", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "Save";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData CheckCustOfSameNameComp(string fname,string lname,string company)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                List<CustomersModel> custlist = CustHP.GetCustomersByFNameLNameComp(fname, lname,company);
                if (custlist.Count > 0)
                    returnObj.Data = custlist;
                else
                    returnObj.Data = null;
                returnObj.IsError = false;
                if (returnObj.Data == null)
                    returnObj.ErrMsg = "False";
                else
                    returnObj.ErrMsg = "True";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "CheckCustOfSameNameComp", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "CheckCustOfSameNameComp";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData GetCustomersSearchData(string fname, string lname, string company,string phones,string emails)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                List<CustomersModel> custlist = CustHP.GetCustomersSearchData(fname, lname, company,phones,emails);
                if (custlist.Count > 0)
                    returnObj.Data = custlist;
                else
                    returnObj.Data = null;
                returnObj.IsError = false;
                if (returnObj.Data == null)
                    returnObj.ErrMsg = "False";
                else
                    returnObj.ErrMsg = "True";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomersSearchData", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomersSearchData";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }


        [HttpGet]
        [Security]
        public ResponseData CheckCustOfSameEmail(string Email)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();

                List<CustomersModel> custlist = CustHP.GetCustomersByEmail(Email);
                if (custlist.Count > 0)
                    returnObj.Data = custlist;
                else
                    returnObj.Data = null;
                returnObj.IsError = false;
                if (returnObj.Data == null)
                    returnObj.ErrMsg = "False";
                else
                    returnObj.ErrMsg = "True";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "CheckCustOfSameEmail", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "CheckCustOfSameEmail";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData CheckCustOfSamePhone(string Phone)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                List<CustomersModel> custlist= CustHP.GetCustomersByPhone(Phone);
                if (custlist.Count > 0)
                    returnObj.Data = custlist;
                else
                    returnObj.Data = null;
                returnObj.IsError = false;
                if (returnObj.Data == null)
                    returnObj.ErrMsg = "False";
                else
                    returnObj.ErrMsg = "True";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = "There is some problem";
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "CheckCustOfSamePhone", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "CheckCustOfSamePhone";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData GetCompleteCustomerDet(int CustomerId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                //int i = Convert.ToInt32("s");
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                CustHP.lang= ControllerContext.RouteData.Values["Language"].ToString();
                returnObj.Data = CustHP.GetCompleteCustomerDet(CustomerId);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {

                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCompleteCustomerDet", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCompleteCustomerDet";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;

                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }


        [HttpGet]
        [Security]
        public ResponseData GetCompleteCustomerDetForProfile(int CustomerId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                //int i = Convert.ToInt32("s");
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                CustHP.lang = ControllerContext.RouteData.Values["Language"].ToString();
                returnObj.Data = CustHP.GetCompleteCustomerDetForProfile(CustomerId);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {

                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCompleteCustomerDetForProfile", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["employeeid"])))
                //{
                //    LogHistObj.EmployeeId = Convert.ToInt32(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]));
                //}
                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["OrgId"])))
                //{
                //    LogHistObj.OrgId = Convert.ToString(ControllerContext.RouteData.Values["OrgId"]);
                //}

                //if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.RouteData.Values["fname"])))
                //{
                //    LogHistObj.fname = Convert.ToString(ControllerContext.RouteData.Values["fname"]);
                //}
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCompleteCustomerDet";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;

               string IsMailSend= SendEmail.SendEmailErr(LogHistObj, conString);
                //if (string.IsNullOrEmpty(IsMailSend))
                //{
                //    returnObj.ErrMsg = IsMailSend;
                //}
            }
            return returnObj;
        }

        //[HttpGet]
        //[Security]
        //public ResponseData GetCompleteCustomerDetSrch(int CustomerId,string SearchVal)
        //{
        //    ResponseData returnObj = new ResponseData();
        //    try
        //    {

        //        CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
        //        returnObj.Data = CustHP.GetCompleteCustomerDet(CustomerId);
        //        returnObj.IsError = false;
        //        returnObj.ErrMsg = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        returnObj.Data = null;
        //        returnObj.IsError = true;
        //        returnObj.ErrMsg = ex.Message;
        //    }
        //    return returnObj;
        //}
        [HttpGet]
        [Security]
        public ResponseData GetCustomerAddressByAddressId(int AddressId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                returnObj.Data = CustAddHP.GetCustomerAddressByAddressId(AddressId);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomerAddressByAddressId", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomerAddressByAddressId";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData GetCustomerEmailBytempid(int tempid)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                returnObj.Data = CustEmailHP.GetCustomerEmailBytempid(tempid);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomerEmailBytempid", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomerEmailBytempid";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData GetCustomerPhoneById(int Id)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                returnObj.Data = CustPhoneHP.GetCustomerPhoneById(Id);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomerPhoneById", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomerPhoneById";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData GetCustomerGrpsByCustIdGrps(int CustomerId, int GroupId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                CustHP.lang = ControllerContext.RouteData.Values["Language"].ToString();
                returnObj.Data = CustGrpsHP.GetCustomerGrpsByCustIdGrps(CustomerId,GroupId);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomerGrpsByCustIdGrps", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomerGrpsByCustIdGrps";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return returnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData GetCustomerListForSearch(string SrchVal)
        {
            
            ResponseData returnObj = new ResponseData();
            try
            {
                if (string.IsNullOrEmpty( SrchVal)== false)
                {
                    CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                    List<CustomersModel> CustList = CustHP.GetCustomerListForSrch(SrchVal);
                    List<string> FinalCustList = new List<string>();
                    foreach (var CustObj in CustList)
                    {
                        FinalCustList.Add(CustObj.jobtitlePartner);
                    }
                    returnObj.Data = FinalCustList;//CustHP.GetCustomerListForSrch(SrchVal);
                    returnObj.IsError = false;
                    returnObj.ErrMsg = "";
                }
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomerListForSearch", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomerListForSearch";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj,conString);
            }
            
            return returnObj;
        }

        [HttpGet]
        [Security]
        public ResponseData GetCustomerListForQuickSearch(string SrchVal)
        {

            ResponseData returnObj = new ResponseData();
            try
            {
                if (string.IsNullOrEmpty(SrchVal) == false)
                {
                    CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                    List<CustomersModel> CustList = CustHP.GetCustomerListForSrch(SrchVal);
                    List<string> FinalCustList = new List<string>();
                    //foreach (var CustObj in CustList)
                    //{
                    //    FinalCustList.Add(CustObj.jobtitlePartner);
                    //}
                    returnObj.Data = CustList.Distinct().ToList();//CustHP.GetCustomerListForSrch(SrchVal);
                    returnObj.IsError = false;
                    returnObj.ErrMsg = "";
                }
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomerListForQuickSearch", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomerListForSearch";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
                
            }

            return returnObj;
        }


        [HttpGet]
        [Security]
        public ResponseData GetCustomerCreditCardDet(int CustomerId, int customercreditCardid)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                CustHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                returnObj.Data = CustHP.GetCustomerCreditCardDet(CustomerId, customercreditCardid);
                returnObj.IsError = false;
                returnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(Convert.ToString(ControllerContext.RouteData.Values["employeeid"]), Convert.ToString(ControllerContext.RouteData.Values["OrgId"]), Convert.ToString(ControllerContext.RouteData.Values["fname"]), ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "GetCustomerCreditCardDet", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                //LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                //LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                //LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "GetCustomerCreditCardDet";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
        [HttpPost]
        //[Security]
        public ResponseData UploadCustImage(HttpPostedFileBase CustomerFile,string OrgId)
        {
            ResponseData returnObj = new ResponseData();
            try
            {
                var httpRequest = HttpContext.Current.Request.Files[0];
                string custImage = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustImagePath"]);
                string Fname= System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
                string Path = custImage  + Fname;
                if (!Directory.Exists(custImage))
                {
                    Directory.CreateDirectory(custImage);
                   

                }
                if (!Directory.Exists(custImage + "//" + OrgId))
                {
                    Directory.CreateDirectory(custImage + "//" + OrgId);
                }
                Path= custImage + "//" + OrgId +"//"+ Fname;
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
                else
                {
                    httpRequest.SaveAs(Path);
                    returnObj.IsError = false;
                    returnObj.ErrMsg = "";
                    returnObj.Data = OrgId + "//" + Fname;
                }
            }
            catch (Exception ex)
            {
                returnObj.Data = null;
                returnObj.IsError = true;
                returnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString.ToString();
                LogHistObj = LogHistHP.GetLogHistoryDet(null, "", "", ex.Message, frame.GetFileLineNumber(), frame.GetFileColumnNumber(), "UploadCustImage", ex.ToString(), AppConfig.APIVersion, "Customer Controller", ex);
                ////LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                ////LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                ////LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                //LogHistObj.Error = ex.Message;
                //LogHistObj.ExcLine = frame.GetFileLineNumber();
                //LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                //LogHistObj.Action = "UploadCustImage";
                //LogHistObj.FullDescription = ex.ToString();
                //LogHistObj.ExeptionType = "ERROR";
                //LogHistObj.APIVersion = AppConfig.APIVersion;
                //LogHistObj.FromPage = "Customer Controller";
                //LogHistObj.OnDate = System.DateTime.Now;
                //LogHistObj.ex = ex;
                string IsMailSend = SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
    }
}