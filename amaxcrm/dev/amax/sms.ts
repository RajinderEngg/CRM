import {Component, OnInit} from "angular2/core";
import {SelectInputComponent} from "../comonComponents/basicComponents/select"
import {AmaxService} from "../services/AmaxService";
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../amaxUtil";
import {ResourceService} from "../services/ResourceService";
import {LocalDict, crmConfig} from "../crmconfig";
import {AmaxCrmSyinc} from "../services/AmaxCrmSyinc";

declare var jQuery;
declare var switch_direction;

@Component({
    name: 'amax-sms',
    pipes: [GroupFilterPipe, GroupParenFilterPipe],
    template: `<div class="row" *ngIf="RES">

    <div class="col s12 card-panel">
<div class="row">
<div class="col s12">
<h1>
                            <i class="mdi-content-select-all green-text"></i>
                            <span class="red-text">{{RES.LBL_HEADING}}</span>  <!--{{RES.CUSTOMER_MASTER.CUST_LABAL}}Customer-->
                        </h1>

</div>
<button class="btn waves-effect waves-light indigo {{ _amaxCrmSyinc.isRtl() ? 'right' : 'left'}}"
                    (click)="openSmsSettings()">
                <i class="mdi-action-settings"></i> {{RES.LBL_SETTING}}
            </button>

</div>
        <div style="overflow: hidden;">
            <div class="col s12">&nbsp;</div>
            <div id="SmsSettingsPannel" class="col s12 animated slideInDown" *ngIf="openSettrings">
               <div class="col s12">
                    <label>{{RES.LBL_SMS_PROVIDERS}}</label>
                    <select class="browser-default" [(ngModel)]= "SelectedProvider.value">
                        
                        <option value="1">Sms 2 you</option>
                        <option value="2">019 Sms</option>
                        
                    </select>
                </div>
                <div class="input-field col s6">
                    
                    <input type="text" id="UserName" class="form-control" [(ngModel)]= "SelectedData.UserName">
                    <label for="UserName" class="active">{{RES.LBL_SMS_USERNAME}}</label>
                </div>
                <div class="input-field col s6">
                    
                    <input class="form-control" type="password" id="Password" [(ngModel)]= "SelectedData.Value"/>
                    <label for="Password" class="active">{{RES.SMS_PASSWORD}}</label>
                </div>
                
                <div class="input-field col s12">
                    
                    <input class="form-control" id="PhNo" type="text" [(ngModel)]="SenderPhoneNumber">
                    <label for="PhNo" class="active">{{RES.LBL_SENDER_PHONE}}</label>
                </div>
                <div class="col s12">&nbsp;</div>
                <div class="col s12">
                    <button class="btn waves-effect waves-light green accent-4" (click)="saveSmsSettings()" style="width:200px!important;">
                        <i class="mdi-content-save"></i> {{RES.LBL_SAVE_SETTING}}
                    </button>
                    <button class="btn waves-effect waves-light  grey lighten-2" (click)="closeSmsSettings()"style="width:200px!important;">
                        <i class="mdi-navigation-close"></i> {{RES.LBL_CLOSE_SETTINGS}}
                    </button>
                </div>
            </div>
            <div class="col s12" *ngIf="!openSettrings">
                <div class="row">
                    <div class="col s6">
                        <div class="row">
                            <div class="col s12">
                                <label>{{RES.LBL_GROUPS}}</label>
                                <div class="k-content {{KendoRTLCSS}}" style="max-height: 230px;overflow-y: auto; padding: 20px 10px 40px; margin-left:10px;">
                                    <div id="groupTree" style="overflow: visible;"> Loading...</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col s6">
                        <div class="row">
                            <div class="col s12">
                                <label>{{RES.LBL_PHONE_TYPES}}</label>
                                <mx-select [data]="PhoneTypeListData" label="Label" selectedval="All" firstvalue="All" 
                                           (onData)="SelectedPhoneType = $event" cssclass="browser-default" id="PhontTypesel"></mx-select>
                            </div>
                            <div class="col s12">&nbsp;</div>
                            <div class="col s12">
                                <div class="row">
                                    <details>
                                        <summary>{{RES.LBL_SEND_LATER}}</summary>
                                        <div class="col s6">
                                            <label>{{RES.SMS_LBL_DATE}}</label>
                                            <div class="k-content">
                                                <input class="form-control" id="sendLaterDate">
                                            </div>
                                        </div>
                                        <div class="col s3">
                                            <label>{{RES.SMS_LBL_HR}}</label>
                                            <select class="browser-default" id="sendLaterHour">
                                                <option>01</option>
                                                <option>02</option>
                                                <option>03</option>
                                                <option>04</option>
                                                <option>05</option>
                                                <option>06</option>
                                                <option>07</option>
                                                <option>08</option>
                                                <option>09</option>
                                                <option>10</option>
                                                <option>11</option>
                                                <option>12</option>
                                                <option>13</option>
                                                <option>14</option>
                                                <option>15</option>
                                                <option>16</option>
                                                <option>17</option>
                                                <option>18</option>
                                                <option>19</option>
                                                <option>20</option>
                                                <option>21</option>
                                                <option>22</option>
                                                <option>23</option>
                                                <option>24</option>
                                            </select>
                                        </div>
                                        <div class="col s3">
                                            <label>{{RES.SMS_LBL_MIN}}</label>
                                            <select class="browser-default" id="sendLaterMin">
                                                <option>00</option>
                                                <option>05</option>
                                                <option>10</option>
                                                <option>15</option>
                                                <option>20</option>
                                                <option>25</option>
                                                <option>30</option>
                                                <option>35</option>
                                                <option>40</option>
                                                <option>45</option>
                                                <option>50</option>
                                                <option>55</option>
                                                <option>60</option>
                                            </select>
                                        </div>
                                    </details>
                                </div>
                            </div>
                            <div class="col s12">&nbsp;</div>
                            <div class="col s12">
                                <label>{{RES.LBL_MESSAGE}}</label>
                                <textarea cols="50" #msg (keyup)="message=msg.value" class="form-control"
                                          placeholder="{{RES.SMS_TXT_PH_MESSAGE}}" (change)="SetdefaultMsgValue()" id="Messagetxtar" [(ngModel)]= "message"></textarea>

                                <span>{{RES.SMS_LBL_MAXCHAR}} : {{msg.value.length||0}}</span>
                            </div>
                        </div>
                    </div>
                    <div class="col s12" id="SelectedCustomers">&nbsp;</div>
                    <div class="col s12">
                        <button class="btn waves-effect waves-light green accent-4" style="width:175px!important;" (click)="SendToSelectedGroups()">{{RES.SMS_BTN_SENDMESSAGE}}</button>
                    </div>
                    <div class="space-4"><div>

                </div>
            </div>
        <br/><br/>
        </div>
    `,
    directives: [SelectInputComponent],
    providers: [AmaxService, AmaxCrmSyinc]
})
export class AmaxSmsComponent implements OnInit {
    private RES: any;
    private openSettrings: boolean = false;

    SmsData: Array<any>;
    SelectedData: Object = {};
    selectedValue: string;
    SmsProvider: Array<any>;
    SelectedProvider: Object = {};

    ConfirmedSend: boolean = false;
    Language: string;
    PhoneTypeListData: Array<any>;
    SelectedPhoneType: Object = {};
    userName: string;
    message: string;
    SenderPhoneNumber: string;
    KendoRTLCSS: string = "";
    ChangeDialog: string = "";
    CHANGEDIR: string = "";



    constructor(private _amaxService: AmaxService, private _resourceService: ResourceService, private _amaxCrmSyinc: AmaxCrmSyinc) {
        this.SelectedData.UserName = "";
        this.SelectedData.Value = "";
        this.SenderPhoneNumber = "";
        this.SelectedPhoneType.Value = "";
        this.SelectedProvider.value = "";
        var empid = localStorage.getItem("employeeid");
        var message = this._resourceService.getCookie(empid+"SMSMessage");
        if (message.length > 0 && message[0] == "=")
            message = message.substring(1, message.length);
        this.message = message;

    }
    SetdefaultMsgValue() {
        //alert('Hello');
        var empid = localStorage.getItem("employeeid");
        this._resourceService.setCookie(empid + "SMSMessage", this.message, 10);

        
        //alert(message);
    }
    ngOnInit() {
        //debugger;
        
        //this._amaxCrmSyinc.on('lsset.' + LocalDict.languageResource, data=> this.RES = data["SCREEN_SMS"]);
        //this.RES = this._amaxCrmSyinc.fetchLanguageResource("SCREEN_SMS");





        //alert('hello');
        //loadingLanguage start
        
        
        this._amaxCrmSyinc.on('lsset.' + LocalDict.languageResource, data=> this.RES = data["SCREEN_SMS"]);
        this.RES = this._amaxCrmSyinc.fetchLanguageResource("SCREEN_SMS");
        //debugger;

        if (!this.RES) {
            if (!localStorage.getItem(LocalDict.selectedLanguage)) {
                localStorage.setItem(LocalDict.selectedLanguage, crmConfig.falbackLanguage);
            }
            this.Language = localStorage.getItem("lang");
            if (this.Language == "he") {
                this.KendoRTLCSS = "k-rtl";
            }
            this._amaxCrmSyinc.loadLanguageResource(localStorage.getItem(LocalDict.selectedLanguage));
        }
        
        //loadingLanguage start
        this.allBind();
        //alert('Bye');
        //var message = this._resourceService.getCookie("SMSMessage");
        //if (message.length > 0 && message[0] == "=")
        //    message = message.substring(1, message.length);
        //this.message = message;
        
    }

    //saveSmsSettings(){
    //    alert("Sms Setting saved.");
    //    this.openSettrings=false;
    //}
    //openSmsSettings(){
    //    this.openSettrings=true;
    //    //setTimeout(function () {
    //    //    jQuery('#SmsSettingsPannel').addClass('animated bounceInDown');
    //    //},200);
    //}
    //closeSmsSettings(){
    //    jQuery('#SmsSettingsPannel').addClass('animated slideOutDown');
    //    setTimeout(()=>{
    //        this.openSettrings=false;
    //    },200);
    //}






    
    saveSmsSettings() {
       // debugger;
        var jsonObject = { UserName: "", Value: "", SelectedProvider: "", SenderPhoneNumber: "" };
        jsonObject.UserName = this.SelectedData.UserName;
        //jsonObject.Value = this.SelectedData.Value;

        //jsonObject.UserName = $("#User_txt").val;//this.SelectedData.UserName;
        //jsonObject.Value = $("#Pass_txt").val;//this.SelectedData.Value;

        jsonObject.SelectedProvider = this.SelectedProvider.value;
        jsonObject.SenderPhoneNumber = this.SenderPhoneNumber;
        
        if (jsonObject.UserName && this.SelectedData.Value && jsonObject.SelectedProvider && jsonObject.SenderPhoneNumber) {
            this._amaxCrmSyinc.storeLocal(LocalDict.SmsSettings, jsonObject);
            var empid = localStorage.getItem("employeeid");
            this._resourceService.setCookie(empid + "SMSDet", JSON.stringify(jsonObject), 10);
            this.closeSmsSettings();
        } else {
            bootbox.alert({
                message: "Please Varifye the settings",
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
    openSmsSettings() {
        //debugger;
        this.openSettrings = true;
        //var jsonObject = JSON.parse(this._amaxCrmSyinc.fetchLocal(LocalDict.SmsSettings));
        var empid = localStorage.getItem("employeeid");
        var data = this._resourceService.getCookie(empid + "SMSDet");
        var jsonObject = {};
        if (data.length > 0)
            jsonObject = jQuery.parseJSON(data.substring(1, data.length));
       // debugger;
        if (jsonObject != null) {
            for (var i = 0; i < this.SmsData.length; i++)
                if (this.SmsData[i].UserName == jsonObject.UserName)
                    this.SelectedData.UserName = this.SmsData[i].UserName;   //&& this.SmsData[i].Value == jsonObject.Value

            for (var i = 0; i < this.SmsProvider.length; i++)
                if (this.SmsProvider[i].value == jsonObject.SelectedProvider) this.SelectedProvider = this.SmsProvider[i];

            this.SenderPhoneNumber = jsonObject.SenderPhoneNumber;
        }
        else {
            this.SelectedData.UserName = "";
            this.SelectedData.Value = "";
            this.SenderPhoneNumber = "";
        }
    }
    closeSmsSettings() {
        this.openSettrings = false;
        this.allBind();
    }

    doNothing() {
    }

    openModel() {
    }


    setSmsCompanyList(data: any): void {
        this.SmsData = data;
        this.SelectedData = this.SmsData[0];
    }
    bindPhoneTypeList(data: any): void {

        this.PhoneTypeListData = data;
        this.SelectedPhoneType = this.PhoneTypeListData[0];
    }
    bindGeneralGroupTree(data: any): void {
       // debugger;
        jQuery("#groupTree").html("Loding...");
        var res = jQuery.parseJSON(data);
        setTimeout(() => {
            jQuery("#groupTree").kendoTreeView({
                loadOnDemand: true,
                checkboxes: {
                    checkChildren: true
                },
                dataSource: res.Data.kendoTree
            });
            jQuery("#sendLaterDate").kendoDatePicker({
                format: "dd-MM-yyyy"
            });
            switch_direction();
        },
            1000);
    }


    getSelectedGroups(): Array<number> {
        var _CheckedGroups = [];
        Kendo_utility.checkedNodeIds(jQuery("#groupTree").data("kendoTreeView").dataSource.view(), _CheckedGroups);
        return _CheckedGroups;
    }

    SendToSelectedGroups() {
        //username:string,company:string,message:string,groups:Array<any>,phoneTypeId:number
        debugger;
        if (this.SelectedPhoneType == undefined || this.SelectedPhoneType == null || this.SelectedPhoneType.Value=="") {
            this.SelectedPhoneType.Value = 0;
        }
        if (this.SelectedPhoneType.Value == 1 && this.SenderPhoneNumber.length != 10) {
            bootbox.alert({
                message: "Sender Must be 10 Digit valid cellphone number",
                className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
            this.openSmsSettings();
            return;
        }
        var _selectedGroups = this.getSelectedGroups();
        var status = "ok";

        if (_selectedGroups.length == 0) status = "No groups selected";
        //else if (!this.SelectedData.UserName || !this.SelectedData.Value) status = "Please select a provider";
        //else if (!this.message) status = "Message can't be empty";

        if (status != "ok") {
            bootbox.alert({
                message: status,
                className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
            return;
        };
        var sendlater = jQuery('#sendLaterDate').val() + " " + jQuery('#sendLaterHour').val() + ":" + jQuery('#sendLaterMin').val();
        if (sendlater.length != 16) sendlater = "";

        var smsSettings = JSON.parse(this._amaxCrmSyinc.fetchLocal(LocalDict.SmsSettings));
        if (!smsSettings) {
            bootbox.alert({
                message: "Sms Settings not found",
                className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
            this.openSmsSettings();
            return false;
        }
        else {
            smsSettings.Value = this.SelectedData.Value;
        }
        this._amaxService.SendSms(
            smsSettings.UserName,
            smsSettings.Value,
            this.message,
            this.getSelectedGroups(),
            this.SelectedPhoneType.Value
            , smsSettings.SelectedProvider,
            !this.ConfirmedSend,
            this.ConfirmedSend,
            smsSettings.SenderPhoneNumber,
            sendlater
        ).subscribe(data=> {
            console.log(data);
           // debugger;
            var _data = jQuery.parseJSON(data).Data;
            
            /*jQuery("#SelectedCustomers").kendoGrid({
                dataSource: {
                    data: _data.Customers
                },
                height: 350,
                selectable: "multiple"
            });*/
            if (!this.ConfirmedSend) {
                if (_data.err) {

                    bootbox.alert({
                        message: _data.err,
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                    this.openSmsSettings();
                    return;
                }
                var RemainingCredit = _data.RemainingCredit;
                var TotalCustomers = _data.TotalCustomers;

                if (RemainingCredit > 0) {
                    if (RemainingCredit > TotalCustomers) {
                        var message = "Total customers : " + TotalCustomers;
                        message += "\nRemaining Creadit for sms : " + RemainingCredit;
                        message += "\n\nProcide to send ?";
                        if (confirm(message)) {
                            this.ConfirmedSend = true;
                            this.SendToSelectedGroups();
                        }
                    }
                } else {
                    if (typeof RemainingCredit != 'undefined')
                        bootbox.alert({
                            message: "No SMS creadit",
                            className: this.ChangeDialog,
                            buttons: {
                                ok: {
                                    //label: 'Ok',
                                    className: this.CHANGEDIR
                                }
                            }
                        });
                    else
                        bootbox.alert({
                            message: "Error processing request",
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
            else {
                this.ConfirmedSend = false;
                switch (_data.status) {
                    case 1:
                        bootbox.alert({
                            message: _data.message,
                            className: this.ChangeDialog,
                            buttons: {
                                ok: {
                                    //label: 'Ok',
                                    className: this.CHANGEDIR
                                }
                            }
                        });
                        break;
                    case 0:
                        if (this.SelectedProvider.value == 1) {
                            if (_data.err.indexOf("ERROR") > -1 && _data.err.indexOf("SENDER_PREFIX") > -1) {
                                bootbox.alert({
                                    message: "Invalid Sender Phone Number",
                                    className: this.ChangeDialog,
                                    buttons: {
                                        ok: {
                                            //label: 'Ok',
                                            className: this.CHANGEDIR
                                        }
                                    }
                                });
                                this.openSmsSettings();
                                return false;
                            }
                        } else if (this.SelectedProvider.value == 2) {
                            bootbox.alert({
                                message: "Unable To Send Message",
                                className: this.ChangeDialog,
                                buttons: {
                                    ok: {
                                        //label: 'Ok',
                                        className: this.CHANGEDIR
                                    }
                                }
                            });
                        }
                        break;
                    default:
                        console.log(data);
                }
            }
        }
            , err=> {
                if (typeof err._body == 'string') {
                    bootbox.alert({
                        message: JSON.parse(err._body).error,
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                } else {
                    bootbox.alert({
                        message: "Unable to connect to the Service",
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                }
            }, () => {
                console.log({
                    message: "Sms send responce compleated!",
                    className: this.ChangeDialog,
                    buttons: {
                        ok: {
                            //label: 'Ok',
                            className: this.CHANGEDIR
                        }
                    }
                });
            }
            )
    }


    allBind() {
       // debugger;

        this.SmsProvider = [
            { name: "Sms 2 You", value: 1 },
            { name: "019 Sms", value: 2 }
        ];
        this.SelectedProvider = this.SmsProvider[0];
        //alert(this._amaxService.GetGeneralGroupTree(0));
        //this.bindGeneralGroupTree(this._amaxService.GetGeneralGroupTree(0));

        this._amaxService.GetGeneralGroupTree().subscribe(

            (data) => {
                var res = jQuery.parseJSON(data);

                jQuery("#groupTree").kendoTreeView({
                    loadOnDemand: true,
                    checkboxes: {
                        //checkChildren: true
                    },
                    ////check: this.onGroupSelect,
                    check: function (e) {
                        this.expandRoot = e.node;

                        this.expand(jQuery(this.expandRoot).find(".k-item").addBack());
                    },
                    dataSource: res.Data.kendoTree
                });
                jQuery("#sendLaterDate").kendoDatePicker({
                    format: "dd-MM-yyyy"
                });
            },
            (err) => {

            },
            () => {

            }

        );
        //jQuery("#sendLaterDate").kendoDatePicker({
        //    format: "dd-MM-yyyy"
        //});


        //this.bindPhoneTypeList(this._resourceService.GetLocalStorage("CellPhoneTypeList"));
        //this.setSmsCompanyList(this._resourceService.GetLocalStorage("SmsCompanyList"));
        this._amaxService.GetDataFromServer({
            SmsCompanyList: {
                uqery: `
                        Select
                            usersms AS UserName,
                            passwordsms AS Value
                        from
                            ApplicationInfo
                    `,
                parameters: {}
            },
            PhoneTypeList: {
                uqery: "SELECT id AS Value, contentHeb+' ('+ contenteng +')' AS Label FROM PhoneTypes",
                parameters: {}
            }
        }).subscribe(
            (data) => {

                //debugger;
                console.log(data);
                var res = jQuery.parseJSON(data);

                this.SmsData = res.Data.data.SmsCompanyList;
                this.PhoneTypeListData = res.Data.data.PhoneTypeList;
                var phonetypeid = "";
                jQuery.each(this.PhoneTypeListData, function () {
                    phonetypeid = this.Label;

                    return false;
                });
                
                //jQuery("#PhontTypesel").attr("selectedval", phonetypeid);
            },
            (error) => { },
            () => { }
            );

    }
    
    //ngOnInit() {
    //    debugger;
    //    alert('hello');
    //    //loadingLanguage start
    //    this._amaxCrmSyinc.on('lsset.' + LocalDict.languageResource, data=> this.RES = data["SCREEN_SMS"]);
    //    this.RES = this._amaxCrmSyinc.fetchLanguageResource("SCREEN_SMS");
    //    if (!this.RES) {
    //        if (!localStorage.getItem(LocalDict.selectedLanguage)) {
    //            localStorage.setItem(LocalDict.selectedLanguage, crmConfig.falbackLanguage);
    //        }
    //        this._amaxCrmSyinc.loadLanguageResource(localStorage.getItem(LocalDict.selectedLanguage));
    //    }
    //    //loadingLanguage start
    //    this.allBind();
    //    alert('Bye');
    //}










}




