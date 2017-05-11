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

    templateUrl: './app/amax/Customer/templates/CustomerProfile.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES, AmaxDate],
    providers: [CustomerService, ResourceService]
})

export class AmaxCustomerProfiles implements OnInit {
    modelInput = {};
    RES: Object = {};
    
    Formtype: string ="CUSTOMER_PROFILE";
    Lang: string="";
    
    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams) {

        this.modelInput = {};
        this.RES.CUSTOMER_PROFILE = {};
                
    }
    
    SetdefaultPage(){
        
        
        this.modelInput = {};
        
        
    }
   
    
    ngOnInit() {
        jQuery(".lean-overlay").css({ "display": "none" });
       
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
       

       
        
    }
}
