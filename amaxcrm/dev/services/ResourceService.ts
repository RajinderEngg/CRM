import {Injectable} from "angular2/core";
import {Http, Headers} from "angular2/http";
import 'rxjs/add/operator/map';
import {LocalDict, crmConfig, serviceConfig} from "../crmconfig";
export interface MenueItem{
    label:string,
    routerUrl:string,
    faicon?:string,
    subMenus?:Array<MenueItem>
}

@Injectable()
export class ResourceService {
    headers: Headers;
    AppUrl: string;
    baseUrl: string;
    ImageUrl: string;
    constructor(private _http: Http) {
        this.headers=new Headers();
        this.headers.append("Content-Type", "application/json");
        this.baseUrl = serviceConfig.serviceApiUrl;
        this.AppUrl = serviceConfig.AppUrl;
        this.ImageUrl = serviceConfig.ImageUrl;
    }




    private getHeader(): Headers {
        var header = new Headers();
        header.append("Content-Type", "application/json");
        //if (sessionStorage.getItem(serviceConfig.accesTokenStoreName)) {
        //    header.append(serviceConfig.accesTokenRequestHeader, sessionStorage.getItem(serviceConfig.accesTokenStoreName));
        //} else {
        //    throw 'Access token not available';
        //}
        return header;
    }


    public setCookie(name: string, value: Object, expireDays: number, path: string = "") {       /*expireDays: number,*/
        //debugger;
        let d: Date = new Date();
        d.setTime(d.getTime() + expireDays * 24 * 60 * 60 * 1000);
        let expires: string = "expires=" + d.toUTCString();
        document.cookie = name + "=" + value + "; " + expires + (path.length > 0 ? "; path=" + path : "");

        //document.cookie = name + "=" + value + "; path=" + path;
    }

    public getCookie(name: string) {
        //debugger;
        let ca: Array<string> = document.cookie.split(';');
        let caLen: number = ca.length;
        let cookieName = name + "=";
        let c: string;

        for (let i: number = 0; i < caLen; i += 1) {
            c = ca[i].replace(/^\s\+/g, "");
            var index = c.indexOf(cookieName);
            if (c.indexOf(cookieName) == 0) {
                return c.substring(cookieName.length, c.length);
            }
            if (c.indexOf(cookieName) == 1 && cookieName == name+"=") {
                return c.substring(cookieName.length, c.length);
            }
        }
        return "";
    }

    public deleteCookie(name) {
        this.setCookie(name, "", -1);
    }


    public LoadLanguageResource(code: string = "he") {
        // the function is not in use
    }


    public GetSelecetdLanguage(code?: string) {
        //debugger;
        if(!code)code="en";
        code = code.trim();
       // debugger;
        //,
        //{ headers: this.headers }
        return this._http.get(
            "/src/lang/"+code+".json")
            .map(
            res => res.json()
            );
    }

    public GetAvailableLanguages():[Object]{
        return [
            {name:"English", code:"en"},
            {name:"עִברִית", code:"he"}
        ];
    }

    public GetMenues() {
       
        var lang = this.getCookie("lang");
        //alert(lang);
        if (lang.length > 0)
            lang = lang.substring(1, lang.length);
        if (lang == "")
            lang = "en";
        return new Promise(resolve=>
            this._http.get(
                "/src/menu-" + lang+".json",
                { headers: this.headers })
                .map(res =>res.json().menu)
                .subscribe(menudata=> {
                    
                    resolve(menudata);
                })
        )
    }

    public GetFormByName(formName:string){
        return this._http.get("/src/forms/"+formName+".json",{headers:this.headers})
            .map(res=>res.json());

        //return new Promise(resolve=>{
        //    this._http.get("/src/forms/"+formName+".json",{headers:this.headers})
        //        .map(res=>res.json())
        //        .subscribe(data=>resolve(data));
        //})
    }

    //localstorage utility
    public GetLocalStorage(key: string): any {
        var data = localStorage.getItem(key);
        return JSON.parse(data);
    }
    public SetLocalStorage(key: string, data: any) {
        localStorage.setItem(key, JSON.stringify(data));
    }

    //sessionStorage utility
    public GetSessionStorage(key: string): any {
        var data = sessionStorage.getItem(key);
        return JSON.parse(data);
    }
    public SetSessionStorage(key: string, data: any) {
        sessionStorage.setItem(key, JSON.stringify(data));
    }
    public GetLangRes(FormType: string, Lang: string): Observable {
        //alert(FormType);
        //debugger;
        //var Lang = localStorage.getItem("lang");
        return this._http.get(
            this.baseUrl + "Dropdown/GetLangRes?FormType=" + FormType + "&Lang=" + Lang,
            { headers: this.getHeader() }
        ).map(res=> res.text());
    }
}