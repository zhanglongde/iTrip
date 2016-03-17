/// <reference path="../../../Scripts/jquery-1.4.4.js" />

$(function () {
    $(".a_scenic_manage").css("background", "url(../../Content/themes/image/shouqie.png)  no-repeat scroll -1px -47px  transparent");
    $(".a_scenic_manage").css("display", "block");
    $(".a_scenic_manage").css("color", "#739242");
    getScenicInfo();
    GetScenicSpotInfo();
    setTimeout(function () {
        shangchuantupian();
        shangchuanyuyin();
    },0);
    enterTrue();
    xianshi();
    modify();
})

//获取景点信息
function GetScenicSpotInfo() {
    $("#scenicSpotName_list").more({
        'address': '/TrafficStatistics/GetScenicSpotInfo/'+getCookie("scenicZoneId"),
        'amount': '4',
        'template': '.ul_list',
        'trigger': '.get_more'   //触发加载更多记录的class属性
    });
}
function getScenicInfo() {
    if (getCookie("UserNameTemp")!= null) {
        $("#userName").text(getCookie("UserNameTemp"));
        $("#scenicName").text(getCookie("scenicZoneName"));
    }
}


function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

//上传图片
function shangchuantupian() {
    $("#uploadify").uploadify({
        'uploader': '../../Content/loadfile/uploadify.swf',
        'script': '/ScenicArea/Upload',
        'cancelImg': '../../Content/loadfile/cancel.png',
        'folder': 'UploadFile',
        'queueID': 'fileQueue',
        'buttonText': '选择图片',
        'auto': false,
        'multi': false,

        'onComplete': function (event, ID, fileObj, response, data) {
            alert("上传成功");
            var value = response;
            $(".tupian").text(response);
        }
    });
}

//上传语音
function shangchuanyuyin() {
    $("#uploadify1").uploadify({
        'uploader': '../../Content/loadfile/uploadify.swf',
        'script': '/ScenicArea/Upload',
        'cancelImg': '../../Content/loadfile/cancel.png',
        'folder': 'UploadFile',
        'queueID': 'fileQueue1',
        'hideButton ': true,
        'buttonText': '选择语音',
        'auto': false,
        'multi': false,
        'sizeLimit': 300 * 1000 * 1024,
        'onComplete': function (event, ID, fileObj, response, data) {
            var value = response;
            if ($(".biaoji").text() == '0') {
                $(".biaoji").text("1");
                if (!document.all) {
                    $('.tupian1').prepend(
               ' <object data="../../Content/dewplayer/dewplayer-vol.swf" width="250" height="65" name="dewplayer" id="dewplayer" type="application/x-shockwave-flash">' +
			'<param name="flashvars" value="mp3=' + response + '" />' +
			'<param name="wmode" value="transparent" />' +
			'</object>');
                }
                else {
                    $('.tupian1').prepend('<embed  class="embed_voice_2" src="../../Content/dewplayer/dewplayer-vol.swf" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" wmode="transparent"quality="high" width="250" height="65" flashvars="mp3=' + response + '"></embed>');
                }

            }
            else {
                $(".tupian1").empty();
                if (!document.all) {
                    $('.tupian1').prepend(
               ' <object data="../../Content/dewplayer/dewplayer-vol.swf" width="250" height="65" name="dewplayer" id="dewplayer" type="application/x-shockwave-flash">' +
			'<param name="flashvars" value="mp3=' + response + '" />' +
			'<param name="wmode" value="transparent" />' +
			'</object>');
                }
                else {
                    $('.tupian1').prepend('<embed  class="embed_voice_2" src="../../Content/dewplayer/dewplayer-vol.swf" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" wmode="transparent"quality="high" width="250" height="65" flashvars="mp3=' + response + '"></embed>');
                } 
            }
            $(".yuyin").text(response);
        }
    });
}
//确定
function enterTrue() {
    $('#btn3').click(function () {
        var spotName = $("#area-name").val();
        var introduce = $("#notice").val();
        //        
        var picpath = $(".tupian").text();
        var soundpath = $(".yuyin").text();
        if ($("#area-name").val() == "" || $("#notice").val() == "") {
            $("#tupian").text("");
            alert("哎~哥们来点字！！");

        }
        else {
            $.post("/ScenicArea/ScenicIntroduction", { scenciZoneId: getCookie("scenicZoneId"), spotName: spotName, introduce: introduce, picpath: picpath, soundpath: soundpath }, function (data) {
                var shownow1 = data;
                $(".no_scenicInfo").hide();
                AddToPageDiv(shownow1, spotName, introduce, picpath, soundpath);
                $("#area-name").val("");
                $("#notice").val("");
                $("#tupian").text("")

            });
        }

    });

}
//显示到界面
function xianshi() {
    $.post("/ScenicArea/Scenicshow", { scenicareaid: getCookie("scenicZoneId") }, function (shownow, status) {
        if (status == "success") {
            if (shownow.length > 0) {
                $(".intro").empty();
                for (var i = 0; i < shownow.length; i++) {
                    var shownow1 = shownow[shownow.length - i - 1].ScenicSpotID;
                    var shownow2 = shownow[shownow.length - i - 1].ScenicSpotName;
                    var shownow3 = shownow[shownow.length - i - 1].ScenicSpotIntroduce;
                    var shownow4 = shownow[shownow.length - i - 1].PicturePath;
                    var shownow5 = shownow[shownow.length - i - 1].StorePath;
                    AddToPageDiv(shownow1, shownow2, shownow3, shownow4, shownow5);
                }
            }
            else {
                $(".intro").prepend(
                 '<span class="no_scenicInfo">没有景点信息</span>'
                );
            }
        }
        else {
            alert("获取未关注用户失败");
        }
    }, "json");
}
//动态加载
function AddToPageDiv(shownow1, shownow2, shownow3, shownow4, shownow5) {
    $(".intro").prepend(
      '<div id="info' + shownow1 + '">' +
    '<div class="intro-1">' +
                    '<div class="intro-img fl">' +
                      '  <img src="' + shownow4 + '" height="100px" />' +
                    '</div>' +
                   ' <div class="intro-w">' +
                       '<div id="music' + shownow1 + '">' +
                 '<embed class="embed_voice" src="../../Content/dewplayer/dewplayer-vol.swf" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" wmode="transparent"quality="high" width="250" height="45" flashvars="mp3=' + shownow5 + '"></embed>' +
                 '<div>' +
                   '<h1 id="IntroName' + shownow1 + '" class="intro-name">' +
                          shownow2 +
                        '</h1>' +
                        '<p id="content' + shownow1 + '" class="intro-content">' +
                            shownow3 +
                       ' </p>' +
                 '   </div>' +
                   ' <div class="intro-btn">' +
                      '  <a id="' + shownow1 + '">' +
                      '修 改' +
                      '</a>' +
                      ' <a id="shanchu' + shownow1 + '">' +
                      '删 除' +
                      '</a>' +
                    '</div>' +
               ' </div>' +
               '</div>'
    );
    $("#" + shownow1).click(function () {
        var scenicspotid = $("#" + shownow1).attr("id");
        $('#btn3').css("display", "none");
        $('#btn4').css("display", "block");
        $.post("/ScenicArea/modifyScenic", { scenicspotid: scenicspotid }, function (modifyresult, status) {
            if (status == "success") {
                for (var i = 0; i < modifyresult.length; i++) {
                    var modifyresult1 = modifyresult[i].ScenicSpotName;
                    var modifyresult2 = modifyresult[i].ScenicSpotIntroduce;
                    var modifyresult3 = modifyresult[i].PicturePath;
                    var modifyresult4 = modifyresult[i].StorePath;
                    //                    var modifyresult3 = modifyresult[i].Picture;
                    $("#jizhuid").val(scenicspotid);
                    $("#area-name").val(modifyresult1);
                    $("#notice").val(modifyresult2);
                    $(".tupian").text(modifyresult3);
                    $(".yuyin").text(modifyresult4);
                    //                    $("#tupian").text(modifyresult3);
                }
            }
            else {

            }

        }, "json");
    });
    $("#" + 'shanchu' + shownow1).click(function () {
        if (confirm("确定删除景点信息？")) {
            var scenicspotid = $("#" + 'shanchu' + shownow1).prev().attr("id");
            $.post("/ScenicArea/Scenicdelete", { scenicspotid: scenicspotid }, function (data) {
                if (data == "true") {
                    alert("删除成功");
                    $("#" + 'info' + shownow1).remove();
                    $("#" + 'xianshi' + shownow1).text("");

                }
            });
        }
    });


}
//修改
function modify() {
    $('#btn4').click(function () {
        var scenicspotid = $("#jizhuid").val();
        var spotName = $("#area-name").val();
        var introduce = $("#notice").val();
        var picpath = $(".tupian").text();
        var sound = $(".yuyin").text();
        $.post("/ScenicArea/modifyinformation", { scenicspotid: scenicspotid, spotName: spotName, introduce: introduce, picpath: picpath, sound: sound }, function (data) {
            if (data == "true") {
                alert("修改成功");
                $('#btn4').css("display", "none");
                $('#btn3').css("display", "block");
                $("#area-name").val("");
                $("#notice").val("");
                $("#tupian").text("");
                $(".tupian1").empty();
            }
        });


    });

}