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

    templateUrl: './app/amax/RecieptType/templates/RecieptTemplate.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault],
    providers: [RecieptService, ResourceService]
})

export class AmaxRecieptTemplate implements OnInit {
    RES: Object = {};
    Formtype: string = "SCREEN_ADDRECIEPTTEMP";
    Lang: string = "";
    _ReceiptTypes = [];
    _RecieptThnksLettersList = [];
    ReceiptId: number = -1;
    ReceiptName: string = "";
    MsgClass: string = "text-primary";
    modelInput: Object = {};
    editmodelInput: Object = {};
    SAVE_BTN_TEXT: string;
    Isbtndisable: string = "";
    ShowLoader: boolean = false;
    ShowMsg: boolean = false;
    content: string = "";
    Msg: string = "";
    baseUrl: string = "";
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    static $inject = ['$scope', '$http', '$templateCache'];
    constructor(private _resourceService: ResourceService, private _RecieptService: RecieptService,
        private _routeParams: RouteParams) {
        
        this.RES.SCREEN_ADDRECIEPTTEMP = {};
        this.ReceiptName = "";
        this.modelInput = {};
        this.modelInput.ReceiptId = "";
        this.modelInput.ThanksLetterNameEng = "";
        this.content = "";
        //alert(_routeParams.params.Id);
        this.modelInput.ThanksLetterId = _routeParams.params.Id;
        this.baseUrl = _resourceService.AppUrl;
        //this.getRceiptThnksLetterDet();
    }
    EditTemplate() {
      
        var response = this.baseUrl + "Template/Edit/" + this.modelInput.ThanksLetterId+"/RecTemp";
        
        document.location = response;
    }
    CancelRecTemplate() {
        var response = this.baseUrl + "ReceiptTemplate/Add/0";
        
        document.location = response;
    }
    CancelBtn() {
        //this.modelInput = {};

        //jQuery("#editor").val("");

        //this.modelInput.ReceiptId = ""; 
        //this.modelInput.ThanksLetterNameEng = "";
        //this.modelInput.ThanksLetterName = "";
        //this.modelInput.MailSubj = "";
        //this.modelInput.MailBody = "";
        var response = this.baseUrl + "ReceiptType/0";
        if (this.modelInput.ReceiptId != "" && this.modelInput.ReceiptId != undefined && this.modelInput.ReceiptId != null) {
            response = this.baseUrl + "ReceiptType/" + this.modelInput.ReceiptId;
        }
        document.location = response;
    }
    getRceiptThnksLetterDet() {
        if (this.modelInput.ThanksLetterId != 0) {
            this._RecieptService.GetRecieptThnksLetter(this.modelInput.ThanksLetterId).subscribe(response=> {
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
                    //this.modelInput.ThanksLetterNameEng = response.Data.ThanksLetterNameEng;

                    this.modelInput = response.Data;
                }
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });
        }
    }
    saveReceiptTemplateData() {
        //debugger;
        this.Isbtndisable = "disabled";
        this.ShowLoader = true;
        //this.modelInput.ReceiptId = parseInt(this.modelInput.ReceiptId);
        this.modelInput.MailBody = jQuery("#editor").val();
        var jdata = JSON.stringify(this.modelInput);
        console.log(jdata)
        this._RecieptService.AddReceiptThnksLetter(jdata).subscribe(response=> {
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
                //this.MsgClass = "text-success";
                
                //this.SAVE_BTN_TEXT = this.RES.SCREEN_ADDRECIEPTTEMP.APP_BTN_SAVE;


                //this.modelInput = {};
                
                //jQuery("#editor").val("");
                    
                //this.modelInput.ReceiptId = ""; 


                
                //this.CancelRecTemplate();
                var response = this.baseUrl + "ReceiptType/0";
                if (this.modelInput.ReceiptId != "" && this.modelInput.ReceiptId != undefined && this.modelInput.ReceiptId != null) {
                    response = this.baseUrl + "ReceiptType/" + this.modelInput.ReceiptId;
                }
                document.location = response;
            }
            this.ShowMsg = true;
            this.Msg = response.ErrMsg;
        },
            error=> console.log(error),
            () => console.log("Save Call Compleated")
        );

    }
    ngOnInit() {
        
        this.Lang = localStorage.getItem("lang");
        //this.getRceiptThnksLetterDet();
        //this.editmodelInput.ThanksLetterNameEng = "Hello";
        //this.modelInput.ThanksLetterNameEng = this.editmodelInput.ThanksLetterNameEng;
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
                if (this.modelInput.ThanksLetterId != 0) {
                    this.SAVE_BTN_TEXT = this.RES.SCREEN_ADDRECIEPTTEMP.APP_BTN_UPDATE;
                }
                else {
                    this.SAVE_BTN_TEXT = this.RES.SCREEN_ADDRECIEPTTEMP.APP_BTN_SAVE;
                }
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        if (this.modelInput.ThanksLetterId != 0) {
            this._RecieptService.GetRecieptThnksLetter(this.modelInput.ThanksLetterId).subscribe(response=> {
               // debugger;
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
                    //this.modelInput = {};
                    this.modelInput = response.Data;
                    jQuery("#editor").val(this.modelInput.MailBody);
                    jQuery("#EditTemp").show();
                    jQuery('#editor').ckeditor(function () {

                    });
                    // this.editmodelInput.ThanksLetterId = response.Data.ThanksLetterId;
                    // alert(this.editmodelInput.ThanksLetterId);
                }
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });
        }
        else {
            jQuery("#editor").val("");
            jQuery("#EditTemp").hide();
            jQuery('#editor').ckeditor(function () {

            });
        }
        //this.modelInput.ThanksLetterNameEng = this.editmodelInput.ThanksLetterNameEng;
        this._RecieptService.BindRecieptType().subscribe(response=> {
            
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
                this._ReceiptTypes = response.Data;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        
        
    }
}
