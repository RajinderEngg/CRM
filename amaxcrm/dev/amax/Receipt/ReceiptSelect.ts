import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {NgSwitch, NgSwitchWhen, NgSwitchDefault} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {RecieptService} from "../../services/RecieptService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";

declare var jQuery;

@Component({

    templateUrl: './app/amax/Receipt/templates/ReceiptSelect.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault],
    providers: [RecieptService, ResourceService]
})

export class AmaxReceiptSelect implements OnInit {
    baseUrl: string;
    RES: Object = {};
    Formtype: string = "SCREEN_RECEIPTCHOOSE";
    Lang: string = "";
    RecieptMode: string = "1";
    _RecieptModes = [];
    _Reciepts = [];
    ReceiptTypeId: string = "-1";
    EmployeeId: string = "";
    MsgClass: string = "text-primary";
    modelInput: Object = {};
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    BaseAppUrl: string = "";
    static $inject = ['$scope', '$location', '$anchorScroll'];
   // @ViewChild('RTLDiv') private myScrollContainer: ElementRef;
    constructor(private _resourceService: ResourceService, private _RecieptService: RecieptService, private _routeParams: RouteParams) {
        
        this.RES.SCREEN_RECEIPTCHOOSE = {};
        this.ReceiptTypeId = "-1";
        this.modelInput = {};
        //this.baseUrl = "http://localhost:3000/#/";
        //debugger;
        this.baseUrl = _resourceService.AppUrl;
        this.EmployeeId = _routeParams.params.Id;
        this.RecieptMode = "1";
        this.BaseAppUrl=_resourceService.AppUrl;
    }
    NextPage() {
        if (this.ReceiptTypeId != "-1") {
            var CustId = this._routeParams.params.CustId;
            document.location = this.BaseAppUrl + "ReceiptCreate/" + CustId + "/" + this.ReceiptTypeId;
        }
        else {
            bootbox.alert({
                message: "Please select Receipt Name and then click on Next button",
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
    SelectReceipt(RecTypeId) {
        this.ReceiptTypeId = RecTypeId;
        //alert(this.ReceiptTypeId);
        this._resourceService.setCookie("ReceiptTypeIdLast_" + this.EmployeeId, this.ReceiptTypeId, 7);   
    }
    GetReceipts(RModel) {
        this._resourceService.setCookie("ReceiptModeLast_" + this.EmployeeId, RModel, 7);   
        this._RecieptService.GetReceipts(this.EmployeeId, RModel).subscribe(resp=> {
            //debugger;
            var response = jQuery.parseJSON(resp);
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
                //debugger;
                this._Reciepts = response.Data;
                
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }

    ngOnInit() {
      //  alert("ddd");
        this.Lang = localStorage.getItem("lang");
        this._resourceService.GetLangRes(this.Formtype, this.Lang).subscribe(response=> {
            
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

        this._RecieptService.GetRecieptModes().subscribe(resp=> {
            //debugger;
            var response = jQuery.parseJSON(resp);
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
                this._RecieptModes = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        //if (this.EmployeeId != "") {
        //debugger;
        var getMode = this._resourceService.getCookie("ReceiptModeLast_" + this.EmployeeId);
        if (getMode.length > 0) {
            this.RecieptMode = getMode.substring(1, getMode.length);
            if (this.RecieptMode == "4") this.RecieptMode = "1";
        } else {
            this.RecieptMode = "1";
        }
        this.GetReceipts(this.RecieptMode);

        var getRecId = this._resourceService.getCookie("ReceiptTypeIdLast_" + this.EmployeeId);
        if (getRecId.length > 0) {
            this.ReceiptTypeId = getRecId.substring(1, getRecId.length);
        } else {
            this.ReceiptTypeId = "-1";
        }
        var rrmode = this.RecieptMode;
        var recid = this.ReceiptTypeId;

        window.setTimeout(function () {
            //alert(rrmode);
            jQuery("input[name=recmode][value=" + rrmode + "]").prop("checked", true);
            jQuery("input[name=ReceiptTypeId][value=" + recid + "]").prop("checked", true);
        }, 1000);
        
        //}
    }
}
