using AmaxExtentions.DbAccess;
using AmaxExtentions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Data;
using System.Linq;
using AmaxService.ServiceModels;
using System.Xml.Linq;
using AmaxService.ThirdPartyApi;
using System.Globalization;
using jsonQ;
using AmaxDataService.DataModel;

namespace AmaxService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class CrmService : ICrmService
    {
        #region Variable And Constructors
        private dynamic responce;
        public bool IsValid;

        public Dictionary<string, object> currentUser { get; set; }
        public string SecurityConnection { get; set; }

        public CrmService()
        {
            responce = new ExpandoObject();
            responce.cost = DateTime.Now;
            responce.error = "";
            IsValid = true;
            //if (WebOperationContext.Current.IncomingRequest.Headers["X-Token"] != null)
            //    try
            //    {
            //        currentUser = XTokenizer.ValidateToken(WebOperationContext.Current.IncomingRequest.Headers["X-Token"], true);
            //        currentUser["SecurityContext"] = StringCipher.Decrypt(currentUser["SecurityContext"].ToString());
            //        IsValid = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        responce.error = ex.Message;
            //    }
        }
        #endregion

        #region Utilitys
        private string getConnectionString() => currentUser["SecurityContext"].ToString();
        private string getControllDbConnectionString() => ConfigurationManager.ConnectionStrings["ControllDb"].ConnectionString;
        private DbAccess getDbAccess() => new DbAccess(getConnectionString());
        private DbAccess getControllDbAccess() => new DbAccess(getControllDbConnectionString());

        private Stream responceDispatcher(dynamic data)
        {
            responce.data = data;
            return responceDispatcher();
        }
        private Stream responceDispatcher()
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";

            WebOperationContext.Current.OutgoingResponse.StatusCode = IsValid || WebOperationContext.Current.IncomingRequest.Method == "OPTIONS" ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.Unauthorized;
            responce.cost = (DateTime.Now - responce.cost).Milliseconds;

            return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responce)));
        }

        private string getServiceVersion() {
            return System.Reflection
                .Assembly.GetExecutingAssembly()
                .GetName()
                .Version
                .ToString();
        }
        #endregion

        #region Service Meathods
        public object Login(LoginModel obj)
        {
            try
            {
                //Dictionary<string, Object> parameter = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj);

                string connection_str = "";
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();
                using (DbAccess db = new DbAccess(getControllDbConnectionString()))
                {
                    parameterDictionary.Add("OrgId", obj.OrgId);
                    connection_str = Convert.ToString(db.ExecuteScalar("SELECT ConnectionString FROM Organization WHERE OrganizationId = @OrgId", parameterDictionary));
                    if (connection_str != "")
                        using (DbAccess da = new DbAccess(connection_str))
                        {
                            parameterDictionary.Clear();
                            parameterDictionary.Add("fname", obj.UserName);
                            parameterDictionary.Add("pass", obj.Password);
                            DataTable dt = da.GetDataTable(@"SELECT TOP 1 employeeid, eType, sysuser, fname, dayEnd, sex, sysdata, UseMobile, IsDigital,TimeZone, lname,
                            dayStart, email, Branchid, DepartmentId, (SELECT CONVERT(bit, CASE WHEN COUNT(Branchid) > 0 THEN 1 ELSE 0 END) AS IsBranchEnabled FROM Branches)
                            AS IsBranchEnabled,authoType FROM Employees WHERE fname = @fname AND CRMPassword = @pass", parameterDictionary);
                            if (dt.Rows.Count == 1)
                            {
                                dt.Columns.Add("SecurityContext");
                                
                                dt.Rows[0]["SecurityContext"] = StringCipher.Encrypt(connection_str);
                                dt.Columns.Add("Language");
                                dt.Rows[0]["Language"] = obj.Language;
                                dt.Columns.Add("OrgId");
                                dt.Rows[0]["OrgId"] = obj.OrgId;
                                responce.token = XTokenizer.Tokenize(dt.Rows[0].ToDynamicObject().Table[0]);
                                IsValid = true;

                                //Version Managements
                                responce.DBVERSION_NET = da.ExecuteScalar("SELECT TOP 1 DBVERSION_NET FROM ApplicationInfo", null);
                                responce.ServiceVersion = getServiceVersion();
                                responce.DatAccessVersion = da.getLibVersion();
                                

                            }
                            else responce.error = "Invalid Credentials";
                        }
                    else
                        responce.error = "Invalid Organization name";
                }
            }
            catch (Exception ex)
            {
                responce.error = "Invalid Data" + ex.Message;
            }
            return responce; //responceDispatcher();
        }

        public object AmaxReportingService(dynamic Parameter)
        {
            if (IsValid)
                try
                {
                    Dictionary<string, Object> parameter = JsonConvert.DeserializeObject<Dictionary<string, object>>(new StreamReader(Parameter).ReadToEnd());
                    Dictionary<string, object> ReportParameters = new Dictionary<string, object>();
                    if (parameter.ContainsKey("Parameters")) ReportParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(parameter["Parameters"].ToString());

                    string strSql = "";
                    switch (parameter["ReportName"].ToString())
                    {
                        case "Accounts":
                            strSql += @"SELECT A.AccountId, AT.AccountTypeName, AT.AccountTpeNameEng, A.AccountNameEng, A.AccountName,
                            A.AccountDetail FROM Accounts AS A INNER JOIN AccountType AS AT ON AT.AccountTypeId = A.AccountTypeId";
                            break;
                        case "AccountType":
                            strSql += @"SELECT * FROM AccountType";
                            break;
                        default:
                            responce.data = new object[] { };
                            responce.error = "Invalid Report Name";
                            break;
                    }
                    if (strSql != "")
                        using (DbAccess da = new DbAccess(getConnectionString()))
                        {
                            responce.data = da.GetDataSet(strSql, ReportParameters, false).Tables;
                        }
                }
                catch (Exception)
                {
                    responce.error = "Invalid Data parsed to server";
                }
            return responce;//responceDispatcher();
        }

        public object ExecuteJson(dynamic Parameter)
        {
            if (IsValid)
                try
                {
                    JSONProcessor jproc = new JSONProcessor(JsonConvert.DeserializeObject<JQL>(new StreamReader(Parameter).ReadToEnd()), getConnectionString());

                    jproc.ProcessJSON();
                    dynamic data = new ExpandoObject();
                    data.status = jproc.OperationState;
                    data.keys = jproc.Keys;
                    data.Operations = jproc.OperationsCount;
                    data.ErrorMessage = jproc.ErrorMessage;
                    //data.Result = jproc.Data;
                    responce.execution = data;
                }
                catch (Exception err)
                {
#if DEBUG
                    responce.error = err.Message;
#endif
                }
            return responce;//responceDispatcher();
        }

        public object GetTreeData(dynamic Parameter)
        {
            var paramters = Parameter;//.ToTypeof<dynamic>();
            string language;
            try
            {
                language = paramters.Lang ?? "";
            }
            catch (Exception ex)
            {
                language = "";
            }; // EmptyString if null
            if (IsValid)
            {
                DataTable dt;
                using (DbAccess da = getDbAccess())
                {
                    dt = da.GetDataTable("SELECT GroupId, GroupName, GroupNameEng, GroupParenCategory FROM CustomerGroupsGeneral WHERE ( IsSupport = 0 and Ishide = 0 and SecurityLevel <= 10) ORDER BY GroupName", null);
                }
                List<dynamic> x = JsonConvert.SerializeObject(dt).ToTypeof<List<dynamic>>();

                var kendoTreeRoot = x.Where(e => e.GroupId == 0).Select(e => new KendoGroupTree()
                {
                    id = e.GroupId,
                    text = language == "en" ? e.GroupNameEng : e.GroupName,
                    expanded = true
                }).ToList<KendoGroupTree>();
                kendoTreeRoot.ForEach(a =>
                {
                    a.items = x.Where(e => e.GroupParenCategory == a.id).Select(e => new KendoGroupTree()
                    {
                        id = e.GroupId,
                        text = language == "en" ? e.GroupNameEng : e.GroupName,
                    }).ToList<KendoGroupTree>();
                    a.items.ForEach(b =>
                    {
                        b.items = x.Where(e => e.GroupParenCategory == b.id).Select(e => new KendoGroupTree()
                        {
                            id = e.GroupId,
                            text = language == "en" ? e.GroupNameEng : e.GroupName,
                        }).ToList<KendoGroupTree>();

                        b.items.ForEach(c =>
                        {
                            c.items = x.Where(e => e.GroupParenCategory == c.id).Select(e => new KendoGroupTree()
                            {
                                id = e.GroupId,
                                text = language == "en" ? e.GroupNameEng : e.GroupName,
                            }).ToList<KendoGroupTree>();

                            c.items.ForEach(d =>
                            {
                                d.items = x.Where(e => e.GroupParenCategory == d.id).Select(e => new KendoGroupTree()
                                {
                                    id = e.GroupId,
                                    text = language == "en" ? e.GroupNameEng : e.GroupName,
                                }).ToList<KendoGroupTree>();
                                d.items.ForEach(f =>
                                {
                                    f.items = x.Where(e => e.GroupParenCategory == f.id).Select(e => new KendoGroupTree()
                                    {
                                        id = e.GroupId,
                                        text = language == "en" ? e.GroupNameEng : e.GroupName,
                                    }).ToList<KendoGroupTree>();
                                    f.items.ForEach(g =>
                                    {
                                        g.items = x.Where(e => e.GroupParenCategory == g.id).Select(e => new KendoGroupTree()
                                        {
                                            id = e.GroupId,
                                            text = language == "en" ? e.GroupNameEng : e.GroupName,
                                        }).ToList<KendoGroupTree>();
                                        g.items.ForEach(h =>
                                        {
                                            h.items = x.Where(e => e.GroupParenCategory == h.id).Select(e => new KendoGroupTree()
                                            {
                                                id = e.GroupId,
                                                text = language == "en" ? e.GroupNameEng : e.GroupName,
                                            }).ToList<KendoGroupTree>();
                                            h.items.ForEach(i =>
                                            {
                                                i.items = x.Where(e => e.GroupParenCategory == i.id).Select(e => new KendoGroupTree()
                                                {
                                                    id = e.GroupId,
                                                    text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                }).ToList<KendoGroupTree>();
                                                i.items.ForEach(j =>
                                                {
                                                    j.items = x.Where(e => e.GroupParenCategory == j.id).Select(e => new KendoGroupTree()
                                                    {
                                                        id = e.GroupId,
                                                        text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                    }).ToList<KendoGroupTree>();
                                                    j.items.ForEach(k =>
                                                    {
                                                        k.items = x.Where(e => e.GroupParenCategory == k.id).Select(e => new KendoGroupTree()
                                                        {
                                                            id = e.GroupId,
                                                            text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                        }).ToList<KendoGroupTree>();
                                                        k.items.ForEach(l =>
                                                        {
                                                            l.items = x.Where(e => e.GroupParenCategory == l.id).Select(e => new KendoGroupTree()
                                                            {
                                                                id = e.GroupId,
                                                                text = language == "en" ? e.GroupNameEng : e.GroupName,
                                                            }).ToList<KendoGroupTree>();
                                                        });
                                                    });
                                                });
                                            });
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
                responce.kendoTree = kendoTreeRoot;
            }
            return responce;// responceDispatcher();
        }

        public object SendSms_Old(dynamic Parameter)
        {
            dynamic responceData = new ExpandoObject();
            if (!IsValid) return responceData;//responceDispatcher();


            #region Initializing and validating payload
            var payload = Parameter; //Parameter.ReadAllText().ToTypeof<dynamic>();

            if (payload.username == null || payload.company == null || payload.message == null
                || payload.groups == null || payload.phoneType == null || payload.providerId == null || payload.returnBalanceAndCustomerCount == null)
            {
                responceData.err = "Invalid data for sending sms";
                return responceData;// responceDispatcher(responceData);             Done on 08/12/2016
            }

            string sender = payload.SenderPhoneNumber;
            if (sender.Length != 10)
            {
                responceData.err = "Sender Phone number must be 10 digits";
                return responceData; // responceDispatcher(responceData);             Done on 08/12/2016
            }
            #endregion

            #region Fetching data for processing
            string Sql_ValidateSmsCredentialsForSendingSms = @"SELECT usersms, passwordsms, Osek as FromMobile From ApplicationInfo WHERE usersms = @Username AND companysms = @Company";
            //string Sql_SelectCustomersForSpecifiedGroups = @"SELECT C.CustomerId, C.FileAs, CP.Phone, CP.* FROM Customers C INNER JOIN CustomerPhones CP ON
            //        c.CustomerId = CP.CustomerId WHERE CP.PhoneTypeId = @PhoneTypeId AND C.CustomerId IN (SELECT DISTINCT Customerid FROM CustomerGroupsGeneralSet
            //        WHERE CustomerGeneralGroupId IN (@GroupIdArr))";
            string Sql_SelectCustomersForSpecifiedGroups = @"SELECT Sms.CustomerId, Sms.FileAs, Sms.CelPhone FROM (SELECT C.CustomerId, C.FileAs, 
                REPLACE(REPLACE(REPLACE(CP.Area+CP.Phone,'-',''),' ',''),'.','') AS CelPhone FROM Customers C INNER JOIN CustomerPhones CP ON c.CustomerId = CP.CustomerId 
                WHERE CP.PhoneTypeId @PhoneTypeId AND (C.Deceased = 0) AND (C.Deleted = 0) AND (C.ActiveStatus = 0)AND C.CustomerId IN (SELECT DISTINCT Customerid FROM
                CustomerGroupsGeneralSet WHERE CustomerGeneralGroupId IN (@GroupIdArr)) @BranchData) AS Sms WHERE LEN(Sms.CelPhone) = 10;";
            //Checking for SysData privilges and branch
            if ((bool)currentUser["IsBranchEnabled"])
                Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@BranchData", (bool)currentUser["sysdata"] ? "" :
                    "AND C.CustomerId IN (SELECT CustomerId FROM CustomersBranches WHERE Branchid = @BranchId)".Replace("@BranchId", currentUser["Branchid"].ToString()));
            else
                Sql_SelectCustomersForSpecifiedGroups.Replace("@BranchData", "");
            //Checking for PhoneType
            if (Convert.ToInt32(payload.phoneType) == 0) Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@PhoneTypeId", " IN  (SELECT id FROM PhoneTypes WHERE CellPhone = 1)");
            else Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@PhoneTypeId", " = @PhoneTypeId");

            
            DataTable dtSmsCredentials = null, dtSelectedCustomers = null;

            using (var da = getDbAccess())
            {
                dtSmsCredentials = da.GetDataTable(Sql_ValidateSmsCredentialsForSendingSms,
                    new { Username = payload.username, Company = payload.company }.ToJson().ToTypeof<Dictionary<string, object>>());
                if (dtSmsCredentials != null && dtSmsCredentials.Rows.Count > 0)
                {
                    string GroupIdArr = payload.groups.ToString();
                    GroupIdArr = GroupIdArr.Replace("\r\n", "").Replace("[", "").Replace("]", ""); //.Replace("\r\n","").Substring(1, GroupIdArr.Length - 2);
                    Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@GroupIdArr", GroupIdArr);

                    dtSelectedCustomers = da.GetDataTable(Sql_SelectCustomersForSpecifiedGroups,
                        new { PhoneTypeId = payload.phoneType }.ToJson().ToTypeof<Dictionary<string, object>>());
                    for (int i = 0; i < dtSelectedCustomers.Rows.Count; i++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (j != i && dtSelectedCustomers.Rows[i]["CelPhone"].ToString().Trim() == dtSelectedCustomers.Rows[j]["CelPhone"].ToString().Trim())
                            {
                                dtSelectedCustomers.Rows.Remove(dtSelectedCustomers.Rows[i]);
                                i--;
                            }
                        }
                    }

                    responceData.TotalCustomers = dtSelectedCustomers.Rows.Count;
                    responceData.Customers = dtSelectedCustomers;
                }
                else
                {
                    responceData.err = "Sms Subscription not found";
                    return responceDispatcher(responceData);
                }
            }
            #endregion

            DateTime sendlaterDateTime = DateTime.Parse("01/01/1970");
            if (payload.sendlater != null && payload.sendlater != "")
                sendlaterDateTime = DateTime.ParseExact(((string)payload.sendlater), "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);


            int providerId = payload.providerId;
            int remainingCreaditsForSms = 0;
            string serviceResponce = "";
            XElement SendSmsXml;
            switch (providerId)
            {
                case 1:
                    #region SMS2You API
                    ThirdPartyApi.SendSMS sms2you = new ThirdPartyApi.SendSMS();

                    #region Building Balance checking XML and checking remaining credit's for SMS
                    if (Convert.ToBoolean(payload.returnBalanceAndCustomerCount))
                    {
                        try
                        {
                            serviceResponce = sms2you.COMMANDS(
                                new XElement("SMS",
                                    new XElement("CMD", new XCData("CREDITS")),
                                    new XElement("USERNAME", new XCData(dtSmsCredentials.Rows[0]["usersms"].ToString())),
                                    new XElement("PASSWORD", new XCData(dtSmsCredentials.Rows[0]["passwordsms"].ToString()))
                                ).ToString());
                            remainingCreaditsForSms = Convert.ToInt32(serviceResponce.Substring(serviceResponce.IndexOf(":") + 1).Trim());
                        }
                        catch (Exception)
                        {
                            responceData.ststue = 0;
                        }
                        responceData.RemainingCredit = remainingCreaditsForSms;
                        break;
                    }
                    #endregion

                    #region Creating XML for SMS sending
                    try
                    {
                        SendSmsXml = new XElement("SMS",
                                        new XElement("USERNAME", new XCData(dtSmsCredentials.Rows[0]["usersms"].ToString())),
                                        new XElement("PASSWORD", new XCData(dtSmsCredentials.Rows[0]["passwordsms"].ToString())),
                                        new XElement("SENDER_PREFIX", new XCData("ALFA")),
                                        new XElement("SENDER_SUFFIX", new XCData(sender)),
                                        new XElement("MSGLNG", "HEB"),
                                        new XElement("MSG", new XCData(payload.message.ToString()))
                                    );
                        XElement mobileList = new XElement("MOBILE_LIST");
                        for (int i = 0; i < dtSelectedCustomers.Rows.Count; i++)
                        {
                            mobileList.Add(new XElement("MOBILE_NUMBER", dtSelectedCustomers.Rows[i]["CelPhone"]));
                        }
                        SendSmsXml.Add(mobileList);
                        //demo
                        //SendSmsXml.Add(new XElement("MOBILE_LIST",
                        //                new XElement("MOBILE_NUMBER", "0526470404"),
                        //                new XElement("MOBILE_NUMBER", "0545416075"))
                        //            );

                        SendSmsXml.Add(
                                        new XElement("UNIODE", "Fasle"),
                                        new XElement("USE_PERSONAL", "False")
                                    );
                        if(sendlaterDateTime.Year != 1970)
                                SendSmsXml.Add(new XElement("SEND_ON_DATE", sendlaterDateTime.ToString("dd-MM-yyyy HH:mm")));

                        serviceResponce = sms2you.SUBMITSMS(SendSmsXml.ToString());
                        if (serviceResponce.Contains("Submit OK"))
                        {
                            responceData.status = 1;
                            if (sendlaterDateTime.Year != 1970)
                                responceData.message = "Message is scheduled to deliver " + dtSelectedCustomers.Rows.Count + " celphones on "
                                    + sendlaterDateTime.ToString("dd-MM-yyyy HH:mm");
                            else
                                responceData.message = "Message is succesfully sended to " + dtSelectedCustomers.Rows.Count + " celphones";
                        }
                        else throw new Exception(serviceResponce);
                    }
                    catch (Exception ex)
                    {
                        responceData.status = 0;
                        responceData.err = ex.Message;
                        break;
                    }
                    #endregion

                    #endregion
                    break;
                case 2:
                    #region 019Sms API

                    #region Building Balance checking XML and checking remaining credit's for SMS
                    if (Convert.ToBoolean(payload.returnBalanceAndCustomerCount))
                    {
                        try
                        {
                            serviceResponce = SmsApi019.TelzarSendMessages(
                                new XElement("balance",
                                        new XElement("user",
                                            new XElement("username", dtSmsCredentials.Rows[0]["usersms"].ToString()),
                                            new XElement("password", dtSmsCredentials.Rows[0]["passwordsms"].ToString()
                                    )
                                )
                            ).ToString());
                            if (XElement.Parse(serviceResponce).Element("status").Value == "0")
                                remainingCreaditsForSms = Convert.ToInt32(XElement.Parse(serviceResponce).Element("balance").Value);
                            else
                                responceData.ststue = 0;
                            responceData.RemainingCredit = remainingCreaditsForSms;
                        }
                        catch (Exception ex)
                        {
                            responceData.err = ex.Message;
                            break;
                        }
                        break;
                    }
                    #endregion

                    #region Creating Xml for sms sending
                    try
                    {
                        SendSmsXml = new XElement("sms",
                            new XElement("user",
                                new XElement("username", dtSmsCredentials.Rows[0]["usersms"].ToString()),
                                new XElement("password", dtSmsCredentials.Rows[0]["passwordsms"].ToString())
                            ),
                            new XElement("source", sender)
                        );

                        XElement mobileList = new XElement("destinations");
                        for (int i = 0; i < dtSelectedCustomers.Rows.Count; i++)
                        {
                            mobileList.Add(new XElement("phone", dtSelectedCustomers.Rows[i]["CelPhone"]));
                        }
                        SendSmsXml.Add(mobileList);
                        //demo
                        //SendSmsXml.Add(
                        //        new XElement("destinations",
                        //            new XElement("phone", "0526470404"),
                        //            new XElement("phone", "0545416075")
                        //        )
                        //    );

                        SendSmsXml.Add(
                                new XElement("message", payload.message.ToString()),
                                new XElement("add_unsubscribe", "0"),
                                new XElement("response", 0)
                            );

                        if (sendlaterDateTime.Year != 1970)
                            SendSmsXml.Add(new XElement("timing", sendlaterDateTime.ToString("dd/MM/yy HH:mm")));

                        serviceResponce = SmsApi019.TelzarSendMessages(SendSmsXml.ToString());
                        if (XElement.Parse(serviceResponce).Element("status").Value == "0")
                        {
                            responceData.status = 1;
                            if (sendlaterDateTime.Year != 1970)
                                responceData.message = "Message is scheduled to deliver " + dtSelectedCustomers.Rows.Count + " on "
                                    + sendlaterDateTime.ToString("dd-MM-yyyy HH:mm");
                            else
                                responceData.message = "Message is succesfully sended to " + dtSelectedCustomers.Rows.Count + " celphones";

                        }
                        else throw new Exception(serviceResponce);
                    }
                    catch (Exception ex)
                    {
                        responceData.status = 0;
                        responceData.err = ex.Message;
                        break;
                    }

                    #endregion

                    #endregion
                    break;
                default:
                    responceData.error = "Unknown SMS provider";
                    break;
            }

            return responceData;// responceDispatcher(responceData);
        }



        public object SendSms(dynamic Parameter)
        {
            
            dynamic responceData = new ExpandoObject();
            if (!IsValid) return responceData;//responceDispatcher();
            #region Initializing and validating payload
            var payload = Parameter;//.ReadAllText().ToTypeof<dynamic>();

            if (payload.username == null || payload.company == null || payload.message == null
                || payload.groups == null || payload.phoneType == null || payload.providerId == null || payload.returnBalanceAndCustomerCount == null)
            {
                responceData.err = "Invalid data for sending sms";
                return responceData;//responceDispatcher(responceData);
            }

            string sender = payload.SenderPhoneNumber;
            if (sender.Length != 10)
            {
                responceData.err = "Sender Phone number must be 10 digits";
                return responceData;//responceDispatcher(responceData);
            }
            #endregion

            #region Fetching data for processing
            var Sql_ValidateSmsCredentialsForSendingSms = @"SELECT usersms, passwordsms,"+
            "Osek as FromMobile From ApplicationInfo"+
            " WHERE usersms = @Username AND passwordsms = @Company";

            var Sql_SelectCustomersForSpecifiedGroups = @"SELECT Sms.CustomerId, Sms.FileAs, Sms.CelPhone FROM (SELECT C.CustomerId, C.FileAs, 
                REPLACE(REPLACE(REPLACE(CP.Area+CP.Phone,'-',''),' ',''),'.','') AS CelPhone FROM Customers C INNER JOIN CustomerPhones CP ON c.CustomerId = CP.CustomerId 
                WHERE CP.PhoneTypeId @PhoneTypeId AND (C.Deceased = 0) AND (C.Deleted = 0) AND (C.ActiveStatus = 0)AND C.CustomerId IN (SELECT DISTINCT Customerid FROM
                CustomerGroupsGeneralSet WHERE CustomerGeneralGroupId IN (@GroupIdArr)) @BranchData) AS Sms WHERE LEN(Sms.CelPhone) = 10;";

            //Checking for SysData privilges and branch
            if (Convert.ToBoolean(currentUser["IsBranchEnabled"])==true)////currentUser["IsBranchEnabled"]  payload.IsBranchEnabled
                Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@BranchData",Convert.ToBoolean( currentUser["sysdata"]) ? "" :
                    "AND C.CustomerId IN (SELECT CustomerId FROM CustomersBranches WHERE Branchid = @BranchId)".Replace("@BranchId", currentUser["Branchid"].ToString()));///currentUser["Branchid"]  payload.Branchid
            else
                Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@BranchData", "");

            //Checking for PhoneType
            if (Convert.ToInt32(payload.phoneType) == 0) Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@PhoneTypeId", " IN  (SELECT id FROM PhoneTypes WHERE CellPhone = 1)");
            else Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@PhoneTypeId", " = @PhoneTypeId");


            DataTable dtSmsCredentials = null, dtSelectedCustomers = null;

            using (var da = getDbAccess())
            {
                dtSmsCredentials = da.GetDataTable(Sql_ValidateSmsCredentialsForSendingSms,
                    new { Username = payload.username, Company = payload.company }.ToJson().ToTypeof<Dictionary<string, object>>());
                if (dtSmsCredentials != null && dtSmsCredentials.Rows.Count > 0)
                {
                    string GroupIdArr = payload.groups.ToString();
                    GroupIdArr = GroupIdArr.Replace("\r\n", "").Replace("[", "").Replace("]", ""); //.Replace("\r\n","").Substring(1, GroupIdArr.Length - 2);
                    Sql_SelectCustomersForSpecifiedGroups = Sql_SelectCustomersForSpecifiedGroups.Replace("@GroupIdArr", GroupIdArr);

                    dtSelectedCustomers = da.GetDataTable(Sql_SelectCustomersForSpecifiedGroups,
                        new { PhoneTypeId = payload.phoneType }.ToJson().ToTypeof<Dictionary<string, object>>());
                    for (int i = 0; i < dtSelectedCustomers.Rows.Count; i++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (j != i && dtSelectedCustomers.Rows[i]["CelPhone"].ToString().Trim() == dtSelectedCustomers.Rows[j]["CelPhone"].ToString().Trim())
                            {
                                dtSelectedCustomers.Rows.Remove(dtSelectedCustomers.Rows[i]);
                                i--;
                            }
                        }
                    }

                    responceData.TotalCustomers = dtSelectedCustomers.Rows.Count;
                    responceData.Customers = dtSelectedCustomers;
                }
                else
                {
                    responceData.err = "Sms Subscription not found";
                    return responceData;//responceDispatcher(responceData);
                }
            }
            #endregion

            var sendlaterDateTime = DateTime.Parse("01/01/1970");
            if (payload.sendlater != null && payload.sendlater != "")
                sendlaterDateTime = DateTime.ParseExact(((string)payload.sendlater), "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);


            int providerId = payload.providerId;
            var remainingCreaditsForSms = 0;
            var serviceResponce = "";
            XElement SendSmsXml;
            switch (providerId)
            {
                

                case 1:
                    #region SMS2You API
                   var sms2you = new SendSMS();

                    #region Building Balance checking XML and checking remaining credit's for SMS
                    if (Convert.ToBoolean(payload.returnBalanceAndCustomerCount))
                    {
                        try
                        {
                            serviceResponce = sms2you.COMMANDS(
                                new XElement("SMS",
                                    new XElement("CMD", new XCData("CREDITS")),
                                    new XElement("USERNAME", new XCData(dtSmsCredentials.Rows[0]["usersms"].ToString())),
                                    new XElement("PASSWORD", new XCData(dtSmsCredentials.Rows[0]["passwordsms"].ToString()))
                                ).ToString());

                            

                            remainingCreaditsForSms = Convert.ToInt32(serviceResponce.Substring(serviceResponce.IndexOf(":") + 1).Trim());
                        }
                        catch (Exception ex)
                        {
                            responceData.ststue = 0;
                        }
                        responceData.RemainingCredit = remainingCreaditsForSms;
                        break;
                    }
                    #endregion

                    #region Creating XML for SMS sending
                    try
                    {
                        SendSmsXml = new XElement("SMS",
                                        new XElement("USERNAME", new XCData(dtSmsCredentials.Rows[0]["usersms"].ToString())),
                                        new XElement("PASSWORD", new XCData(dtSmsCredentials.Rows[0]["passwordsms"].ToString())),
                                        new XElement("SENDER_PREFIX", new XCData("ALFA")),
                                        new XElement("SENDER_SUFFIX", new XCData(sender)),
                                        new XElement("MSGLNG", "HEB"),
                                        new XElement("MSG", new XCData(payload.message.ToString()))
                                    );
                        var mobileList = new XElement("MOBILE_LIST");
                        for (int i = 0; i < dtSelectedCustomers.Rows.Count; i++)
                        {
                            mobileList.Add(new XElement("MOBILE_NUMBER", dtSelectedCustomers.Rows[i]["CelPhone"]));
                        }
                        SendSmsXml.Add(mobileList);
                        //demo
                        //SendSmsXml.Add(new XElement("MOBILE_LIST",
                        //                new XElement("MOBILE_NUMBER", "0526470404"),
                        //                new XElement("MOBILE_NUMBER", "0522576091"))
                        //            );

                        SendSmsXml.Add(
                                        new XElement("UNIODE", "Fasle"),
                                        new XElement("USE_PERSONAL", "False")
                                    );
                        if (sendlaterDateTime.Year != 1970)
                            SendSmsXml.Add(new XElement("SEND_ON_DATE", sendlaterDateTime.ToString("dd-MM-yyyy HH:mm")));

                        serviceResponce = sms2you.SUBMITSMS(SendSmsXml.ToString());
                        if (serviceResponce.Contains("Submit OK"))
                        {
                            responceData.status = 1;
                            if (sendlaterDateTime.Year != 1970)
                                responceData.message = "Message is scheduled to deliver " + dtSelectedCustomers.Rows.Count + " celphones on "
                                    + sendlaterDateTime.ToString("dd-MM-yyyy HH:mm");
                            else
                                responceData.message = "Message is succesfully sended to " + dtSelectedCustomers.Rows.Count + " celphones";
                        }
                        else throw new Exception(serviceResponce);
                    }
                    catch (Exception ex)
                    {
                        responceData.status = 0;
                        responceData.err = ex.Message;
                        break;
                    }
                    #endregion

                    #endregion
                    break;
                case 2:
                    #region 019Sms API

                    #region Building Balance checking XML and checking remaining credit's for SMS
                    if (Convert.ToBoolean(payload.returnBalanceAndCustomerCount))
                    {
                        try
                        {
                            string username = dtSmsCredentials.Rows[0]["usersms"].ToString();
                            string psw = dtSmsCredentials.Rows[0]["passwordsms"].ToString();
                            serviceResponce = SmsApi019.TelzarSendMessages(
                                new XElement("balance",
                                        new XElement("user",
                                            new XElement("username", dtSmsCredentials.Rows[0]["usersms"].ToString()),
                                            new XElement("password", dtSmsCredentials.Rows[0]["passwordsms"].ToString()
                                    )
                                )
                            ).ToString());
                            if (XElement.Parse(serviceResponce).Element("status").Value == "0")
                                remainingCreaditsForSms = Convert.ToInt32(XElement.Parse(serviceResponce).Element("balance").Value);
                            else
                                responceData.ststue = 0;
                            responceData.RemainingCredit = remainingCreaditsForSms;
                        }
                        catch (Exception ex)
                        {
                            responceData.err = ex.Message;
                            break;
                        }
                        break;
                    }
                    #endregion

                    #region Creating Xml for sms sending
                    try
                    {
                        SendSmsXml = new XElement("sms",
                            new XElement("user",
                                new XElement("username", dtSmsCredentials.Rows[0]["usersms"].ToString()),
                                new XElement("password", dtSmsCredentials.Rows[0]["passwordsms"].ToString())
                            ),
                            new XElement("source", sender) //sender
                        );

                        var mobileList = new XElement("destinations");
                        for (int i = 0; i < dtSelectedCustomers.Rows.Count; i++)
                        {
                            mobileList.Add(new XElement("phone", dtSelectedCustomers.Rows[i]["CelPhone"]));
                        }
                        SendSmsXml.Add(mobileList);
                        //demo
                        //SendSmsXml.Add(
                        //        new XElement("destinations",
                        //            new XElement("phone", "0526470404"),
                        //            new XElement("phone", "0522576091")
                        //        )
                        //    );

                        SendSmsXml.Add(
                                new XElement("message", payload.message.ToString()),
                                new XElement("add_unsubscribe", "0"),
                                new XElement("response", 0)
                            );

                        if (sendlaterDateTime.Year != 1970)
                            SendSmsXml.Add(new XElement("timing", sendlaterDateTime.ToString("dd/MM/yy HH:mm")));

                        serviceResponce = SmsApi019.TelzarSendMessages(SendSmsXml.ToString());
                        if (XElement.Parse(serviceResponce).Element("status").Value == "0")
                        {
                            responceData.status = 1;
                            if (sendlaterDateTime.Year != 1970)
                                responceData.message = "Message is scheduled to deliver " + dtSelectedCustomers.Rows.Count + " on "
                                    + sendlaterDateTime.ToString("dd-MM-yyyy HH:mm");
                            else
                                responceData.message = "Message is succesfully sended to " + dtSelectedCustomers.Rows.Count + " celphones";

                        }
                        else throw new Exception(serviceResponce);
                    }
                    catch (Exception ex)
                    {
                        responceData.status = 0;
                        responceData.err = ex.Message;
                        break;
                    }

                    #endregion

                    #endregion
                    break;
                default:
                    responceData.error = "Unknown SMS provider";
                    break;
            }

            return responceData;//responceDispatcher(responceData);
        }



        public object ExecuteDataService(dynamic Parameter)
        {
            Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
            try
            {
                var payloadData = Parameter.ToTypeof<Dictionary<string, object>>();
                string queryName = payloadData["query"].ToString();
                var parameterDictionary = payloadData["parameters"].ToString().ToTypeof<Dictionary<string, object>>();
                List<string> tables = new List<string>();
                string strSql = "";
                switch (queryName)
                {
                    case "InitalizeCRM":
                        //SmsCompanyList
                        strSql += "Select usersms AS UserName, companysms AS Value from ApplicationInfo;";
                        //CellPhoneTypeList
                        strSql += "SELECT 0 AS Value, 'את כל (All)' as Label UNION ALL SELECT id AS Value, contentHeb+' ('+ contenteng +')' AS Label FROM PhoneTypes WHERE CellPhone = 1;";
                        //PhoneTypeList
                        strSql += "SELECT id AS Value, contentHeb+' ('+ contenteng +')' AS Label FROM PhoneTypes;";
                        //GeneralGroupData
                        strSql += "SELECT GroupId, GroupName, GroupNameEng, GroupParenCategory FROM CustomerGroupsGeneral WHERE ( IsSupport = 0 and Ishide = 0 and SecurityLevel <= "+currentUser["authoType"]+") ORDER BY GroupName;";

                        tables.Add("SmsCompanyList");
                        tables.Add("CellPhoneTypeList");
                        tables.Add("PhoneTypeList");
                        tables.Add("GeneralGroupData");
                        break;
                    default:
                        throw new NotImplementedException("METHOD_NOT_IMPLEMENTED");
                }
                if (strSql!="")
                {
                    using (var da = getDbAccess())
                    {
                        var ds = da.GetDataSet(strSql, parameterDictionary);
                        for (int i = 0; i < tables.Count; i++)
                        {
                            data.Add(tables[i], ds.Tables[i]);
                        }
                    }
                }
            }
            catch (NotImplementedException ex)
            {
                responce.error = ex.Message;
            }
            catch (Exception ex) {
                responce.error = ex.Message;
            }
            return responce;//responceDispatcher(data);
        }
        #endregion

        #region Dev methods Works only on debug library
#if DEBUG
        public object DevQuery(object Parameter)
        {
            if (IsValid)
            {
                try
                {
                    var payloadData = Parameter.ToTypeof<Dictionary<string, Dictionary<string, object>>>();
                    //var payloadData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(Parameter);
                    //Dictionary<string, Dictionary<string, object>> payloadData = new Dictionary<string, Dictionary<string, object>>();
                    //string[] temp = Parameter.ToString().Split(':');
                    ////payloadData.Add(Parameter.ToJson().ToString()); //=(Dictionary < string, Dictionary < string, object>>)Parameter ;
                    using (DbAccess da = new DbAccess(getConnectionString()))
                    {
                        responce.data = new Dictionary<string, dynamic>();
                        foreach (var payload in payloadData)
                        {
                            responce.data[payload.Key] = da.GetDataTable(
                                payload.Value["uqery"].ToString(),
                               payload.Value["parameters"].ToTypeof<Dictionary<string, object>>()
                            );
//.ToTypeof<Dictionary<string, object>>()
//Error converting value \"SELECT GroupId, GroupName, GroupNameEng, GroupParenCategory FROM CustomerGroupsGeneral WHERE
//( IsSupport = 0 and Ishide = 0 and SecurityLevel <= 10) ORDER BY GroupName\" to type
//'System.Collections.Generic.Dictionary`2[System.String,System.Object]'. Path 'uqery', line 2, position 180.
// on var payloadData = Parameter.ToTypeof<Dictionary<string, Dictionary<string, object>>>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    responce.exception = ex.Message;
                }

            }


            return responce;// responceDispatcher();
        }
#endif
        #endregion
    }
}
