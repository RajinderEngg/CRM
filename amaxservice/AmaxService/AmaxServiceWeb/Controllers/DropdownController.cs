using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AmaxService.HelperClasses;
using AmaxDataService.DataModel;

namespace AmaxServiceWeb.Controllers
{
    public class DropdownController : ApiController
    {
        // GET api/<controller>
        DataBind db;
        public DropdownController()
        {
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
        public ResponseData BindCustType()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetCustType();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindEmployees()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetEmployees();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindSources()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetCustSources();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindSuffixes()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetSuffixes();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindPhoneTypes()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetPhoneTypes();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindAddressType()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetAddressType();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindGroups()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetGroups();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindCountries()
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetCountries();
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindStates(string countryName=null)
        {

            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetStates(countryName);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
        [HttpGet]
        public ResponseData BindCities(string CountryCode=null,string stateName=null)
        {
            ResponseData ReturnObj = new ResponseData();
            try
            {
                ReturnObj.Data = db.GetCities(CountryCode,stateName);
                ReturnObj.IsError = false;
                ReturnObj.ErrMsg = "";
            }
            catch (Exception ex)
            {
                ReturnObj.Data = null;
                ReturnObj.IsError = true;
                ReturnObj.ErrMsg = ex.Message;
            }
            return ReturnObj;
        }
    }
}