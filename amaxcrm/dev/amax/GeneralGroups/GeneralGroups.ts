import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {RouteParams} from "angular2/router";
import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {AmaxCrmSyinc} from "../../services/AmaxCrmSyinc";
import {ResourceService} from "../../services/ResourceService";
import {AmaxService} from "../../services/AmaxService";
import {GeneralGroupsService} from "../../services/GeneralGroupsService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";
declare var jQuery;
@Component({

    templateUrl: './app/amax/GeneralGroups/templates/GeneralGroups.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault],
    providers: [AmaxService, AmaxCrmSyinc, ResourceService, GeneralGroupsService]
})
export class AmaxGeneralGroups implements OnInit {
    modelInput = {};
    custSearchData: Object = [];
    RES: Object = {};
    Formtype: string ="GENERAL_GROUP";
    Lang: string="";
    ShowMoreText: string = "More";

    ShowLoader: boolean = false;
    ShowMsg: boolean = false;
    GroupText: string="Show Groups";
    Msg: string = "";
    MsgClass: string = "text-primary";
    Isbtndisable: string = "";
    GroupIds: string = "";
    baseUrl: string;
    KendoRTLCSS: string = "";
    _Groups = [];
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    

    constructor(private _resourceService: ResourceService, private _routeParams: RouteParams, private _amaxService: AmaxService, private _GeneralGroupsService: GeneralGroupsService) {
        
        this.modelInput.CustomerAddresses = [];
        this.modelInput.CustomerPhones = [];
        this.modelInput.CustomerEmails = [];
        this.modelInput.CustomerGroups = [];
        
        this.modelInput.employeeid = "";
        this.modelInput.CustomerType = "";
        this.modelInput.CameFromCustomer = "";
        this.modelInput.Safixid = "";
        this.modelInput.Gender = "0";
        this.RES.GENERAL_GROUP = {};
        this.Formtype = "GENERAL_GROUP";
        this.GroupIds = "";
        this.baseUrl = _resourceService.AppUrl;
     

       
        
    }
    private _cachedResult: any;
    private _previousContext: any;
    
   
    SetdefaultPage(): observavble {
       
        
        
        this.modelInput = {};

        this.Formtype = "GENERAL_GROUP";
        this.GroupIds = "";

       

       
        this.ShowMsg = false;
        this.Msg = "";
    }
    
   

    GetCustData(): Observable {
        this.GroupIds = "";
        var _CheckedGroups = [];
        //debugger;
        Kendo_utility.checkedNodeIds(jQuery("#groupTree").data("kendoTreeView").dataSource.view(), _CheckedGroups);
        for (var i = 0; i < _CheckedGroups.length; i++) {
            this.GroupIds = this.GroupIds+  _CheckedGroups[i]+",";
        }

        
        //alert(this.GroupIds);
        if (this.GroupIds.length > 0) {
            this.GroupIds = this.GroupIds.substring(0, this.GroupIds.length - 1);
            /////////////////Creating Cache///////////////////

            this._resourceService.setCookie("GeneralGroup_Cache", this.GroupIds, 10);
        }
        this._GeneralGroupsService.GetCompleteCustDet(this.GroupIds).subscribe(response=> {
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
                this.modelInput = response.Data;
                var burl = this.baseUrl;
                jQuery.each(this.modelInput, function () {
                    this.BUrl = burl+"Customer/Add/";
                });

                var dataSource = this.modelInput;
                jQuery("#grid").kendoGrid({
                    dataSource: dataSource,
                    pageable: {
                        pageSizes: true,
                        buttonCount: 5,
                        pageSize: 20
                    },
                    sortable: true,
                    scrolleble: true,
                    selectable: true,
                    height: 400,
                    
                    columns: [
                        {
                            field: "CustomerId", title: this.RES.GENERAL_GROUP.KENDOGRID_CUSTID,
                            //template: '<a href="#=CustomerId#">#:CustomerId#</a>' http://c.amax.co.il/#/Customer/Add/
                            template: function (dataItem) {
                                return "<a href='" + kendo.htmlEncode(dataItem.BUrl)+ kendo.htmlEncode(dataItem.CustomerId) + "'>" + kendo.htmlEncode(dataItem.CustomerId) + "</a>";
                            }
                        },
                        { field: "FileAs", title: this.RES.GENERAL_GROUP.KENDOGRID_FILEAS }

                    ],
                });
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    ngOnInit() {
       
        

        

        this.Lang = localStorage.getItem("lang");
        this.SetdefaultPage();
      

       

        if (this.Lang == "he") {
            this.KendoRTLCSS = "k-rtl";
        }
        else {
            this.KendoRTLCSS = "";
        }

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
       
        
        
        
        
        //Tree Group
        

       this._amaxService.GetGeneralGroupTree().subscribe(

           (data) => {
               var res = jQuery.parseJSON(data);

               jQuery("#groupTree").kendoTreeView({
                   loadOnDemand: true,
                   checkboxes: {
                       //checkChildren: true
                   },
                   check: function (e) {
                       this.expandRoot = e.node;

                       this.expand(jQuery(this.expandRoot).find(".k-item").addBack());
                   },
                   dataSource: res.Data.kendoTree
               });
               var dataSource = null;
               jQuery("#grid").kendoGrid({
                   dataSource: dataSource,
                   pageable: {
                       pageSizes: true,
                       buttonCount: 5,
                       pageSize: 20
                   },
                   sortable: true,
                   scrolleble: true,
                   selectable: true,
                   height: 400,
                   columns: [
                       {
                           field: "CustomerId", title: this.RES.GENERAL_GROUP.KENDOGRID_CUSTID ,
                           template: "<a>#:CustomerId#</a>"
                       },
                       { field: "FileAs", title: this.RES.GENERAL_GROUP.KENDOGRID_FILEAS  }

                   ],
               });

               debugger;
               var jdata = this._resourceService.getCookie("GeneralGroup_Cache");
               if (jdata != undefined && jdata != undefined && jdata != "") {
                   jdata = jdata.substring(1, jdata.length);
                   this.GroupIds = jdata;
                   var grpids = this.GroupIds.split(',');
                   var bindgrps = "";
                   for (var i = 0; i < grpids.length; i++) {
                       bindgrps += grpids[i] + ";";
                   }
                   Kendo_utility.checkingNodeIds(jQuery("#groupTree").data("kendoTreeView").dataSource.view(), bindgrps.substring(0, bindgrps.length - 1));
                   this.GetCustData();
               }

           },
           (err) => {

           },
           () => {

           }

       );
       
       

    }
}
