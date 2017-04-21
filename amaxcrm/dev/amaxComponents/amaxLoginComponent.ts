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
        debugger;
        this.languageEmitter.emit(evt);
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
        if (lang == "he")
            lang = "עִברִית";
        else {
            lang = "English";
        }
        this.Language = lang;
    }
}