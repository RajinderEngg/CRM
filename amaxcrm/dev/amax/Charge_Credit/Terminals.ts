import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {CustomerService} from "../../services/CustomerService";
import {ChargeCreditService} from "../../services/ChargeCreditService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";
import {AutocompleteContainer} from '../../autocomplete/autocomplete-container';
import {Autocomplete} from '../../autocomplete/autocomplete.component';

export const AUTOCOMPLETE_DIRECTIVES = [Autocomplete, AutocompleteContainer];
declare var jQuery;
@Component({

    templateUrl: './app/amax/Charge_Credit/templates/Terminals.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, AUTOCOMPLETE_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES],
    providers: [CustomerService, ResourceService, ChargeCreditService]
})

export class AmaxTerminals implements OnInit {
    modelInput = {};
    RES: Object = {};
    Formtype: string ="TERMINAL_SCREEN";
    Lang: string = "";
    CustomerId: number = -1;
    static $inject = ['$scope', '$location', '$anchorScroll'];
    BaseAppUrl: string = "";
    _TerminalList = [];
    TerminalNumber: string = "";
    ChangeDialog: string = "";
    CHANGEDIR: string = "";

    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams, private _ChargeCreditService: ChargeCreditService) {
        
        this.RES.TERMINAL_SCREEN = {};
        this.BaseAppUrl = _resourceService.AppUrl;
        this.CustomerId = _routeParams.params.Id;
       
    }
    BackPage() {
        document.location = this.BaseAppUrl + "Customer/Add/" + this.CustomerId;
    }
    NextPage() {
        if (this.TerminalNumber != undefined && this.TerminalNumber != null && this.TerminalNumber != "") {
            document.location = this.BaseAppUrl + "ChargeCredit/" + this.CustomerId + "/" + this.TerminalNumber;
        }
        else {
            bootbox.alert({
                message: 'Please select Terminal number first',
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
    ChangeTerminal(TerminalObj) {
        this.TerminalNumber = TerminalObj.Value;
        
    }

    ngOnInit() {
        if (localStorage.getItem("lang") == "") {
            localStorage.setItem("lang", "en");
        }
        this.Lang = localStorage.getItem("lang");
        
      
        
       this._resourceService.GetLangRes(this.Formtype, this.Lang).subscribe(response=> {
           //debugger;
           response = jQuery.parseJSON(response);
           if (response.IsError == true) {
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
               this.RES = response.Data;
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });          

       
        //Terminals
       this._customerService.CheckIsOpenCharge().subscribe(response=> {
            response = jQuery.parseJSON(response);
            if (response.IsError == true) {
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
                this._TerminalList = response.Data;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
}
