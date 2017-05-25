
import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {CustomerService} from "../../services/CustomerService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";

import { AmaxDate } from '../../comonComponents/basicComponents';

declare var jQuery;
declare var swal;
declare var moment;

@Component({

    templateUrl: './app/amax/Customer/templates/CustomerService.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES, AmaxDate],
    providers: [CustomerService, ResourceService]
})

export class AmaxCustomerServices implements OnInit {
    
    modelInput = {};
    
    RES: Object = {};
    baseUrl: string = "";
    Formtype: string ="CUSTOMER_SERVICE";
    Lang: string="";
    //modelInput.lname= "";
    ShowMore: boolean = false;
    IsRecordEditMode: boolean = false;
    ShowMoreText: string = "More";
    CustFileImage: string = "DefaultUser.jpg";
    ShowLoader: boolean = false;
    ShowMsg: boolean = false;
    ShowGroups: boolean = true;
    GroupText: string="Show Groups";
    Msg: string = "";
    MsgClass: string = "text-primary";
    Isbtndisable: string = "";
    IsFileAsSaveBtn: string = "";
    IsFileAsCancelBtn: string = "";
    languageArray = [];

    Address: Object = {};
    PhoneModel: Object = {};
    EmailModel: Object = {};
    IsShowAll: boolean = false;
    CustList: Object = {};
    SAVE_BTN_TEXT: string = "";
    
    BTN_PHADD: string = "";
    EditPhoneData: Object = {};
    EditAddressData: Object = {};
    EditEmailData: Object = {};
    adId: string;
    IsFileAsOpen: boolean;
    ADD_NEW_CUST_TEXT: string;
    CSSTEXT: string;
    IsFileAstxtShow: boolean = false;
    FILEAS_BTN_TEXT: string = "";
    cssFileAsBtn: string = "";
    IsCancel: boolean = false;
    SearchVal: string = "";
    EnterCount: number = 0;
    StopTimeOut: any;
    CustIdText: string = "";
    static $inject = ['$scope', '$location', '$anchorScroll'];
    BaseAppUrl: string = "";
    PhIndex: number = 0;
    KendoRTLCSS: string = "";
    CHANGEDIR: string = "";
    ChangeDialog: string = "";
    IsPopUp: boolean = true;   
    
    _Employees = [];
    _ServiceTypes = [];
    _Minutes = [];
    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams) {
        
        this.RES.CUSTOMER_SERVICE = {};           
        this.modelInput.customerid = _routeParams.params.Id;
        this.BaseAppUrl = _resourceService.AppUrl;
        this.baseUrl = _resourceService.baseUrl;
        
    }
    
    SetdefaultPage(){
        
        
        this.modelInput = {};
        
        
    }
    dateSelectionChange(evt) {
        console.log(evt);
        this.modelInput.StartTime = evt;
        // alert(this.modelInput.BirthDate);
        //this.validateLogin();
    }
    ValidateModel()
    {
    var msg = "";
    var IsValidData = true;
    if (this.modelInput.StartTime != "") {
        if (moment(this.modelInput.StartTime, "DD-MM-YYYY", true).isValid() == false) {
            //bootbox.alert({ message: "Date is not valid" });

            IsValidData = false;
            msg += "<br>Date is not valid";
            //return false;
        }
    }
        
    if (this.modelInput.GetTime != null && this.modelInput.GetTime != "" && this.modelInput.GetTime != undefined) {
        var timedet = this.modelInput.GetTime.split(':');
        if (timedet.length == 2) {
            var hr = timedet[0];
            var IsValid = true;
            for (var i = 0; i < hr.length; i++) {
                if (hr[i] != "0" && hr[i] != "1" && hr[i] != "2" && hr[i] != "3" && hr[i] != "4" && hr[i] != "5" && hr[i] != "6" && hr[i] != "7" && hr[i] != "8" && hr[i] != "9") {
                    IsValid = false;
                    IsValidData = false;
                    msg += "<br>Please enter valid time";
                    break;
                }
            }
            if (IsValid == true) {
                var min = timedet[1];
                for (var i = 0; i < hr.length; i++) {
                    if (min[i] != "0" && min[i] != "1" && min[i] != "2" && min[i] != "3" && min[i] != "4" && min[i] != "5" && min[i] != "6" && min[i] != "7" && min[i] != "8" && min[i] != "9") {
                        IsValid = false;
                        IsValidData = false;
                        msg += "<br>Please enter valid time";
                        break;
                    }
                }
            }        
        }
        else {
            IsValidData = false;
            msg += "<br>Please enter valid time";
        }
    }
   

    if (IsValidData == false) {
        bootbox.alert({
            message: msg,
            className: this.ChangeDialog,
            buttons: {
                ok: {
                    //label: 'Ok',
                    className: this.CHANGEDIR
                }
            }
        });
    }
    return IsValidData;

}
    saveCustomerServiceData() {
        var curdate = new Date()
        this.modelInput.TimeZone = curdate.getTimezoneOffset()
        
        this.Isbtndisable = "disabled";
        this.ShowLoader = true;
        if (this.ValidateModel) {
            var hrmin = this.modelInput.GetTime.split(':');
            this.modelInput.StartHour = parseInt(hrmin[0]);
            this.modelInput.StartMinute = parseInt(hrmin[1]);
            var jdata = JSON.stringify(this.modelInput);
            console.log(jdata)
            this._resourceService.deleteCookie("TempImageName");
            this._customerService.AddCustomerService(jdata).subscribe(response=> {
                console.log(response);
                response = jQuery.parseJSON(response);
                this.Isbtndisable = "";
                this.ShowLoader = false;

                if (response.IsError == true) {
                    //alert(response.ErrMsg);
                    this.MsgClass = "text-danger";
                }
                else {
                    //alert(response.ErrMsg);
                    
                    this.MsgClass = "text-success";
                    //var empid = localStorage.getItem("employeeid");
                    //this._resourceService.setCookie(empid + "cust", this.modelInput.CustomerType, 10);
                    //this._resourceService.setCookie(empid + "emp", this.modelInput.employeeid, 10);
                    //this._resourceService.setCookie(empid + "src", this.modelInput.CameFromCustomer, 10);
                    //if (this.modelInput.CustomerAddresses.length > 0)
                    //    this._resourceService.setCookie(empid + "ccode", this.modelInput.CustomerAddresses[this.modelInput.CustomerAddresses.length - 1].CountryCode, 10);
                    //// debugger;
                    ////document.location = this.BaseAppUrl + "Customer/Add/-1";
                    ////debugger;
                    //this.TempmodelInput = response.Data;
                    //this.modelInput = response.Data;
                    //this.editCustDet(this.modelInput);
                    
                    //this.IsPopUp = true;
                    //this.SetdefaultPage();
                    // Reset form values
                    //this._CustTypes = response.Data;
                    
                    
                }
                this.ShowMsg = true;
                this.Msg = response.ErrMsg;
            },
                error=> console.log(error),
                () => console.log("Save Call Compleated")
            );

        }
        
        ////alert(this.modelInput.BirthDate);
        //if (this.modelInput.BirthDate != "") {
        //    if (moment(this.modelInput.BirthDate, "DD-MM-YYYY", true).isValid() == false) {
        //        bootbox.alert({ message: "Birthdate is not valid" });

        //        this.Isbtndisable = "";
        //        this.ShowLoader = false;
        //        return false;
        //    }
        //}
        //this.modelInput.Title = jQuery("#Title").val();
        //if (count <= 1 || this.modelInput.CustomerAddresses == undefined || this.modelInput.CustomerAddresses == null) {

        //    if (this.modelInput.CustomerPhones != undefined && this.modelInput.CustomerPhones != null) {
        //        var phtemp = [];
        //        jQuery('input[name^="ph"]').each(function () {
        //            phtemp.push(jQuery(this).val());
        //        });
        //        var artemp = [];
        //        jQuery('input[name^="ar"]').each(function () {
        //            artemp.push(jQuery(this).val());
        //        });
        //        var pretemp = [];
        //        jQuery('input[name^="pre"]').each(function () {
        //            pretemp.push(jQuery(this).val());
        //        });
        //        var i = 0;
        //        jQuery.each(this.modelInput.CustomerPhones, function () {
        //            if (this.IsSms == true) {
        //                this.IsSms = "1";
        //            }
        //            else {
        //                this.IsSms = "0";
        //            }
        //            if (this.phpublish == true) {
        //                this.phpublish = "1";
        //            }
        //            else {
        //                this.phpublish = "0";
        //            }
        //            this.Phone = phtemp[i];
        //            this.Area = artemp[i];
        //            this.Prefix = pretemp[i];
        //            i++;
        //            //var temp = this.PhoneTypeId.split(';');
        //            //this.PhoneTypeId = parseInt(temp[1]);
        //            //this.PhoneType = temp[0];
        //        });

        //    }
        //    if (this.modelInput.CustomerEmails != undefined && this.modelInput.CustomerEmails != null) {
        //        jQuery.each(this.modelInput.CustomerEmails, function () {
        //            if (this.publish == true) {
        //                this.publish = "1";
        //            }
        //            else {
        //                this.publish = "0";
        //            }
        //            // debugger;
        //            if (this.Newslettere == true) {
        //                this.Newslettere = false;
        //            }
        //            else {
        //                this.Newslettere = true;
        //            }

        //            i++;
        //        });
        //    }
        //    var jdata = JSON.stringify(this.modelInput);
        //    console.log(jdata)
        //    this._resourceService.deleteCookie("TempImageName");
        //    this._customerService.AddCustomer(jdata).subscribe(response=> {
        //        console.log(response);
        //        response = jQuery.parseJSON(response);
        //        this.Isbtndisable = "";
        //        this.ShowLoader = false;

        //        if (response.IsError == true) {
        //            //alert(response.ErrMsg);
        //            this.MsgClass = "text-danger";
        //        }
        //        else {
        //            //alert(response.ErrMsg);
                    
        //            this.MsgClass = "text-success";
        //            var empid = localStorage.getItem("employeeid");
        //            this._resourceService.setCookie(empid + "cust", this.modelInput.CustomerType, 10);
        //            this._resourceService.setCookie(empid + "emp", this.modelInput.employeeid, 10);
        //            this._resourceService.setCookie(empid + "src", this.modelInput.CameFromCustomer, 10);
        //            if (this.modelInput.CustomerAddresses.length > 0)
        //                this._resourceService.setCookie(empid + "ccode", this.modelInput.CustomerAddresses[this.modelInput.CustomerAddresses.length - 1].CountryCode, 10);
        //            // debugger;
        //            //document.location = this.BaseAppUrl + "Customer/Add/-1";
        //            //debugger;
        //            this.TempmodelInput = response.Data;
        //            this.modelInput = response.Data;
        //            this.editCustDet(this.modelInput);
                    
        //            //this.IsPopUp = true;
        //            //this.SetdefaultPage();
        //            // Reset form values
        //            //this._CustTypes = response.Data;
                    
                    
        //        }
        //        this.ShowMsg = true;
        //        this.Msg = response.ErrMsg;
        //    },
        //        error=> console.log(error),
        //        () => console.log("Save Call Compleated")
        //    );
        //}
        //else {
        //    bootbox.alert({
        //        message: this.RES.CUSTOMER_MASTER.APP_MSG_ISMAINADD, className: this.ChangeDialog,
        //        buttons: {
        //            ok: {
        //                //label: 'Ok',
        //                className: this.CHANGEDIR
        //            }
        //        }
        //    });
        //    this.Isbtndisable = "";
        //    this.ShowLoader = false;
        //}


    }
 


    ngOnInit() {
       
        this.SAVE_BTN_TEXT = "Save";
       this._resourceService.GetLangRes(this.Formtype, this.Lang).subscribe(response=> {
           //debugger;
           response = jQuery.parseJSON(response);
           if (response.IsError == true) {
               bootbox.alert({
                   message: response.ErrMsg, className: this.ChangeDialog,
                   buttons: {
                       ok: {
                           //label: 'Ok',
                           className: this.CHANGEDIR
                       }
                   }
               });
           }
           else {
               this.RES = response.Data;
               //alert(this.RES);
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
       this._customerService.GetEmployees().subscribe(response=> {
           response = jQuery.parseJSON(response);
           if (response.IsError == true) {
               bootbox.alert({
                   message: response.ErrMsg, className: this.ChangeDialog,
                   buttons: {
                       ok: {
                           //label: 'Ok',
                           className: this.CHANGEDIR
                       }
                   }
               });
           }
           else {
               this._Employees = response.Data;
               if (this.modelInput.employeeid == "" || this.modelInput.employeeid == undefined || this.modelInput.employeeid == null) {
                   var empid;
                   jQuery.each(this._Employees, function () {
                       empid = this.Value;
                       return false;
                   });
                   this.modelInput.employeeid = empid;
               }
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });

       this._customerService.GetServiceTypes().subscribe(response=> {
           response = jQuery.parseJSON(response);
           if (response.IsError == true) {
               bootbox.alert({
                   message: response.ErrMsg, className: this.ChangeDialog,
                   buttons: {
                       ok: {
                           //label: 'Ok',
                           className: this.CHANGEDIR
                       }
                   }
               });
           }
           else {
               this._ServiceTypes = response.Data;
               
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
       this._customerService.GetMinutes().subscribe(response=> {
           response = jQuery.parseJSON(response);
           if (response.IsError == true) {
               bootbox.alert({
                   message: response.ErrMsg, className: this.ChangeDialog,
                   buttons: {
                       ok: {
                           //label: 'Ok',
                           className: this.CHANGEDIR
                       }
                   }
               });
           }
           else {
               this._Minutes = response.Data;

           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
    }
}
