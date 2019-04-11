var table = $('#userListTable').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax": function (data, callback, settings) {

        //data的参数请参考： https://segmentfault.com/a/1190000004478726
        var param = {};
        param.draw = data.draw;
        param.pageNum = (data.start / data.length) + 1;
        param.pageSize = data.length;

        param.sex = $('#select-sex option:selected').val();
        param.username = $("#input-username").val();
        param.realname = $("#input-realname").val();
        param.phone = $('#input-phone').val();
        param.email = $('#input-email').val();
        $.ajax({
            type: "GET",
            data: param,
            url: "/SystemMng/QueryUserList",
            dataType: "json",
            success: function (data) {
                //成功后回调自动渲染
                callback(data);
            }
        });
    },
    'columns': [
        { 'data': 'id' },
        { 'data': 'userName' },
        { 'data': 'realName' }, 
        { 'data': 'mobile' },
        { 'data': 'email' },
        { 'data': 'createTimeString' }, 
        {
            'data': 'isDisable',
            'render': function (data, type, row) {
                if (row.isDisable == 0)
                    return '<span style="color:#19be6b" >' + row.isDisableString+ '</span>';
                else
                    return '<span style="color:#ed3f14" >' + row.isDisableString + '</span>';
            }
        },
        {
            'data': null,
            'render': function (data, type, row) {
                return '<a class="btn btn-info btn-sm"  title="编辑" onClick=btn_edit(' + row.id + ')><i class="fa fa-edit"></i>编辑</a>     ' +
                    '<a class="btn btn-danger btn-sm"  title="删除" onClick=btn_delete(' + row.id + ')><i class="fa fa-trash "  title="删除" style="cursor:pointer"></i>删除</a>';
            }
        },
    ],
    //datatable设置参数 http://www.datatables.club/reference/option/
    'paging': true,         //启用分页
    'lengthChange': true,   //设置每页数量
    'searching': false,
    'ordering': false,
    'info': true,
    'autoWidth': false,
    //设置中文
    'language': {
        "sProcessing": "玩命加载中...",
        "sLengthMenu": "每页显示显示 _MENU_",
        "sZeroRecords": "没有匹配结果",
        "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
        "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
        "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
        "sInfoPostFix": "",
        "sSearch": "搜索:",
        "sUrl": "",
        "sEmptyTable": "表中数据为空",
        "sLoadingRecords": "玩命加载中...",
        "sInfoThousands": ",",
        "oPaginate": {
            "sFirst": "首页",
            "sPrevious": "上页",
            "sNext": "下页",
            "sLast": "末页"
        },
        "oAria": {
            "sSortAscending": ": 以升序排列此列",
            "sSortDescending": ": 以降序排列此列"
        }
    }
});


//给行绑定选中事件
$('#userListTable tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        table.$('tr.selected').removeClass('selected');//多选则去掉
        $(this).addClass('selected');
    }
});

//搜索
$("#btn-search").on('click', function () {
    table.draw();
});

//编辑
function btn_edit(uid) {
    $.ajax({
        url: '/SystemMng/QueryUserDetail?userid=' + uid,
        type: 'get',  
        success: function (res) {
            if (res.code == '200') {
                var user = res.data;
                $('#user-id').val(user.id);
                $("#user-username").val(user.userName);
                $("#user-name").val(user.realName);
                $("#user-phone").val(user.mobile);
                $("#user-email").val(user.email);

                var dialog = $('#userinfo-dialog');
                dialog.modal('toggle');
                dialog.find('.btn-primary').text('保存修改');
            }
            else {
                toastr.error(res.msg);
            }
        },
        error: function () {
            toastr.error('服务器出错了');
        }
    });
};
 
//新增
$("#btn_add").on('click', function () { 
    $('#user-id').val("0");
    $("#user-username").val("");
    $("#user-password").val("");
    $("#user-password2").val("");
    $("#user-name").val("");
    $("#user-phone").val("");
    $("#user-email").val("");

    var dialog = $('#userinfo-dialog');
    dialog.modal('toggle'); 
    dialog.find('.btn-primary').text('新增保存');
});

//删除
function btn_delete(uid) {
    ShowBox.confirm({ message:"确认删除吗？"}).on(function (e) {
        if (!e) return;
        $.ajax({
            url: '/SystemMng/DeleteUser?userId='+uid,
            type: 'Post',
            success: function (res) {
                if (res.code == '200') {
                    toastr.success('删除成功'); 
                    table.draw();
                }
                else {
                    toastr.error(res.msg);
                }
            },
            error: function () {
                toastr.error('服务器出错了');
            }
        }); 
    });
};

//保存修改
function save() {
    var data = {};
    data.id = $('#user-id').val();
    data.userName = $("#user-username").val();
    data.password = $("#user-password").val();
    data.password2 = $("#user-password2").val();

    data.realName = $("#user-name").val();
    data.mobile = $("#user-phone").val();
    data.email = $("#user-email").val();
    if (data.userName.length == 0) {
        toastr.warning('请填写登录系统的用户名');
        return;
    } 
    if (parseInt(data.id)==0) {
        if (data.password.length == 0) {
            toastr.warning('请填写登录系统的密码');
            return;
        }
        if (data.password2 != data.password) {
            toastr.warning('二次确认密码与密码不一致');
            return;
        } 
    }
    if (data.realName.length == 0) {
        toastr.warning('请填写用户姓名');
        return;
    }
    if (data.mobile.length == 0) {
        toastr.warning('请填写手机号码');
        return;
    }
    $.ajax({
        url: '/SystemMng/SaveUser',
        type: 'Post',
        data: data,
        dataType: "json", 
        success: function (res) {
            if (res.code == '200') {
                toastr.success('操作成功');

                var dialog = $('#userinfo-dialog');
                dialog.modal('hide'); 
                table.draw();
            }
            else {
                toastr.error(res.msg);
            }
        },
        error: function () {
            toastr.error('服务器出错了');
        }
    });
};