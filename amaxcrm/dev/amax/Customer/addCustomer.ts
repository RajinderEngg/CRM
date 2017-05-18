
import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
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

    templateUrl: './app/amax/Customer/templates/customer.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, AUTOCOMPLETE_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, AmaxDate],
    providers: [CustomerService, ResourceService]
})

export class AmaxCustomers implements OnInit {
    baseUrl: string = "";
    ImageUrl: string = "";
    modelInput = {};
    TempmodelInput = {};
    custSearchData: Object = [];
    RES: Object = {};
    SelectedPhType: Object = {};
    tempstreetmsg: string = "";
    Formtype: string ="CUSTOMER_MASTER";
    Lang: string="";
    //modelInput.lname= "";
    ShowMore: boolean = false;
    IsRecordEditMode: boolean = false;
    ShowMoreText: string = "More";
    CustFileImage: string = "DefaultUser.jpg";
    ShowLoader: boolean = false;
    ShowMsg: boolean = false;
    ShowGroups: boolean = true;
    GroupText: string="Show Groups";
    Msg: string = "";
    MsgClass: string = "text-primary";
    Isbtndisable: string = "";
    IsFileAsSaveBtn: string = "";
    IsFileAsCancelBtn: string = "";
    languageArray = [];

    Address: Object = {};
    PhoneModel: Object = {};
    EmailModel: Object = {};
    IsShowAll: boolean = false;
    CustList: Object = {};
    SAVE_BTN_TEXT: string = "";
    
    BTN_PHADD: string = "";
    EditPhoneData: Object = {};
    EditAddressData: Object = {};
    EditEmailData: Object = {};
    adId: string;
    IsFileAsOpen: boolean;
    ADD_NEW_CUST_TEXT: string;
    CSSTEXT: string;
    IsFileAstxtShow: boolean = false;
    FILEAS_BTN_TEXT: string = "";
    cssFileAsBtn: string = "";
    IsCancel: boolean = false;
    SearchVal: string = "";
    EnterCount: number = 0;
    StopTimeOut: any;
    CustIdText: string = "";
    static $inject = ['$scope', '$location', '$anchorScroll'];
    BaseAppUrl: string = "";
    PhIndex: number = 0;
    KendoRTLCSS: string = "";
    CHANGEDIR: string = "";
    ChangeDialog: string = "";
    IsPopUp: boolean = true;

    

    //IsFileAsSave: boolean = false;
    
    //Email: string = "";
    //CustomerEmail: Object = {};
    //modelInput.CustomerEmails = [];
    
    _CustTypes = [];
    _Sources = [];
    _Employees = [];
    _Suffixes = [];
    _PhoneTypes = [];
    _AddressTypes = [];
    _Groups = [];
    _Countries = [];
    _States = [];
    _Cities = [];
    _CustTitles = [];
    private selectedCar: string = '';
    private asyncSelectedCar: string = '';
    private autocompleteLoading: boolean = false;
    private autocompleteNoResults: boolean = false;
    private autocompleteSelect: boolean = false;
    
    private getCurrentContext() {
        
        return this;
    }
    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams) {
        
        this.modelInput.BirthDate = "";
        this.modelInput.CustomerAddresses = [];
        this.modelInput.CustomerPhones = [];
        this.modelInput.CustomerEmails = [];
        this.modelInput.CustomerGroups = [];
        this.Address.CountryCode="";
        this.Address.StateId = "";
        this.modelInput.employeeid = "";
        this.modelInput.CustomerType = "";
        this.modelInput.CameFromCustomer = "";
        this.modelInput.Safixid = "";
        //this.modelInput.ImageFileName = "DefaultUser.jpg";
        this.modelInput.Gender = "0";
        this.PhoneModel.PhoneTypeId = "";
        this.RES.CUSTOMER_MASTER = {};
        this.IsShowAll = false;
        this.SAVE_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_SAVE;
        this.BTN_PHADD = this.RES.CUSTOMER_MASTER.APP_BTN_PHADD;
        this.ADD_NEW_CUST_TEXT = this.RES.CUSTOMER_MASTER.APP_LBL_NEW_CUST;
        this.modelInput.CustomerEmails = [{ Email: "", EmailName: "", Newslettere: true, publish: 1, NewsOrder: "News1", EPublishOrder:"EPub1" }]
        this.modelInput.CustomerPhones = [{ PhoneTypeId: "", Prefix: "", Area: "", Phone: "", IsSms: 1, Comments: "", IsShowRemarks: false, phpublish: 1, SMSOrder: "SMS1",PublishOrder: "Pub1"}]
       // debugger;
        var empid = localStorage.getItem("employeeid");
        
        var ccode = this._resourceService.getCookie(empid + "ccode");
        if (ccode.length > 0)
            ccode = ccode.substring(1, ccode.length);

        this.modelInput.CustomerAddresses = [{
            Street: "", Street2: "", CityName: "", Zip: "", CountryCode: ccode, StateId: "", AddressTypeId: "",
            ForDelivery: true, MainAddress: true, MainOrder: "MainAddr1", DelvryOrder: "Delvry1"
        }]

        
        

        var custtype = this._resourceService.getCookie(empid + "cust");
        if (custtype.length > 0) {
            
            custtype = custtype.substring(1, custtype.length);
        }
        this.modelInput.CustomerType = custtype;


        var emp = this._resourceService.getCookie(empid + "emp");
        if (emp.length > 0)
            emp = emp.substring(1, emp.length);
        this.modelInput.employeeid = emp;

        var source = this._resourceService.getCookie(empid + "src");
        if (source.length > 0)
            source = source.substring(1, source.length);
        else {

        }
        this.modelInput.CameFromCustomer = source;
        this.CSSTEXT = "mdi-content-add";
        this.cssFileAsBtn = "mdi-content-create";
        this.IsFileAstxtShow = false;
        clearTimeout(this.StopTimeOut);
        this.modelInput.CustomerId = _routeParams.params.Id;
        this.BaseAppUrl = _resourceService.AppUrl;
        this.baseUrl = _resourceService.baseUrl;
        this.ImageUrl = _resourceService.ImageUrl;
        this.TempmodelInput = this.modelInput;
        
        //alert(this._resourceService.getCookie(empid + "cust"));
        //this.ShowMoreText = "More";
        
    }
    private _cachedResult: any;
    private _previousasyncSelectedCar: string='';
    
    dateSelectionChange(evt) {
        console.log(evt);
        this.modelInput.BirthDate = evt;
       // alert(this.modelInput.BirthDate);
        //this.validateLogin();
    }

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
                               bootbox.alert({message: response.ErrMsg,
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
       // debugger;
        if (e.item != undefined && e.item != "" && e.item != null) {
            //alert(CompData[0]);
            this._customerService.GetCompleteCustDet(CompData[0].trim()).subscribe(response=> {
                //debugger;
                response = jQuery.parseJSON(response);
                if (response.IsError == true) {
                    //alert(response.ErrMsg);
                    bootbox.alert({message: response.ErrMsg,
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
                   // alert(this.modelInput.BirthDate);
                    this.SAVE_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_UPDATE;
                    this.ADD_NEW_CUST_TEXT = this.RES.CUSTOMER_MASTER.APP_LBL_NEW_CUST;
                    this.CSSTEXT = "mdi-content-create";
                    if (this.modelInput.CustomerEmails.length == 0) {
                        this.modelInput.CustomerEmails = [{ Email: "", EmailName: this.modelInput.FileAs, Newslettere: false }]
                    }
                    else {
                        jQuery.each(this.modelInput.CustomerEmails, function () {
                            if (this.Newslettere == "1") {

                                this.Newslettere = false;

                            }
                            else {
                                this.Newslettere = true;
                            }
                        });
                    }
                    if (this.modelInput.CustomerPhones.length == 0) {
                        var phid = "";
                        jQuery.each(this._PhoneTypes, function () {
                            if (this.Text == "CellPhone") {

                                phid = this.Value;
                                return false;
                            }
                        });
                        
                        this.modelInput.CustomerPhones = [{ PhoneTypeId: phid, Prefix: "", Area: "", Phone: "", IsSms: 0, Comments: "", phpublish: 0 }];
                        // debugger;
                    
                    }
                    if (this.modelInput.CustomerAddresses.length == 0) {
                        var empid = localStorage.getItem("employeeid");

                        var ccode = this._resourceService.getCookie(empid + "ccode");
                        if (ccode.length > 0)
                            ccode = ccode.substring(1, ccode.length);
                        var adid = "";
                        var comptext = "Home";
                        if (this.modelInput.Company != "" && this.modelInput.Company != undefined && this.modelInput.Company != null) {
                            comptext = "Work";
                        }
                        jQuery.each(this._AddressTypes, function () {
                            if (this.Text == comptext) {
                                adid = this.Value;
                                return false;
                            }
                        });

                        this.modelInput.CustomerAddresses = [{ Street: "", Street2: "", CityName: "", Zip: "", CountryCode: ccode, StateId: "", AddressTypeId: adid, ForDelivery: false, MainAddress: false }];
                    }

                    this.CustIdText = "( " + this.modelInput.CustomerId + " )";
                    this.IsFileAstxtShow = false;
                    //this.IsCancel = true;
                    //this.HideShowFileAstxt();
                    this.CancelFileAstxt();
                    this.IsShowAll = true;
                    //this.bindGroup();
                    //alert(this.RES);
                    this.bindGroupTree(true);
                }
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });
        }
    }
    OpenProfile() {
        if (this.modelInput != undefined && this.modelInput.CustomerId != undefined && this.modelInput.CustomerId >= 0) {
            var custId = this.modelInput.CustomerId;
            if (custId != -1) {
                document.location = this.BaseAppUrl + "Customer/Profile/" + custId;
            }
        }
    }
    OpenNewReceipt() {
        if (this.modelInput != undefined && this.modelInput.CustomerId != undefined && this.modelInput.CustomerId >= 0) {
            var custId = this.modelInput.CustomerId;
            if (custId != -1) {
                var emid = localStorage.getItem("employeeid");
                document.location = this.BaseAppUrl + "ReceiptSelect/" + emid + "/" + custId;
            }
        }
    }
    OpenChargeCreditPage() {
        this.Isbtndisable = "disabled";
        this._customerService.CheckIsOpenCharge().subscribe(response=> {
            console.log(response);
            response = jQuery.parseJSON(response);
            

            if (response.IsError == true) {
                bootbox.alert({
                    message: response.ErrMsg, className: this.ChangeDialog,
                    buttons: {    
                        ok: {
                            //label: 'Ok',
                            className: this.CHANGEDIR
                        } }});
            }
            else {
                //debugger;
                if (response.Data != undefined && response.Data != null && response.Data.length == 1) {
                    var custId = -1;
                    if (this.TempmodelInput != undefined && this.TempmodelInput.CustomerId != undefined && this.TempmodelInput.CustomerId >= 0) {
                        custId = this.TempmodelInput.CustomerId
                    }
                    if (this.modelInput != undefined && this.modelInput.CustomerId != undefined && this.modelInput.CustomerId >= 0) {
                        custId = this.modelInput.CustomerId
                    }
                    if (custId != -1) {
                        document.location = this.BaseAppUrl + "ChargeCredit/" + custId + "/" + response.Data[0].Value;
                    }
                    else {
                        
                        bootbox.alert({
                            message: 'Please save new or load previous customer and then click on charge credit button',
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
                else if (response.Data != undefined && response.Data != null && response.Data.length > 1) {
                    //debugger;
                    var custId = -1;
                    if (this.TempmodelInput != undefined && this.TempmodelInput.CustomerId != undefined && this.TempmodelInput.CustomerId >= 0) {
                        custId = this.TempmodelInput.CustomerId
                    }
                    if (this.modelInput != undefined && this.modelInput.CustomerId != undefined && this.modelInput.CustomerId >= 0) {
                        custId = this.modelInput.CustomerId
                    }
                    if (custId != -1) {
                        document.location = this.BaseAppUrl + "Terminals/Show/" + custId;
                    }
                    else {
                        bootbox.alert({
                            message: 'Please save new or load previous customer and then click on charge credit button',
                            className: this.ChangeDialog,
                            buttons: {
                                ok: {
                                    //label: 'Ok',
                                    className: this.CHANGEDIR
                                }
                            }
                        });
                    }
                   // alert(response.Data.length + ' Terminal Screen hello');
                }
                else {
                    bootbox.alert({
                        message: this.RES.CUSTOMER_MASTER.APP_MSG_CHARGECREDIT,
                        className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }}
                    });
                }
            }
            this.ShowMsg = true;
            this.Msg = response.ErrMsg;
        },
            error=> console.log(error),
            () => console.log("Save Call Compleated")
        );
        this.Isbtndisable = "";
    }
    StopTimer(): observable {
        bootbox.alert({
            message: 'From Stop Timer' + this.StopTimeOut, className: this.ChangeDialog,
            buttons: {
                ok: {
                    //label: 'Ok',
                    className: this.CHANGEDIR
                }
            }
        });
        clearTimeout(this.StopTimeOut);
    }
    SetdefaultPage(){
        document.location = this.BaseAppUrl + "Customer/Add/-1";
        this.IsFileAstxtShow = true;
        this.IsCancel = true;
        
        var empid = localStorage.getItem("employeeid");
        this.asyncSelectedCar = "";
        
        this.modelInput = {};
        this.CustFileImage = "DefaultUser.jpg";
        this.modelInput.BirthDate = "";
        this.modelInput.CustomerId = -1;
        this.CustIdText = "";
        this.HideShowFileAstxt();
        var custtype = this._resourceService.getCookie(empid + "cust");
        if (custtype.length > 0)
            custtype = custtype.substring(1, custtype.length);
        this.modelInput.CustomerType = custtype;
        var emp = this._resourceService.getCookie(empid + "emp");
        if (emp.length > 0)
            emp = emp.substring(1, emp.length);
        this.modelInput.employeeid = emp;
        var source = this._resourceService.getCookie(empid + "src");
        if (source.length > 0)
            source = source.substring(1, source.length);
        this.modelInput.CameFromCustomer = source;

        this.ShowMore = false;
        //this.ShowMoreText = "More"; 
        this.ShowMoreText = this.RES.CUSTOMER_MASTER.APP_LNK_LBL_MORE;
        this.SAVE_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_SAVE;
        this.CSSTEXT = "mdi-content-add";
        this.ADD_NEW_CUST_TEXT = this.RES.CUSTOMER_MASTER.APP_LBL_NEW_CUST;
        this.ShowGroups = true;
        this.showhideGroups();
        //this.GroupText = this.RES.CUSTOMER_MASTER.APP_LBL_SHOWGROUPS;

        

        var phid = "";
        var SMS = 0;
        var publish = 0;
        var epublish = 0;
        jQuery.each(this._PhoneTypes, function () {
            if (this.Text == "CellPhone") {

                phid = this.Value;
                SMS = 1;
                publish = 1;
                epublish = 1;
                return false;
            }
        });

        this.modelInput.CustomerPhones = [{ PhoneTypeId: phid, Prefix: "", Area: "", Phone: "", IsSms: SMS, Comments: "", phpublish: publish }]
        //debugger;

        this.modelInput.CustomerEmails = [{ Email: "", EmailName: "", Newslettere: false, publish: epublish }]
        var cntrycode = this._resourceService.getCookie(empid + "ccode");
        if (cntrycode.length > 0)
            cntrycode = cntrycode.substring(1, cntrycode.length);

        var adid = "";
        jQuery.each(this._AddressTypes, function () {
            if (this.Text == "Home") {

                adid = this.Value;
                return false;
            }

        });


        this.modelInput.CustomerAddresses = [{ Street: "", Street2: "", CityName: "", Zip: "", CountryCode: cntrycode, StateId: "", AddressTypeId: adid, ForDelivery: false, MainAddress: false }]
        this.modelInput.CustomerGroups = [];
        this.Address.CountryCode = "";
        this.Address.StateId = "";
        this.modelInput.Safixid = "";
        this.modelInput.Gender = "0";
        this.ShowMsg = false;
        this.Msg = "";
        this.IsShowAll = false;
        this._customerService.GetGeneralGroups(this.IsShowAll).subscribe(
            (data) => {
                // debugger;
                if (this.IsShowAll == false) {
                    jQuery("#groupTree").html("Loding...");
                    var res = jQuery.parseJSON(data).Data
                    jQuery("#groupTree").kendoTreeView({
                        loadOnDemand: true,
                        checkboxes: {
                          //  checkChildren: true
                        },
                        check: function (e) {
                            this.expandRoot = e.node;

                            this.expand(jQuery(this.expandRoot).find(".k-item").addBack());
                        },
                        //check: this.onGroupSelect,
                        dataSource: res
                    });
                }
                else {
                    jQuery("#groupTree1").html("Loding...");
                    var res = jQuery.parseJSON(data).Data
                    jQuery("#groupTree1").kendoTreeView({
                        loadOnDemand: true,
                        checkboxes: {
                      //      checkChildren: true
                        },
                        check: function (e) {
                            this.expandRoot = e.node;

                            this.expand(jQuery(this.expandRoot).find(".k-item").addBack());
                        },
                        dataSource: res
                    });
                }
            },
            (err) => {

            },
            () => {

            }
        );

        this.IsPopUp = true;
    }
    CancelFileAstxt() {
        this.IsFileAstxtShow = true;
        this.IsFileAstxtShow = false;
        this.IsCancel = true;
        jQuery("#FileAstxt").hide();
        jQuery("#FileAsSpn").show();
        this.FILEAS_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_FILEAS;
        if (this.modelInput.CustomerId != undefined && this.modelInput.CustomerId != null && parseInt( this.modelInput.CustomerId) >-1) {
            jQuery("#FileAsSaveBtn").show();
            this.cssFileAsBtn = "mdi-content-create";
            jQuery("#FileAsCancelBtn").hide();
        }
        else {
            jQuery("#FileAsSaveBtn").hide();
            jQuery("#FileAsCancelBtn").hide();
        }
    }
    
    HideShowFileAstxt(): observable {
       
        if (this.IsFileAstxtShow == false) {
            this.IsFileAstxtShow = true;
            jQuery("#FileAstxt").show();
            jQuery("#FileAsSpn").hide();
            this.IsCancel = false;
            this.FILEAS_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_SAVEFILEAS;
           // alert(this.modelInput.CustomerId);
            if (this.modelInput.CustomerId != undefined && this.modelInput.CustomerId != null && parseInt( this.modelInput.CustomerId) >-1) {
                jQuery("#FileAsSaveBtn").show();
                this.cssFileAsBtn = "mdi-content-save";
                jQuery("#FileAsCancelBtn").show();
            }
            else {
                jQuery("#FileAsSaveBtn").hide();
                jQuery("#FileAsCancelBtn").hide();
            }
        }
        else {
            this.IsFileAstxtShow = false;
            jQuery("#FileAstxt").hide();
            jQuery("#FileAsSpn").show();
            this.FILEAS_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_FILEAS;
            
            if (this.modelInput.CustomerId != undefined && this.modelInput.CustomerId != null && parseInt(this.modelInput.CustomerId)>-1) {
                jQuery("#FileAsSaveBtn").show();
                this.cssFileAsBtn = "mdi-content-create";
                jQuery("#FileAsCancelBtn").hide();
                
                if (this.modelInput.FileAs != "" && this.modelInput.FileAs != undefined && this.modelInput.FileAs != null && this.IsCancel == false) {
                    this._customerService.SaveFileAs(this.modelInput.CustomerId, this.modelInput.FileAs).subscribe(response=> {
                        response = jQuery.parseJSON(response);
                        //alert('hello');
                        if (response.IsError == true) {
                            //alert(response.ErrMsg);
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
                        //this.IsFileAsSave = false;
                        bootbox.alert({
                            message: response.ErrMsg, className: this.ChangeDialog,
                            buttons: {
                                ok: {
                                    //label: 'Ok',
                                    className: this.CHANGEDIR
                                }
                            }
                        });
                        this.IsCancel = true;
                        //}
                        //else {

                        //}
                    });
                }
                else {
                    bootbox.alert({
                        message: this.RES.CUSTOMER_MASTER.APP_EMPTYFILEAS, className: this.ChangeDialog,
                        buttons: {
                            ok: {
                                //label: 'Ok',
                                className: this.CHANGEDIR
                            }
                        }
                    });
                    this.bindFileAs();
                    this.IsFileAstxtShow = false;
                    this.HideShowFileAstxt();
                }
            }
            else {
                jQuery("#FileAsSaveBtn").hide();
                jQuery("#FileAsCancelBtn").hide();
            }
        }
        
    }
    SetEmailName(): observable {
        if (this.modelInput.FileAs != undefined && this.modelInput.FileAs != null) {
            if (this.modelInput.CustomerEmails.length > 0) {
                this.modelInput.CustomerEmails[this.modelInput.CustomerEmails.length - 1].EmailName = this.modelInput.FileAs;
            }
        }
    }
    CheckCustWithSameName(): observable {

    }
    SetDefaultCust() {
        //alert();
        //debugger;

    }

    setdefaultAddress() {
        
        this.bindFileAs();
        this.CheckCustWithfnamelnamecompphsemails();
        var adid = "";
        var adtext = "Home";

        if (this.modelInput.Company != "" && this.modelInput.Company != undefined) {
            adtext = "Work";
            
        }
        jQuery.each(this._AddressTypes, function () {
            if (this.Text == adtext) {
                adid = this.Value;
                return false;
            }

        });
        
        this.modelInput.CustomerAddresses[this.modelInput.CustomerAddresses.length - 1].AddressTypeId = adid;
    }
    bindFileAs(): observable {
        
        if (this.modelInput.FileAs == "" || this.modelInput.FileAs == undefined) {
            //debugger;
            if ((this.modelInput.Company == "" || this.modelInput.Company == undefined) {
                var fileastext = "";
                if (this.modelInput.fname != "" && this.modelInput.fname != undefined && this.modelInput.lname != "" && this.modelInput.lname != undefined) {
                    fileastext = this.modelInput.lname + " " + this.modelInput.fname;
                    
                }
                else if (this.modelInput.fname != "" && this.modelInput.fname != undefined && (this.modelInput.lname == "" || this.modelInput.lname == undefined)) {
                    fileastext = " " + this.modelInput.fname;
                }
                else if ((this.modelInput.fname == "" || this.modelInput.fname == undefined) && (this.modelInput.lname != "" && this.modelInput.lname != undefined)) {
                    fileastext = this.modelInput.lname + " ";
                }
                this.modelInput.FileAs = fileastext;
            }
            else if ((this.modelInput.lname == "" || this.modelInput.lname == undefined) && (this.modelInput.fname == "" || this.modelInput.lname == undefined)) {
                var fileastext = "";
                if ((this.modelInput.Company != "" && this.modelInput.Company != undefined)) {
                    fileastext = this.modelInput.Company;
                }
                this.modelInput.FileAs = fileastext;
            }
            else
                this.modelInput.FileAs = "(" + this.modelInput.Company + ") " + this.modelInput.lname + " " + this.modelInput.fname; //+ " " & m_strSpouse
            if (this.modelInput.CustomerEmails.length > 0) {
                this.modelInput.CustomerEmails[this.modelInput.CustomerEmails.length - 1].EmailName = this.modelInput.FileAs;
            }
        }
        this.SetEmailName();
    }
    bindGroup(): void {
        //alert(this.IsShowAll); this function is calling on click of checkbox
        var isshow = false;
        if (this.IsShowAll == true) {
            isshow = false
            this.IsShowAll = false;
        }
        else {
            this.IsShowAll = true;
            isshow = true;
        }

        this.bindGroupTree(isshow);
    }di
    saveCustomerData(): void {
        //debugger;
        this.Isbtndisable = "disabled";
        this.ShowLoader = true;
        this.getSelectedGroups();
       
        var count = 0;
        
        if (this.modelInput.CustomerAddresses != undefined && this.modelInput.CustomerAddresses != null) {
            jQuery.each(this.modelInput.CustomerAddresses, function () {
                if (this.MainAddress == true) {
                    count = count + 1;
                }
                if (count > 1) {
                    //bootbox.alert("Main Address sholud be only one");

                    this.Isbtndisable = "";
                    this.ShowLoader = false;
                    return false;
                }
            });
        }
        //alert(this.modelInput.BirthDate);
        if (this.modelInput.BirthDate != "") {
            if (moment(this.modelInput.BirthDate, "DD-MM-YYYY", true).isValid() == false) {
                bootbox.alert({ message: "Birthdate is not valid" });

                this.Isbtndisable = "";
                this.ShowLoader = false;
                return false;
            }
        }
        this.modelInput.Title = jQuery("#Title").val();
        if (count <= 1 || this.modelInput.CustomerAddresses == undefined || this.modelInput.CustomerAddresses == null) {

            if (this.modelInput.CustomerPhones != undefined && this.modelInput.CustomerPhones != null) {
                var phtemp = [];
                jQuery('input[name^="ph"]').each(function () {
                    phtemp.push(jQuery(this).val());
                });
                var artemp = [];
                jQuery('input[name^="ar"]').each(function () {
                    artemp.push(jQuery(this).val());
                });
                var pretemp = [];
                jQuery('input[name^="pre"]').each(function () {
                    pretemp.push(jQuery(this).val());
                });
                var i = 0;
                jQuery.each(this.modelInput.CustomerPhones, function () {
                    if (this.IsSms == true) {
                        this.IsSms = "1";
                    }
                    else {
                        this.IsSms = "0";
                    }
                    if (this.phpublish == true) {
                        this.phpublish = "1";
                    }
                    else {
                        this.phpublish = "0";
                    }
                    this.Phone = phtemp[i];
                    this.Area = artemp[i];
                    this.Prefix = pretemp[i];
                    i++;
                    //var temp = this.PhoneTypeId.split(';');
                    //this.PhoneTypeId = parseInt(temp[1]);
                    //this.PhoneType = temp[0];
                });

            }
            if (this.modelInput.CustomerEmails != undefined && this.modelInput.CustomerEmails != null) {
                jQuery.each(this.modelInput.CustomerEmails, function () {
                    if (this.publish == true) {
                        this.publish = "1";
                    }
                    else {
                        this.publish = "0";
                    }
                   // debugger;
                    if (this.Newslettere == true) {
                        this.Newslettere = false;
                    }
                    else {
                        this.Newslettere = true;
                    }
                    
                    i++;
                });
            }
            var jdata = JSON.stringify(this.modelInput);
            console.log(jdata)
            this._resourceService.deleteCookie("TempImageName");
            this._customerService.AddCustomer(jdata).subscribe(response=> {
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
                    
                    this.MsgClass = "text-success";
                    var empid = localStorage.getItem("employeeid");
                    this._resourceService.setCookie(empid + "cust", this.modelInput.CustomerType, 10);
                    this._resourceService.setCookie(empid + "emp", this.modelInput.employeeid, 10);
                    this._resourceService.setCookie(empid + "src", this.modelInput.CameFromCustomer, 10);
                    if (this.modelInput.CustomerAddresses.length>0)
                        this._resourceService.setCookie(empid + "ccode", this.modelInput.CustomerAddresses[this.modelInput.CustomerAddresses.length - 1].CountryCode, 10);
                   // debugger;
                    //document.location = this.BaseAppUrl + "Customer/Add/-1";
                    //debugger;
                    this.TempmodelInput = response.Data;
                    this.modelInput = response.Data;
                    this.editCustDet(this.modelInput);
                    
                    //this.IsPopUp = true;
                    //this.SetdefaultPage();
                    // Reset form values
                    //this._CustTypes = response.Data;
                    
                    
                }
                this.ShowMsg = true;
                this.Msg = response.ErrMsg;
            },
                error=> console.log(error),
                () => console.log("Save Call Compleated")
            );
        }
        else {
            bootbox.alert({
                message: this.RES.CUSTOMER_MASTER.APP_MSG_ISMAINADD, className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
            this.Isbtndisable = "";
            this.ShowLoader = false;
        }
    }

    CheckCustWithfnamelnamecompphsemails(): observable {
        
            //this.modelInput.ImageFileName = this._resourceService.getCookie("TempImageName");
        if (this.IsPopUp == true) {
            var jdata = JSON.stringify(this.modelInput);
            //debugger;
            var fname = "";
            var lname = "";
            var company = "";
            var phones = "";
            var emails = "";

            if (this.modelInput.fname == undefined)
                fname = "";
            else
                fname = this.modelInput.fname;

            if (this.modelInput.lname == undefined)
                lname = "";
            else
                lname = this.modelInput.lname;
            if (this.modelInput.Company == undefined)
                company = "";
            else
                company = this.modelInput.Company;

            jQuery('input[name^="ph"]').each(function () {

                if (jQuery(this).val() != "" && jQuery(this).val() != undefined && jQuery(this).val() != null && jQuery(this).val().length >= 3) {
                    phones += jQuery(this).val() + "','";
                }
            });
            if (phones.length > 0) phones = phones.substring(0, phones.length - 3);
            jQuery.each(this.modelInput.CustomerEmails, function () {
                if (this.Email != "" && this.Email != undefined && this.Email != null && this.Email.length >= 3) {
                    emails += this.Email + "','";
                }

            });
            if (emails.length > 0) emails = emails.substring(0, emails.length - 3);
            if ((fname != "" && fname.length >= 2 && lname != "" && lname.length >= 2)
                || (company != "" && company.length >= 3)
                || (phones != "")
                || (emails != "")) {

                this._customerService.GetCustomersSearchData(fname, lname, company, phones, emails).subscribe(response=> {
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
                        this.CustList = {};
                        this.CustList = response.Data;
                        if (response.Data != null && response.Data != undefined) {
                            this.custSearchData = response.Data;
                            jQuery('#CustModal').openModal()
                            //jQuery('#CustModal').modal('open');
                            //jQuery("#CustModal").show(1000);
                        }
                        //alert(this.RES);
                    }
                }, error=> {
                    console.log(error);
                }, () => {
                    console.log("CallCompleted")
                });
            }
        }
        //this.bindFileAs();
    }
    CloseModalPop() {
        this.ClosePopUp();
        this.IsPopUp = false;
    }
    ClosePopUp() {
        jQuery("#CustModal").closeModal();
        jQuery(".lean-overlay").css({ "display": "none" });
    }

    CheckCustWithfnamelname(fname, lname,company): observable {
        var jdata = JSON.stringify(this.modelInput);
        //debugger;
        if (this.modelInput.fname == undefined)
            fname = "";
        else
            fname = this.modelInput.fname;

        if (this.modelInput.lname == undefined)
            lname = "";
        else
            lname = this.modelInput.lname;
        if (this.modelInput.Company == undefined)
            company = "";
        else
            company = this.modelInput.Company;
        this._customerService.CheckCustWithSameName(fname, lname, company).subscribe(response=> {
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
                this.CustList = {};
                this.CustList = response.Data;
                if (response.Data != null && response.Data != undefined) {
                    this.custSearchData = response.Data;
                    jQuery("#CustModal").openModal();
                    //jQuery("#CustModal").show(1000);
                }
                //alert(this.RES);
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        //this.bindFileAs();
    }




    CheckCustWithEmail(): observable {
        
        //var Email = "";
        //if (this.EmailModel.Email != "" && this.EmailModel.Email != undefined)
        //    Email = this.EmailModel.Email;
        //this._customerService.CheckCustWithSameEmail(Email).subscribe(response=> {
        //    //debugger;
        //    response = jQuery.parseJSON(response);
        //    if (response.IsError == true) {
        //        alert(response.ErrMsg);
        //    }
        //    else {
        //        this.CustList = {};
        //        this.CustList = response.Data;
        //        if (response.Data != null && response.Data != undefined) {
        //            this.custSearchData = response.Data;
        //            jQuery("#CustModal").modal("show");
        //        }
        //        //alert(this.RES);
        //    }
        //}, error=> {
        //    console.log(error);
        //}, () => {
        //    console.log("CallCompleted")
        //});
    }
    CheckCustWithPhone(): observable {
        var Phone = "";
        if (this.PhoneModel.Phone != "" && this.PhoneModel.Phone != undefined)
            Phone = this.PhoneModel.Phone;
        this._customerService.CheckCustWithSamePhone(this.PhoneModel.Phone).subscribe(response=> {
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
                this.CustList = {};
                this.CustList = response.Data;
                if (response.Data != null && response.Data != undefined) {
                    this.custSearchData = response.Data;
                    jQuery("#CustModal").openModal();
                }
                //alert(this.RES);
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    ShowRemarks(PhObj): Observable {
        jQuery.each(this.modelInput.CustomerPhones, function () {
            if (this == PhObj) {
                this.IsShowRemarks = true;
            }

        });
    }
    showAddPopup(): observable {
        this.Address = {};
        this.PhoneModel = {};
        this.PhoneModel.PhoneTypeId = "";
        this.Address.CountryCode = "";
        this.Address.StateId = "";
        this.Address.CityName = "";
        this.Address.AddressTypeId = "";
        this.EmailModel = {};
        this.BTN_PHADD = this.RES.CUSTOMER_MASTER.APP_BTN_PHADD
        

    }
    showhideGroups(): observable {
        //debugger;
        if (this.ShowGroups == false) {
            this.ShowGroups = true;
            this.GroupText = this.RES.CUSTOMER_MASTER.APP_LBL_HIDEGROUP;
            jQuery("#GrpDiv").show(1000);
        }
        else {
            this.ShowGroups = false;
            this.GroupText = this.RES.CUSTOMER_MASTER.APP_LBL_SHOWGROUPS;
            jQuery("#GrpDiv").hide(1000);
        }

    }


    CanaddAddress(adobj): boolean {
        //alert('Hello');
        return (adobj.Street != undefined && adobj.Street != "")
            && (adobj.Street2 != undefined && adobj.Street2 != "")
            //&& (adobj.CityName != undefined && adobj.CityName != "")
            && (adobj.Zip != undefined && adobj.Zip != "") 
            && (adobj.CountryCode != undefined && adobj.CountryCode != "" )
            //&& (this.Address.StateId != undefined && this.Address.StateId != "")
            && (adobj.AddressTypeId != undefined && adobj.AddressTypeId != "");
    }
    AddAddresses(adobj): observable {
        
        var IsMainAdd = false;
        adobj.CityName = jQuery("#City").val();
        
        if (this.CanaddAddress(adobj)) {

            var empid = localStorage.getItem("employeeid");
            this._resourceService.setCookie(empid + "ccode", adobj.CountryCode, 10);
            var adid = "";
            var adtext = "Home";
            if (this.modelInput.Company != "" && this.modelInput.Company != undefined) {
                adtext = "Work";
            }
            jQuery.each(this._AddressTypes, function () {
                if (this.Text == adtext) {
                    adid = this.Value;
                    return false;
                }

            });
            var AddresObj = { Street: "", Street2: "", CityName: "", Zip: "", CountryCode: adobj.CountryCode, StateId: "", AddressTypeId: adid, ForDelivery: false, MainAddress: false,, MainOrder: "MainAddr" + (this.modelInput.CustomerAddresses.length+1).toString(), DelvryOrder: "Delvry" + (this.modelInput.CustomerAddresses.length+1).toString() };
                
                //jQuery.each(this.modelInput.CustomerAddresses, function () {
                //    if (this.MainAddress == true && adobj.MainAddress == true && this != adobj) {
                        
                //        adobj.MainAddress = false;
                //        return false;
                //    }
                //});
                if (IsMainAdd == false) {
                    this.modelInput.CustomerAddresses.push(AddresObj);
                }
            
        }
        else {
            var msg = '';
            if (adobj.Street == undefined || adobj.Street == "") {
                //msg += '\nStreet is not filled'; APP_AL_MSG_STREET
                msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_MSG_STREET;
            }
            if (adobj.Street2 == undefined || adobj.Street2 == "") {
                //msg += '\nArea is not filled';
                msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_MSG_AREA;
            }
            //if (adobj.CityName == undefined || adobj.CityName == "")
            //    //msg += '\nCity is not filled'; 
            //    msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_MSG_CITY;
            if (adobj.Zip == undefined || adobj.Zip == "") {
                //msg += '\nZip is not filled'; 
                msg += '\n'+this.RES.CUSTOMER_MASTER.APP_AL_MSG_ZIP;
            }
            if (adobj.CountryCode == undefined || adobj.CountryCode == "") {
                //msg += '\nCountry is not selected';
                msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_MSG_COUNTRY;
            }
            //if (this.Address.StateId == undefined || this.Address.StateId == "") {
            //    msg += '\nState is not selected';
            //}
            if (adobj.AddressTypeId == undefined || adobj.AddressTypeId == "") {
                //msg += '\nAddress type is not selected';
                msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_MSG_ADTYPE;
            }
            bootbox.alert({
                message: msg, className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
        }

    }
    CanaddPhone(phoneObj): boolean {
        //debugger;
        //alert(this.PhoneModel.PhoneTypeId + ' ' + this.PhoneModel.PhoneType + ' ' + this.PhoneModel.Prefix + ' ' + this.PhoneModel.Area + ' ' + this.PhoneModel.Phone);
        return (phoneObj.PhoneTypeId != undefined && phoneObj.PhoneTypeId != "")
            // && (this.PhoneModel.PhoneType != undefined&& this.PhoneModel.PhoneType != "" )
            //&& (this.PhoneModel.Prefix != undefined&& this.PhoneModel.Prefix != ""  )
            //&& (this.PhoneModel.Area != undefined&&this.PhoneModel.Area != ""  )
            //&& (phoneObj.Phone != undefined && phoneObj.Phone != "");
            //&& (this.PhoneModel.Prefix != undefined && this.PhoneModel.Prefix.length != 3);            ;
    }
    AddPhones(phoneObj): observable {
        if (this.CanaddPhone(phoneObj)) {

           // debugger;
            //if (this.IsRecordEditMode == false) {
            var phid = "";
            var SMS = 0;
            var publish = 0;
            jQuery.each(this._PhoneTypes, function () {
                if (this.Text == "CellPhone") {

                    phid = this.Value;
                    SMS = 1;
                    publish = 1;
                    return false;
                }
            });
            var PhoneObj = { PhoneTypeId: phid, PhoneType: "", Prefix: "", Area: "", Phone: "", IsSms: SMS, Comments: "", IsShowRemarks: false, phpublish: publish, SMSOrder: "SMS" + (this.modelInput.CustomerPhones.length + 1).toString(), PublishOrder: "Pub" + (this.modelInput.CustomerPhones.length + 1).toString() };
               
            this.modelInput.CustomerPhones.push(PhoneObj);
            
            
            //this.CheckCustWithfnamelnamecompphsemails();
        }
        else {
            var msg = '';
            if (phoneObj.PhoneTypeId == undefined || phoneObj.PhoneTypeId == "") {
                //msg += '\nPhone type is not selected';
                msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_REGMSG_PHTYPE;
            }
            //if (phoneObj.Phone == undefined || phoneObj.Phone == "") {
            //    //msg += '\nPhone number is not filled';
            //    msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_REGMSG_PHNO;
            //}
            //if (this.PhoneModel.Prefix.length!=3) {
            //    msg += '\nPrefix must of 3 numeric digits';
            //}
            bootbox.alert({
                message: msg,className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });

        }
        
    }
    CanaddEmail(EmailObj): boolean {
        //debugger;
        //alert('Hello');
        return (EmailObj.EmailName != undefined && EmailObj.EmailName != "");
        //(EmailObj.Email != undefined && EmailObj.Email != "") &&
             
    }
    AddEmails(EmailObj): observable {
        //debugger;
        if (this.CanaddEmail(EmailObj)) {
            var epublish = 0;
            jQuery.each(this._PhoneTypes, function () {
               if (this.Text == "CellPhone") {

                    
                    
                   epublish = 1;
                    return false;
                }
            });
            //if (this.IsRecordEditMode == false) {
            var eObj = {};
            eObj.Email = "";
            eObj.EmailName = this.modelInput.FileAs;
            eObj.Newslettere = true;
            eObj.publish = epublish;
            eObj.NewsOrder= "News" + (this.modelInput.CustomerEmails.length+1).toString();
            eObj.EPublishOrder= "EPub" + (this.modelInput.CustomerEmails.length + 1).toString();
                this.modelInput.CustomerEmails.push(eObj);
                //this.CheckCustWithfnamelnamecompphsemails();
            
        }
        else {
            var msg = '';
            //if (EmailObj.Email == undefined || EmailObj.Email == "")
            //    //msg += '\nEmail is not filled';
            //    msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_REGMSG_EMAIL;
            if (EmailObj.EmailName == undefined || EmailObj.EmailName == "") {
                //msg += '\nName is not filled';
                msg += '\n' + this.RES.CUSTOMER_MASTER.APP_AL_REGMSG_ENAME;
            }
            bootbox.alert({
                message: msg, className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });

        }
        
        // this.modelInput.CustomerAddresses = this.CustomerAddresses;
    }
    
    editCustDet(Obj) {
        this._customerService.GetCompleteCustDet(Obj.CustomerId).subscribe(response=> {
          //  debugger;
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
                this.modelInput = response.Data;
                this.SAVE_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_UPDATE;
                //this.ADD_NEW_CUST_TEXT = this.RES.CUSTOMER_MASTER.APP_LBL_UPDATE_CUST;
                this.CSSTEXT = "mdi-content-add";
                if (this.modelInput.CustomerEmails.length == 0) {
                    this.modelInput.CustomerEmails = [{ Email: "", EmailName: this.modelInput.FileAs, Newslettere: false, publish: 0, NewsOrder: "News1", EPublishOrder: "EPub1" }]
                }
                else {
                    var count = 1;

                    jQuery.each(this.modelInput.CustomerEmails, function () {
                        this.NewsOrder = "News" + count;
                        this.EPublishOrder = "EPub" + count++;
                        if (this.Newslettere == "1") {
                            this.Newslettere = false;
                        }
                        else {
                            this.Newslettere = true;
                        }
                    });
                }
                if (this.modelInput.CustomerPhones.length == 0) {
                    var phid = "";

                    jQuery.each(this._PhoneTypes, function () {

                        if (this.Text == "CellPhone") {

                            phid = this.Value;
                            return false;
                        }
                    });

                    this.modelInput.CustomerPhones = [{ PhoneTypeId: phid, Prefix: "", Area: "", Phone: "", IsSms: 0, Comments: "", phpublish: 0, SMSOrder: "SMS1", PublishOrder: "Pub1" }];
                    // debugger;
                    
                }
                else {
                    var count = 1;
                    jQuery.each(this.modelInput.CustomerPhones, function () {

                        this.SMSOrder = "SMS" + count;
                        this.PublishOrder = "Pub" + count++;
                    });
                }
                if (this.modelInput.CustomerAddresses.length == 0) {
                    var empid = localStorage.getItem("employeeid");

                    var ccode = this._resourceService.getCookie(empid + "ccode");
                    if (ccode.length > 0)
                        ccode = ccode.substring(1, ccode.length);
                    var adid = "";
                    var comptext = "Home";
                    if (this.modelInput.Company != "" && this.modelInput.Company != undefined && this.modelInput.Company != null) {
                        comptext = "Work";
                    }
                    jQuery.each(this._AddressTypes, function () {
                        if (this.Text == comptext) {

                            adid = this.Value;
                            return false;
                        }

                    });
                    this.modelInput.CustomerAddresses = [{ Street: "", Street2: "", CityName: "", Zip: "", CountryCode: ccode, StateId: "", AddressTypeId: adid, ForDelivery: false, MainAddress: false, MainOrder: "MainAddr1", DelvryOrder: "Delvry1" }];
                }
                else {
                    var count = 1;
                jQuery.each(this.modelInput.CustomerAddresses, function () {
                    this.MainOrder = "MainAddr" + count;
                    this.DelvryOrder = "Delvry" + count++;
                });
                }
                //var treeview = jQuery("#groupTree").data("kendoTreeView");

                //var bar = treeview.findById("Bar");

                
                //jQuery.each(this.modelInput.CustomerGroups, function () {
                //    var data = jQuery("#groupTree").data("kendoTreeView").dataSource.getByUid(this.CustomerGeneralGroupId);
                //    if (data) {
                //        data.set("checked", true);
                //    }
                //    //var GroupNode = treeview.findById(this.CustomerGeneralGroupId);
                //    //treeview.dataItem(GroupNode).set("checked", true);
                //});
                this.CustIdText = "( " + this.modelInput.CustomerId + " )";
                this.IsFileAstxtShow = false;
                this.HideShowFileAstxt();
                //this.modelInput.ImageFileName = "DefaultUser.jpg";
                if (this.modelInput.ImageFileName != undefined && this.modelInput.ImageFileName != null && this.modelInput.ImageFileName != "") {
                    var OrgId = "";
                    var empid = localStorage.getItem("employeeid");
                    if (empid != null && empid != undefined) {
                        OrgId = localStorage.getItem(empid + "_OrgId");
                    }
                    this.CustFileImage = OrgId+"//"+this.modelInput.ImageFileName;
                }
                else {
                    this.CustFileImage = "DefaultUser.jpg";
                }
                
                
                //alert(this.RES);
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        this.ClosePopUp();
        //////For Closing Display Popup/////////////
        this.IsPopUp = false;

    }
    CheckPhoneType(PhoneObj) {
        //debugger;
        //alert(PhoneObj.PhoneTypeId + " | " + jQuery("#PhoneType").val());
        var pretemp = [];
        jQuery('select[name^="phtype"]').each(function () {
            pretemp.push(jQuery(this).val());
        });
        var index = 0;
        jQuery.each(this.modelInput.CustomerPhones, function () {
            if (this == PhoneObj) {

                return false
            }
            index = index + 1;
        });

        if (pretemp[index] != undefined && pretemp[index] != null && pretemp[index] != "") {
            var PhoneTypeId = pretemp[index];
            this._customerService.GetPhoneTypeDet(PhoneTypeId).subscribe(
                (data) => {
                    //debugger;
                    //
                    var response = jQuery.parseJSON(data);
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
                        if (response.Data != undefined && response.Data != null && response.Data != "") {
                            
                            //debugger;
                            
                            //alert(index + " | " + this.modelInput.CustomerPhones[index].IsSms + " | " + response.Data.Text);
                            if (response.Data.Text == "1") {
                                this.modelInput.CustomerPhones[index].IsSms = 1;
                                this.modelInput.CustomerPhones[index].phpublish = 1;
                                this.modelInput.CustomerEmails[this.modelInput.CustomerEmails.length - 1].publish = 1;
                            }
                            else {
                                this.modelInput.CustomerPhones[index].phpublish = 0;
                                this.modelInput.CustomerPhones[index].IsSms = 0;
                                this.modelInput.CustomerEmails[this.modelInput.CustomerEmails.length - 1].publish = 0;
                            }

                        }
                        //alert(this.RES);
                    }
                    //var treeviewDataSource = jQuery("#groupTree").data("kendoTreeView").dataSource.view();
                },
                (err) => {

                },
                () => {

                });
            }
    }
    editEmailDet(EmailObj): observable {
        //debugger;
        var index = 0;
        this.IsRecordEditMode = true;
        this.BTN_PHADD = this.RES.CUSTOMER_MASTER.APP_BTN_PHEDIT;
        

        
        this.EmailModel.Email = EmailObj.Email;
        this.EmailModel.EmailName = EmailObj.EmailName;
        this.EmailModel.Newslettere = EmailObj.Newslettere;


        this.EditEmailData = {};
        this.EditEmailData = EmailObj;
    }
    delEmailDet(EmailObj): observable {
        //debugger;
        if (this.modelInput.CustomerEmails.length > 1) {

            var index = 0;
            jQuery.each(this.modelInput.CustomerEmails, function () {
                if (this == EmailObj) {
                    return false
                }
                index = index + 1;

            });
            this.modelInput.CustomerEmails.splice(index, 1);
        }
    }

    editAddressDet(AddressObj): observable {
        //debugger;
        var index = 0;
        this.IsRecordEditMode = true;
        this.BTN_PHADD = this.RES.CUSTOMER_MASTER.APP_BTN_PHEDIT;
        
       // AddressObj.CityName = jQuery("#City").val();

        this.Address.Street = AddressObj.Street;
        this.Address.Street2 = AddressObj.Street2;
        this.Address.CityName = AddressObj.CityName;
        this.Address.Zip = AddressObj.Zip;
        this.Address.CountryCode = AddressObj.CountryCode;
        this.Address.StateId = AddressObj.StateId;
        this.Address.AddressTypeId = AddressObj.AddressTypeId;
        this.Address.MainAddress = AddressObj.MainAddress;
        this.Address.ForDelivery = AddressObj.ForDelivery;


        this.EditAddressData = {};
        this.EditAddressData = AddressObj;
    }

    delAddressDet(AddressObj): observable {
       // debugger; 
        if (this.modelInput.CustomerAddresses > 1) {
            var index = 0;
            jQuery.each(this.modelInput.CustomerAddresses, function () {
                if (this == AddressObj) {
                    return false
                }
                index = index + 1;
            });
            this.modelInput.CustomerAddresses.splice(index, 1);
        }
    }
    editPhoneDet(PhoneObj): observable {
        
        var index = 0;
        this.BTN_PHADD = this.RES.CUSTOMER_MASTER.APP_BTN_PHEDIT
        var temp = PhoneObj.PhoneTypeId.split(';');
        
        this.PhoneModel.PhoneTypeId = PhoneObj.PhoneType + ";" + PhoneObj.PhoneTypeId;
        
        this.PhoneModel.PhoneType = PhoneObj.PhoneType;
        this.PhoneModel.Prefix = PhoneObj.Prefix;
        this.PhoneModel.Area = PhoneObj.Area;
        this.PhoneModel.Phone = PhoneObj.Phone;
        this.PhoneModel.IsSms = PhoneObj.IsSms;
        this.PhoneModel.Comments = PhoneObj.Comments;
        this.EditPhoneData = {};
        this.EditPhoneData = PhoneObj;
    }
    delPhoneDet(PhoneObj): observable {
       // debugger;
        if (this.modelInput.CustomerPhones.length > 1) {
            var index = 0;
            jQuery.each(this.modelInput.CustomerPhones, function () {
                if (this == PhoneObj) {
                    return false
                }
                index = index + 1;
            });
            this.modelInput.CustomerPhones.splice(index, 1);
        }
    }


    getSelectedGroups(): void {
        this.modelInput.CustomerGroups = [];
        var _CheckedGroups = [];
        if (this.IsShowAll == false) {
            Kendo_utility.checkedNodeIds(jQuery("#groupTree").data("kendoTreeView").dataSource.view(), _CheckedGroups);
        }
        else {
        
            Kendo_utility.checkedNodeIds(jQuery("#groupTree1").data("kendoTreeView").dataSource.view(), _CheckedGroups);
        }
        for (var i = 0;i<_CheckedGroups.length;i++){
            var GObj = {};
            GObj.CustomerGeneralGroupId = _CheckedGroups[i];
            this.modelInput.CustomerGroups.push(GObj);
     
        }
        
    }

    More(): observable {
       // alert("call");
        if (this.ShowMore == true) {
            this.ShowMore = false;

            //this.ShowMoreText = "More";
            this.ShowMoreText = this.RES.CUSTOMER_MASTER.APP_LNK_LBL_MORE;
        }
        else {
        this.ShowMore = true;
        //this.ShowMoreText = "Less"; 
        this.ShowMoreText = this.RES.CUSTOMER_MASTER.APP_LNK_LBL_LESS;
        }
        this.BindCustTitles();
    }
    bindGroupTree(Isshowall): observable {
        this._customerService.GetGeneralGroups(Isshowall).subscribe(
            (data) => {
                //debugger;
                //
                //alert(Isshowall);
                if (Isshowall == false) {
                    jQuery("#groupTree").html("Loding...");
                    var res = jQuery.parseJSON(data).Data
                    jQuery("#groupTree").kendoTreeView({
                        loadOnDemand: true,
                        checkboxes: {
                          //  checkChildren: true
                        },
                        check: function (e) {
                            this.expandRoot = e.node;

                            this.expand(jQuery(this.expandRoot).find(".k-item").addBack());
                        },
                        //check: this.onGroupSelect,
                        dataSource: res
                    });
                    var grpids = "";
                    
                    jQuery.each(this.modelInput.CustomerGroups, function () {
                        grpids += this.CustomerGeneralGroupId + ";";
                    });
                    if (grpids.length > 0) {
                        Kendo_utility.checkingNodeIds(jQuery("#groupTree").data("kendoTreeView").dataSource.view(), grpids.substring(0, grpids.length - 1))
                    }
                }
                else {
                    jQuery("#groupTree1").html("Loding...");
                    var res = jQuery.parseJSON(data).Data
                    jQuery("#groupTree1").kendoTreeView({
                        loadOnDemand: true,
                        checkboxes: {
                            //checkChildren: true
                        },
                        check: function (e) {
                            this.expandRoot = e.node;

                            this.expand(jQuery(this.expandRoot).find(".k-item").addBack());
                        },
                        //check: this.onGroupSelect,
                        dataSource: res
                    });
                    var grpids = "";
                    jQuery.each(this.modelInput.CustomerGroups, function () {
                        grpids += this.CustomerGeneralGroupId + ";";
                    });
                    if (grpids.length > 0) {
                        Kendo_utility.checkingNodeIds(jQuery("#groupTree1").data("kendoTreeView").dataSource.view(), grpids.substring(0, grpids.length - 1))
                    }
                }
                //var treeviewDataSource = jQuery("#groupTree").data("kendoTreeView").dataSource.view();
            },
            (err) => {

            },
            () => {

            }
        );
        
    }
    GetDataForSearch(event: any): observable {
        
        //this.SearchVal = jQuery("#Searchtxt").val();
        //alert(event.keyCode);
        //if (this.SearchVal != undefined && this.SearchVal != "" && this.SearchVal != null && event.keyCode == 13) {
            //alert(this.autocompleteSelect + " " + this.autocompleteNoResults);
        //    this.EnterCount++;
        //    if (this.EnterCount >= 2) {
        //        this._customerService.GetCompleteSearch(this.SearchVal).subscribe(response=> {
                   
        //            response = jQuery.parseJSON(response);
        //            if (response.IsError == true) {
        //                alert(response.ErrMsg);
        //            }
        //            else {
        //                this.CustList = {};
        //                this.CustList = response.Data;
        //                if (response.Data != null && response.Data != undefined) {
        //                    this.custSearchData = response.Data;
        //                    jQuery("#CustModal").modal("show");

        //                }

        //            }
        //        }, error=> {
        //            console.log(error);
        //        }, () => {
        //            console.log("CallCompleted")
        //        });
        //        this.EnterCount = 0;
        //    }
               
        //}
        //this.SearchVal = "";
    }

    BindCustTitles() {

        this._customerService.GetCustTitles().subscribe(response=> {
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
                var titletypeaheadSource = [];
                jQuery.each(response.Data, function () {
                    var newtemp = {};
                    newtemp.id = this.Value;
                    newtemp.name = this.Text;
                    titletypeaheadSource.push(newtemp);
                });
                this._CustTitles = response.Data;
                jQuery("#Title").typeahead({
                    //data: this._Cities,
                    source: titletypeaheadSource,
                    //display: "text",
                    dataType: "JSON",
                    //hint: true,
                    //highlight: true,
                    //minLength: 1,
                });

                
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    FindImageFile(input) {
        //debugger;
        this.modelInput.CustomerImageFile = input.target.files;
        var data = new FormData();
        var file = this.modelInput.CustomerImageFile[0];
        
        data.append('file', file);
      //  var uploaddata = {};
        //  uploaddata.CustomerUploadedFile = data;
        debugger;
        var OrgId = "";
        var empid = localStorage.getItem("employeeid");
        if (empid != null && empid != undefined) {
            OrgId=localStorage.getItem(empid + "_OrgId");
        }
        var xhr = this._customerService.UploadCustomerImage(data, OrgId);

        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    debugger;
                    var response = xhr.responseText;
                    if (response != undefined && response != "" && response != null) {
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
                            //this.setCookie("TempImageName", response.Data, 10);
                            
                            this.CustFileImage = response.Data;
                            this.modelInput.ImageFileName = response.Data;
                            //this.modelInput.ImageFileName = response.Data;

                        }
                    }

                } else {

                }
            }
        };

        
        //    .(response=> {
        //    //debugger;
        //    response = jQuery.parseJSON(response);
        //    if (response.IsError == true) {
        //        bootbox.alert({
        //            message: response.ErrMsg, className: this.ChangeDialog,
        //            buttons: {
        //                ok: {
        //                    //label: 'Ok',
        //                    className: this.CHANGEDIR
        //                }
        //            }
        //        });
        //    }
        //    else {
        //        debugger;
        //        var res = jQuery.parseJSON(response);
        //        //this.modelInput.ImageFileName = response.Data;
                
        //    }
        //}, error=> {
        //    console.log(error);
        //}, () => {
        //    console.log("CallCompleted")
        //});
        //input.srcElement.form.submit();
        //var imagepath = jQuery("#CustImage").val().split('\\');
        //if (imagepath.length > 0) {
        //    this.CustFileImage = imagepath[2];
        //    var iname = this.modelInput.CustomerId.toString() + ".jpg";
        //    this.modelInput.ImageFileName = iname;
        //}
        //alert(jQuery("#CustImage").val());
    }
    ClearImage() {
        jQuery("#CustImage").val('');
        this.modelInput.ImageFileName = "";
        
        this.CustFileImage = "DefaultUser.jpg";
        //alert(jQuery("#CustImage").val());
    }
    ngOnInit() {
        //
        this._resourceService.deleteCookie("TempImageName");
        jQuery(".lean-overlay").css({ "display": "none" });
        this.BindCustTitles();
       // debugger;
        //bootbox.alert("This is the default alert!");
        if (localStorage.getItem("lang") == "") {
            localStorage.setItem("lang", "en");
        }
        if (this._resourceService.getCookie("lang") == "") {
            
            this._resourceService.setCookie("lang", "en", 10);
        }
        this.IsCancel = false;
        this.showhideGroups();
        this.IsFileAstxtShow = true;
        
        //this.modelInput.CustomerId = -1;
        
        if (this.modelInput.CustomerId >= 0) {
            //this.IsFileAstxtShow = false;
            this.editCustDet(this.modelInput);

            this.SAVE_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_UPDATE;
            //this.ADD_NEW_CUST_TEXT = this.RES.CUSTOMER_MASTER.APP_LBL_UPDATE_CUST;
            //this.CSSTEXT = "mdi-content-add";
            
            if (this.modelInput.CustomerAddresses.length == 0) {
                var empid = localStorage.getItem("employeeid");

                var ccode = this._resourceService.getCookie(empid + "ccode");
                if (ccode.length > 0)
                    ccode = ccode.substring(1, ccode.length);
                var adid = "";
                var comptext = "Home";
                if (this.modelInput.Company != "" && this.modelInput.Company != undefined && this.modelInput.Company != null) {
                    comptext = "Work";
                }
                jQuery.each(this._AddressTypes, function () {
                    if (this.Text == comptext) {

                        adid = this.Value;
                        return false;
                    }

                });
                this.modelInput.CustomerAddresses = [{ Street: "", Street2: "", CityName: "", Zip: "", CountryCode: ccode, StateId: "", AddressTypeId: adid, ForDelivery: false, MainAddress: false, MainOrder: "MainAddr1", DelvryOrder: "Delvry1" }];
            }
            this.CustIdText = "( " + this.modelInput.CustomerId + " )"
            this.IsFileAstxtShow = false;
            //this.HideShowFileAstxt();
        }
        else {
            this.CustFileImage = "DefaultUser.jpg";
        }
        this.Lang = this._resourceService.getCookie("lang");
        if (this.Lang.length > 0) {
            this.Lang = this.Lang.substring(1, this.Lang.length);
        }
        this.HideShowFileAstxt();
       //this.RES = jQuery.parseJSON(this._customerService.GetLangRes(this.Formtype, this.Lang)).Data; //jQuery.parseJSON(localStorage.getItem("langresource"));
        
        if (this.Lang == "he") {
            this.KendoRTLCSS = "k-rtl";
            this.CHANGEDIR = "rtlmodal";
            this.ChangeDialog = "input_right";
            //jQuery(".bootbox-close-button").css("float", "left!important");
            //jQuery(".modal-footer button:before").css("float", "left!important");
            //jQuery(".modal-footer button:after").css("float", "left!important");
        }
        else {
            this.CHANGEDIR = "ltrmodal";
            this.ChangeDialog = "input_left";
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
               this.ShowMoreText = this.RES.CUSTOMER_MASTER.APP_LNK_LBL_MORE;
               this.GroupText = this.RES.CUSTOMER_MASTER.APP_LBL_SHOWGROUPS;
               this.SAVE_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_SAVE;
               this.ADD_NEW_CUST_TEXT = this.RES.CUSTOMER_MASTER.APP_LBL_NEW_CUST;
               this.FILEAS_BTN_TEXT = this.RES.CUSTOMER_MASTER.APP_BTN_FILEAS;
               //alert(this.RES);
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
       //this.ShowMoreText = "More";
       
       ////Cities
       



       var CountryCode = this.Address.CountryCode;
       var StateName = this.Address.StateId;
       this._customerService.GetCities(CountryCode, StateName).subscribe(response=> {
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
               var typeaheadSource = [];
               jQuery.each(response.Data, function () {
                   var newtemp = {};
                   newtemp.id = this.Value;
                   newtemp.name = this.Text;
                   typeaheadSource.push(newtemp);
               });
               this._Cities = response.Data;
               jQuery('#City').typeahead({
                   //data: this._Cities,
                   source: typeaheadSource,
                   //display: "text",
                   dataType: "JSON",
                   //hint: true,
                   //highlight: true,
                   //minLength: 1,
               });
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });

       
       
          


        this.languageArray = this._resourceService.GetAvailableLanguages();
        this._customerService.GetCustomerTypes().subscribe(resp=> {
            
            var response = jQuery.parseJSON(resp);
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
                this._CustTypes = response.Data;
                if (this.modelInput.CustomerType == "" || this.modelInput.CustomerType == undefined || this.modelInput.CustomerType == null) {
                    var CusttypeId;
                    jQuery.each(this._CustTypes, function () {
                        CusttypeId = this.Value;
                        return false;
                    });
                    this.modelInput.CustomerType = CusttypeId;
                }
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        
        ////Sources
        this._customerService.GetSources().subscribe(resp=> {
            var response = jQuery.parseJSON(resp);
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
                this._Sources = response.Data;
                if (this.modelInput.CameFromCustomer == "" || this.modelInput.CameFromCustomer == undefined || this.modelInput.CameFromCustomer == null) {
                    var Source;
                    jQuery.each(this._Sources, function () {
                        Source = this.Value;
                        return false;
                    });
                    this.modelInput.CameFromCustomer = Source;
                }
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        //GetEmployees
        this._customerService.GetEmployees().subscribe(response=> {
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
                this._Employees = response.Data;
                if (this.modelInput.employeeid == "" || this.modelInput.employeeid == undefined || this.modelInput.employeeid == null) {
                    var empid;
                    jQuery.each(this._Employees, function () {
                        empid = this.Value;
                        return false;
                    });
                    this.modelInput.employeeid = empid;
                }
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        //GetSuffixes
        this._customerService.GetSuffixes().subscribe(response=> {
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
                this._Suffixes = response.Data;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        //GetPhoneTypes
        this._customerService.GetPhoneTypes().subscribe(response=> {
           // debugger;
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
                this._PhoneTypes = response.Data;
                var phid = "";
                jQuery.each(this._PhoneTypes, function () {
                    if (this.Text == "CellPhone") {

                        phid = this.Value;
                        return false;
                    }
                });
                this.modelInput.CustomerPhones[0].PhoneTypeId = phid;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });


        var epublish = 0;
        if (this.modelInput.CustomerPhones.length == 0) {
            var phid = "";

            jQuery.each(this._PhoneTypes, function () {
                if (this.Text == "CellPhone") {

                    phid = this.Value;
                    epublish = 1;
                    return false;
                }
            });

            this.modelInput.CustomerPhones = [{ PhoneTypeId: phid, Prefix: "", Area: "", Phone: "", IsSms: epublish, Comments: "", phpublish: epublish, SMSOrder: "SMS1", PublishOrder:"Pub1" }];
            // debugger;
                
        }
        if (this.modelInput.CustomerEmails.length == 0) {
            this.modelInput.CustomerEmails = [{ Email: "", EmailName: this.modelInput.FileAs, Newslettere: false, publish: epublish, NewsOrder: "News1", EPublishOrder:"EPub1" }]
        }

        //GetAddressTypes
        this._customerService.GetAddressTypes().subscribe(response=> {
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
                this._AddressTypes = response.Data;
               // debugger;
                var adid = "";
                jQuery.each(this._AddressTypes, function () {
                    if (this.Text == "Home") {

                        adid = this.Value;
                        return false;
                    }

                });
                this.modelInput.CustomerAddresses[0].AddressTypeId = adid;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        //Groups
        
        this._customerService.GetGroups().subscribe(response=> {
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
                this._Groups = response.Data;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        ////Countries
        
        this._customerService.GetCountries().subscribe(response=> {
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
                this._Countries = response.Data;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        ////States
        var CountryCode= this.Address.CountryCode;
        this._customerService.GetStates(CountryCode).subscribe(response=> {
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
                this._States = response.Data;
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        
        
        //Tree Group

        //this.bindGroupTree(this.IsShowAll);
        this._customerService.GetGeneralGroups(this.IsShowAll).subscribe(
            (data) => {
               // debugger;
                if (this.IsShowAll == false) {
                    jQuery("#groupTree").html("Loding...");
                    var res = jQuery.parseJSON(data).Data
                    jQuery("#groupTree").kendoTreeView({
                        loadOnDemand: true,
                        checkboxes: {
                            checkChildren: true
                        },
                        //check: this.onGroupSelect,
                        dataSource: res

                    });
                    var grpids = "";
                    jQuery.each(this.modelInput.CustomerGroups, function () {
                        grpids += this.CustomerGeneralGroupId + ";";
                    });
                    if (grpids.length > 0) {
                        Kendo_utility.checkingNodeIds(jQuery("#groupTree").data("kendoTreeView").dataSource.view(), grpids.substring(0, grpids.length - 1))
                    }
                }
                else {
                    jQuery("#groupTree1").html("Loding...");
                    var res = jQuery.parseJSON(data).Data
                    jQuery("#groupTree1").kendoTreeView({
                        loadOnDemand: true,
                        checkboxes: {
                            checkChildren: true
                        },
                        //check: this.onGroupSelect,
                        dataSource: res
                    });
                    var grpids = "";
                    jQuery.each(this.modelInput.CustomerGroups, function () {
                        grpids += this.CustomerGeneralGroupId + ";";
                    });
                    if (grpids.length > 0) {
                        Kendo_utility.checkingNodeIds(jQuery("#groupTree1").data("kendoTreeView").dataSource.view(), grpids.substring(0, grpids.length - 1))
                    }
                }
            },
            (err) => {

            },
            () => {

            }
        );
        


        //alert(moment().format('D MMM YYYY'));       
       // this.baseUrl + "Dropdown/BindAutoCompleteSrch"
        var SrchData = null;
           
        //alert('Hi');
        jQuery("#EmailTable tbody tr td a[name=delEbtn]").not(":last").hide();
        jQuery("#EmailTable tbody tr a[name=addEbtn]").not(":last").show();
        //$('.modal').modal();
        
    }
}
