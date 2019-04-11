$(function () {
    $.ajax({
        url: "/PageMng/GetNavigationTree",
        dataType: "json",
        type: "get",
        success: function (res) {
            if (res.code == "200") {
                var str = "";
                for (var idx = 0; idx < res.data.items.length; idx++) {
                    var node = res.data.items[idx];
                    var handle = idx == 0 ? "dd-uncheck" : "dd-handle"; 

                    str += " <li class='dd-item' data-id='" + node.navId + "'>"
                        + "<div class='" + handle + " dd-collapsed'>" + node.text
                        + "<div class='pull-right action-buttons'>"
                        + "<a class='text-primary' title='添加子导航' href='javascript:void(0)' onclick='navigation_Add(\"" + node.text + "\"," + node.navId +")'> <i class='fa fa-plus-circle fa-lg'></i></a>"
                        + "<a class='text-green' title='编辑' href='javascript:void(0)' onclick='navigation_Edit(\"" + node.text + "\"," + node.navId +")'> <i class='fa fa-edit fa-lg'></i></a>";
                    if (handle == "dd-handle") {
                        str += "<a class='text-red' title='删除' href='javascript:void(0)' onclick='navigation_Delete(\"" + node.text + "\"," + node.navId +")'><i class='fa fa-trash fa-lg'></i></a>";
                    }
                    str += "</div></div>"; 

                    if (node.nodes.length > 0) {
                        str += "<ol class='dd-list'>";
                        for (var i = 0; i < node.nodes.length; i++) {
                            var child = node.nodes[i];
                            str += " <li class='dd-item' data-id='" + child.navId + "'>"
                                + "<div class='dd-handle dd-collapsed'> " + child.text
                                + "<div class='pull-right action-buttons'>"
                                + "<a class='text-green' title='编辑' href='javascript:void(0)' onclick='navigation_Edit(\"" + child.text + "\"," + child.navId + ")'> <i class='fa fa-edit fa-lg'></i></a>"
                                + "<a class='text-red' title='删除' href='javascript:void(0)' onclick='navigation_Delete(\"" + child.text + "\"," + child.navId +")'><i class='fa fa-trash fa-lg'></i></a></div>"
                                + "</div>"
                                + "</li>";
                        }
                        str += "</ol>";
                    }
                    str += "</li>";
                }
                $("#nestable-ol").append(str); 
            }
            else {
                toastr.error(res.msg);
            }
            //点击标签旁边的按钮不触发移动
            $('.dd-handle a').on('mousedown', function (e) {
                e.stopPropagation();
            });
        },
        error: function () {

        }
    });
     
   
})

function navigation_Add(parentName, parentId) {
    $("#btn-save").text("保存新增");
    $('#nav-parentid').val(parentId);
    $('#nav-id').val("0");
    $('#nav-name').val("");
    $('#nav-url').val("");
    $('#nav-target').val("1");
    $('#nav-disable').val("0");

    var dialog = $('#navigation-dialog');
    dialog.modal('toggle');
    dialog.find('.box-title').text("当前导航：" + parentName);
}

function navigation_Edit(navName, navid) { 
    $("#btn-save").text("保存修改");
    $('#nav-parentid').val("0");
    
    $.ajax({
        url: "/PageMng/GetNavigationDetail?id=" + navid,
        dataType: "json",
        type: "get",
        success: function (res) {
            if (res.code == "200") {
                var data = res.data;
                $('#nav-id').val(data.id);
                $('#nav-name').val(data.name);
                $('#nav-url').val(data.url);
                $('#nav-target').val(data.target);
                $('#nav-disable').val(data.isDisable);
                var dialog = $('#navigation-dialog');
                dialog.modal('toggle');
                dialog.find('.box-title').text("当前导航：" + navName);
            }
            else {
                toastr.error(res.msg);
            }
        },
        error: function () {

        }
    });

}


function navigation_Delete(navName, navid) {
    ShowBox.confirm({ message: "如果当前删除的导航有子导航，那将会连同子导航一起删除，确认删除【" + navName + "】吗？" }).on(function (e) {
        toastr.success("删除成功：" + navid);
    });
}

function save() {
    var navId = $('#nav-id').val();
    var parentid = $('#nav-parentid').val();
    var name = $('#nav-name').val();
    if (name.length == 0 || name == " ") {
        toastr.warning("请填写名称");
        return;
    }
    var param = {};
    param.navigationId = navId;
    param.parentNavigationId = parentid;
    param.name = name;
    $.ajax({
        url: "/PageMng/EditNavigation",
        dataType: "json",
        type: "post",
        data: param,
        success: function (res) {
            if (res.code == "200") {
                toastr.success(res.msg);
            }
            else {
                toastr.error(res.msg);
            }
        },
        error: function () {

        }
    });
}
 