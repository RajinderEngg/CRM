import {Component} from "angular2/core";
import {onEvent, crmEvent} from "../amaxUtil";


@Component({
    selector:'mx-breadcrumb',
    templateUrl:'./app/amaxComponents/templates/amaxCrmBreadcrumb.html',
})
export class AmaxCrmBreadcrumbComponent{
    breadcrumbs:Array<any>;
    constructor(){
        this.breadcrumbs=[
            {
                icon:'fa-home',
                name:'Home'
            },
            {
                icon:'fa-rocket',
                name:'Office'
            }
        ];
        
        onEvent.listenEvent(crmEvent.breadcrumbChange,this.updateBreadCrum)

    }
    updateBreadCrum(evt){
        this.breadcrumbs = evt.evtData;
    }
}