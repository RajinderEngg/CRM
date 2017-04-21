import {Component, OnInit} from "angular2/core";
import {NgSwitch, NgSwitchWhen, NgSwitchDefault} from 'angular2/common'
import {ResourceService} from "../../services/ResourceService";
import {RouteParams} from "angular2/router";
import {AmaxService} from "../../services/AmaxService";
import { jsonQ } from '../../jsonQ';


declare var jQuery;

@Component({
    providers: [ResourceService,AmaxService],
    directives:[NgSwitch, NgSwitchWhen, NgSwitchDefault],
    template: `
        <form *ngIf="ShowForm">
            <div class="row">
                <div class="col-sm-12">
                    <div *ngFor="#group of FormObject" class="row">
                        <h1>{{group.name}}</h1>
                        <div *ngFor="#field of group.fields" class="col-xs-12 col-md-6 {{field.hiden ? 'hide' : ''}}">
                            <div class="row" style="padding-top: 15px;">
                                <div class="col-xs-12 col-sm-5 col-md-4"><label>{{field.caption||field.name}}</label></div>
                                <div class="col-xs-12 col-sm-7 col-md-8" [ngSwitch]="field.type">
                                    <template [ngSwitchWhen]="select">
                                        <select>
                                            <option>Option 1</option>
                                            <option>Option 2</option>
                                            <option>Option 3</option>
                                            <option>Option 4</option>
                                            <option>Option 5</option>
                                        </select>
                                    </template>
                                    <template ngSwitchDefault>
                                        <input id="{{group.tableName}}_{{field.name}}" class="form-control" name="{{field.name}}" value="{{field.default}}" type="text">
                                    </template>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12">
                    <button type="button" (click)="saveFormData()" class="btn btn-primary pull-right" style="margin-top: 20px;">Save</button>
                </div>
            </div>
        </form>
    `
})
export class AmaxForms implements OnInit {
    FormObject:Array<any>;
    ShowForm:boolean=false;

    constructor(private _resourceService:ResourceService, private _amaxService:AmaxService, private _routeParams:RouteParams) {
    }

    saveFormData(){
        var jsQ = new jsonQ("DemoTransation","DemoHashKey");

        for(var frm of this.FormObject){
            jsQ.addNewInsert(frm.tableName,frm.pKey,frm.pKeyName);
            for(var fld of frm.fields){
                if(!fld.dummy){
                    console.log(jQuery("form #"+frm.tableName+ "_" +fld.name).val());
                    console.log("form #"+frm.tableName+ "_" +fld.name);


                    jsQ.Insert(fld.name, jQuery("form #"+frm.tableName+"_"+fld.name).val());
                }
            }
            jsQ.addToList();
        }
        console.log( JSON.stringify(jsQ.toJsonQObject()));
        this._amaxService.ExecuteJson(jsQ.toJsonQObject()).subscribe(data=>{
            console.log(data);
            },
            error=>console.log(error),
            ()=>console.log("Call Compleated")
        );
    }

    ngOnInit() {
        var frm = this._routeParams.get('frm');
        console.log(frm);
        
        this._resourceService.GetFormByName(this._routeParams.get('frm')).subscribe(data=>{
            this.FormObject = data;
            this.ShowForm=true;
        },error=>{
            console.log(error);
        },()=>{
            console.log("CallCompleated")
        });
    }
}