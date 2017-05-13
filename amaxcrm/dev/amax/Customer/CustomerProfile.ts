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
    Receipts = [];
    Formtype: string ="CUSTOMER_PROFILE";
    Lang: string="";
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams, private _RecieptService: RecieptService) {

        this.modelInput = {};
        this.RES.CUSTOMER_PROFILE = {};
        this.modelInput.CustomerAddresses = [];
        this.modelInput.CustomerPhones = [];
        this.modelInput.CustomerEmails = [];
        this.modelInput.CustomerGroups = [];
        this.modelInput.Receipts = [];
        this.modelInput.CustomerId = _routeParams.params.Id;
        this.BaseUrl = _resourceService.AppUrl;             
    }
    
    SetdefaultPage(){
        
        
        this.modelInput = {};
        
        
    }
    CustomerDetail(CustomerId) {
        this._customerService.GetCompleteCustDet(CustomerId).subscribe(response=> {
            // debugger;
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
                this.modelInput = response.Data;
                this.modelInput.Receipts = [];
                
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    
    ngOnInit() {
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

       this.CustomerDetail(this.modelInput.CustomerId);

       this._RecieptService.GetReceiptByCustomerId(this.modelInput.CustomerId).subscribe(response=> {
           // debugger;
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
               this.Receipts = response.Data;


           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
    }
}
