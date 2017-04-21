import { Injectable } from 'angular2/core';
import { Http, Headers  } from 'angular2/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Observable";
import {serviceConfig} from '../crmconfig';

@Injectable()
export class GeneralGroupsService {
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
    public GetCompleteCustDet(GroupIds): Observable {
        //var Lang = localStorage.getItem("lang");

        return this.http.get(
            this.baseUrl + "GeneralGroups/GetCustomersListOfGroups?GroupIds=" + GroupIds,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
}