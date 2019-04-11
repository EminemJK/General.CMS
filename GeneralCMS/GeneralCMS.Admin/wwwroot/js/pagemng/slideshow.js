//预览图片
function previewimg(obj) {
    var gparent = $(obj).parent();
    var url = gparent.css("background-image").replace('url(', '').replace(')', '');
    $('#imgshowbox').css("background-image", "url(" + url + ")");
    var dialog = $('#previewimgDialog');
    dialog.modal();
}

//添加
function btn_add(navId) {
    $('#nav-id').val(navId);
    $('#img-id').val("0");
    $("#img-title").val("");
    $("#img-subhead").val("");
    $("#img-url").css("background-image", "url('/images/sys/nopic.png')");
    $("#img-linkurl").val(""); 
    $("#img-sort").val("0");

    var dialog = $('#imgplay-dialog');
    dialog.modal('toggle');
    dialog.find('.btn-primary').text('新增保存');
}

//修改
function btn_edit(obj, navId) {
   
    $('#nav-id').val(navId);

    var tds = $(obj).parent().siblings(); 
    $('#img-id').val($(tds[1]).eq(0).text());
    $("#img-title").val($(tds[2]).eq(0).text());
    $("#img-subhead").val($(tds[3]).eq(0).text());

    var bgTd = $(tds[4]).children().eq(0);
    var url = bgTd.css("background-image").replace('url(', '').replace(')', ''); 
    $("#img-url").css("background-image", "url(" + url + ")"); 
    $("#img-linkurl").val($(tds[5]).eq(0).text()); 
    $(".select2").val($(tds[6]).attr("value"));
    $("#img-sort").val($(tds[7]).eq(0).text());

    var dialog = $('#imgplay-dialog');
    dialog.modal('toggle');
    dialog.find('.btn-primary').text('新增保存');
}

//保存修改
function save() {
    var data = {};
    data.NavigationID = $('#nav-id').val(),
        data.Id = $("#img-id").val(),
        data.Title = $("#img-title").val(),
        data.Subhead = $("#img-subhead").val(),
        data.ImgUrl = $("#img-url").css("background-image").replace('url(', '').replace(')', '').replace('"', '').replace('\"', ''),
        data.LinkUrl = $("#img-linkurl").val(),
        data.Target = $(".select2").val(),
        data.Sort = $("#img-sort").val();
    if (data.ImgUrl.length <= 0 || data.ImgUrl.indexOf('nopic.png')>=0) {
        toastr.warning("请上传一张背景图片");
        return;
    } 
    $.ajax({
        url: '/PageMng/SlideshowSave',
        type: 'Post',
        data: data,
        dataType: "json",
        success: function (res) {
            if (res.code == '200') {
                toastr.success('操作成功'); 
                var dialog = $('#imgplay-dialog');
                dialog.modal('hide'); 
                window.location.reload();
            }
            else {
                toastr.error(res.msg);
            }
        },
        error: function () {
            toastr.error('服务器出错了');
        }
    });
}

//多选获取Ids
function multiSelectCheck(obj) {
    var trs = $(obj).parent().parent().find(".table-tr");
    var ids = []; 
    for (var idx = 0; idx < trs.length; idx++)
    {
        var tds = $(trs[idx]).children();
        var icheck = $(tds[0]).find("input[type='checkbox'].flat-green").is(':checked');
        if (icheck) {
            var aa = tds[1];
            var id = $(tds[1]).text();
            ids.push(id);
        }
    }
   
    return ids; 
}

//启用禁用
function slideshowSwitchOff(obj, status) {
    var ids = multiSelectCheck(obj);
    if (ids.length == 0) {
        toastr.warning("请勾选对应的Id");
        return;
    }
    var data = {};
    data.Ids = ids;
    data.Off = status;
    $.ajax({
        url: '/PageMng/SlideshowSwitchOff',
        type: 'Post',
        data: data,
        dataType: "json",
        success: function (res) {
            if (res.code == '200') {
                toastr.success('操作成功'); 
                window.location.reload();
            }
            else {
                toastr.error(res.msg);
            }
        },
        error: function () {
            toastr.error('服务器出错了');
        }
    });
}

  

//删除
function slideshowDelete(obj) {
    var ids = multiSelectCheck(obj);
    if (ids.length == 0) {
        toastr.warning("请勾选对应的Id");
        return;
    } 

    ShowBox.confirm({ message: "确认删除吗？" }).on(function (e) {
        if (!e) return;
        var data = {};
        data.Ids = ids;
        $.ajax({
            url: '/PageMng/SlideshowDelete',
            type: 'Post',
            data: data,
            dataType: "json",
            success: function (res) {
                if (res.code == '200') {
                    toastr.success('操作成功');
                    window.location.reload();
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

}

 