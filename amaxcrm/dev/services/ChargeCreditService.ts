import { Injectable } from 'angular2/core';
import { Http, Headers  } from 'angular2/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Observable";
import {serviceConfig} from '../crmconfig';

@Injectable()
export class ChargeCreditService {
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

    

    public Save(SaveObject: string) {
        return this.http.post(
            this.baseUrl + "CheargeCredit/GetChargeAshrait",                   //URL for the request
            SaveObject,
            { headers: this.getHeader() }                                //{ headers: header }
            //HEADERS for the request
        ).map(res => res.text());
    }

    
    
    

    public BindTermDet(TermNo: string): Observable {
        return this.http.get(
            this.baseUrl + "CheargeCredit/GetTerminalDetByTermNo?TerminalNo=" + TermNo,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
   
    public BindCurrencyList(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindCurrencyList",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public BindCreditTypeList(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindCreditTypeList",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public BindChargeTypeList(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindChargeTypeList",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public BindYears(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindYears",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public BindMonths(): Observable {
        return this.http.get(
            this.baseUrl + "Dropdown/BindMonths",
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public GetWebServiceResponce(Url): Observable {
        return this.http.get(
            this.baseUrl + "CheargeCredit/GetWebServiceResponce?Url="+url,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public IsInsertTotblLastUpdate(CustomerId): Observable {
        return this.http.get(
            this.baseUrl + "CheargeCredit/IsInsertTotblLastUpdate?CustomerId=" + CustomerId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public InsertTotblLastUpdate(CustomerId, SumtoBill): Observable {
        return this.http.get(
            this.baseUrl + "CheargeCredit/InsertTotblLastUpdate?CustomerId=" + CustomerId + "&SumtoBill=" + SumtoBill,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public UpdateOwnerId(CustomerId, OwnerId): Observable {
        return this.http.get(
            this.baseUrl + "CheargeCredit/UpdateOwnerId?CustomerId=" + CustomerId + "&OwnerId=" + OwnerId,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
    public getPrint(DealNumber,TermNo): Observable {
        return this.http.get(
            this.baseUrl + "CheargeCredit/getPrint?DealNumber=" + DealNumber + "&TerminalNo=" + TermNo,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
}