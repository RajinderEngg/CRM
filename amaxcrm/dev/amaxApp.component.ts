///<reference path="services/AmaxService.ts"/>
import { Component, OnInit } from 'angular2/core';
import { AmaxCrmUIComponent } from './amaxComponents/amaxCrmUIComponent';
import { AmaxLoginComponent } from './amaxComponents/amaxLoginComponent';
import { AmaxService } from './services/AmaxService';
import {ResourceService} from "./services/ResourceService";
import {indexComponent} from "./amax/quickaccess/index";
import {defaultComponent} from "./amax/quickaccess/default";
import {RouteConfig} from "angular2/router";
import {AmaxLogoutComponent} from "./amax/logout";
import {AmaxReport} from "./amax/reports/amaxReports";
import {AmaxForms} from "./amax/forms/amaxForms";
import {Observable} from "rxjs/Observable";
import {AmaxEmployeeProfile} from "./amax/employee/profile";
import {AmaxEmployeeSettings} from "./amax/employee/settings";
import {AmaxSmsComponent} from "./amax/sms";
import {AmaxCustomers} from "./amax/Customer/addCustomer";

import {AmaxSearchCustomers} from "./amax/Customer/SearchCustomer";

import {AmaxReciept} from "./amax/RecieptType/Reciept";
import {AmaxRecieptTemplate} from "./amax/RecieptType/RecieptTemplate";
import {AmaxTemplate} from "./amax/RecieptType/Template";
import {AmaxGeneralGroups} from "./amax/GeneralGroups/GeneralGroups";
import {AmaxTerminals} from "./amax/Charge_Credit/Terminals";
import {AmaxChargeCredit} from "./amax/Charge_Credit/ChargeCreditCard";
import {AmaxReceiptSelect} from "./amax/Receipt/ReceiptSelect";
import {AmaxReceiptCreate} from "./amax/Receipt/ReceiptCreate";

@Component({
    selector: 'mx-app',
    template: `
        <div dir="{{ RES.APP_DIR }}">
            <mx-login
                *ngIf="!IsLogedIn"
                [(res)]="RES.SCREEN_LOGIN"
                [dataModel]="_userModel" 
                (ondata)="validateUser($event)" 
                (onlanguage)="changeLanguage($event)"
            ></mx-login>
            <div *ngIf="IsLogedIn" class="no-skin">
                <mx-ui></mx-ui>
            </div>
        </div>
	`,
    directives: [AmaxLoginComponent, AmaxCrmUIComponent, AmaxLogoutComponent, AmaxLoginComponent],
    providers: [AmaxService, ResourceService]
})

@RouteConfig([
    { path:"/index",                    name:"IndexRouter",             component:indexComponent },
    { path:"/default",                  name:"DefaultRouter",           component:defaultComponent },

    //Reports Routing
    { path:"/report",                   name:"Reports",                 component: AmaxReport },

    //Forms routing
    //{ path:"/form", name:"Forms", component:AmaxForms },
    { path:"/form/:frm",                name:"Forms",                   component:AmaxForms },

    //Employee related routing
    { path:"/employee/profile",         name:"Profile",                 component: AmaxEmployeeProfile },
    { path:"/employee/settings",        name:"Settings",                component: AmaxEmployeeSettings },


    //Module Routing
     { path:"/sms",                      name:"Sms",                     component: AmaxSmsComponent},


    { path:"/blank",                    name:"Blank",                   component: AmaxLogoutComponent}, //for the testing
	
	//Customer Routing
    { path: "/Customer/Add/:Id", name: "AddCustomer", component: AmaxCustomers },

    //Customer Routing
    { path: "/Customer/Search/:ForPopup/:FromPage/:ForBack", name: "SearchCustomer", component: AmaxSearchCustomers },

    //Reciept Type
    { path: "/ReceiptType/:Id", name: "ReceiptType", component: AmaxReciept },

    { path: "/ReceiptTemplate/Add/:Id", name: "RecieptTemplate", component: AmaxRecieptTemplate },
    
    { path: "/Template/Edit/:Id/:FPage", name: "Template", component: AmaxTemplate },
    { path: "/GeneralGroups/View", name: "GeneralGroups", component: AmaxGeneralGroups },
    //Templates
    { path: "/Terminals/Show/:Id", name: "Terminals", component: AmaxTerminals },
    //ChargeCredit
    { path: "/ChargeCredit/:Id/:TermNo", name: "ChargeCreditCard", component: AmaxChargeCredit },
    //ChargeCredit
    { path: "/ReceiptSelect/:Id/:CustId", name: "ReceiptSelect", component: AmaxReceiptSelect },
    //ChargeCredit
    { path: "/ReceiptCreate/:Id/:ReceiptTypeId", name: "ReceiptCreate", component: AmaxReceiptCreate }
])

export class AmaxAppComponent implements OnInit {
    FormTypeForm: string = "SCREEN_LOGIN";
    baseUrl: string;
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    constructor(private _amaxSservice: AmaxService, private _languageService: ResourceService) {
        //Loading the language resource
        this.RES.SCREEN_LOGIN = {};
        this.FormTypeForm = "SCREEN_LOGIN";
        this.baseUrl = this._languageService.AppUrl;
        
    }
    IsLogedIn: boolean = false;
    public LoginData: Object;
    public RES: Object = {};
        

    loadUserInformation() {
        if (sessionStorage.getItem('userInformation') != null) {
            this.LoginData = JSON.parse(sessionStorage.getItem('userInformation'));
            if (this.LoginData) this.IsLogedIn = true;
        }
    }
   getlangres(langcode) {
       this._languageService.GetLangRes(this.FormTypeForm, langcode).subscribe(response=> {
          // debugger;
           response = $.parseJSON(response);
           if (response.IsError == true) {
               //alert(response.ErrMsg);
               bootbox.alert({
                   message: response.ErrMsg,
                   className: this.ChangeDialog,
                   buttons: {
                       ok: {
                           //label: 'Ok',
                           className: this.CHANGEDIR
                       }
                   }
               });
           }
           else {
               
               localStorage.setItem("langresource", JSON.stringify(response.Data));
               localStorage.setItem("lang", langcode);
               this.RES = response.Data;

           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
   }
validateLogin() {
    //this.IsLogedIn = true; 
    if (this._userModel["userName"] != undefined && this._userModel["userName"] != null && this._userModel["userName"] != ""
        && this._userModel["password"] != undefined && this._userModel["password"] != null && this._userModel["password"] != ""
        && this._userModel["orgName"] != undefined && this._userModel["orgName"] != null && this._userModel["orgName"] != "") {
        this._amaxSservice.validateLogin(this._userModel["userName"], this._userModel["password"], this._userModel["orgName"], this._userModel["rememberMe"]).subscribe(
            data => {
                var dta = $.parseJSON(data).Data;
                //debugger;
                if (dta.error != undefined && dta.error != null && dta.error != "") {
                    if (dta.error != "") {
                        //alert(dta.error);
                        bootbox.alert({ message: dta.error,
                            className: this.ChangeDialog,
                            buttons: {
                                ok: {
                                    //label: 'Ok',
                                    className: this.CHANGEDIR
                                }
                            }
                    });
                    }
                }
                else {
                    this.IsLogedIn = true;
                    this.LoginData = dta;
                    sessionStorage.setItem('XToken', dta["token"])
                    sessionStorage.setItem('sessionInformation', atob(dta["token"].split('.')[0]));
                    sessionStorage.setItem('userInformation', atob(dta["token"].split('.')[1]));

                    //Featching Userinformation
                
                    this.LoginData = JSON.parse(atob(dta["token"].split('.')[1]));
                    localStorage.setItem("employeeid", this.LoginData.employeeid);
                    //localStorage.setItem("OrgId", this._userModel["orgName"]);
                    if (localStorage.getItem("lang") == "" || localStorage.getItem("lang") == undefined || localStorage.getItem("lang") == null) {
                        localStorage.setItem("lang", "en");
                    }
                    //alert(this._userModel["rememberMe"].toString());
                    //  debugger;
                    //alert(this._userModel["rememberMe"]);
                    if (this._userModel["rememberMe"] == true || this._userModel["rememberMe"] == "true") {

                        var lang = localStorage.getItem("lang");
                        //alert(lang);
                        //this._languageService.setCookie("langresource", lang,10);
                        this._languageService.setCookie("RememberKey", data, 10);
                        this._languageService.setCookie("UserName", this._userModel["userName"], 10);
                        this._languageService.setCookie("password", this._userModel["password"], 10);
                        this._languageService.setCookie("orgName", this._userModel["orgName"], 10);
                        this._languageService.setCookie("rememberMe", this._userModel["rememberMe"], 10);
                        this._languageService.setCookie("lang", lang, 10);
                    }
                }

            },
            
            error => console.error(error),
            () => {
                
            }
        )
    }
    else {
        if (this._userModel["userName"] == undefined && this._userModel["userName"] == null || this._userModel["userName"] == "") {
            //alert("Please enter valid username");
            bootbox.alert({
                message: "Please enter valid username",
                className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
        }
        if (this._userModel["password"] == undefined && this._userModel["password"] == null || this._userModel["password"] == "") {
            //alert("Please enter valid password");
            bootbox.alert({
                message: "Please enter valid password",
                className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
        }
        if (this._userModel["orgName"] == undefined && this._userModel["orgName"] == null || this._userModel["orgName"] == "") {
            //alert("Please enter valid organization");
            bootbox.alert({
                message: "Please enter valid password",
                className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
        }
    }
    }

_userModel = {};
_LoggeduserModel = {};

    validateUser(evt) {
        console.log(evt);
        this.validateLogin();
    }

    public changeLanguageByLangId(LanguageId) {
        //debugger;
        console.log("Changing Language...");
        if (LanguageId) {
            this._languageService.GetSelecetdLanguage(LanguageId).subscribe(
                data => {
                    localStorage.setItem("langresource", JSON.stringify(data));

                    localStorage.setItem("lang", LanguageId);
                    this._userModel["lang"] = LanguageId;

                },
                error => console.log("Unable to load Language Data"),
                () => {
                    console.log("Language resource loaded");
                }
            );

            this._languageService.GetLangRes(this.FormTypeForm, LanguageId).subscribe(response=> {
                //debugger;
                response = $.parseJSON(response);
                if (response.IsError == true) {
                    //alert(response.ErrMsg);
                    bootbox.alert({
                        message: response.ErrMsg,
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                }
                else {
                    localStorage.setItem("langresource", JSON.stringify(response.Data));
                    localStorage.setItem("lang", LanguageId);
                    this.RES = response.Data;

                }
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });


        }
        else {
            console.error("Code not specified");
        }
    }


    public changeLanguage(evt) {
        //debugger;
        console.log("Changing Language...");
        if (evt.code) {
            this._languageService.GetSelecetdLanguage(evt.code).subscribe(
                data => {
                    localStorage.setItem("langresource", JSON.stringify(data));

                    localStorage.setItem("lang", evt.code);
                    this._userModel["lang"] = evt.code;
                    
                },
                error => console.log("Unable to load Language Data"),
                () => {
                    console.log("Language resource loaded");
                }
            );

            this._languageService.GetLangRes(this.FormTypeForm, evt.code).subscribe(response=> {
                //debugger;
                response = $.parseJSON(response);
                if (response.IsError == true) {
                    //alert(response.ErrMsg);
                    bootbox.alert({
                        message: response.ErrMsg,
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                }
                else {
                    localStorage.setItem("langresource", JSON.stringify(response.Data));
                    localStorage.setItem("lang", evt.code);
                    this.RES = response.Data;
                    
                }
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });


        }
        else {
            console.error("Code not specified");
        }
    }


    ngOnInit() {
        //debugger;

        this.FormTypeForm = "SCREEN_LOGIN";

        var UserName = this._languageService.getCookie("UserName");
        //alert(UserName[0]);
        if (UserName.length > 0 && UserName[0]=="=") 
            var UserName = UserName.substring(1,UserName.length);
        //var password = this._languageService.getCookie("password");

        //if (password.length > 0) 
        //    var password = password.substring(1, password.length);
        var orgName = this._languageService.getCookie("orgName");

        if (orgName.length > 0) 
            var orgName = orgName.substring(1, orgName.length);
        var rememberMe = this._languageService.getCookie("rememberMe");

        if (rememberMe.length > 0)
            var rememberMe = rememberMe.substring(1, rememberMe.length);
        else {
            rememberMe = "true";
        }
        var lang = this._languageService.getCookie("lang");
        
        if (lang.length > 0)
            var lang = lang.substring(1, lang.length);
        if (lang == "")
            lang = "en";
       
        localStorage.setItem("lang", lang);
                this._userModel = {
                    userName: UserName,
                    password: "",
                    orgName: orgName,
                    lang: lang,
                    rememberMe: rememberMe
        }
                this._languageService.GetLangRes(this.FormTypeForm, lang).subscribe(response=> {
                    
                    response = $.parseJSON(response);
                    if (response.IsError == true) {
                        //alert(response.ErrMsg);
                        bootbox.alert({
                            message: response.ErrMsg,
                            className: this.ChangeDialog,
                            buttons: {
                                ok: {
                                    //label: 'Ok',
                                    className: this.CHANGEDIR
                                }
                            }
                        });
                    }
                    else {
                        //debugger;
                        localStorage.setItem("langresource", JSON.stringify(response.Data));
                        localStorage.setItem("lang", lang);
                        this.RES = response.Data;

                    }
                }, error=> {
                    console.log(error);
                }, () => {
                    console.log("CallCompleted")
                });
        //this.RES = {
            
        //     "APP_DIR": "ltr",
        //     "APP_LANG": "en",
        //     "SCREEN_LOGIN": {
        //         "COMPANY_NAME": "Amax",
        //         "APP_TYPE": "C.R.M",
        //         "APP_BASELINE": "© Amax - software solutions",
        //         "APP_LBL_INFO": "Please Enter Your Information",
        //         "APP_LBL_USER": "Username",
        //         "APP_LBL_PASS": "Password",
        //         "APP_LBL_ORG": "Organization ID",
        //         "APP_LBL_REMEMBER": "Remember Me",

        //         "APP_BTN_LOGIN": "Login",
        //         "APP_LBL_FORGOTPASS": "I forgot my password",
        //         "APP_LBL_REGISTER": "I want to register"
        //     },
        //     "CUSTOMER_MASTER": {
        //         "APP_LBL_CUST": "Customer",
        //         "APP_LBL_CARD": "Card",
        //         "APP_LBL_NEW_CUST": "Add New Customer",
        //         "APP_TXT_REQD": "(Required-*)",
        //         "APP_TXT_PH_LNAME": "Last Name*",
        //         "APP_TXT_PH_FNAME": "First Name*",
        //         "APP_TXT_PH_MNAME": "Middle Name",
        //         "APP_TXT_PH_CNAME": "Company Name*",
        //         "APP_DP_CTYPE": "Select Customer Type",
        //         "APP_DP_EMP": "Select Contact Employee",
        //         "APP_DP_SOURCE": "Select Source",

        //         "APP_TXT_PH_CCODE": "Customer Code",
        //         "APP_TXT_PH_BDATE": "Birth Date",
        //         "APP_TXT_PH_JOB": "Job Title",
        //         "APP_TXT_PH_TITLE": "Title",
        //         "APP_DP_SUFFIX": "Select Suffix",
        //         "APP_DP_GENDER": "Select Gender",
        //         "APP_DP_GENDER_M": "Male",
        //         "APP_DP_GENDER_F": "Female",
        //         "APP_LBL_PHONE": "Phones",
        //         "APP_LBL_ADD_PH": "Add Phone",
        //         "APP_DP_PTYPE": "Select Phone Type*",
        //         "APP_TXT_PH_PREFIX": "Prefix",
        //         "APP_TXT_PH_AREA": "Area",

        //         "APP_TXT_PH_PHONE": "Phone*",
        //         "APP_LBL_SMS": "For SMS",
        //         "APP_CHK_PH_SMS": "For SMS",
        //         "APP_TXT_PH_COMMENT": "Comments",

        //         "APP_BTN_PHADD": "Add",
        //         "APP_BTN_PHCLOSE": "Close",

        //         "APP_BTN_EADD": "Add",
        //         "APP_BTN_ECLOSE": "Close",

        //         "APP_BTN_ADADD": "Add",
        //         "APP_BTN_ADCLOSE": "Close",

        //         "APP_GRD_LBL_PTYPE": "Phone Type",
        //         "APP_GRD_LBL_PHNO": "Prefix-Area-Phone",
        //         "APP_GRD_LBL_SMS": "For SMS",
        //         "APP_GRD_LBL_REM": "Remarks",
        //         "APP_LNK_EMAIL": "Emails",
        //         "APP_LBL_EMAIL": "Add Email",
        //         "APP_TXT_PH_EMAIL": "Email*",
        //         "APP_TXT_PH_ENAME": "Name*",

        //         "APP_LBL_NLETTER": "NewsLetter",
        //         "APP_GRD_LBL_EMAIL": "Email",
        //         "APP_GRD_LBL_ENAME": "Name",
        //         "APP_GRD_LBL_NLETTER": "Newsletter",
        //         "APP_LNK_LBL_ADDRS": "Addresses",
        //         "APP_LBL_ADDRESS": "Add Address",
        //         "APP_LBL_PH_STR": "Street*",
        //         "APP_LBL_PH_ADAREA": "Area*",
        //         "APP_LBL_PH_CITY": "City*",
        //         "APP_LBL_PH_ZIP": "Zip*",
        //         "APP_DP_COUNTRY": "Select Country*",
        //         "APP_DP_STATE": "Select State",
        //         "APP_DP_ADDRTYPE": "Select AddressType*",

        //         "APP_LBL_DELIVERY": "Delivery",
        //         "APP_LBL_MADDRESS": "Is Main Address",
        //         "APP_GRD_LBL_STREET": "Street",
        //         "APP_GRD_LBL_ADAREA": "Area",
        //         "APP_GRD_LBL_CITY": "CityName",
        //         "APP_GRD_LBL_ZIP": "Zip",
        //         "APP_GRD_LBL_COUNTRY": "CountryCode",
        //         "APP_GRD_LBL_STATE": "StateId",
        //         "APP_GRD_LBL_ADDRESS": "AddressTypeId",
        //         "APP_GRD_LBL_DELIVERY": "Delivery",
        //         "APP_GRD_LBL_MADDRESS": "MainAddress",
        //         "APP_LBL_GRPS": "Choose Groups",
        //         "APP_LBL_LOAD": "Loading",
        //         "APP_BTN_SAVE": "Save Changes",
        //         "APP_LNK_LBL_MORE": "More",
        //         "APP_LNK_LBL_LESS": "Less"

        //     }
                // }
                //debugger;    
                var temp = this._languageService.getCookie("RememberKey");
                if (temp != "" && temp != undefined) {
                    var password = this._languageService.getCookie("password");

        if (password.length > 0) 
            var password = password.substring(1, password.length);
                    this._LoggeduserModel = {
                userName: UserName,
                password: password,
                orgName: orgName,
                lang: lang,
                rememberMe: rememberMe
            }


            this.IsLogedIn = true;
            if (this._LoggeduserModel["userName"] != undefined && this._LoggeduserModel["userName"] != null && this._LoggeduserModel["userName"] != ""
                && this._LoggeduserModel["password"] != undefined && this._LoggeduserModel["password"] != null && this._LoggeduserModel["password"] != ""
                && this._LoggeduserModel["orgName"] != undefined && this._LoggeduserModel["orgName"] != null && this._LoggeduserModel["orgName"] != "") {
                this._amaxSservice.validateLogin(this._LoggeduserModel["userName"], this._LoggeduserModel["password"], this._LoggeduserModel["orgName"], this._LoggeduserModel["rememberMe"]).subscribe(
                    data => {
                        var dta = $.parseJSON(data).Data;
                        //debugger;
                        if (dta.error != undefined && dta.error != null && dta.error != "") {
                            if (dta.error != "") {
                                bootbox.alert({
                                    message: dta.error,
                                    className: this.ChangeDialog,
                                    buttons: {
                                        ok: {
                                            //label: 'Ok',
                                            className: this.CHANGEDIR
                                        }
                                    }
                                });
                            }
                        }
                        else {
                            this.IsLogedIn = true;
                            this.LoginData = dta;
                            sessionStorage.setItem('XToken', dta["token"])
                            sessionStorage.setItem('sessionInformation', atob(dta["token"].split('.')[0]));
                            sessionStorage.setItem('userInformation', atob(dta["token"].split('.')[1]));

                            //Featching Userinformation
                
                            this.LoginData = JSON.parse(atob(dta["token"].split('.')[1]));
                            localStorage.setItem("employeeid", this.LoginData.employeeid);
                            //localStorage.setItem("OrgId", this._LoggeduserModel["orgName"]);
                            if (localStorage.getItem("lang") == "" || localStorage.getItem("lang") == undefined || localStorage.getItem("lang") == null) {
                                localStorage.setItem("lang", "en");
                            }
                            //alert(this._LoggeduserModel["rememberMe"].toString());
                            //  debugger;
                            //alert(this._LoggeduserModel["rememberMe"]);
                            if (this._LoggeduserModel["rememberMe"] == true || this._LoggeduserModel["rememberMe"] == "true") {

                                var lang = localStorage.getItem("lang");
                                //alert(lang);
                                //this._languageService.setCookie("langresource", lang,10);
                                this._languageService.setCookie("RememberKey", data, 10);
                                this._languageService.setCookie("UserName", this._LoggeduserModel["userName"], 10);
                                this._languageService.setCookie("password", this._LoggeduserModel["password"], 10);
                                this._languageService.setCookie("orgName", this._LoggeduserModel["orgName"], 10);
                                this._languageService.setCookie("rememberMe", this._LoggeduserModel["rememberMe"], 10);
                                this._languageService.setCookie("lang", lang, 10);
                            }
                        }

                    },

                    error => console.error(error),
                    () => {

                    }
                )
            }
            else {
                if (this._LoggeduserModel["userName"] == undefined && this._LoggeduserModel["userName"] == null || this._LoggeduserModel["userName"] == "") {
                    //alert("Please enter valid username");
                    bootbox.alert({
                        message: "Please enter valid username",
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                }
                if (this._LoggeduserModel["password"] == undefined && this._LoggeduserModel["password"] == null || this._LoggeduserModel["password"] == "") {
                    //alert("Please enter valid password");
                    bootbox.alert({
                        message: "Please enter valid password",
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                }
                if (this._LoggeduserModel["orgName"] == undefined && this._LoggeduserModel["orgName"] == null || this._LoggeduserModel["orgName"] == "") {
                    //alert("Please enter valid organization");
                    bootbox.alert({
                        message: "Please enter valid password",
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                }
            }


        }
        // if(!localStorage.getItem("langresource")) {
        //     //this._languageService.GetSelecetdLanguage("en").subscribe(
        //     //    data=>{
        //     //        console.log(data);
        //     //        localStorage.setItem("langresource", JSON.stringify(data));
        //     //        this.RES = data;
        //     //    },
        //     //    error=>console.log("Unable to load Language Data"),
        //     //    ()=> {
        //     //        console.log("Language resource loaded");
        //     //    }
        //     //);
        //     this.RES = this._languageService.GetSelecetdLanguage("en");
        // }else{
        //     //this.RES=JSON.parse(localStorage.getItem('langresource'));
        // }
    }
}