import {Component, OnDestroy} from "angular2/core";
import {onEvent} from "../../amaxUtil";
@Component({
    template:`
        <h1>Default Component</h1>
        
    `
})
export class defaultComponent implements OnDestroy{
    constructor(){
        //document.addEventListener('myCustomeEvt',this.onMyCustomeEvt);
        onEvent.listenEvent('myCustomeEvt',this.onMyCustomeEvt);
    }
    onMyCustomeEvt(a:any){
        console.log(arguments.length);
        console.log(a.evtData);
    }

    ngOnDestroy() {
        //document.removeEventListener('myCustomeEvt',this.onMyCustomeEvt);
        onEvent.removeEvtListner('myCustomeEvt',this.onMyCustomeEvt);
    }
}