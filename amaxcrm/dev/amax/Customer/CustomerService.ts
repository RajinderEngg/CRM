
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
        this.modelInput.CustomerId = _routeParams.params.Id;
        this.BaseAppUrl = _resourceService.AppUrl;
        this.baseUrl = _resourceService.baseUrl;
        
    }
    
    SetdefaultPage(){
        
        
        this.modelInput = {};
        
        
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
