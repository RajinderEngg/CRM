﻿using AmaxServiceWeb.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AmaxService.ServiceModels;
using AmaxDataService.DataModel;
using AmaxService.HelperClasses;
using System.Diagnostics;

namespace AmaxServiceWeb.Controllers
{
    public class ReceiptController : ApiController
    {
        // GET api/<controller>
        RecieptTypeHelper RcptTypeHP;
        ReceiptCreateHelper RcptCreateHP;
        DataBind db;
        public ReceiptController()
        {
            RcptTypeHP = new RecieptTypeHelper();
            RcptCreateHP = new ReceiptCreateHelper();
            db = new DataBind();
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        [HttpGet]
        [Security]
        public ResponseData GetReceipts(string lang, int EmpId, int RecModes)
        {
            ResponseData ReturnObj = new ResponseData();
            try

            {
                RcptTypeHP.SecurityconString= ControllerContext.RouteData.Values["SecurityContext"].ToString();
                RcptTypeHP.LangValue = lang;
                // Get EmpDetail - outhtype Based on EmpId

                ReturnObj.Data = RcptTypeHP.GetReceipts(EmpId, RecModes);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "GetRecieptTypes";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptType Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj,conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData CheckLeadcurrency(string CurrencyId, string StrLeadCurrency, DateTime ValueDate)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                RcptTypeHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = RcptCreateHP.CheckLeadcurrency(CurrencyId,StrLeadCurrency, ValueDate);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "CheckLeadcurrency";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptType Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }
        [HttpGet]
        [Security]
        public ResponseData IsValidCustomerReceipt(int CustId, int ReceiptId)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                RcptTypeHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                ReturnObj.Data = RcptCreateHP.IsValidCustomerReceipt(CustId,ReceiptId);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                LogHistoryModel LogHistObj = new LogHistoryModel();
                string conString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "IsValidCustomerReceipt";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "RecieptType Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return ReturnObj;
        }

        [HttpPost]
        [Security]
        public ResponseData Save(ReseiptsModel saveObj)
        {
            ResponseData returnObj = new ResponseData();
            RcptCreateHP.SecurityconString = ControllerContext.RouteData.Values["SecurityContext"].ToString();
            try
            {
                db.SecurityConString= ControllerContext.RouteData.Values["SecurityContext"].ToString();
                KeyPair ReceiptDetail = db.GetReceiptDetail();
                //string OldReceiptNo = saveObj.RecieptNo;
                saveObj.RecieptDate = ReceiptDetail.Text;
                saveObj.RecieptNo = ReceiptDetail.Value;
                if (saveObj.CustomerId == null)
                    saveObj.CustomerId = -2;
                if (saveObj.AddressId == null)
                    saveObj.AddressId = 0;

                if (saveObj.WhatFor == null)
                    saveObj.WhatFor = "";
                if (saveObj.CurrencyId == null)
                    saveObj.CurrencyId = "";
                if (saveObj.TotalInWords == null)
                    saveObj.TotalInWords = "";
                if (saveObj.Total == null)
                    saveObj.Total = 0;
                if (saveObj.associationId == null)
                    saveObj.associationId = 0;
                if (saveObj.EmployeeId == null)
                    saveObj.EmployeeId = 0;
                if (saveObj.ThanksLetter == null)
                    saveObj.ThanksLetter = false;
                if (saveObj.ThanksLetterId == null)
                    saveObj.ThanksLetterId = 0;
                if (saveObj.PrinterId == null)
                    saveObj.PrinterId = 0;
                if (saveObj.OriginalWasPrinted == null)
                    saveObj.OriginalWasPrinted = false;
                if (saveObj.Credit4Digit == null)
                    saveObj.Credit4Digit = "";
                if (saveObj.StateId == null)
                    saveObj.StateId = "";
                if (saveObj.CityName == null)
                    saveObj.CityName = "";
                if (saveObj.CountryCode == null)
                    saveObj.CountryCode = "";
                if (saveObj.Street == null)
                    saveObj.Street = "";
                if (saveObj.Street2 == null)
                    saveObj.Street2 = "";
                if (saveObj.Zip == null)
                    saveObj.Zip = "";
                if (saveObj.fname == null)
                    saveObj.fname = "";
                if (saveObj.lname == null)
                    saveObj.lname = "";
                if (saveObj.Titel == null)
                    saveObj.Titel = "";
                if (saveObj.MiddleName == null)
                    saveObj.MiddleName = "";
                if (saveObj.Company == null)
                    saveObj.Company = "";
                if (saveObj.Safix == null)
                    saveObj.Safix = "";
                if (saveObj.Address_Remark == null)
                    saveObj.Address_Remark = "";
                if (saveObj.WhatForInThanksLet == null)
                    saveObj.WhatForInThanksLet = "";
                if (saveObj.LeadCurrency == null)
                    saveObj.LeadCurrency = "";
                if (saveObj.TotalInLeadCurrent == null)
                    saveObj.TotalInLeadCurrent = 0;
                if (saveObj.CustomizeLine == null)
                    saveObj.CustomizeLine = "";
                if (saveObj.ReceiptNoKeva == null)
                    saveObj.ReceiptNoKeva = "";
                if (saveObj.ReceiptTypeKeva == null)
                    saveObj.ReceiptTypeKeva = 0;
                if (saveObj.KeVaHistoryId == null)
                    saveObj.KeVaHistoryId = 0;
                if (saveObj.digitalEmployeeId == null)
                    saveObj.digitalEmployeeId = 0;
                if (saveObj.digitalfileName == null)
                    saveObj.digitalfileName = "";
                if (saveObj.digitalPath == null)
                    saveObj.digitalPath = "";
                if (saveObj.digitalDate == null)
                    saveObj.digitalDate = "";
                if (saveObj.RowDate == null)
                    saveObj.RowDate = "";
                if (saveObj.isCredit == null)
                    saveObj.isCredit = 0;
                int RowCount = 0;
                foreach (var ReciptLineObj in saveObj.ReceiptLines)
                {

                    //if (ReciptLineObj.RecieptRoWID == null)
                        ReciptLineObj.RecieptRoWID = RowCount++;
                    //if (ReciptLineObj.RecieptType == null)
                        ReciptLineObj.RecieptType = saveObj.RecieptType;

                    //if (ReciptLineObj.RecieptNo == null)
                        ReciptLineObj.RecieptNo = saveObj.RecieptNo;
                    if (ReciptLineObj.ProjectId == null)
                        ReciptLineObj.ProjectId = 0;
                    if (ReciptLineObj.PayTypeId == null)
                        ReciptLineObj.PayTypeId = 0;
                    if (ReciptLineObj.Amount == null)
                        ReciptLineObj.Amount = 0;
                    if (ReciptLineObj.USDVal == null)
                        ReciptLineObj.USDVal = 0;
                    if (ReciptLineObj.ValueDate == null)
                        ReciptLineObj.ValueDate = "";
                    if (ReciptLineObj.CheckNo == null)
                        ReciptLineObj.CheckNo = "";
                    if (ReciptLineObj.BankId == null)
                        ReciptLineObj.BankId = 0;
                    if (ReciptLineObj.BranchNo == null)
                        ReciptLineObj.BranchNo = "";
                    if (ReciptLineObj.AccountNo == null)
                        ReciptLineObj.AccountNo = "";
                    if (ReciptLineObj.details == null)
                        ReciptLineObj.details = "";
                    if (ReciptLineObj.DonationTypeId == null)
                        ReciptLineObj.DonationTypeId = 0;
                    if (ReciptLineObj.ImageName == null)
                        ReciptLineObj.ImageName = "";
                    if (ReciptLineObj.AccountId == null)
                        ReciptLineObj.AccountId = 0;
                    if (ReciptLineObj.CameFrom == null)
                        ReciptLineObj.CameFrom = 0;
                    if (ReciptLineObj.Bank == null)
                        ReciptLineObj.Bank = "";
                    if (ReciptLineObj.AmountInLeadCurrent == null)
                        ReciptLineObj.AmountInLeadCurrent = 0;
                    if (ReciptLineObj.ReferenceDate == null)
                        ReciptLineObj.ReferenceDate = "";
                    if (ReciptLineObj.OldReceiptId == null)
                        ReciptLineObj.OldReceiptId = 0;
                    if (ReciptLineObj.Payed == null)
                        ReciptLineObj.Payed = false;
                    if (ReciptLineObj.For_Invoice == null)
                        ReciptLineObj.For_Invoice = "";
                    if (ReciptLineObj.IsExport == null)
                        ReciptLineObj.IsExport = false;
                    if (ReciptLineObj.WasDeposit == null)
                        ReciptLineObj.WasDeposit = false;
                    if (ReciptLineObj.DepositeNo == null)
                        ReciptLineObj.DepositeNo = 0;
                    if (ReciptLineObj.DepositeDate == null)
                        ReciptLineObj.DepositeDate = "";
                    if (ReciptLineObj.DepositeToAccountId == null)
                        ReciptLineObj.DepositeToAccountId = 0;
                    if (ReciptLineObj.DepositeRemark == null)
                        ReciptLineObj.DepositeRemark = "";
                    if (ReciptLineObj.TotalDeposit == null)
                        ReciptLineObj.TotalDeposit = 0;
                    if (ReciptLineObj.KevaInstitute == null)
                        ReciptLineObj.KevaInstitute = "";
                    if (ReciptLineObj.CreditCardType == null)
                        ReciptLineObj.CreditCardType = "";
                    if (ReciptLineObj.RowDate == null)
                        ReciptLineObj.RowDate = "";
                }
                int g = RcptCreateHP.SaveReceipt(saveObj);
                if (g > 0)
                {
                    returnObj.Data = saveObj;
                    returnObj.IsError = false;
                    returnObj.ErrMsg = "Receipt has been successfully saved with ReceiptNo (" + saveObj.RecieptNo + ")";    
                }
                else {
                    returnObj.IsError = true;
                    returnObj.ErrMsg = "There is problem in saving";
                    
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
                LogHistObj.EmployeeId = Convert.ToInt32(ControllerContext.RouteData.Values["employeeid"].ToString());
                LogHistObj.OrgId = ControllerContext.RouteData.Values["OrgId"].ToString();
                LogHistObj.fname = ControllerContext.RouteData.Values["fname"].ToString();
                LogHistObj.Error = ex.Message;
                LogHistObj.ExcLine = frame.GetFileLineNumber();
                LogHistObj.ExcPlace = frame.GetFileColumnNumber();
                LogHistObj.Action = "Save";
                LogHistObj.FullDescription = ex.ToString();
                LogHistObj.ExeptionType = "ERROR";
                LogHistObj.APIVersion = AppConfig.APIVersion;
                LogHistObj.FromPage = "Receipt Controller";
                LogHistObj.OnDate = System.DateTime.Now;
                LogHistObj.ex = ex;
                SendEmail.SendEmailErr(LogHistObj, conString);
            }
            return returnObj;
        }
    }
}