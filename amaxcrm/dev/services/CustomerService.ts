import { Injectable } from 'angular2/core';
import { Http, Headers  } from 'angular2/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Observable";
import {serviceConfig} from '../crmconfig';

@Injectable()
export class CustomerService {
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

    public AddCustomer(SaveObject: string) {                //SaveObject: string
       // debugger;
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        var lang = localStorage.getItem("lang");
        //var CustomerGroupsGeneralSetModel = {};
        //CustomerGroupsGeneralSetModel.CustomerId = 2;
        //CustomerGroupsGeneralSetModel.CustomerGeneralGroupId = 3;
        
        // var data = { "CustomerId": 2, "CustomerGeneralGroupId": 4 };//JSON.stringify(CustomerGroupsGeneralSetModel);
        //,                   //Data for the request
        //{ headers: header }
        //this.http.post("Customer/SaveTest", data, { headers: header }).
        //    map(res => res.json()).subscribe(response=> {
        //        alert(response);
        //    });'CustomerId=2&CustomerGeneralGroupId=4';//
        //var jdata = JSON.stringify(SaveObject);
        return this.http.post(
            this.baseUrl + "Customer/Save?lang=" + lang,                   //URL for the request
            SaveObject,
            { headers: this.getHeader() }                                //{ headers: header }
            //HEADERS for the request
        ).map(res => res.text());
    }

    public SaveFileAs(CustomerId: number, FileAs: string) {                //SaveObject: string
        var lang = localStorage.getItem("lang");
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        return this.http.post(
            this.baseUrl + "Customer/SaveFileAs?CustomerId=" + CustomerId + "&FileAs=" + FileAs + "&lang=" + lang,                   //URL for the request
            null,
            { headers: this.getHeader() }                                //{ headers: header }
            //HEADERS for the request
        ).map(res => res.text());
    }

    


    //Queryes
    public GetSources(): Observable{
        return this.http.get(
            this.baseUrl + "Dropdown/BindSources",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public GetCustomerTypes(): Observable {
        //debugger;

        var Lang = localStorage.getItem("lang");
             
//var langvalue = $.parseJSON(Lang)
        //alert(this.baseUrl + "Dropdown/BindCustType");
        return this.http.get(
            this.baseUrl + "Dropdown/BindCustType?lang=" + Lang,
            {headers:this.getHeader()}
        ).map(res=> res.text());
    }

    public GetEmployees(): Observable {
        //var Lang = localStorage.getItem("lang");
        return this.http.get(
            this.baseUrl + "Dropdown/BindEmployees",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    //public GetGroups(): Observable {
    //    return this.http.get(
    //        this.baseUrl + "/Source",
    //        { headers: this.getHeader() }
    //    ).map(res=> res.json().data);
    //}
    public GetSuffixes(): Observable {
        var Lang = localStorage.getItem("lang");
        return this.http.get(
            this.baseUrl + "Dropdown/BindSuffixes?lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetPhoneTypes(): Observable {
        var Lang = localStorage.getItem("lang");
        return this.http.get(
            this.baseUrl + "Dropdown/BindPhoneTypes?lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetAddressTypes(): Observable {
        var Lang = localStorage.getItem("lang");
        return this.http.get(
            this.baseUrl + "Dropdown/BindAddressType?lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetGroups(): Observable {
        var Lang = localStorage.getItem("lang");
        return this.http.get(
            this.baseUrl + "Dropdown/BindGroups?lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCountries(): Observable {
        var Lang = localStorage.getItem("lang");
        return this.http.get(
            this.baseUrl + "Dropdown/BindCountries?lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetStates(CountryName): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindStates?countryName=" + CountryName,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCities(CountryName,StateName): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindCities?CountryCode=" + CountryName + "&stateName=" + StateName,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetGeneralGroups(IsShowAll): Observable {
        var Lang = localStorage.getItem("lang");
        
        return this.http.get(
            this.baseUrl + "Dropdown/GroupTree?lang=" + Lang + "&IsShowAll=" + IsShowAll,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public CheckCustWithSameName(fname,lname,company): Observable {
        //var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Customer/CheckCustOfSameNameComp?fname=" + fname + "&lname=" + lname + "&company=" + company,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public CheckCustWithSameEmail(Email): Observable {
        //var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Customer/CheckCustOfSameEmail?Email=" + Email,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public CheckCustWithSamePhone(Phone): Observable {
        //var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Customer/CheckCustOfSamePhone?Phone=" + Phone,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCompleteCustDet(CustomerId): Observable {
        //var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Customer/GetCompleteCustomerDet?CustomerId=" + CustomerId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCustomersSearchData(fname,lname,company,phones,emails): Observable {
        //var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "Customer/GetCustomersSearchData?fname=" + fname + "&lname=" + lname + "&company=" + company + "&phones=" + phones + "&emails=" + emails,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetAutoCompleteSrch(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindAutoCompleteSrch",
            { headers: this.getHeader() }
        ).map(res=> res.text());
       // var bestPictures = {
           // datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
           // queryTokenizer: Bloodhound.tokenizers.whitespace,
           // prefetch: '../data/films/post_1960.json',
         
    }
    public GetCompleteSearch(SrchVal:any): Observable {
        return this.http.get(
            this.baseUrl + "Customer/GetCustomerListForSearch?SrchVal=" + SrchVal,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCompleteQuickSearch(SrchVal: any): Observable {
        return this.http.get(
            this.baseUrl + "Customer/GetCustomerListForQuickSearch?SrchVal=" + SrchVal,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public CheckIsOpenCharge(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindTerminalList",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCustomerCreditCardDet(CustomerId, customercreditCardid): Observable {
        return this.http.get(
            this.baseUrl + "Customer/GetCustomerCreditCardDet?CustomerId=" + CustomerId + "&customercreditCardid=" + customercreditCardid,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetPhoneTypeDet(PhoneTypeId): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/GetPhoneTypeDet?PhoneTypeId=" + PhoneTypeId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetCustTitles(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/GetCustTitles",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
}