import {Component} from "angular2/core";


@Component({
    name:"mx-treeview",
    template:`
        <h1>Hello World</h1>
        <ul id="mx-treeview">
            <li data-expanded="true">
                <span class="k-sprite folder"></span>
                My Web Site
                <ul>
                    <li data-expanded="true">
                        <span class="k-sprite folder"></span>images
                        <ul>
                            <li><span class="k-sprite image"></span>logo.png</li>
                            <li><span class="k-sprite image"></span>body-back.png</li>
                            <li><span class="k-sprite image"></span>my-photo.jpg</li>
                        </ul>
                    </li>
                    <li data-expanded="true">
                        <span class="k-sprite folder"></span>resources
                        <ul>
                            <li data-expanded="true">
                                <span class="k-sprite folder"></span>pdf
                                <ul>
                                    <li><span class="k-sprite pdf"></span>brochure.pdf</li>
                                    <li><span class="k-sprite pdf"></span>prices.pdf</li>
                                </ul>
                            </li>
                            <li><span class="k-sprite folder"></span>zip</li>
                        </ul>
                    </li>
                    <li><span class="k-sprite html"></span>about.html</li>
                    <li><span class="k-sprite html"></span>contacts.html</li>
                    <li><span class="k-sprite html"></span>index.html</li>
                    <li><span class="k-sprite html"></span>portfolio.html</li>
                </ul>
            </li>
        </ul>
    `
})
export class AmaxTreeView{
    constructor(){}
}