import {Component, Output, Input, EventEmitter, OnInit} from "angular2/core";
import {NgSwitch, NgSwitchWhen, NgSwitchDefault} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {RecieptService} from "../../services/RecieptService";
import {CustomerService} from "../../services/CustomerService";
import { jsonQ } from '../../jsonQ';
import {GroupFilterPipe, GroupParenFilterPipe, Kendo_utility} from "../../amaxUtil";
import { AmaxDate } from '../../comonComponents/basicComponents';
import {ChargeCreditService} from "../../services/ChargeCreditService";
declare var jQuery;
declare var moment;
@Component({
    templateUrl: './app/amax/Receipt/templates/ReceiptCreate.html',
    directives: [NgSwitch, NgSwitchWhen, NgSwitchDefault, AmaxDate],
    providers: [RecieptService, ResourceService, CustomerService, ChargeCreditService]
})

export class AmaxReceiptCreate implements OnInit {
    baseUrl: string;
    RES: Object = {};
    Formtype: string = "SCREEN_RECEIPTCREATE";
    Lang: string = "";
    _Banks = [];
    _CustomerNotes = [];
    _Addresses = [];
    _PayTypes = [];
    _Accounts = [];
    _Goals = [];
    _ProjectCats = [];
    _Projects = [];
    _Currencies = [];
//    modelInput.ReceiptLines = [];
    _ThankLetters = [];
    RowCount:number = 0;
    Isbtndisable: string = "";
    SAVE_BTN_TEXT: string="Save";
    MsgClass: string = "text-primary";
    modelInput: Object = {};
    saveInput: Object = {};
    ChangeDialog: string = "";
    CHANGEDIR: string = "";
    CustId: number;
    ShowMoreText: string = "";
    ShowMore: boolean = false;
    IsBankDetShow: boolean = false;
    DefaultDate: string = "";
    IPopUpOpen: boolean = false;
    static $inject = ['$scope', '$location', '$anchorScroll'];
   // @ViewChild('RTLDiv') private myScrollContainer: ElementRef;
    constructor(private _resourceService: ResourceService, private _RecieptService: RecieptService, private _CustomerService: CustomerService, private _routeParams: RouteParams, private _ChargeCreditService: ChargeCreditService) {
        
        this.RES.SCREEN_RECEIPTCREATE = {};
        this.modelInput = {};
        //this.modelInput.AddModel = {};
        this.modelInput.ReceiptLines = [];
        
        this.IPopUpOpen = false;
        this.IsBankDetShow = false;
        this.modelInput.ReceiptTypeId = _routeParams.params.ReceiptTypeId;
        //this.baseUrl = "http://localhost:3000/#/";
        // debugger;
       // alert(this.modelInput.ReceiptTypeId);
        this.baseUrl = _resourceService.AppUrl;
        this.modelInput.CustomerId = _routeParams.params.Id;
        this.modelInput.CustomerName = "";
        //this.modelInput.AddModel.ReferenceDate = "";
       // this.ShowMoreText = "More";
        this.DefaultDate = moment(new Date()).format('DD-MM-YYYY');
        this.Lang = localStorage.getItem("lang");
        var MText = "";
        if (this.Lang == "en") {
            MText = "More";
        }
        else {
            MText = "יותר";
        }
        var DupObj = {
            Amount: 0, ValueDate: this.DefaultDate, PayTypeId: 1, AccountId: "", AccountNo: "",
            BranchNo: "", Bank: "", CreditCardType: "", DonationTypeId: "", ProjectCategoryId: "", ProjectId: "", ReferenceDate: this.DefaultDate,
            For_Invoice: "", RecievedCustId: "", Payed: false, DepositeRemark: "", ShowMore: false, ShowMoreText: MText,
            CashCSS: "grey", CreditCSS: "white", BankCSS: "white", OtherCSS: "white", IsShowOthers: false, IsCreditShow: false, IsBankDetShow: false
        };

        this.modelInput.ReceiptLines.push(DupObj);
        
    }
    dateVSelectionChange(evt, dataobj) {
        console.log(evt);
        dataobj.ValueDate = evt;
        // alert(this.modelInput.BirthDate);
        //this.validateLogin();
    }


    dateSelectionChange(evt, dataobj) {
        console.log(evt);
        dataobj.ReferenceDate = evt;
        // alert(this.modelInput.BirthDate);
        //this.validateLogin();
    }

    More(modelobj){
        // alert("call");
        if (modelobj.ShowMore == true) {
            modelobj.ShowMore = false;
            if (this.Lang == "en") {
                modelobj.ShowMoreText = "More";
            }
            else {
                modelobj.ShowMoreText = "יותר";
            }
            //this.ShowMoreText = this.RES.CUSTOMER_MASTER.APP_LNK_LBL_MORE;
        }
        else {
            modelobj.ShowMore = true;
            if (this.Lang == "en") {
                modelobj.ShowMoreText = "Less";
            }
            else {
                modelobj.ShowMoreText = "פָּחוּת";
            }
            //this.ShowMoreText = this.RES.CUSTOMER_MASTER.APP_LNK_LBL_LESS;
        }
    }
    BankDetailShow(PayTypeId,dataobj) {
        if (PayTypeId != 1 && PayTypeId != 3)// Not Cash and not Credit card
        {
            dataobj.IsBankDetShow = true;
            dataobj.IsCreditShow = false;
            this.BindAutoCompleteBank();
        } else if (PayTypeId == 3) {
            dataobj.IsCreditShow = true;
            dataobj.IsBankDetShow = false;
        }
        else {// Only For Cash
            dataobj.IsBankDetShow = false;
            dataobj.IsCreditShow = false;
        }
    }
    setdefaultmode() {
        //document.location = this.baseUrl + "ReceiptCreate/" + this.modelInput.CustomerId + " /" + this.modelInput.ReceiptId;
        this.IPopUpOpen = false;
        this.RowCount = 0;
        window.scrollTo(0, 0);
        var _ReceiptTypeId = this.modelInput.ReceiptTypeId
        var PrevReceiptType = this.modelInput.RecieptType;

        var CustId = this.modelInput.CustomerId;
        var CustName = this.modelInput.CustomerName;
        var addressId = this.modelInput.AddressId;
        var thnkLetterId = this.modelInput.ThanksLetterId;
        var PrintValueDate = this.modelInput.ValueDate;
        var associationName = this.modelInput.associationName;
        var PrinterId = this.modelInput.PrinterId;
        var associationid = this.modelInput.associationId;
        this.modelInput = {};
        this.modelInput.CustomerId = CustId;
        this.modelInput.CustomerName = CustName;
        
        this.modelInput.AddressId = addressId;
        this.modelInput.associationName = associationName;
        
        this.modelInput.ThanksLetterId = thnkLetterId;
        this.modelInput.PrintValueDate = PrintValueDate;
        this.modelInput.ReceiptTypeId = _ReceiptTypeId;
        this.modelInput.PrinterId = PrinterId;
        this.modelInput.associationId = associationid;
        this.modelInput.ReceiptLines = [];
        var DupObj = {
            Amount: 0, ValueDate: this.DefaultDate, PayTypeId: 1, AccountId: "", AccountNo: "",
            BranchNo: "", Bank: "", CreditCardType: "", DonationTypeId: "", ProjectCategoryId: "", ProjectId: "", ReferenceDate: this.DefaultDate,
            For_Invoice: "", RecievedCustId: CustId, Payed: false, DepositeRemark: "", ShowMore: false, ShowMoreText: "More",
            CashCSS: "grey", CreditCSS: "white", BankCSS: "white", OtherCSS: "white", IsShowOthers: false, IsCreditShow: false, IsBankDetShow: false
        };

        this.modelInput.ReceiptLines.push(DupObj);
        //this.GetCustomerDetail(CustId);
                            
        //this.modelInput.RecieptType = PrevReceiptType;
        this._RecieptService.GetRecieptType(this._routeParams.params.ReceiptTypeId).subscribe(resp=> {
            // debugger;
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
                this.modelInput.RecieptType = response.Data[0].RecieptNameEng;
                this.modelInput.CurrencyId = response.Data[0].CurrencyId;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        // this.IsBankDetShow = false;

        this._RecieptService.GetEmployee().subscribe(resp=> {
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
                this.modelInput.EmployeeId = response.Data[0].Value;
                this.modelInput.EmployeeName = response.Data[0].Text;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        this._RecieptService.GetReceiptDetail().subscribe(resp=> {
            // debugger;
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
                this.modelInput.RecieptNo = response.Data.Value;
                this.modelInput.RecieptDate = response.Data.Text;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    saveReceiptData(IsExit) {
        debugger;
        //if (this.IPopUpOpen == false)
        //{
        var EmpName = jQuery("#EmpId").val();
        this._RecieptService.GetEmployeeFromEmpName(EmpName).subscribe(resp=> {
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
                this.modelInput.EmployeeId = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        if (this.modelInput.CustomerId != null && this.modelInput.CustomerId != "" && this.modelInput.CustomerId != undefined
            && this.modelInput.CurrencyId != null && this.modelInput.CurrencyId != "" && this.modelInput.CurrencyId != undefined
            && this.modelInput.ReceiptLines.length > 0
            && this.modelInput.associationId != null && this.modelInput.associationId != "" && this.modelInput.associationId != undefined
            && this.modelInput.EmployeeId != null && this.modelInput.EmployeeId != "-1" && this.modelInput.EmployeeId != undefined
            && this.modelInput.ThanksLetterId != null && this.modelInput.ThanksLetterId != "" && this.modelInput.ThanksLetterId != undefined
            && this.modelInput.PrinterId != null && this.modelInput.PrinterId != "" && this.modelInput.PrinterId != undefined
            && this.modelInput.RecieptNo != null && this.modelInput.RecieptNo != "" && this.modelInput.RecieptNo != undefined
        ) {

            this.modelInput.RecieptType = this.modelInput.ReceiptTypeId;
            var LineCount = this.modelInput.ReceiptLines.length;
            var CheckRowValid = true;
            if (LineCount < 1) {
                CheckRowValid = false;
            }
            //alert(JSON.stringify(this.modelInput));
            
            for (var cnt in this.modelInput.ReceiptLines) // for acts as a foreach
            {
                var ErrorMessage = "Row Number - " + (cnt + 1) + " is not valid <br>";
                if (this.ValidateRowModel(this.modelInput.ReceiptLines[cnt], ErrorMessage) == false) {
                    CheckRowValid = false;
                    break;
                }
            }

            if (CheckRowValid == true) {

                var jdata = JSON.stringify(this.modelInput);
                //console.log(jdata);
                this._RecieptService.AddReceipt(jdata).subscribe(response=> {
                    console.log(response);
                    response = jQuery.parseJSON(response);
                    this.Isbtndisable = "";

                    if (response.IsError == true) {
                        alert(response.ErrMsg);
                        this.MsgClass = "text-danger";
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
                        this._resourceService.deleteCookie("ReceiptCreate_Cache");
                        
                        if (IsExit == true) {
                            document.location = this.baseUrl + "ReceiptSelect/" + this.modelInput.EmployeeId + " /" + this.modelInput.CustomerId;
                        }
                        else {
                            this.setdefaultmode();
                            this.modelInput.PrintRecieptNo = response.Data.RecieptNo;
                            this.CustId = response.CustomerId;
                        }
                    }

                },
                    error=> console.log(error),
                    () => console.log("Save Call Compleated")
                );

            }

        }
        else {
            var msg = "";
            if (this.modelInput.CustomerId == null || this.modelInput.CustomerId == "" || this.modelInput.CustomerId == undefined) {
                msg += "<br>Please enter customerid";
            }
            if (this.modelInput.CurrencyId == null || this.modelInput.CurrencyId == "" || this.modelInput.CurrencyId == undefined) {
                msg += "<br>Please select currency";
            }
            if (this.modelInput.associationId == null || this.modelInput.associationId == "" || this.modelInput.associationId == undefined) {
                msg += "<br>Please enter Deposited By";
            }
            if (this.modelInput.EmployeeId == null || this.modelInput.EmployeeId == "" || this.modelInput.EmployeeId == undefined) {
                msg += "<br>Please enter valid employee";
            }
            if (this.modelInput.ThanksLetterId == null || this.modelInput.ThanksLetterId == "" || this.modelInput.ThanksLetterId == undefined) {
                msg += this.modelInput.ThanksLetterId;
                msg += "<br>Please select print template";
            }
            if (this.modelInput.PrinterId == null || this.modelInput.PrinterId == "" || this.modelInput.PrinterId == undefined) {
                msg += "<br>Please set printer name in database";
            }
            if (this.modelInput.RecieptNo == null || this.modelInput.RecieptNo == "" || this.modelInput.RecieptNo == undefined) {
                msg += "<br>Receipt no is not set";
            }
            if (this.modelInput.ReceiptLines.length == 0) {
                msg += "<br>Please atleast one amount detail in grid";
            }
            bootbox.alert({
                message: msg,
                className: this.ChangeDialog,
                buttons: {
                    ok: {
                        //label: 'Ok',
                        className: this.CHANGEDIR
                    }
                }
            });
        }
    //}
    }

    delModel(ModelObj): observable {
        debugger;
        if (this.modelInput.ReceiptLines.length > 1) {
            var index = 0;
            jQuery.each(this.modelInput.ReceiptLines, function () {
                if (this == ModelObj) {
                    return false
                }
                index = index + 1;

            });
            this.modelInput.ReceiptLines.splice(index, 1);
            this.BindTotal();
        }
    }
    BindTotal() {
        var ttotal = 0;
        //debugger;
        jQuery.each(this.modelInput.ReceiptLines, function () {
            ttotal += parseFloat(this.Amount);

        });
        this.modelInput.Total = ttotal;

        var vDate = this.modelInput.ValueDate;
        console.log(vDate);
        if (vDate == null || vDate == "") vDate = this.DefaultDate;
        var curID = this.modelInput.CurrencyId;
        //alert(curID);
        this._RecieptService.GetLeadcurrency(curID, "NIS", vDate).subscribe(response=> {

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

                //var n = num.toFixed(2);
                this.modelInput.TotalInLeadCurrent = parseFloat(response.Data) * ttotal;
                this.modelInput.TotalInLeadCurrent = this.modelInput.TotalInLeadCurrent.toFixed(2);
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        
    }
    createReceiptPDF() {
        
        if (this.modelInput.PrintRecieptNo != null) {
            var Recipt_No = this.modelInput.PrintRecieptNo;
            var Reciept_Type = this.modelInput.ReceiptTypeId;
            var Customer_Id = this.modelInput.CustomerId;
            var ThanksLetter_Id = this.modelInput.ThanksLetterId;
            var LeadCurrencyId = this.modelInput.CurrencyId;
            this._RecieptService.CreateReceiptPdf(Customer_Id, ThanksLetter_Id, Recipt_No, Reciept_Type, LeadCurrencyId).subscribe(response=> {
                debugger;
                response = jQuery.parseJSON(response);
                if (response.IsError == false) {
                    var l = response.Data.toString().substring(0, 5);
                    if (response.Data.toString().substring(0, 5) == "Link:") {
                        var PrintData = response.Data.toString().substring(5, response.Data.toString().length);
                        var windowObject = window.open(PrintData, 'Print', 'scrollbars=yes,resizable=yes,width=1050,height=650');
                        //windowObject.document.write(PrintData);
                        //windowObject.document.close();
                    }
                    else {
                        bootbox.alert({
                            message: response.Data,
                            className: this.ChangeDialog,
                            buttons: {
                                ok: {
                                    //label: 'Ok',
                                    className: this.CHANGEDIR
                                }
                            }
                        });
                    }
                } else {

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

    DupModel(ModelObj) {
        var MObj = {};
        this.RowCount++;
        ModelObj.RecieptRoWID = this.RowCount;
        MObj = ModelObj;
        
        var Vdate = MObj.ValueDate.split('-');
        var NewVDate = new Date(parseInt( Vdate[2]),parseInt(Vdate[1]),parseInt(Vdate[0]));
        NewVDate.setMonth(NewVDate.getMonth());
        var NVDate = moment(NewVDate).format('DD-MM-YYYY');
        //var Month = parseInt(Vdate[1]) + 1;
        //MObj.ValueDate = NewVDate;
        var DupObj = { Amount: ModelObj.Amount, ValueDate: NewVDate, PayTypeId: ModelObj.PayTypeId, AccountId: ModelObj.AccountId, AccountNo: ModelObj.AccountNo, BranchNo: ModelObj.BranchNo, Bank: ModelObj.Bank, CreditCardType: ModelObj.CreditCardType, DonationTypeId: ModelObj.DonationTypeId, ProjectCategoryId: ModelObj.ProjectCategoryId, ProjectId: ModelObj.ProjectId, ReferenceDate: ModelObj.ReferenceDate, For_Invoice: ModelObj.For_Invoice, RecievedCustId: ModelObj.RecievedCustId, Payed: ModelObj.Payed, DepositeRemark: ModelObj.DepositeRemark  };
        this.modelInput.ReceiptLines.push(DupObj);
        var index = 0;
        var TotalRows=this.modelInput.ReceiptLines.length;
        jQuery.each(this.modelInput.ReceiptLines, function () {
            if (index == (TotalRows - 1)) {
                
                this.ValueDate = NVDate;
            }
                index++;
        });
        this.BindTotal();
    }

    ValidateRowModel(CurrentModel, msg) {
        //msg = "";
        var IsValidData = true;
        if (CurrentModel.ValueDate != "") {
            if (moment(CurrentModel.ValueDate, "DD-MM-YYYY", true).isValid() == false) {
                bootbox.alert({ message: "Value Date is not valid" });
                return false;
            }
        }
        if (CurrentModel.ReferenceDate != "") {
            if (moment(CurrentModel.ReferenceDate, "DD-MM-YYYY", true).isValid() == false) {
                bootbox.alert({ message: "Reference Date is not valid" });
                return false;
            }
        }
        
            //var msg = "";
            if (CurrentModel.Amount == null || CurrentModel.Amount == "" || CurrentModel.Amount == undefined) {
                IsValidData = false;
                msg += "<br>Please enter amount";
            }
            if (CurrentModel.PayTypeId == null || CurrentModel.PayTypeId == "" || CurrentModel.PayTypeId == undefined) {
                IsValidData = false;
                msg += "<br>Please select paytype";
            }
            if (CurrentModel.ProjectId == null || CurrentModel.ProjectId == "" || CurrentModel.ProjectId == undefined) {
                IsValidData = false;
                msg += "<br>Please select project";
            }
            if (CurrentModel.DonationTypeId == null || CurrentModel.DonationTypeId == "" || CurrentModel.DonationTypeId == undefined) {
                IsValidData = false;
                msg += "<br>Please select goal";
            }
            if (CurrentModel.AccountId == null || CurrentModel.AccountId == "" || CurrentModel.AccountId == undefined) {
                IsValidData = false;
                msg += "<br>Please select account";
            }
            if (CurrentModel.PayTypeId == 8) //Bank
            {
                if (CurrentModel.AccountNo == null || CurrentModel.AccountNo == "" || CurrentModel.AccountNo == undefined) {
                    IsValidData = false;
                    msg += "<br>Please enter account no";
                }
                if (CurrentModel.BranchNo == null || CurrentModel.BranchNo == "" || CurrentModel.BranchNo == undefined) {
                    IsValidData = false;
                    msg += "<br>Please select branch no";
                }
                if (CurrentModel.Bank == null || CurrentModel.Bank == "" || CurrentModel.Bank == undefined) {
                    IsValidData = false;
                    msg += "<br>Please enter bank";
                }
            }
            if (CurrentModel.PayTypeId == 3) //Credit
            {
                if (CurrentModel.CreditCardType == null || CurrentModel.CreditCardType == "" || CurrentModel.CreditCardType == undefined) {
                    IsValidData = false;
                    msg += "<br>Please enter CreditCard";
                }
            }
            
            if (IsValidData == false) {
                bootbox.alert({
                    message: msg,
                    className: this.ChangeDialog,
                    buttons: {
                        ok: {
                            //label: 'Ok',
                            className: this.CHANGEDIR
                        }
                    }
                });
            }
            return IsValidData;
        
    }
    AddReceiptLine(CurrentModel) {
        // this.modelInput.AddModel.Bank = jQuery("#Bank").val();
        if (this.ValidateRowModel(CurrentModel, "")) {
            var MText = "";
            if (this.Lang == "en") {
                MText = "More";
            }
            else {
                MText = "יותר";
            }
            var ModelObj = {
                Amount: 0, ValueDate: this.DefaultDate, PayTypeId: "", AccountId: "", AccountNo: "",
                BranchNo: "", Bank: "", CreditCardType: "", DonationTypeId: "", ProjectCategoryId: "", ProjectId: "", ReferenceDate: this.DefaultDate,
                For_Invoice: "", RecievedCustId: "", Payed: false, DepositeRemark: "", ShowMore: false, ShowMoreText: MText,
                CashCSS: "grey", CreditCSS: "white", BankCSS: "white", OtherCSS: "white", IsShowOthers: false, IsCreditShow: false, IsBankDetShow: false
            };
            CurrentModel.RecievedCustId = this.CustId;

            this.modelInput.ReceiptLines.push(ModelObj);
            this.BindTotal();
        }
    }
    OpenCustSearch() {
        this.IPopUpOpen = true;
        localStorage.setItem("TempReceiptId", this.modelInput.ReceiptTypeId);

        if (this.modelInput != undefined && this.modelInput != null) {
            var jdata = JSON.stringify(this.modelInput);
            this._resourceService.setCookie("ReceiptCreate_Cache", jdata, 10);
        }
        document.location = this.baseUrl + "Customer/Search/0/ReceiptCreate";
        //jQuery('#CustSearchModal  .modal-content').html('<object data="' + this.baseUrl+'Customer/Search/1/ReceiptCreate" style="width:100%;height:500px"/>');
        //jQuery('#CustSearchModal').openModal();

    }
    OpenNotes() {

        //debugger;
        //if (this.IPopUpOpen == false) {
            jQuery('#NoteModal').openModal();
        //}
        //else {
        //    this.modelInput.CustomerId = this._routeParams.params.Id;
        //    window.open(this.baseUrl + "ReceiptCreate/" + this.modelInput.CustomerId + "/" + this.modelInput.ReceiptTypeId, "_self");
            
        //    jQuery('#CustSearchModal').closeModal();
        //    jQuery(".lean-overlay").css({ "display": "none" });
        //    this.setdefaultmode();
        //}
    }
    OpenTemplates() {
        jQuery('#TemplateModal').openModal();
    }
    ChooseNote(objct) {
        this.modelInput.CustomerNote = objct.Text;
        this.modelInput.CustomerNoteId = objct.Value;
    }
    ChooseThanksLetters(objct) {
        debugger;
        this.modelInput.ThanksLetterName = objct.Text;
        this.modelInput.ThanksLetterId = objct.Value;
    }
    GetCustomerDetail() {
        var CustId = this.modelInput.CustomerId;
        this._CustomerService.GetCompleteCustDet(CustId).subscribe(resp=> {
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
                response = response.Data;
                this._Addresses = response.CustomerAddresses;
                var Mainaddress;
                jQuery.each(response.CustomerAddresses, function () {
                    if (this.MainAddress == true) {
                        Mainaddress = this.AddressId;
                      
                    }
                });
                this.modelInput.AddressId = Mainaddress;
                this.modelInput.CustomerId = response.CustomerId;
                this.modelInput.ReceiptLines[0].RecievedCustId = response.CustomerId;
                this.CustId = response.CustomerId;
                this.modelInput.CustomerName = response.lname + " " + response.fname;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        
    }
    
    ChoosePayType(dataobject, pTypeId) {
       // debugger;
        dataobject.CashCSS = "white"
        dataobject.CreditCSS = "white"
        dataobject.BankCSS = "white"
        dataobject.OtherCSS = "white"
        dataobject.IsCreditShow = false;
        //var pTypeId = "";
        if (pTypeId != 0) {
            dataobject.PayTypeId = pTypeId;
            if (pTypeId == 1) {
                dataobject.CashCSS = "grey";
                //dataobject.IsBankDetShow == false;
            }
            else if (pTypeId==3) {
                dataobject.CreditCSS = "grey";
                //dataobject.IsBankDetShow == true;
                dataobject.IsCreditShow = true;
            }
            else if (pTypeId==8)//
            {
                dataobject.BankCSS = "grey"
              //  dataobject.IsBankDetShow == true;
            }
            dataobject.IsShowOthers = false;
            
        }
        else{
            dataobject.PayTypeId = 0;
            dataobject.IsShowOthers = true;
            dataobject.OtherCSS = "grey";
            //dataobject.IsBankDetShow == false;
        }

        if (pTypeId != 1 && pTypeId != 3)// Not Cash and not Credit card
        {
            dataobject.IsBankDetShow = true;
            dataobject.IsCreditShow = false;
            this.BindAutoCompleteBank();

        } else if (pTypeId == 3) {
            dataobject.IsCreditShow = true;
            dataobject.IsBankDetShow = false;
        }
        else {// Only For Cash
            dataobject.IsBankDetShow = false;
            dataobject.IsCreditShow = false;
        }
    }

    BindProjects(CatId) {

        this.BindProjectFromProjCat(CatId);
    }
    BindProjectFromProjCat(CatId) {
        this._RecieptService.GetProjects(CatId).subscribe(resp=> {
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
                this._Projects = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }

    BindAutoCompleteBank() {
        this._RecieptService.GetBanks().subscribe(resp=> {
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
                //this._Banks = response.Data;
                var typeaheadSource = [];
              //  debugger;
                jQuery.each(response.Data, function () {
                    var newtemp = {};
                    newtemp.id = this.Value;
                    newtemp.name = this.Text;
                    typeaheadSource.push(newtemp);
                });
                this._Banks = response.Data;
                jQuery('.Bank').typeahead({
                    source: typeaheadSource,
                    dataType: "JSON",
                });
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }
    ngOnInit() {
        
        //jQuery(".lean-overlay").css({ "display": "none" });
        jQuery("#NoteModal").closeModal();
        jQuery(".lean-overlay").css({ "display": "none" });
        window.scrollTo(0, 0);
        debugger;

        var jdata = this._resourceService.getCookie("ReceiptCreate_Cache");
        if (jdata != undefined && jdata != undefined && jdata != "") {
            jdata = jdata.substring(1, jdata.length);
            this.modelInput = jQuery.parseJSON(jdata);
            this.modelInput.CustomerId = this._routeParams.params.Id;
            this.GetCustomerDetail();
        }
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
        
        this._RecieptService.GetCustomerNotes().subscribe(resp=> {
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
                this._CustomerNotes = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        
        this.BindAutoCompleteBank();
        this._RecieptService.GetThanksLetters(this.modelInput.ReceiptTypeId).subscribe(resp=> {
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
                this._ThankLetters = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        this._RecieptService.GetAssociation().subscribe(resp=> {
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
                this.modelInput.associationId = response.Data[0].Value;
                this.modelInput.associationName = response.Data[0].Text;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        this._RecieptService.GetPrinter().subscribe(resp=> {
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
                this.modelInput.PrinterId = response.Data[0].Value;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        this._RecieptService.GetEmployee().subscribe(resp=> {
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
                this.modelInput.EmployeeId = response.Data[0].Value;
                this.modelInput.EmployeeName = response.Data[0].Text;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        this._RecieptService.GetPayType().subscribe(resp=> {
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
                this._PayTypes = response.Data;
                
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        this._RecieptService.GetAccounts().subscribe(resp=> {
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
                this._Accounts = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        this._RecieptService.GetGoals().subscribe(resp=> {
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
                this._Goals = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        this._RecieptService.GetProjectCats().subscribe(resp=> {
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
                this._ProjectCats = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        this.BindProjectFromProjCat(-1);
        this._RecieptService.GetCurrenciesFDB().subscribe(resp=> {
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
                this._Currencies = response.Data;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        this._RecieptService.GetRecieptType(this._routeParams.params.ReceiptTypeId).subscribe(resp=> {
           // debugger;
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
                this.modelInput.RecieptType = response.Data[0].RecieptNameEng;
                this.modelInput.CurrencyId = response.Data[0].CurrencyId;
                
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        this._RecieptService.GetReceiptDetail().subscribe(resp=> {
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
                this.modelInput.RecieptNo = response.Data.Value;
                this.modelInput.RecieptDate = this.DefaultDate;

            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
        var CustId = this.modelInput.CustomerId;
        this.GetCustomerDetail(CustId);

    }
}
