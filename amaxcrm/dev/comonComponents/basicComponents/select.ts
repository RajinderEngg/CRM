import {Component, Input, Output, EventEmitter, OnInit} from 'angular2/core';

@Component({
    selector: 'mx-select',
    template: `
        <select #opt (change)="changeSelection(opt)" [(ngModel)]="default" class="{{cssclass}}">
            <option value="{{firstvalue}}">{{firstvalue}}</option>
            <option *ngFor="#item of data" value="{{lblValue(item)}}">{{lblValue(item)}}</option>
        </select>
    `
})
export class SelectInputComponent implements OnInit {
    @Input("data") data: [Object];
    @Input("label") label: string;
    @Input("selectedval") selectedval: string;
    @Input("firstvalue") firstvalue: string;
    @Input("cssclass") cssclass: string;
    //@Input("selectedval") selectedval: string;

    default: string;
    lblValue(item): string {
        return item[this.label];
    }


    @Output("onData") onData = new EventEmitter<Object>();

    changeSelection(selectedOption): boolean {
        //alert(selectedOption.value);
        // debugger;
        localStorage.setItem('lang', selectedOption.value)
        for (var index = 0; index < this.data.length; index++) {
            if (this.data[index][this.label] == selectedOption.value) {
                this.onData.emit(this.data[index]);
                return true;
            }
        }
        this.onData.emit({});
        return false;
    }

    ngOnInit(): any {
        if (this.selectedval != "" && this.selectedval != undefined && this.selectedval != null)
            this.default = this.selectedval;
       

    }
}