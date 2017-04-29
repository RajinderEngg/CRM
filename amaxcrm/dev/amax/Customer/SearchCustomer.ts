import { bootstrap } from 'angular2/platform/browser';   ///////////////Used for Redux//////////////////////////  
import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams, ROUTER_PROVIDERS, APP_BASE_HREF} from "angular2/router";   ////////////////////ROUTER_PROVIDERS, APP_BASE_HREF Used For Redux////
import {Component, Output, Input, EventEmitter, OnInit, enableProdMode, provide} from "angular2/core";/////enableProdMode,provide is used for Redux
import {CustomerService} from "../../services/CustomerService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";
import {AutocompleteContainer} from '../../autocomplete/autocomplete-container';
import {Autocomplete} from '../../autocomplete/autocomplete.component';
import { AmaxDate } from '../../comonComponents/basicComponents';

export const AUTOCOMPLETE_DIRECTIVES = [Autocomplete, AutocompleteContainer];
declare var jQuery;
declare var swal;
declare var moment;
@Component({

    templateUrl: './app/amax/Customer/templates/CustomerSearch.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, AUTOCOMPLETE_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, AmaxDate],
    providers: [CustomerService, ResourceService]
})

export class AmaxSearchCustomers implements OnInit {
    modelInput = {};
    custSearchData: Object = [];
    CHANGEDIR: string = "";
    RES: Object = {};
    Lang: string = "";
    ChangeDialog: string = "";
    ForPopUp: number = 0;
    static $inject = ['$scope', '$location', '$anchorScroll'];
    BaseAppUrl: string = "";
    Formtype: string = "CUSTOMER_SEARCH";
    Isbtndisable: string = "";
    ShowLoader: boolean = false;
    FromPage: string = "";
    SearchContent: string = "";
    EditIconCss: string = "";
    IsExtended: boolean = false;
    Extended: boolean = false;
    IsRowFound: boolean = false;
    private selectedCar: string = '';
    private asyncSelectedCar: string = '';
    private autocompleteLoading: boolean = false;
    private autocompleteNoResults: boolean = false;
    private autocompleteSelect: boolean = false;

    private getCurrentContext() {

        return this;
    }
    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams) {
    
        this.RES.CUSTOMER_SEARCH = {};
        this.modelInput = {};
        this.modelInput.IsExtended = false;
        this.modelInput.Extended = false;
        this.modelInput.custSearchData = [];
        this.ForPopUp = _routeParams.params.ForPopup;
        this.FromPage = _routeParams.params.FromPage;
        if (this.FromPage == "ReceiptCreate") {
            this.EditIconCss = "mdi-notification-sync";
        }
        else {
            this.EditIconCss = "mdi-content-create";
        }
        
        //if (this.ForPopUp == 1) {
            
        //    jQuery('mx-navbar').css({ "display": "none" });
        //    jQuery('footer').css({ "display": "none" });
        //    jQuery('mx-breadcrumb').css({ "display": "none" });
        //}
        this.HideForPopUp(this.ForPopUp);
        this.BaseAppUrl = _resourceService.AppUrl;
        
    }

    private _cachedResult: any;
    private _previousasyncSelectedCar: string = '';

    private getAsyncData(context: any): Function {

        var SrchVal = context.asyncSelectedCar;
      
        // if (SrchVal != undefined && SrchVal != null && SrchVal != "") {

       
        // debugger;
        if (this._previousasyncSelectedCar == context.asyncSelectedCar) {
            //clearTimeout(this.StopTimeOut);
            return this._cachedResult;
        }
        else {
            //alert(this._previousasyncSelectedCar + " | " + context.asyncSelectedCar);
            if (context.asyncSelectedCar != "") {
                this._previousasyncSelectedCar = context.asyncSelectedCar;
                //  this.StopTimeOut = setTimeout(() => {
                //    alert(SrchVal);
                this._customerService.GetCompleteSearch(SrchVal).subscribe(response=> {
                    // debugger;
                    response = jQuery.parseJSON(response);
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
                        context.carsExample1 = response.Data;
                        //return context.carsExample1;

                    }
                }, error=> {
                    console.log(error);
                }, () => {
                    console.log("CallCompleted")
                });
                // }, 500);

                this._cachedResult = context.carsExample1;
            }
            else {
                this._cachedResult = [];
            }
            return this._cachedResult;
        }


    }

    private changeAutocompleteLoading(e: boolean) {
        this.autocompleteLoading = e;
    }

    private changeAutocompleteNoResults(e: boolean) {
        this.autocompleteSelect = false;
        this.autocompleteNoResults = e;
    }
    private autocompleteOnSelect(e: any) {
        this.autocompleteSelect = true;
        console.log(`Selected value: ${e.item}`);
        var CompData = e.item.split('|');
        //debugger;
        if (e.item != undefined && e.item != "" && e.item != null) {
            //alert(CompData[0]);


            if (this.FromPage == "ReceiptCreate") {
                var ReceiptId = localStorage.getItem("TempReceiptId");
                
                document.location=this.BaseAppUrl + "ReceiptCreate/" + CompData[0].trim() + "/" + ReceiptId;
                if (this.modelInput != undefined && this.modelInput != null) {
                    var jdata = JSON.stringify(this.modelInput);
                    if (this.FromPage == "ReceiptCreate") {
                        this._resourceService.setCookie(this.FromPage + "_Search_Cache", jdata, 10);
                        this._resourceService.setCookie(this.FromPage + "_Search_auto_Cache", this.asyncSelectedCar, 10);
                    }
                }
            }
            else {
                document.location=this.BaseAppUrl + "Customer/Add/" + CompData[0].trim();
            }
        }
    }


    HideForPopUp(ForPopUp) {
        if (ForPopUp == 1) {

            jQuery('mx-navbar').css({ "display": "none" });
            jQuery('footer').css({ "display": "none" });
            jQuery('mx-breadcrumb').css({ "display": "none" });
        }
    } 
    ChangeExtendedAttr() {
        this.modelInput = {};
        this.modelInput.custSearchData = [];
       
        this.modelInput.SearchContent = "";
        this.asyncSelectedCar = "";
        this.modelInput.IsRowFound = false;
        this.modelInput.IsExtended = jQuery("#Extended_").prop("checked");
       
    }
    OpenCustomerCard(CustObj) {
      
        if (this.FromPage == "ReceiptCreate") {
            var ReceiptId = localStorage.getItem("TempReceiptId");
            parent.window.open(this.BaseAppUrl + "ReceiptCreate/" + CustObj.CustomerId + "/" + ReceiptId,"_self");
            
        }
        else {
            parent.window.location.replace(this.BaseAppUrl + "Customer/Add/" + CustObj.CustomerId);
        }

    }
    SearchCustomer() {
        debugger;
        this.Isbtndisable = "disabled";
        this.ShowLoader = true;
        this._customerService.GetCompleteQuickSearch(this.modelInput.SearchContent).subscribe(response=> {
            
            response = jQuery.parseJSON(response);
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
                this.modelInput.custSearchData = response.Data;
                if (this.modelInput.custSearchData.length == 0) {
                    this.modelInput.IsRowFound = false;
                }
                else {
                    this.modelInput.IsRowFound = true;
                }
                if (this.modelInput != undefined && this.modelInput != null) {
                    var jdata = JSON.stringify(this.modelInput);
                    if (this.FromPage == "ReceiptCreate") {
                        this._resourceService.setCookie(this.FromPage + "_Search_Cache", jdata, 10);
                        this._resourceService.setCookie(this.FromPage + "_Search_auto_Cache", this.asyncSelectedCar, 10);
                    }
                }
            }
        }
        //    , error=> {
        //    console.log(error);
        //}, () => {
        //    console.log("CallCompleted")
        //    }
        );
        
        this.Isbtndisable = "";
        this.ShowLoader = false;
    }
    SetdefaultPage(){
      
        this.modelInput = {};
        this.modelInput.SearchContent = "";
        this.modelInput.custSearchData = [];
    }
    
    ngOnInit() {
        
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
        debugger;

        var jdata = this._resourceService.getCookie(this.FromPage + "_Search_Cache");
        if (jdata != undefined && jdata != undefined && jdata != "") {
            jdata = jdata.substring(1, jdata.length);
            this.modelInput = jQuery.parseJSON(jdata);
        }
        var asyn = this._resourceService.getCookie(this.FromPage + "_Search_auto_Cache");
        if (asyn != undefined && asyn != undefined && asyn != "") {
            asyn = asyn.substring(1, asyn.length);
            this.asyncSelectedCar = asyn;
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
               //alert(this.RES);
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
       
        
    }
}
