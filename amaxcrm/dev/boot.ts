import {bootstrap} from 'angular2/platform/browser';
import {AmaxAppComponent} from "./amaxApp.component";
import {ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy} from "angular2/router";
import {HTTP_PROVIDERS} from "angular2/http";
import {provide} from "angular2/core";


bootstrap(AmaxAppComponent,[ROUTER_PROVIDERS,HTTP_PROVIDERS, provide(LocationStrategy,{useClass:HashLocationStrategy})]);