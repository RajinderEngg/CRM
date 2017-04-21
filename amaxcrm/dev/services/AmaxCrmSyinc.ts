import {Injectable} from 'angular2/core';
import {ResourceService} from "./ResourceService";
import {LocalDict, crmConfig} from "../crmconfig";

@Injectable()
export class AmaxCrmSyinc {
    constructor(private _resourceService: ResourceService) { }
    FormType: string = "SCREEN_SMS";
    on(eventName:string, handler:any) {
        document.addEventListener(eventName,(e)=> handler(e['detail']));
    }

    off(eventName:string, handler:any) {
        document.removeEventListener(eventName,(e)=> handler(e['detail']));
    }

    emit(eventName:string, eventData:any) {
        var evt = new CustomEvent(eventName, eventData);
        evt.initCustomEvent(eventName,true,true,eventData);
        document.dispatchEvent(evt);
    }

    storeLocal(key:string, value:any){
        localStorage.setItem(key, JSON.stringify(value));
        this.emit('lsset.'+key,value);
    }
    storeSession(key:string, value:any){
        sessionStorage.setItem(key, JSON.stringify(value));
        this.emit('ssset.'+key,value);
    }

    fetchLocal(key: string): string{
        
        return localStorage.getItem(key);
    }
    fetchSession(key: string): string{
        
        return localStorage.getItem(key);
    }

    fetchLocalJSON(key: string): any{
        
        return JSON.parse(this.fetchLocal(key));
    }
    fetchLocalEval(key:string):any{
        return eval(this.fetchLocal(key));
    }


    fetchLanguageResource(resourceName: string): any{
        //debugger;
        var _resource; //= this.fetchLocalJSON(LocalDict.languageResource);
        var languageCode = localStorage.getItem("lang");
        this._resourceService.GetLangRes("SCREEN_SMS", languageCode).subscribe(response=> {
            //debugger;
            response = $.parseJSON(response);
            if (response.IsError == true) {
                alert(response.ErrMsg);
            }
            else {
                //localStorage.setItem("langresource", JSON.stringify(response.Data));
                //localStorage.setItem("lang", evt.code);
                _resource = response.Data;
                //this.storeLocal(LocalDict.languageResource, response.Data);
                //localStorage.setItem(LocalDict.selectedLanguage, languageCode);
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });

        if(_resource){
            try{
                return _resource[resourceName];
            }catch(ex){
                console.error("Error while parsing resource!");
                console.error(ex);
                return false;
            }
        }
        else{
            var selectedLanguage = localStorage.getItem(LocalDict.selectedLanguage) || crmConfig.falbackLanguage;
            localStorage.setItem(LocalDict.selectedLanguage,selectedLanguage);

            this.loadLanguageResource(selectedLanguage);
            return false
        }
    }
    loadLanguageResource(languageCode:string){
        //this._resourceService.GetSelecetdLanguage(languageCode).subscribe(
        //    data=> {
        //        debugger;
        //        this.storeLocal(LocalDict.languageResource,data);
        //        localStorage.setItem(LocalDict.selectedLanguage,languageCode);
        //    },
        //    error=>{
        //        console.error("Error while featching language json");
        //        console.error(error);
        //    },
        //    ()=>{
        //        console.log('Language resource compleate');
        //    }
        //);
        languageCode = localStorage.getItem("lang");
        this._resourceService.GetLangRes(this.FormType, languageCode).subscribe(response=> {
           // debugger;
            response = $.parseJSON(response);
            if (response.IsError == true) {
                alert(response.ErrMsg);
            }
            else {
                //localStorage.setItem("langresource", JSON.stringify(response.Data));
                //localStorage.setItem("lang", evt.code);
                //this.RES = response.Data;
                this.storeLocal(LocalDict.languageResource, response.Data);
                localStorage.setItem(LocalDict.selectedLanguage, languageCode);
            }
        }, error=> {
            console.log(error);
        }, () => {
            console.log("CallCompleted")
        });
    }

    isRtl(): boolean{
        //debugger;
        var selectedLanguage = localStorage.getItem(LocalDict.selectedLanguage) || crmConfig.falbackLanguage
        switch (selectedLanguage){
            case "he":
                return false;
            default:
                return true;
        }
        return true;
    }
}