//import {NgModule} from '@angular/core';
//import {FormsModule} from '@angular/forms';
import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {NgSwitch, NgSwitchWhen, NgSwitchDefault} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {RecieptService} from "../../services/RecieptService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";
//import {CKEditorModule} from 'ng2-ckeditor';

declare var jQuery;
declare var CKEDITOR;
//@NgModule({
//    imports: [
//        CKEditorModule
//    ]
//    //,declarations: [
//    //    App,
//    //],
//    //bootstrap: [App]
//})
@Component({

    templateUrl: './app/amax/RecieptType/templates/Template.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault],
    providers: [RecieptService, ResourceService]
})

export class AmaxTemplate implements OnInit {
    RES: Object = {};
    Formtype: string = "SCREEN_EDITTEMP";
    Lang: string = "";
    MsgClass: string = "text-primary";
    modelInput: Object = {};
    Isbtndisable: string = "";
    ShowLoader: boolean = false;
    ShowMsg: boolean = false;
    FPage: string="";
    Msg: string = "";
    OrgId: string = "";
    baseUrl: string;
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    static $inject = ['$scope', '$http', '$templateCache'];
    constructor(private _resourceService: ResourceService, private _RecieptService: RecieptService,
        private _routeParams: RouteParams) {
        
        this.RES.SCREEN_EDITTEMP = {};
        
        this.modelInput = {};
        
        //alert(_routeParams.params.Id);
        this.modelInput.ThanksLetterId = _routeParams.params.Id;
        this.FPage = _routeParams.params.FPage;
        this.baseUrl = _resourceService.AppUrl;
        //this.getRceiptThnksLetterDet();
    }
    CancelBtn() {
        //this.modelInput = {};

        //jQuery("#editor").val("");

        //this.modelInput.MailBody = "";
        var redresponse = this.baseUrl + "ReceiptTemplate/Add/" + this.modelInput.ThanksLetterId;
        if (this.FPage == "Rec") {
            if (this.modelInput.ThanksLetterId > 0) {

                //alert('Hello');
                this._RecieptService.GetRecieptThnksLetter(this.modelInput.ThanksLetterId).subscribe(response=> {
                    console.log(response);
                    response = jQuery.parseJSON(response);
                    this.Isbtndisable = "";
                    this.ShowLoader = false;

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
                        if (response.Data.ReceiptId != undefined && response.Data.ReceiptId != "" && response.Data.ReceiptId != null) {
                            redresponse = this.baseUrl + "ReceiptType/" + response.Data.ReceiptId;
                        }
                        else {
                            redresponse = this.baseUrl + "ReceiptType/0";
                        }
                        document.location = redresponse;

                    }
                    this.ShowMsg = true;
                    this.Msg = response.ErrMsg;
                },
                    error=> console.log(error),
                    () => console.log("Save Call Compleated")
                );

            }
        }
        else {
            document.location = redresponse;
        }
    }
    saveTemplateData() {
        if (this.modelInput.ThanksLetterId > 0) {
            this.Isbtndisable = "disabled";
            this.ShowLoader = true;
            //this.modelInput.ReceiptId = parseInt(this.modelInput.ReceiptId);
            this.modelInput.MailBody = jQuery("#editor").val();
            var jdata = JSON.stringify(this.modelInput);
            console.log(jdata)
            this._RecieptService.SaveTemplate(this.modelInput.ThanksLetterId, this.modelInput.MailBody).subscribe(response=> {
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
                    // debugger;

                    var redresponse = this.baseUrl + "ReceiptTemplate/Add/" + this.modelInput.ThanksLetterId;
                    if (this.FPage == "Rec") {
                        if (this.modelInput.ThanksLetterId > 0) {


                            this._RecieptService.GetRecieptThnksLetter(this.modelInput.ThanksLetterId).subscribe(response=> {
                                console.log(response);
                                response = jQuery.parseJSON(response);
                                this.Isbtndisable = "";
                                this.ShowLoader = false;

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
                                    if (response.Data.ReceiptId != undefined && response.Data.ReceiptId != "" && response.Data.ReceiptId != null) {
                                        redresponse = this.baseUrl + "ReceiptType/" + response.Data.ReceiptId;
                                    }
                                    else {
                                        redresponse = this.baseUrl + "ReceiptType/0";
                                    }
                                    document.location = redresponse;
                                }
                                this.ShowMsg = true;
                                this.Msg = response.ErrMsg;
                            },
                                error=> console.log(error),
                                () => console.log("Save Call Compleated")
                            );

                        }
                    }
                    else {
                        document.location = redresponse;
                    }


                    this.MsgClass = "text-success";



                    //this.modelInput = {};

                    jQuery("#editor").val("");
                    this.modelInput.MailBody = "";
                }
                this.ShowMsg = true;
                this.Msg = response.ErrMsg;
            },
                error=> console.log(error),
                () => console.log("Save Call Compleated")
            );
        }
    }
    ngOnInit() {
        
        this.Lang = localStorage.getItem("lang");
        this.OrgId = localStorage.getItem("OrgId");

        
        if (this.modelInput.ThanksLetterId > 0) {
            this._RecieptService.GetTemplate(this.modelInput.ThanksLetterId).subscribe(response=> {
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
                    jQuery("#editor").val("");
                    jQuery('#editor').ckeditor(function () {

                    });
                }
                else {
                    //this.modelInput.ThanksLetterNameEng = response.Data.ThanksLetterNameEng;

                    this.modelInput.MailBody = response.Data;
                    //if (this.modelInput.MailBody != undefined && this.modelInput.MailBody != null) {
                    jQuery("#editor").val(this.modelInput.MailBody);
                        jQuery('#editor').ckeditor(function () {

                        });
                   // }
                    
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });
        }
        else {
            jQuery("#editor").val("");

            jQuery('#editor').ckeditor(function () {

            });
        }
        

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
                //if (this.modelInput.ThanksLetterId != 0) {
                //    this.SAVE_BTN_TEXT = this.RES.SCREEN_ADDRECIEPTTEMP.APP_BTN_UPDATE;
                //}
                //else {
                //    this.SAVE_BTN_TEXT = this.RES.SCREEN_ADDRECIEPTTEMP.APP_BTN_SAVE;
                //}
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        

        
        
    }
}
