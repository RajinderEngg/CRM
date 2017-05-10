import { Injectable } from 'angular2/core';
import { Http, Headers  } from 'angular2/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Observable";
import {serviceConfig} from '../crmconfig';

@Injectable()
export class RecieptService {
    baseUrl: string;
    constructor(private http: Http) {
        this.baseUrl = serviceConfig.serviceApiUrl;
    }

    private getHeader():Headers{
        var header = new Headers();
        header.append("Content-Type", "application/json");
        if(sessionStorage.getItem(serviceConfig.accesTokenStoreName)){
            header.append(serviceConfig.accesTokenRequestHeader, sessionStorage.getItem(serviceConfig.accesTokenStoreName));
        }else {
            throw 'Access token not available';
        }
        return header;
    }

    public GetRecieptTypeList(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "RecieptType/GetRecieptTypes?lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public BindRecieptType(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindReceiptTypes",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetReceiptDetail(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/GetReceiptDetail",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetRecieptType(ReceiptId): Observable {
        return this.http.get(
            this.baseUrl + "RecieptType/GetRecieptType?RecieptTypeId=" + ReceiptId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetRecieptThnksLettersList(ReceiptId): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "RecieptThnksLetter/GetRecieptTnksLtrsByRcptId?RecieptTypeId=" + ReceiptId+"&lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetRecieptThnksLetter(ThnksLetterId): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "RecieptThnksLetter/GetRecieptThnksLetter?ThnksLetterId=" + ThnksLetterId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public AddReceipt(SaveObject: string) {                //SaveObject: string
        // debugger;
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        
        return this.http.post(
            this.baseUrl + "Receipt/Save",                   //URL for the request
            SaveObject,
            { headers: this.getHeader() }                                //{ headers: header }
            //HEADERS for the request
        ).map(res => res.text());
    }

    public DeleteReceiptThnksLetter(ThnksLetterId): Observable {

        
        return this.http.get(
            this.baseUrl + "RecieptThnksLetter/DeleteReceiptThnksLetter?ThnksLetterId=" + ThnksLetterId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public GetReceiptThnksLetter(ThnksLetterId): Observable {

        
        return this.http.get(
            this.baseUrl + "RecieptThnksLetter/GetReceiptThnksLetter?ThnksLetterId=" + ThnksLetterId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public AddReceiptThnksLetter(SaveObject: string) {                //SaveObject: string
       // debugger;
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        return this.http.post(
            this.baseUrl + "RecieptThnksLetter/Save",                   //URL for the request
            SaveObject,
            { headers: this.getHeader() }                                //{ headers: header }
            //HEADERS for the request
        ).map(res => res.text());
    }
    public GetTemplate(ThnksLetterId): Observable {
    
        return this.http.get(
            this.baseUrl + "Template/GetTemplate?ThnksLetterId=" + ThnksLetterId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public SaveTemplate(ThnksLetterId, Source): Observable {
    
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        return this.http.post(
            this.baseUrl + "Template/SaveTemplate?ThnksLetterId=" + ThnksLetterId + "&Source=" + Source,
            null,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }


    public GetRecieptModes(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/GetReceiptModes?lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetReceipts(EmployeeId, Mode): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Receipt/GetReceipts?Lang=" + Lang + "&EmpId=" + EmployeeId + "&RecModes=" + Mode,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    // For Receipt Creation
    public GetCustomerNotes(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/BindCustomerNoteList",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetThanksLetters(ReceiptId): Observable {

        return this.http.get(
            this.baseUrl + "Dropdown/BindThanksLetters?ReceiptId=" + ReceiptId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetAssociation(): Observable {

        return this.http.get(
            this.baseUrl + "Dropdown/BindAssociation",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetPrinter(): Observable {

        return this.http.get(
            this.baseUrl + "Dropdown/GetPrinter",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetEmployee(): Observable {

        return this.http.get(
            this.baseUrl + "Dropdown/BindEmployee",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetEmployeeFromEmpName(EmpName): Observable {

        return this.http.get(
            this.baseUrl + "Dropdown/BindEmployeeFromEmpName?EmpName=" + EmpName,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCurrenciesFDB(): Observable {

        return this.http.get(
            this.baseUrl + "Dropdown/BindCurrencyListFromDb",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetBanks(): Observable {

        return this.http.get(
            this.baseUrl + "Dropdown/GetBanks",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetPayType(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/GetPayType",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetAccounts(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/GetAccounts",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetGoals(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/GetGoals",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetProjectCats(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/GetProjectCats",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetProjects(CatId): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/GetProjects?ProjectCatId=" + CatId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetLeadcurrency(FromCurrencyId, ToCurrency, ValueDate): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Receipt/GetLeadcurrency?FromCurrencyId=" + FromCurrencyId + "&ToCurrency=" + ToCurrency + "&ValueDate="+ValueDate,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public CreateReceiptPdf(CustomerId, ThankLetterId, RecieptNumber, RecieptType,CurrencyId): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Receipt/ReceiptPdf?CustomerId=" + CustomerId + "&ThankLetterId=" + ThankLetterId + "&RecieptCode=" + RecieptNumber 
            + "&RecieptType=" + RecieptType + "&LeadCurrencyId=" + CurrencyId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetProducts(ProdCatId, ProdNo, ProdName): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Receipt/GetProductsForSearch?ProdCatId=" + ProdCatId + "&ProdNo=" + ProdNo + "&ProdName=" + ProdName,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetProdCats(): Observable {
        var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Dropdown/GetProductCats",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
}