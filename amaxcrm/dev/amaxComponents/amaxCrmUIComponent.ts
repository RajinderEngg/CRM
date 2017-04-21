import { Component, OnInit } from 'angular2/core';
import {AmaxCrmNavbarComponent} from "./amaxCrmNavbarComponent";
import {AmaxCrmSidebarComponent} from "./amaxCrmSidebarComponent";
import {AmaxCrmBreadcrumbComponent} from "./amaxCrmBreadcrumbComponent";
import {ROUTER_DIRECTIVES} from "angular2/router";
declare var jQuery;
@Component({
    selector: 'mx-ui',
    templateUrl : './app/amaxComponents/templates/amaxCrmUI.html',
    directives:[ROUTER_DIRECTIVES, AmaxCrmNavbarComponent, AmaxCrmSidebarComponent, AmaxCrmBreadcrumbComponent]
})
export class AmaxCrmUIComponent implements OnInit{
    ngOnInit() {

        ////var windWidth = jQuery(window).width();
        ////if (windWidth > 250) {
        //    var brwidth = screen.width;
        //    var sideWidth = jQuery("#slide-out").width();
        //    jQuery(".breadcrumbs").css("width", brwidth - sideWidth );
        ////}
    }
}



    // template : `
    //     <mx-navbar></mx-navbar>
    //     <div class="main-container" id="main-container">
    //         <mx-sidebar></mx-sidebar>
    //     </div><!-- /.main-container -->
    // `,
    //directives: [AmaxCrmNavbarComponent, AmaxCrmSidebarComponent]