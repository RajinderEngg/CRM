var AMAX_CRM = {

    enableSidebar : function () {
        //initiate sidebar function
        var $sidebar = $('.sidebar');
        if ($.fn.ace_sidebar) $sidebar.ace_sidebar();
        if ($.fn.ace_sidebar_scroll) $sidebar.ace_sidebar_scroll({
            //for other options please see documentation
            'include_toggle': false || ace.vars['safari'] || ace.vars['ios_safari'] //true = include toggle button in the scrollbars
        });
        if ($.fn.ace_sidebar_hover)    $sidebar.ace_sidebar_hover({
            'sub_hover_delay': 750,
            'sub_scroll_style': 'no-track scroll-thin scroll-margin scroll-visible'
        });
    },

    checkedNodeIds:function(nodes, checkedNodes) {
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].checked) {
                checkedNodes.push(nodes[i].id);
            }

            if (nodes[i].hasChildren) {
                checkedNodeIds(nodes[i].children.view(), checkedNodes);
            }
        }
    }
}