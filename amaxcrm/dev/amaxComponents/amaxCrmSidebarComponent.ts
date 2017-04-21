import {Component, OnInit} from 'angular2/core';
import {ROUTER_DIRECTIVES} from "angular2/router";
import {ResourceService, MenueItem} from "../services/ResourceService";

declare var AMAX_CRM;

@Component({
    selector: 'mx-sidebar',
    templateUrl:'./app/amaxComponents/templates/amaxCrmSidebar.html',
    directives:[ROUTER_DIRECTIVES],
    providers:[ResourceService]
})
export class AmaxCrmSidebarComponent implements OnInit{
    SideNav: Promise<any>;
    UserName: string = "";
    constructor(private _resourceService:ResourceService){}
    LogOut() {
        var empid = localStorage.getItem("employeeid");
        localStorage.clear();
        sessionStorage.clear();
        //debugger;
        //var data = this._resourceService.getCookie("
        //    this._resourceService.setCookie("UserDet", data, 10);

        //var data = this._resourceService.getCookie("RememberKey");  
        //this._resourceService.setCookie("UserDet", data, 10); 
        this._resourceService.deleteCookie("RememberKey");
        this._resourceService.deleteCookie(empid + "cust");
        this._resourceService.deleteCookie(empid + "emp");
        this._resourceService.deleteCookie(empid + "src");
        this._resourceService.deleteCookie(empid + "ccode");
        this._resourceService.deleteCookie(empid + "SMSDet");
        this._resourceService.deleteCookie(empid + "SMSMessage");
        window.location.href = "/";
    }
    ngOnInit() {


        
        AMAX_CRM.enableSidebar();

        var Uname = this._resourceService.getCookie("UserName");
        if (Uname.length > 0 && Uname[0] == "=") {
            Uname = Uname.substring(1, Uname.length);
        }
        this.UserName = Uname;
        this.SideNav = this.SideNav = this._resourceService.GetMenues();


        //Main Left Sidebar Menu
        $('.sidebar-collapse').sideNav({
            edge: 'left', // Choose the horizontal origin    
        });

        // FULL SCREEN MENU (Layout 02)
        $('.menu-sidebar-collapse').sideNav({
            menuWidth: 240,
            edge: 'left', // Choose the horizontal origin     
            //closeOnClick:true, // Set if default menu open is true
            menuOut: false // Set if default menu open is true
        
        });

        // HORIZONTAL MENU (Layout 03)
        $('.dropdown-menu').dropdown({
            inDuration: 300,
            outDuration: 225,
            constrain_width: false, // Does not change width of dropdown to that of the activator
            hover: true, // Activate on hover
            gutter: 0, // Spacing from edge
            belowOrigin: true // Displays dropdown below the button
        });


        // Perfect Scrollbar
        $('select').not('.disabled').material_select();
        var leftnav = $(".page-topbar").height();
        var leftnavHeight = window.innerHeight - leftnav;
        $('.leftside-navigation').height(leftnavHeight).perfectScrollbar({
            suppressScrollX: true
        });
        var righttnav = $("#chat-out").height();
        $('.rightside-navigation').height(righttnav).perfectScrollbar({
            suppressScrollX: true
        }); 


    }
}