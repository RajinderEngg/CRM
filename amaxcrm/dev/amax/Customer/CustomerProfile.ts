import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {CustomerService} from "../../services/CustomerService";
import {RecieptService} from "../../services/RecieptService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";

import { AmaxDate } from '../../comonComponents/basicComponents';


declare var jQuery;
declare var swal;
declare var moment;

@Component({

    templateUrl: './app/amax/Customer/templates/CustomerProfile.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES, AmaxDate],
    providers: [CustomerService, ResourceService, RecieptService]
})

export class AmaxCustomerProfiles implements OnInit {
    modelInput = {};
    RES: Object = {};
    BaseUrl: string = "";
    ImageUrl: string = "";
    Receipts = [];
    Formtype: string ="CUSTOMER_PROFILE";
    Lang: string="";
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    CustFileName: string = "";
    IsEmailRowsShow:boolean=false;
    IsPhonesRowsShow: boolean =false;
    IsAddressRowsShow: boolean = false;
    IsGroupRowsShow: boolean = false;
    IsWebsiteRowsShow: boolean = false;
    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams, private _RecieptService: RecieptService) {
    
        this.modelInput = {};
        this.RES.CUSTOMER_PROFILE = {};
        this.modelInput.CustomerAddresses = [];
        this.modelInput.CustomerPhones = [];
        this.modelInput.CustomerEmails = [];
        this.modelInput.CustomerGroups = [];
        this.modelInput.CustomersService = [];
        this.modelInput.CustomerWebsites = [];
        this.modelInput.Receipts = [];
        this.modelInput.CustomerId = _routeParams.params.Id;
        this.BaseUrl = _resourceService.AppUrl;
        this.ImageUrl = _resourceService.ImageUrl;   
        this.CustFileName = "DefaultUser.jpg";   
    }
    
    SetdefaultPage(){
        
        
        this.modelInput = {};
        
        
    }
    ShowHideEmailRows() {
        if (this.IsEmailRowsShow == false) {
            var count = 0;
            jQuery.each(this.modelInput.CustomerEmails, function () {
                if (count == 0) {

                    this.CSSStlye = "display:block";

                }
                else {
                    this.CSSStlye = "display:none";
                }
                count++;
            });
            this.IsEmailRowsShow = true;
        }
        else {
            jQuery.each(this.modelInput.CustomerEmails, function () {
                    this.CSSStlye = "display:block";                
            });
            this.IsEmailRowsShow = false;
        }
    }
    ShowHidePhonesRows() {
        if (this.IsPhonesRowsShow == false) {
            var count = 0;
            jQuery.each(this.modelInput.CustomerPhones, function () {
                if (count == 0) {

                    this.CSSStlye = "display:block";

                }
                else {
                    this.CSSStlye = "display:none";
                }
                count++;
            });
            this.IsPhonesRowsShow = true;
        }
        else {
            jQuery.each(this.modelInput.CustomerPhones, function () {
                this.CSSStlye = "display:block";
            });
            this.IsPhonesRowsShow = false;
        }
    }
    ShowHideaddressesRows() {
        if (this.IsAddressRowsShow == false) {
            var count = 0;
            jQuery.each(this.modelInput.CustomerAddresses, function () {
                if (count == 0) {

                    this.CSSStlye = "display:block";

                }
                else {
                    this.CSSStlye = "display:none";
                }
                count++;
            });
            this.IsAddressRowsShow = true;
        }
        else {
            jQuery.each(this.modelInput.CustomerAddresses, function () {
                this.CSSStlye = "display:block";
            });
            this.IsAddressRowsShow = false;
        }
    }
    ShowHideGroupsRows() {
        if (this.IsGroupRowsShow == false) {
            var count = 0;
            jQuery.each(this.modelInput.CustomerGroups, function () {
                if (count == 0) {

                    this.CSSStlye = "display:block";

                }
                else {
                    this.CSSStlye = "display:none";
                }
                count++;
            });
            this.IsGroupRowsShow = true;
        }
        else {
            jQuery.each(this.modelInput.CustomerGroups, function () {
                this.CSSStlye = "display:block";
            });
            this.IsGroupRowsShow = false;
        }
    }
    ShowHideWebsiteRows() {
        if (this.IsWebsiteRowsShow == false) {
            var count = 0;
            jQuery.each(this.modelInput.CustomerWebsites, function () {
                if (count == 0) {

                    this.CSSStlye = "display:block";

                }
                else {
                    this.CSSStlye = "display:none";
                }
                count++;
            });
            this.IsWebsiteRowsShow = true;
        }
        else {
            jQuery.each(this.modelInput.CustomerWebsites, function () {
                this.CSSStlye = "display:block";
            });
            this.IsWebsiteRowsShow = false;
        }
    }
    CustomerDetail(CustomerId) {
       // debugger;
        this._customerService.GetCompleteCustDetForProfile(CustomerId).subscribe(response=> {
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
                //debugger;
                this.modelInput = response.Data;
                //this.modelInput.Receipts = [];
                if (this.modelInput.ImageFileName != null && this.modelInput.ImageFileName != "") {
                    var OrgId = "";
                    var empid = localStorage.getItem("employeeid");
                    if (empid != null && empid != undefined) {
                        OrgId = localStorage.getItem(empid + "_OrgId");
                    }
                    this.CustFileName = OrgId+"//"+this.modelInput.ImageFileName;
                }
               // debugger;

                this.IsEmailRowsShow = false;
                this.ShowHideEmailRows();
                this.IsPhonesRowsShow = false;
                this.ShowHidePhonesRows();
                this.IsAddressRowsShow = false;
                this.ShowHideaddressesRows();
                this.IsGroupRowsShow = false;
                this.ShowHideGroupsRows();
                this.IsWebsiteRowsShow = false;
                this.ShowHideWebsiteRows();
                //jQuery("#EmailTable").children('tbody').hide();
                //jQuery("#EmailTable").children('tbody').children('tr:first-child').hide();
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    
    ngOnInit() {

   
        this.CustFileName = "DefaultUser.jpg"; 
        jQuery(".lean-overlay").css({ "display": "none" });
        this.modelInput.CustomerId = this._routeParams.params.Id;
        if (localStorage.getItem("lang") == "") {
            localStorage.setItem("lang", "en");
        }
        if (this._resourceService.getCookie("lang") == "") {

            this._resourceService.setCookie("lang", "en", 10);

        }
        
        this.Lang = this._resourceService.getCookie("lang");
        if (this.Lang.length > 0) {
            this.Lang = this.Lang.substring(1, this.Lang.length);
        }
        
        
        
        if (this.Lang == "he") {
            
            this.CHANGEDIR = "rtlmodal";
            this.ChangeDialog = "input_right";
        }
        else {
            this.CHANGEDIR = "ltrmodal";
            this.ChangeDialog = "input_left";
        }

       // debugger;
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
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
       if (this.modelInput.CustomerId != undefined && this.modelInput.CustomerId != null && this.modelInput.CustomerId != "") {
           this.CustomerDetail(this.modelInput.CustomerId);


           //this._RecieptService.GetReceiptByCustomerId(this.modelInput.CustomerId).subscribe(response=> {
           //    // debugger;
           //    response = jQuery.parseJSON(response);
           //    if (response.IsError == true) {
           //        bootbox.alert({
           //            message: response.ErrMsg, className: this.ChangeDialog,
           //            buttons: {
           //                ok: {
           //                    //label: 'Ok',
           //                    className: this.CHANGEDIR
           //                }
           //            }
           //        });
           //    }
           //    else {
           //        this.Receipts = response.Data;


           //    }
           //}, error=> {
           //    console.log(error);
           //}, () => {
           //    console.log("CallCompleted")
           //});
       }
       else {
           bootbox.alert({
               message: "Customer not found", className: this.ChangeDialog,
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
