import {Component, Output, Input, EventEmitter, OnInit} from 'angular2/core';
import { SelectInputComponent } from '../comonComponents/basicComponents';
import {ResourceService} from "../services/ResourceService";


@Component({
    selector:'mx-login',
    templateUrl:'./app/amaxComponents/templates/login.html',
    directives: [SelectInputComponent ],
    providers:[ResourceService]
})
export class AmaxLoginComponent implements OnInit{
    ForgetIcon: string = "";
    RegIcon: string = "";
    ForgetTextCSS: string = "";
    RegTextCSS: string = "";
    @Input("dataModel") modelInput;
    @Input("res") RES:Object;

    @Output("ondata") dataEmitter = new EventEmitter();
    @Output("onlanguage") languageEmitter = new EventEmitter();

    languageArray = [];
    Language: string = "English";
    constructor(private _languageService: ResourceService) {
        this.Language = "English";
    }

    languageSelectionChange(evt) {
        //debugger;
        if (evt.code == "he") {
            this.RegTextCSS = "left-align";
            this.ForgetTextCSS = "right-align";
            this.ForgetIcon = "mdi-navigation-arrow-forward";
            this.RegIcon = "mdi-navigation-arrow-back";
        }
        else {
            this.ForgetTextCSS = "left-align";
            this.RegTextCSS = "right-align";
            this.ForgetIcon = "mdi-navigation-arrow-back";
            this.RegIcon = "mdi-navigation-arrow-forward";
        }
        this.languageEmitter.emit(evt);
    }
    checkLogin(keyCode) {
        //alert(keyCode);
        if (keyCode == 13) {
            this.Validate();
        }
    }
    Validate() {
        this.dataEmitter.emit(this.modelInput);
    }

    ngOnInit() {
        this.languageArray = this._languageService.GetAvailableLanguages();
        var lang = this._languageService.getCookie("lang");
        ///alert(lang);
        if (lang.length > 0)
            var lang = lang.substring(1, lang.length);
        if (lang == "he") {
            lang = "עִברִית";
            this.RegTextCSS = "left-align";
            this.ForgetTextCSS = "right-align";
            this.ForgetIcon = "mdi-navigation-arrow-forward";
            this.RegIcon = "mdi-navigation-arrow-back";
        }
        else {
            lang = "English";
            this.RegTextCSS = "right-align";
            this.ForgetTextCSS = "leftt-align";
            this.ForgetIcon = "mdi-navigation-arrow-back";
            this.RegIcon = "mdi-navigation-arrow-forward";
        }
        this.Language = lang;
    }
}