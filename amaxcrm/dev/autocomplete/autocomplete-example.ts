/**
 * Created by Tareq Boulakjar. from angulartypescript.com
 */

import {Component, ElementRef, ViewEncapsulation} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {AutocompleteContainer} from './autocomplete-container';
import {Autocomplete} from './autocomplete.component';
export const AUTOCOMPLETE_DIRECTIVES = [Autocomplete, AutocompleteContainer];


/*Angular 2 Autocomplete Example*/
@Component({
    selector: 'my-app',

    template:`
                <div class='container-fluid'>
                    <h3>Angular 2 Autocomplete Example</h3>
                    <h4>The Selected Car: {{selectedCar}}</h4>
                    <input [(ngModel)]="selectedCar"
                           [autocomplete]="carsExample2"
                           (autocompleteOnSelect)="autocompleteOnSelect($event)"
                           [autocompleteOptionField]="'name'"
                           class="form-control">

                    <h3>Asynchronous results</h3>
                    <h4>Model: {{asyncSelectedCar}}</h4>
                    <input [(ngModel)]="asyncSelectedCar"
                           [autocomplete]="getAsyncData(getCurrentContext())"
                           (autocompleteLoading)="changeAutocompleteLoading($event)"
                           (autocompleteNoResults)="changeAutocompleteNoResults($event)"
                           (autocompleteOnSelect)="autocompleteOnSelect($event)"
                           [autocompleteOptionsLimit]="7"
                           placeholder="Locations loaded with timeout"
                           class="form-control">
                    <div [hidden]="autocompleteLoading!==true">
                        <i class="glyphicon glyphicon-refresh ng-hide" style=""></i>
                    </div>
                    <div [hidden]="autocompleteNoResults!==true" class="" style="">
                        <i class="glyphicon glyphicon-remove"></i> Empty Query !
                    </div>
                </div>
               `,
    directives: [AUTOCOMPLETE_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES],
})
export class Angular2Autocomplete {
    private selectedCar:string = '';
    private asyncSelectedCar:string = '';
    private autocompleteLoading:boolean = false;
    private autocompleteNoResults:boolean = false;
    private carsExample1:Array<string> = ['BMW', 'Audi','Mercedes','Porsche','Volkswagen','Opel','Maserati','Volkswagen','BMW Serie 1','BMW Serie 2'];
    private carsExample2:Array<any> = [
        {id: 1, name: 'BMW'},
        {id: 2, name: 'Audi'},
        {id: 3, name: 'Mercedes'},
        {id: 4, name: 'Porsche'},
        {id: 5, name: 'Volkswagen'},
        {id: 6, name: 'Opel'},
        {id: 7, name: 'Maserati'},
        {id: 8, name: 'Volkswagen'},
        {id: 9, name: 'BMW Serie 1'},
        {id: 10, name: 'BMW Serie 2'},
    ];


    private getCurrentContext() {
        return this;
    }

    private _cachedResult:any;
    private _previousContext:any;

    private getAsyncData(context:any):Function {
        if (this._previousContext === context) {
            return this._cachedResult;
        }

        this._previousContext = context;
        let f:Function = function ():Promise<string[]> {
            let p:Promise<string[]> = new Promise((resolve:Function) => {
                setTimeout(() => {
                    let query = new RegExp(context.asyncSelectedCar, 'ig');
                    return resolve(context.carsExample1.filter((state:any) => {
                        return query.test(state);
                    }));
                }, 500);
            });
            return p;
        };
        this._cachedResult = f;
        return this._cachedResult;
    }

    private changeAutocompleteLoading(e:boolean) {
        this.autocompleteLoading = e;
    }

    private changeAutocompleteNoResults(e:boolean) {
        this.autocompleteNoResults = e;
    }

    private autocompleteOnSelect(e:any) {
        console.log(`Selected value: ${e.item}`);
    }
}