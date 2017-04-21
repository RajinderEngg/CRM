import {Component, OnInit} from "angular2/core";
import {Observable} from "rxjs/Observable";
import {AmaxService} from "../../services/AmaxService";
import {RouteParams, ROUTER_DIRECTIVES} from "angular2/router";

declare var jQuery;

@Component({
    template: `
        <h1>{{ReportName}}</h1>
        
        <div id="formReport">
        </div>
    `,
    directives:[ROUTER_DIRECTIVES],
    providers: [AmaxService]
})
export class AmaxReport implements OnInit {
    ReportData:Observable;
    ReportName:string;

    constructor(private _amaxService:AmaxService, private _routeParams:RouteParams) {
    }
    redirectTo(location:string){
        alert(location);
    }
    ngOnInit() {
        var rpt =this._routeParams.get('rpt');
        if (rpt) {
            switch(rpt){
                case "Accounts":
                    this.ReportName = "Accounts";
                    this.ReportData = this._amaxService.GetReport(rpt, {});

                    this.ReportData.subscribe(data=> {
                        jQuery("#formReport").kendoGrid({
                            dataSource: {
                                data: data[0]
                            },
                            height:350,
                            columns:[
                                {
                                    template: "<div (click)='redirectTo(\"form?frm=Accounts\")'>#: AccountName #</div></a>",
                                    field: "AccountName",
                                    title: "Account Name"
                                },
                                {
                                    template: "<div>#: AccountTypeName # ( #: AccountTpeNameEng # )</div>",
                                    field: "AccountTypeName",
                                    title: "Account Type"
                                },
                                {
                                    template:"<div>#: AccountDetail #</div>",
                                    field:"AccountDetail",
                                    title:"Account Detail"
                                }
                            ]
                        });
                    });
                    break;
                case "AccountType":
                    this.ReportName = "Accounts Types";
                    this.ReportData = this._amaxService.GetReport(rpt, {})

                    this.ReportData.subscribe(data=> {
                        console.log("1. Hello World");
                        console.log("2. Hello World");
                        console.log("3. Hello World");
                        console.log(data);

                        jQuery("#formReport").kendoGrid({
                            dataSource: {
                                data: data[0]
                            },
                            height:350,
                            selectable:"multiple",
                            columns:[
                                {
                                    template: "<div href=''>#: AccountTypeName # ( #: AccountTpeNameEng # )</div>",
                                    field: "AccountTypeName",
                                    title: "Account Type"
                                }
                            ]
                        });
                    });
                    break;
            }
        }
    }
}
