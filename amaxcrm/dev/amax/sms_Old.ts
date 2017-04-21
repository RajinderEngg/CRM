import {Component, OnInit} from "angular2/core";
import  {SelectInputComponent} from "../comonComponents/basicComponents/select"
import {AmaxService} from "../services/AmaxService";
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../amaxUtil";

declare var jQuery;

@Component({
    name:'amax-sms',
    pipes:[GroupFilterPipe, GroupParenFilterPipe],
    template:`
        <div class="row">
            <div class="col-xs-12">

                <div class="row">
                    <div class="col-xs-12 col-sm-4 col-md-6">
                        <label>Select Sms Provider</label>
                        <mx-select [data]="SmsData" label="Value" (onData)="SelectedData = $event" cssclass="form-control"></mx-select>
                    </div>
                    <div class="col-xs-12 col-sm-4 col-md-6">
                        <label>Select Phone Type</label>
                        <mx-select [data]="PhoneTypeListData" label="Label" (onData)="SelectedPhoneType = $event" cssclass="form-control"></mx-select>
                    </div>
                    <div class="col-xs-12 col-sm-4 col-md-3">
                        <label>Company</label>
                        <input class="form-control" type="text" readonly value="{{SelectedData.Value}}"/>
                    </div>
                    <div class="col-xs-12 col-sm-4 col-md-3">
                        <label>Username</label>
                        <input class="form-control" type="text" readonly value="{{SelectedData.UserName}}"/>
                    </div>
                    <div class="col-sm-12">
                        <label>Select Group</label>
                        <div class="k-content" style="margin: 4px auto;width: 100%; padding: 4px 10px 20px;">
                            <div id="groupTree" style="overflow: visible;"> Loading... </div>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <label>Message :</label>
                        <textarea #msg (keyup)="message=msg.value" class="form-control" placeholder="Type your messages"></textarea>
                        <span>{{msg.value.length||0}} of 120</span>
                        
                    </div>
                </div>
            </div>
            <div class="col-xs-12">
                <button class="btn btn-primary" (click)="SendToSelectedGroups()">Send Message</button>
            </div>
        </div>
    `,
    directives:[SelectInputComponent],
    providers:[AmaxService]
})
export class AmaxSmsComponent implements OnInit{
    //RES: Object;
    SmsData:Array<any>;
    SelectedData:any;

    PhoneTypeListData:Array<any>;
    SelectedPhoneType:any;
    userName:string;
    message:string;



    doNothing(){}


    openModel(){
        
    }

    constructor(private  _amaxService:AmaxService){
        this.SelectedData = {Name:"", Value:"", Company:"", UserName:""};
    }
    ngOnInit() {
        //debugger;
        //this.RES = jQuery.parseJSON(localStorage.getItem("langresource"));
        this._amaxService.GetDataFromServer({
            SmsCompanyList:{
                uqery:`
                        Select
                            usersms AS UserName,
                            companysms AS Value
                        from
                            ApplicationInfo
                    `,
                parameters:{}
            },
            PhoneTypeList:{
                uqery:"SELECT id AS Value, contentHeb+' ('+ contenteng +')' AS Label FROM PhoneTypes",
                parameters:{}
            }
        }).subscribe(
            (data) => {
                //debugger;
                console.log(data);
                var res = jQuery.parseJSON(data);
                
                this.SmsData = res.Data.data.SmsCompanyList;
                this.PhoneTypeListData = res.Data.data.PhoneTypeList;
            },
            (error)=>{},
            ()=>{}
        );
        
        this._amaxService.GetGeneralGroupTree().subscribe(
        
            (data) => {
                var res = jQuery.parseJSON(data);

                jQuery("#groupTree").kendoTreeView({
                    checkboxes: {
                        checkChildren: true
                    },
                    //check: this.onGroupSelect,
                    dataSource: res.Data.kendoTree
                });
            },
            (err) => {

            },
            () => {
            
            }
            
        );
    }


    getSelectedGroups(): Array<number>{
        //debugger;
        var _CheckedGroups = [];
        Kendo_utility.checkedNodeIds(jQuery("#groupTree").data("kendoTreeView").dataSource.view(),_CheckedGroups);
        return _CheckedGroups;
    }

    SendToSelectedGroups() {
        //debugger;
        //username:string,company:string,message:string,groups:Array<any>,phoneTypeId:number
        var _selectedGroups = this.getSelectedGroups();
        var status = "ok";

        if(_selectedGroups.length == 0) status = "No groups selected";
        else if(!this.SelectedData.UserName ||!this.SelectedData.Value) status="Please select a provider";
        else if(!this.SelectedPhoneType.Value) status="Select Phone type";
        else if(!this.message) status = "Message can't be empty";

        if(status!="ok"){
            alert(status);
            return;
        };

        this._amaxService.SendSms(
            this.SelectedData.UserName,
            this.SelectedData.Value,
            this.message,
            this.getSelectedGroups(),
            this.SelectedPhoneType.Value
        ).subscribe(data=> {
            debugger;
                console.log(data)
            var res = jQuery.parseJSON(data);
            alert(res.Data.err);

                
            }
            ,err=>{
                console.log(err)
            },()=>{
                console.log("Sms send responce compleated!");
            }
        )
    }
}