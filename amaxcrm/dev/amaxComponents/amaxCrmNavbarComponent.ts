import {Component, OnInit} from 'angular2/core';
import {ROUTER_DIRECTIVES} from "angular2/router";
import {ResourceService} from "../services/ResourceService";

declare var jQuery;
@Component({
    selector: 'mx-navbar',
    templateUrl:'./app/amaxComponents/templates/amaxCrmNavbar.html',
    directives:[ROUTER_DIRECTIVES]
})
export class AmaxCrmNavbarComponent implements OnInit{
    LogoCSS: string = "";
    toolsCSS: string = "";
    LangDDCSS: string = "";
    Lang: string = "";
    languageArray = [];
    LanguageId: string = "";
    IsRTL: string = "";
    _userModel = {};
    constructor(private _resourceService: ResourceService) {
        var lang = _resourceService.getCookie("lang");
        if (lang.length > 0)
            lang = lang.substring(1, lang.length);
        if (lang == "")
            lang = "en";
        this.LanguageId = lang;
       // alert(this.LanguageId);
    }
    ChangeLanguage() {
        this.LanguageId = jQuery("#LangDD").val();
        ////localStorage.setItem("lang", this.LanguageId);
        ////var lang = localStorage.getItem("lang");
        //////this._resourceService.deleteCookie("lang");

        //////localStorage.clear();
        //////sessionStorage.clear();
        ////this._resourceService.setCookie("lang", this.LanguageId,10 );
        //////localStorage.clear();
        
        
        ////window.location.href = "/";
        //console.log("Changing Language...");
        
        //this.logout();
        //if (this.LanguageId) {
        //    this._resourceService.GetSelecetdLanguage(this.LanguageId).subscribe(
        //        data => {
        //            //localStorage.setItem("langresource", JSON.stringify(data));

        //            localStorage.setItem("lang", this.LanguageId);
        //            //this._resourceService.setCookie("lang", this.LanguageId, 10);
                    

        //        },
        //        error => console.log("Unable to load Language Data"),
        //        () => {
        //            console.log("Language resource loaded");
        //        }
        //    );

        //}
        localStorage.setItem("lang", this.LanguageId);
        this._resourceService.setCookie("lang", this.LanguageId, 10);

        debugger;
        var UserDet = JSON.parse(sessionStorage.getItem('userInformation'));
        //this._userModel = {
        //    userName: UserName,
        //    password: "",
        //    orgName: orgName,
        //    lang: lang,
        //    rememberMe: rememberMe
        //}
        //if (temp != "" && temp != undefined) {
        //    var dta = $.parseJSON(temp).Data;
        //    this.LoginData = dta;
        //    var userdet = JSON.parse(atob(dta["token"].split('.')[1]));
        //    userdet.Language = lang;
        //    var stinguserdet = JSON.stringify(userdet);
        //    dta["token"].split('.')[1] = JSON.stringify(userdet);
        //    var uts = dta["token"].split('.')[1];
        //    alert(userdet.Language);
        //    sessionStorage.setItem('XToken', dta["token"])
        //    sessionStorage.setItem('sessionInformation', atob(dta["token"].split('.')[0]));
        //    sessionStorage.setItem('userInformation', atob(dta["token"].split('.')[1]));
        //    var t = sessionStorage.getItem('sessionInformation');
        //    var ut = sessionStorage.getItem('userInformation');
        //    //Featching Userinformation
        //    this.LoginData = JSON.parse(atob(dta["token"].split('.')[1]));
        //    //var langres = this.getCookie("langresource");
        //    this.getlangres(this.LoginData.Language);
        //    localStorage.setItem("lang", this.LoginData.Language);
        //    localStorage.setItem("employeeid", this.LoginData.employeeid);
        //    this.IsLogedIn = true;
        //}
        window.location.href = "/";
        
    }
    logout() {
        var empid = localStorage.getItem("employeeid");
        localStorage.clear();
        sessionStorage.clear();
        //debugger;
        //var data = this._resourceService.getCookie("
        //    this._resourceService.setCookie("UserDet", data, 10);

        //var data = this._resourceService.getCookie("RememberKey");  
        //this._resourceService.setCookie("UserDet", data, 10); 
        this._resourceService.deleteCookie("RememberKey");   
        this._resourceService.deleteCookie(empid + "cust");  
        this._resourceService.deleteCookie(empid + "emp");  
        this._resourceService.deleteCookie(empid + "src");  
        this._resourceService.deleteCookie(empid + "ccode");  
        this._resourceService.deleteCookie(empid + "SMSDet");
        this._resourceService.deleteCookie(empid +"SMSMessage");
        window.location.href="/";
    }


    ngOnInit() {
        this.languageArray = this._resourceService.GetAvailableLanguages();
        var lng = this._resourceService.getCookie("lang");
        if (lng.length > 0)
            lng = lng.substring(1, lng.length);
        if (lng == "")
            lng = "en";
        this.Lang = lng;
        //alert(this.Lang+" lang");
        if (this.Lang == "he") {
            this.IsRTL = "rtl";
        }
        else {
            this.IsRTL = "ltr";
        }
    }
}