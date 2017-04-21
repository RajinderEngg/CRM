import { Injectable } from 'angular2/core';
import { Http, Headers  } from 'angular2/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Observable";
import {serviceConfig} from '../crmconfig';


declare var jQuery;
@Injectable()
export class AmaxService {
    baseUrl: string;
    constructor(private http: Http) {
        this.baseUrl = serviceConfig.serviceApiUrl;
    }

    private getHeader(): Headers {
        //debugger;
        var header = new Headers();
        header.append("Content-Type", "application/json; charset=utf-8");
        if (sessionStorage.getItem(serviceConfig.accesTokenStoreName)) {
            header.append(serviceConfig.accesTokenRequestHeader, sessionStorage.getItem(serviceConfig.accesTokenStoreName));
        } else {
            throw 'Access token not available';
        }
        return header;
    }

    public validateLogin(UserID: string, Password: string, OrganizationName: string, rememberMe?: boolean) {
        //debugger;
        var lang = "en";
        var Language = localStorage.getItem("lang");
        if (Language != "" && Language != null)
            lang = Language;
        var userInfo = {
            OrgId: OrganizationName,
            UserName: UserID,
            Password: Password,
            Language: lang

        }
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        // header.append("Data-Type", "json");
        //header.append("Content-Type", "text/plain");
        

        var jdata = jQuery.param(userInfo);
        //debugger;
        return this.http.post(
            (this.baseUrl + "Service/Login"),                     //URL for the request
            jdata,                   //Data for the request
            { headers: header }                         //HEADERS for the request
        ).map(res => res.text());



    }

    public GetReport(ReportName: string, parameters: Object): Observable {
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        var reportinfo = {
            ReportName: ReportName,
            Parameters: parameters
        }
        var jdata = jQuery.param(reportinfo);
        return this.http.post(
            this.baseUrl + "Service/AmaxReportingService",
            jdata,
            { headers: header }
        ).map(res=> res.json().data);
    }

    public ExecuteJson(jsonQData: any): Observable {
        var header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        var jdata = jQuery.param(jsonQData);
        return this.http.post(
            this.baseUrl + "Service/ExecuteJson",
            jdata,
            { headers: header }
        ).map(res=> res.json().data);
    }


    //Queryes
    public GetDataFromServer(query): Observable {
        // debugger;
        
        var params = JSON.stringify(query);
        
        return this.http.post(
            this.baseUrl + "Service/DevQuery",
            params,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public GetGeneralGroupData(): Observable {
        //debugger;
        var GetGeneralGroupData = {
            uqery: "SELECT GroupId, GroupName, GroupNameEng, GroupParenCategory FROM CustomerGroupsGeneral WHERE ( IsSupport = 0 and Ishide = 0 and SecurityLevel <= 10) ORDER BY GroupName",
            parameters: {}
        }
        var jdata = JSON.stringify(GetGeneralGroupData);
        return this.http.post(
            this.baseUrl + "Service/DevQuery",
            jdata,
            { headers: this.getHeader() }
        ).map(res=> res.json().data);
    }

    public GetGeneralGroupTree(): Observable {
        //debugger;
        var lang = localStorage.getItem("lang");
        var req = '{"Lang":"' + lang + '"}';

        return this.http.post(
            this.baseUrl + "Service/GetTreeData",
            req,
            { headers: this.getHeader() }
        ).map(res=> res.text());
        //alert(rest);
        //return rest.kendoTree;
    }

    //public SendSms(username: string, company: string, message: string, groups: Array<number>, phoneTypeId: number) {
    //    //debugger;
    //    var header = new Headers();
    //    header.append("Content-Type", "application/x-www-form-urlencoded");
    //    var smsdet = {
    //        username: username,
    //        company: company,
    //        message: message,
    //        groups: groups,
    //        phoneType: phoneTypeId
    //    }
    //    var jdata = JSON.stringify(smsdet);
    //    return this.http.post(
    //        this.baseUrl + "Service/SendSms",
    //        //jQuery.param({
    //        //    username: username,
    //        //    company: company,
    //        //    message: message,
    //        //    groups: groups,
    //        //    phoneType: phoneTypeId
    //        //}),
    //        jdata,
    //        { headers: this.getHeader()  }
    //    ).map(res=> res.text());
    //}

    public SendSms(username: string, company: string, message: string, groups: Array<number>, phoneTypeId: number, providerId: number, returnBalanceAndCustomerCount: boolean, isConfirmed?: boolean = false, SenderPhoneNumber: string = "", sendlater: string = "") {
        var IsBranchEnabled = localStorage.getItem("IsBranchEnabled");
        var Branchid = localStorage.getItem("Branchid");
        return this.http.post(
            this.baseUrl + "Service/SendSms",
            JSON.stringify({
                username: username,
                company: company,
                message: message,
                groups: groups,
                phoneType: phoneTypeId,
                providerId: providerId,
                returnBalanceAndCustomerCount: returnBalanceAndCustomerCount,
                isConfirmed: isConfirmed,
                SenderPhoneNumber: SenderPhoneNumber,
                sendlater: sendlater,
                IsBranchEnabled: IsBranchEnabled,
                Branchid: Branchid
            }),
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    public ExecuteDataService(query: string, parameters: any) {
        return this.http.post(
            this.baseUrl + "Service/ExecuteDataService",
            JSON.stringify({ query: query, parameters: parameters }),
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }

    //Utility service
    public CreateCatche() {
        this.ExecuteDataService("InitalizeCRM", {}).subscribe(
            initialData=> {
                localStorage.setItem('SmsCompanyList', JSON.stringify(initialData["SmsCompanyList"]));
                localStorage.setItem('CellPhoneTypeList', JSON.stringify(initialData["CellPhoneTypeList"]));
                localStorage.setItem('PhoneTypeList', JSON.stringify(initialData["PhoneTypeList"]));
                localStorage.setItem('GeneralGroupData', JSON.stringify(initialData["GeneralGroupData"]));
            },
            error=> {
                console.log(error);
                localStorage.clear();
                sessionStorage.clear();
            },
            () => { });
    }


}