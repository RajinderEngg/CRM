import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {NgSwitch, NgSwitchWhen, NgSwitchDefault} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {RecieptService} from "../../services/RecieptService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";

declare var jQuery;

@Component({

    templateUrl: './app/amax/RecieptType/templates/RecieptType.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault],
    providers: [RecieptService, ResourceService]
})

export class AmaxReciept implements OnInit {
    baseUrl: string;
    RES: Object = {};
    Formtype: string = "SCREEN_RECIEPTTYPE";
    Lang: string = "";
    _RecieptTypeList = [];
    _RecieptThnksLettersList = [];
    ReceiptId: number = -1;
    ReceiptName: string = "";
    MsgClass: string = "text-primary";
    modelInput: Object = {};
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    static $inject = ['$scope', '$location', '$anchorScroll'];
   // @ViewChild('RTLDiv') private myScrollContainer: ElementRef;
    constructor(private _resourceService: ResourceService, private _RecieptService: RecieptService, private _routeParams: RouteParams) {
        
        this.RES.SCREEN_RECIEPTTYPE = {};
        this.ReceiptName = "";
        this.modelInput = {};
        //this.baseUrl = "http://localhost:3000/#/";
        //debugger;
        this.baseUrl = _resourceService.AppUrl;
        this.ReceiptId = _routeParams.params.Id;
    }
    
    bindReceiptThnksLetterList() {
        this._RecieptService.GetRecieptThnksLettersList(this.ReceiptId).subscribe(resp=> {
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
                this._RecieptThnksLettersList = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    //app.controller('TestCtrl', function($scope, $location, $anchorScroll) {
        ViewTemplates(ReceiptObj) {
            //debugger;
            this.ReceiptId = ReceiptObj.RecieptTypeId;
            this.ReceiptName = ReceiptObj.RecieptNameEng + " | " + ReceiptObj.RecieptName;
            this.bindReceiptThnksLetterList(); 
            var dist = jQuery('#RTLDiv').offset().top - jQuery('#RTDiv').offset().top;
            
            window.scrollTo(0, dist+10);
            //var response = this.baseUrl + "/ReceiptType/#RTLDiv";
            //alert(response);
            //document.location = response;
            //$location.hash('RTLDiv');
            //$anchorScroll();
            //this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
            
        }
    //}
    deleteReceiptThnksLetter(RTLObj) {
        if (confirm("Do you want to delete")) {
            this._RecieptService.DeleteReceiptThnksLetter(RTLObj.ThanksLetterId).subscribe(resp=> {
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
                    //this.MsgClass = "text-danger";
                }
                else {
                    //this.MsgClass = "text-success";
                    //this._RecieptTypeList = response.Data;
                    var index = 0;
                    jQuery.each(this._RecieptThnksLettersList, function () {
                        if (this == RTLObj) {
                            return false
                        }
                        index = index + 1;
                    });
                    this._RecieptThnksLettersList.splice(index, 1);
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
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });
        }
    }
    AddRTLScreen() {
        //if (confirm("Do you want to add new")) {

            var response = this.baseUrl +"ReceiptTemplate/Add/0";
            //alert(response);
            document.location = response;
            
        //}
    }
    EditTemplate(RcptThnksLtrObj) {
        var response = this.baseUrl + "Template/Edit/" + RcptThnksLtrObj.ThanksLetterId+"/Rec";
        //alert(response);
        document.location = response;
    }
    EditReceiptThnksLetter(RcptThnksLtrObj) {
        var response = this.baseUrl + "ReceiptTemplate/Add/" + RcptThnksLtrObj.ThanksLetterId;
        //alert(response);
        document.location = response;
    }
    ngOnInit() {
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

        this._RecieptService.GetRecieptTypeList().subscribe(resp=> {
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
                this._RecieptTypeList = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        if (this.ReceiptId != 0) {
            this.bindReceiptThnksLetterList();
        }
    }
}
