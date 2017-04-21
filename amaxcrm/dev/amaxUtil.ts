import {Pipe} from "angular2/core";

let onEvent = {
    riceEvent: (evtName:string, eventData:any)=>{
        var evt = new Event(evtName);
        evt["evtData"] = eventData;
        document.dispatchEvent(evt);
        console.log(evtName+ " event rice");
    },

    listenEvent:(eventName:string, callback:any)=>{
        console.log(eventName+" is binded");
        document.addEventListener(eventName,callback);
    },

    removeEvtListner:(eventName:string, callback:any)=>{
        console.log(eventName+" is unbinde");
        document.removeEventListener(eventName,callback);
    }
};

let crmEvent={
    breadcrumbChange:"crm.breadcrumb.change"
}

let Kendo_utility = {
    checkingNodeIds: function (nodes, checkedNodes) {
        
        var checkinnodes = checkedNodes.split(';');
        for (var i = 0; i < nodes.length; i++) {
            for (var j = 0; j < checkinnodes.length; j++) {
                if (nodes[i].id == checkinnodes[j]) {
                    nodes[i].set("checked", true);
                }
            }
            if (nodes[i].hasChildren) {

                Kendo_utility.checkingNodeIds(nodes[i].children.view(), checkedNodes);
            }
        }
    },
    checkedNodeIds: function (nodes, checkedNodes) {
        
        
        for (var i = 0; i < nodes.length; i++) {
            //console.log(nodes[i]);
            if (nodes[i].checked) {
                checkedNodes.push(nodes[i].id);
            }

            if (nodes[i].hasChildren) {
                Kendo_utility.checkedNodeIds(nodes[i].children.view(), checkedNodes);
            }
        }
    },
    checkedNodetexts: function (nodes, checkedNodes) {


        for (var i = 0; i < nodes.length; i++) {
            console.log(nodes[i].text);
            if (nodes[i].checked) {
                checkedNodes.push(nodes[i].text);
            }

            if (nodes[i].hasChildren) {
                Kendo_utility.checkedNodeIds(nodes[i].children.view(), checkedNodes);
            }
        }
    },
    getTree: function (inptData, groupId) {
        var x = inptData.filter(function (e) {
            return e.GroupParenCategory == groupId && e.GroupId != 0;
        });
        var outputData = [];
        for (var i = 0; i < x.length; i++) {
            outputData.push({
                id: x[i].GroupId,
                text: x[i].GroupName,
                expanded: false,
                items: Kendo_utility.getTree(inptData, x[i].GroupId)
            });
        }
        return outputData;
    }
}
//pipes start
@Pipe({
    name:'filterbygroup'
})
class GroupFilterPipe{
    transform(value,[groupId]){
        return value.filter((o)=>{
            return o.GroupId == groupId;
        });
    }
}
@Pipe({
    name:'filterbygroupparent'
})
class GroupParenFilterPipe{
    transform(value,[parentgroupid]){
        return value.filter((o)=>{
            return o.GroupParenCategory == parentgroupid;
        });
    }
}
//pipes end


export {
    onEvent, crmEvent, GroupFilterPipe,GroupParenFilterPipe,Kendo_utility
}