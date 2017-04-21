import {NgSwitch, NgSwitchWhen, NgSwitchDefault, CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {CustomerService} from "../../services/CustomerService";
import {ChargeCreditService} from "../../services/ChargeCreditService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";
import {AutocompleteContainer} from '../../autocomplete/autocomplete-container';
import {Autocomplete} from '../../autocomplete/autocomplete.component';

export const AUTOCOMPLETE_DIRECTIVES = [Autocomplete, AutocompleteContainer];
declare var jQuery;
@Component({

    templateUrl: './app/amax/Charge_Credit/templates/ChargeCredit.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, AUTOCOMPLETE_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES],
    providers: [CustomerService, ResourceService, ChargeCreditService]
})

export class AmaxChargeCredit implements OnInit {
    modelInput = {};
    modelInputTemp = {};
    RES: Object = {};
    Formtype: string ="CHARGECREDIT_SCREEN";
    Lang: string = "";
    CustomerId: number = -1;
    static $inject = ['$scope', '$location', '$anchorScroll'];
    BaseAppUrl: string = "";
    _TerminalList = [];
    TerminalNumber: string = "";
    RemPaymentsText: string = "";
    NumOfPaymentsText: string = "";
    Isbtndisable: string = "";
    MsgClass: string = "";
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    ShowLoader: boolean;
    SAVE_BTN_TEXT: string;
    IsCreditCancel: boolean = false;
    IsCreditNoOfPay: boolean = false;
    Installments:any=0;
    InstallmentPay: any = 0;
    isfindcreditCardOwnerID: boolean = true;
    PrintUrl: string = "https://secure.cardcom.co.il/Note/ProgramNote.aspx";/*?DealNumber=" + oDealNumber + "&DisplayData=3&Termianl=" + oTermianl + "&DefPrint=false";*/
    _Mnths = [];
    _Years = [];
    _ChargeTypes = [];
    _Currency = [];
    _CreditTypes = [];

    constructor(private _resourceService: ResourceService, private _customerService: CustomerService, private _routeParams: RouteParams, private _ChargeCreditService: ChargeCreditService) {
        
        this.RES.CHARGECREDIT_SCREEN = {};
        this.BaseAppUrl = _resourceService.AppUrl;
        this.modelInput.CustomerId = _routeParams.params.Id;
        this.modelInput.oTerminalNumber = _routeParams.params.TermNo;
        
        this.modelInput.oCurrency = "Nis";
        this.modelInput.ChargeType = "0";
       // this.OpenDiv();
        this.modelInput.CreditType = "0";
        //this.modelInputTemp.DealNumber = "test";
        //this.modelInputTemp.oTerminalNumber = "test";
    //    modelInput.oCurrency        
    }
    ChangeOwnerId() {
        this.isfindcreditCardOwnerID = false;
    }
    CalculateConstPay() {
        if (this.modelInput.oSumToBill != undefined && this.modelInput.oSumToBill != null && this.modelInput.oSumToBill != ""
            && this.modelInput.ofirstpaymentsum != undefined && this.modelInput.ofirstpaymentsum != null && this.modelInput.ofirstpaymentsum != ""
            && this.modelInput.oNumOfPayments != undefined && this.modelInput.oNumOfPayments != null && this.modelInput.oNumOfPayments != "") {
            var RemAmt = parseFloat(this.modelInput.oSumToBill) - parseFloat(this.modelInput.ofirstpaymentsum);
            if (RemAmt >= 0) {
                if (parseInt(this.modelInput.oNumOfPayments) == 1) {
                    if (RemAmt > 0) {
                        bootbox.alert({
                            message: "Please give full amount in first payment or increase th no of payments",
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
                        this.NumOfPaymentsText = this.modelInput.oNumOfPayments + " payments, first payment: " + this.modelInput.ofirstpaymentsum;
                        this.RemPaymentsText = this.modelInput.ofirstpaymentsum+" and 0 payments of 0";
                    }
                }
                else if (parseInt(this.modelInput.oNumOfPayments) > 1) {
                    //debugger;
                    this.InstallmentPay = parseFloat(RemAmt / parseFloat(this.modelInput.oNumOfPayments - 1));
                    this.modelInput.oconstpatment = this.InstallmentPay;
                    
                    this.NumOfPaymentsText = this.modelInput.oNumOfPayments + " payments, first payment: " + this.modelInput.ofirstpaymentsum;
                    this.RemPaymentsText = this.modelInput.ofirstpaymentsum + " and " + parseFloat(this.modelInput.oNumOfPayments - 1) + " payments of " +
                        this.InstallmentPay.toFixed(2);
                    this.modelInput.oNumOfPayments = parseFloat(this.modelInput.oNumOfPayments) + 1;
                    
                }
            }
        }
    }
    ChangeCreditType(obj) {
        
        this.modelInput.CreditType = obj.Value;
        if (obj.Value != 0) {
            this.IsCreditNoOfPay = true;
        }
        else {
            this.IsCreditNoOfPay = false;
            
        }
        this.modelInput.ofirstpaymentsum = "";
        this.modelInput.oNumOfPayments = "";
        this.NumOfPaymentsText = "";
        this.RemPaymentsText;
    }
    OpenDiv() {
        var savetext = "";
        this.modelInput.ChargeType = jQuery("#Chargetype").val();
        //debugger;
        var chrgtype = this.modelInput.ChargeType;
        jQuery.each(this._ChargeTypes, function () {
           
            if (this.Value == chrgtype) {

                savetext = this.Text;
                return false;
            }
        });
        this.SAVE_BTN_TEXT = savetext;
        if (this.modelInput.ChargeType != "0") {
            this.IsCreditCancel = true;
        }
        else if (this.modelInput.ChargeType == "0") {
            this.IsCreditCancel = false; 
            
            
        }
        this.modelInput.ouserpassword = "";
    }
    CheckCreditCardDet() {
        if (jQuery("input:checked").val() != undefined && jQuery("input:checked").val() != null) {
            if (this.modelInput.UseToken != undefined && this.modelInput.UseToken != null) { } else {
                this.modelInput.UseToken = false;
            }
            this.modelInput.CompanySlika = "1";

            if (this.modelInput.CreditType == "0") {
                this.modelInput.oNumOfPayments = "1";

            }
            else {
                if (this.modelInput.oNumOfPayments != undefined && this.modelInput.oNumOfPayments != null) {
                    if (this.modelInput.oNumOfPayments != "") {
                        this.modelInput.oNumOfPayments = parseInt(this.modelInput.oNumOfPayments + 1);
                    }
                    else {
                        this.modelInput.oNumOfPayments = "1";
                    }
                }
            }
            if (this.modelInput.ChargeType == "0") {
                this.modelInput.odealtype = "1";
            }
            else if (this.modelInput.ChargeType == "1") {
                this.modelInput.odealtype = "51";
            }
            else if (this.modelInput.ChargeType == "2") {
                this.modelInput.odealtype = "52";
            }
            if (this.modelInput.odealtype == "1") {
                //swal({
                //    title: "Are you sure?",
                //    text: "Note that you requested charge the customer, will continue!",
                //    type: "warning",
                //    showCancelButton: true,
                //    confirmButtonColor: "#DD6B55",
                //    confirmButtonText: "Yes",
                //    cancelButtonText: "No",
                //    closeOnConfirm: false,
                //    closeOnCancel: false
                //},
                //    function (isConfirm) {
                //        if (isConfirm) {
                //        }
                //        else {
                //            return false;
                //        }
                //    });

                //bootbox.confirm({
                //    message: "Note that you requested charge the customer, will continue?",
                //    buttons: {
                //        confirm: {
                //            label: 'Yes',
                //            className: 'btn-success'
                //        },
                //        cancel: {
                //            label: 'No',
                //            className: 'btn-danger'
                //        }
                //    },
                //    callback: function (result) {
                //        if (result == false) {
                //            return false;
                //        }
                //        //console.log('This was logged in the callback: ' + result);
                //    }
                //});


                if (confirm("Note that you requested charge the customer, will continue?") == true) { }
                else {
                    return false;
                }
            }
            var IsInsert = "";
            this._ChargeCreditService.IsInsertTotblLastUpdate(this.modelInput.CustomerId).subscribe(resp=> {
                
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
                    IsInsert = response.Data;
                    if (IsInsert.length > 0) {

                        //swal({
                        //    title: "Are you sure?",
                        //    text: IsInsert,
                        //    type: "warning",
                        //    showCancelButton: true,
                        //    confirmButtonColor: "#DD6B55",
                        //    confirmButtonText: "Yes",
                        //    cancelButtonText: "No",
                        //    closeOnConfirm: false,
                        //    closeOnCancel: false
                        //},
                        //    function (isConfirm) {
                        //        if (isConfirm) {
                        //            this._ChargeCreditService.InsertTotblLastUpdate(this.modelInput.CustomerId, this.modelInput.oSumToBill).subscribe(resp=> {

                        //                var response1 = jQuery.parseJSON(resp);
                        //                if (response1.IsError == true) {
                        //                    swal(response1.ErrMsg);
                        //                }
                        //                else {
                        //                }
                        //            }, error=> {
                        //                console.log(error);
                        //            }, () => {
                        //                console.log("CallCompleted")
                        //            });
                        //        }
                        //        else {
                        //            return false;
                        //        }
                        //    });
                        
                        if (confirm(IsInsert) == true) {

                            this._ChargeCreditService.InsertTotblLastUpdate(this.modelInput.CustomerId, this.modelInput.oSumToBill).subscribe(resp=> {

                                var response1 = jQuery.parseJSON(resp);
                                if (response1.IsError == true) {
                                    bootbox.alert({
                                        message: response1.ErrMsg,
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
                                }
                            }, error=> {
                                console.log(error);
                            }, () => {
                                console.log("CallCompleted")
                            });
                        }
                        else {
                            return false;
                        }
                    }
                }
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });



            if (this.isfindcreditCardOwnerID == false) {

                //swal({
                //    title: "Are you sure?",
                //    text: "You want to update a customer ID card!",
                //    type: "warning",
                //    showCancelButton: true,
                //    confirmButtonColor: "#DD6B55",
                //    confirmButtonText: "Yes",
                //    cancelButtonText: "No",
                //    closeOnConfirm: false,
                //    closeOnCancel: false
                //},
                //    function (isConfirm) {
                //        if (isConfirm) {
                //            this._ChargeCreditService.UpdateOwnerId(this.modelInput.CustomerId, this.modelInput.oCardOwnerId).subscribe(resp=> {

                //                var response = jQuery.parseJSON(resp);
                //                if (response.IsError == true) {
                //                    swal(response.ErrMsg);
                //                }
                //                else {
                //                }
                //            }, error=> {
                //                console.log(error);
                //            }, () => {
                //                console.log("CallCompleted")
                //            });
                //        }
                //        else {
                //            return false;
                //        }
                //    });
              
                if (confirm("You want to update a customer ID card?") == true) {
                    this._ChargeCreditService.UpdateOwnerId(this.modelInput.CustomerId, this.modelInput.oCardOwnerId).subscribe(resp=> {
                        
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
                        }
                    }, error=> {
                        console.log(error);
                    }, () => {
                        console.log("CallCompleted")
                    });
                }
                else {
                    return false;
                }
                
            }
            var jdata = JSON.stringify(this.modelInput);
            this._ChargeCreditService.Save(jdata).subscribe(resp=> {
                
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

                    // document.location = this.BaseAppUrl + "ChargeCredit/" + this.modelInput.CustomerId + "/" + this.modelInput.oTerminalNumber;
                    this.modelInput.DealNumber = response.Data.DealNumber;
                    this.modelInputTemp = this.modelInput;
                    this.modelInput = {};
                    this.modelInput.oTerminalNumber = this._routeParams.params.TermNo;
                    this.modelInput.CustomerId = this._routeParams.params.Id;
                    this.modelInput.oCurrency = "Nis";
                    this.modelInput.ChargeType = "0";
                    this._ChargeTypes = [];
                    this._ChargeCreditService.BindChargeTypeList().subscribe(resp=> {
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
                            this._ChargeTypes = response.Data;
                            var savetext = "";
                            var chrgtype = this.modelInput.ChargeType;
                            jQuery.each(this._ChargeTypes, function () {

                                if (this.Value == chrgtype) {

                                    savetext = this.Text;
                                    return false;
                                }
                            });
                            this.SAVE_BTN_TEXT = savetext;

                        }
                    }, error=> {
                        console.log(error);
                    }, () => {
                        console.log("CallCompleted")
                    });
                    this.modelInput.CreditType = "0";
                    this._ChargeCreditService.BindTermDet(this.modelInput.oTerminalNumber).subscribe(resp=> {
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
                            this.modelInput.ousername = response.Data.instituteName;

                        }
                    }, error=> {
                        console.log(error);
                    }, () => {
                        console.log("CallCompleted")
                    });
                    this._customerService.GetCompleteCustDet(this.modelInput.CustomerId).subscribe(resp=> {
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
                            this.modelInput.CustomerName = response.Data.fname + " " + response.Data.lname;
                            this.modelInput.oCardOwnerId = response.Data.CustomerCode;
                        }
                    }, error=> {
                        console.log(error);
                    }, () => {
                        console.log("CallCompleted")
                    });
                    //debugger;
                    //var PrintUrl = this.PrintUrl + "?DealNumber=" + response.Data.DealNumber +
                    //    "&DisplayData=3&Termianl=" + this.modelInput.oTerminalNumber +
                    //    "&DefPrint=false";
                    this.PrintData(response.Data.DealNumber, this.modelInput.oTerminalNumber);
                }
            }, error=> {
                console.log(error);
            }, () => {
                console.log("CallCompleted")
            });
            
        }
        else {
            bootbox.alert({
                message: 'Please select credit type',
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
    ClearCardNo() {
        this.modelInput.oCardNumber = "";
    }
    PrintPrevData() {
        //debugger;
        if (this.modelInputTemp.DealNumber != undefined && this.modelInputTemp.DealNumber != null
            && this.modelInputTemp.oTerminalNumber != undefined && this.modelInputTemp.oTerminalNumber != null) {
            this.PrintData(this.modelInputTemp.DealNumber, this.modelInputTemp.oTerminalNumber);
        }
        else {
            bootbox.alert({
                message: "Please do any transection and then click on print button",
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
    CancelBtn() {
        document.location = this.BaseAppUrl + "Customer/Add/" + this._routeParams.params.Id;
    }
    PrintData(DealNumber,Terminal) {
        var PrintData = "";
        this._ChargeCreditService.getPrint(DealNumber,Terminal).subscribe(resp=> {
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
                PrintData = response.Data;
                //var OpenWindow = window.open("");
                var windowObject = window.open('', 'Print','scrollbars=yes,resizable=yes,width=1050,height=650');
                windowObject.document.write(PrintData);
                windowObject.document.close();
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }

    OpenNewReceipt() {
        if (this.modelInput != undefined) {
            var emid = localStorage.getItem("employeeid");
            document.location = this.BaseAppUrl + "ReceiptSelect/" + emid;
        }
    }
    ngOnInit() {
        if (localStorage.getItem("lang") == "") {
            localStorage.setItem("lang", "en");
        }
        this.Lang = localStorage.getItem("lang");
        
      
        
       
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

       this._ChargeCreditService.BindTermDet(this.modelInput.oTerminalNumber).subscribe(resp=> {
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
               this.modelInput.ousername = response.Data.instituteName;

           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });

       this._customerService.GetCompleteCustDet(this.modelInput.CustomerId).subscribe(resp=> {
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
               this.modelInput.CustomerName = response.Data.fname + " " + response.Data.lname;
               this.modelInput.oCardOwnerId = response.Data.CustomerCode;
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });

       this._ChargeCreditService.BindCurrencyList().subscribe(resp=> {
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
               this._Currency = response.Data;
               
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });

       this._ChargeCreditService.BindCreditTypeList().subscribe(resp=> {
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
               this._CreditTypes = response.Data;
               var rbid = "#" + this.modelInput.CreditType.toString();
               jQuery(rbid).prop("checked", true);
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });

       this._ChargeCreditService.BindChargeTypeList().subscribe(resp=> {
           
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
               this._ChargeTypes = response.Data;
               var savetext = "";
               var chrgtype = this.modelInput.ChargeType;
               jQuery.each(this._ChargeTypes, function () {
                   
                   if (this.Value == chrgtype) {

                       savetext = this.Text;
                       return false;
                   }
               });
               this.SAVE_BTN_TEXT = savetext;
              
           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
       
       this._ChargeCreditService.BindYears().subscribe(resp=> {
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
               this._Years = response.Data;

           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });

       this._ChargeCreditService.BindMonths().subscribe(resp=> {
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
               this._Mnths = response.Data;

           }
       }, error=> {
           console.log(error);
       }, () => {
           console.log("CallCompleted")
       });
       window.setTimeout(function () { $("[name='CreditType']:first").attr("checked", true); },1000);
    }
}
