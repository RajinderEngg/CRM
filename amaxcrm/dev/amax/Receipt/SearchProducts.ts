import { bootstrap } from 'angular2/platform/browser';   ///////////////Used for Redux//////////////////////////  
import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams, ROUTER_PROVIDERS, APP_BASE_HREF} from "angular2/router";   ////////////////////ROUTER_PROVIDERS, APP_BASE_HREF Used For Redux////
import {Component, Output, Input, EventEmitter, OnInit, enableProdMode, provide} from "angular2/core";/////enableProdMode,provide is used for Redux
import {CustomerService} from "../../services/CustomerService";
import {RecieptService} from "../../services/RecieptService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";
import { AmaxDate } from '../../comonComponents/basicComponents';

declare var jQuery;
declare var swal;
declare var moment;
@Component({

    templateUrl: './app/amax/Receipt/templates/ProductsSearch.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES, AmaxDate],
    providers: [CustomerService, ResourceService, RecieptService]
})

export class AmaxSearchProducts implements OnInit {
    modelInput = {};
    CustomerId: string;
    custSearchData: Object = [];
    _ProdCats: Object = [];
    CHANGEDIR: string = "";
    RES: Object = {};
    Lang: string = "";
    ChangeDialog: string = "";
    ForPopUp: number = 0;
    static $inject = ['$scope', '$location', '$anchorScroll'];
    BaseAppUrl: string = "";
    Formtype: string = "RECEIPT_SEARCH";
    Isbtndisable: string = "";
    ShowLoader: boolean = false;
    FromPage: string = "";
    SearchContent: string = "";
    EditIconCss: string = "";
    IsExtended: boolean = false;
    Extended: boolean = false;
    IsRowFound: boolean = false;
    IsDirect: boolean = false;
    ForBack: string = "";

    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams, private _RecieptService: RecieptService) {

        this.RES.RECEIPT_SEARCH = {};
        this.modelInput = {};
        this.CustomerId = _routeParams.params.Id;
        this.modelInput.receiptSearchData = [];
        
        this.IsDirect = true;
        this.EditIconCss = "mdi-notification-sync";
        //this.ForPopUp = _routeParams.params.ForPopup;
        //this.FromPage = _routeParams.params.FromPage;
        
       
        //if (this.FromPage == "ReceiptCreate") {
        //    this.EditIconCss = "mdi-notification-sync";
        //    this.IsDirect = true;
        //   // this.ForBack = _routeParams.params.ForBack;
        //}
        //else {
        //    this.EditIconCss = "mdi-content-create";
        //    this.IsDirect = false;
        //}
        
        //if (this.ForPopUp == 1) {
            
        //    jQuery('mx-navbar').css({ "display": "none" });
        //    jQuery('footer').css({ "display": "none" });
        //    jQuery('mx-breadcrumb').css({ "display": "none" });
        //}
        // this.HideForPopUp(this.ForPopUp);
        this.BaseAppUrl = _resourceService.AppUrl;

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
    OpenReceiptCard(ProdObj) {
       // debugger;
        if (ProdObj != undefined && ProdObj != null) {
            var jdata = JSON.stringify(ProdObj);
            this._resourceService.setCookie("ReceiptCreate_Product", jdata, 10);
            var ReceiptId = localStorage.getItem("TempReceiptId");
            parent.window.open(this.BaseAppUrl + "ReceiptCreate/" + this.CustomerId + "/" + ReceiptId, "_self");
        }

    }
    //OpenCustomerCard(CustObj) {

    //    if (this.FromPage == "ReceiptCreate") {
    //        var ReceiptId = localStorage.getItem("TempReceiptId");
    //        parent.window.open(this.BaseAppUrl + "ReceiptCreate/" + CustObj.CustomerId + "/" + ReceiptId, "_self");

    //    }
    //    else {
    //        parent.window.location.replace(this.BaseAppUrl + "Customer/Add/" + CustObj.CustomerId);
    //    }

    //}
    SearchReceipt() {
        //  debugger;
        this.Isbtndisable = "disabled";
        this.ShowLoader = true;
        if (this.modelInput.ProdCatId == undefined || this.modelInput.ProdCatId == null) {
            this.modelInput.ProdCatId = "-1";
        }
        if (this.modelInput.PartNumber == undefined || this.modelInput.PartNumber == null) {
            this.modelInput.PartNumber = "";
        }
        if (this.modelInput.ProdNameDis == undefined || this.modelInput.ProdNameDis == null) {
            this.modelInput.ProdNameDis = "";
        }
        
        this._RecieptService.GetProducts(this.modelInput.ProdCatId, this.modelInput.PartNumber, this.modelInput.ProdNameDis).subscribe(response=> {

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
                this.modelInput.receiptSearchData = response.Data;
                if (this.modelInput.receiptSearchData.length == 0) {
                    this.modelInput.IsRowFound = false;
                }
                else {
                    this.modelInput.IsRowFound = true;
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
    BackPage() {
        //if (this.FromPage == "ReceiptCreate") {
        //    debugger;
            var ReceiptId = localStorage.getItem("TempReceiptId");
            parent.window.open(this.BaseAppUrl + "ReceiptCreate/" + this.CustomerId + "/" + ReceiptId, "_self");
        //}
    }
    SetdefaultPage() {

        this.modelInput = {};
        this.modelInput.SearchContent = "";
        this.modelInput.custSearchData = [];
    }

    ngOnInit() {
        this.modelInput.PartNumber = "";
        this.modelInput.ProdNameDis = "";
        this.modelInput.IsRowFound = false;

        this._RecieptService.GetProducts(-2, "", "").subscribe(response=> {

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
                this.modelInput.receiptSearchData = response.Data;
                if (this.modelInput.receiptSearchData.length == 0) {
                    this.modelInput.IsRowFound = false;
                }
                else {
                    this.modelInput.IsRowFound = true;
                }
            }
        }
            //    , error=> {
            //    console.log(error);
            //}, () => {
            //    console.log("CallCompleted")
            //    }
        );
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

        var jdata = this._resourceService.getCookie("_Search_Cache");
        if (jdata != undefined && jdata != undefined && jdata != "") {
            jdata = jdata.substring(1, jdata.length);
            this.modelInput = jQuery.parseJSON(jdata);
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
        this._RecieptService.GetProdCats().subscribe(response=> {
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
                this._ProdCats = response.Data;
                this.modelInput.ProdCatId = "-1";
                //alert(this.RES);
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

    }
}
