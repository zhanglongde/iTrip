/// <reference path="../../../Scripts/jquery-1.4.4.js" />
/// <reference path="../../loadfile/swfobject.js" />
/// <reference path="../../loadfile/jquery.uploadify.v2.1.4.min.js" />

$(function () {
    GetUserInfo();
    GetScenicZoneInfo();
    ChangeUserScenicInfo();
    uploadheadimg();
    uploadscenicimg();
})

//获取用户信息
function GetUserInfo() {
    if (getCookie("UserNameTemp") != null) {
        $.post("/ModifyInformation/GetUserInfo", { "userName": getCookie("UserNameTemp") }, function (userInfo) {
            $("#userName").val(userInfo[0].UserName);
            $("#password").val(userInfo[0].Password);
            $("#confirmPwd").val(userInfo[0].Password);
            $("#email").val(userInfo[0].Email);
            $("#phone").val(userInfo[0].Mobilephone);
            if (userInfo[0].HeadAdr != "") {
                $("#headimg").attr("src", userInfo[0].HeadAdr);
            }
        }, "json");
    }
}
//获取景区信息
function GetScenicZoneInfo() {
    if (getCookie("UserNameTemp") != null) {
        $.post("/ModifyInformation/GetScenicZoneInfo", { "userName": getCookie("UserNameTemp") }, function (scenicZoneInfo) {
            $("#scenicZoneName").val(scenicZoneInfo[0].ScenicZoneName);
            $("#scenicZoneAdr").val(scenicZoneInfo[0].ScenicZoneAdr);
            $("#scenciIntroduction").val(scenicZoneInfo[0].ScenicZoneIntroduce);
            $("#traverNotice").val(scenicZoneInfo[0].ScenicNotic);
            $("#scenicPhone").val(scenicZoneInfo[0].Scenicphone);
            $("#openTime").val(scenicZoneInfo[0].ScenicTime);
            if (scenicZoneInfo[0].ScenicImg != "") {
                $("#scenicimg").attr("src", scenicZoneInfo[0].ScenicImg);
            }
        }, "json");
    }
}
function ChangeUserScenicInfo() {
    $("#submit_btn").click(function () {
        if (getCookie("UserNameTemp") != $("#userName").val()) {
            $.post("/Register/IfUserNameExist", { "userName": $("#userName").val()}, function (data) {
                if (data == "true") {
                    alert("用户名已存在");
                }
                else {
                    ChangeUserScenicInfo2();
                }
            });
        }
        else {
            ChangeUserScenicInfo2();
        }

    });
}
function ChangeUserScenicInfo2() { 
        var userNameOld = getCookie("UserNameTemp");
        var UserInfo = new Object();
        var ScenciInfo = new Object();
        UserInfo.UserName = $("#userName").val();
        UserInfo.Password = $("#password").val();
        UserInfo.Mobilephone = $("#phone").val();
        UserInfo.Email = $("#email").val();
        UserInfo.HeadAdr = $("#headimg").attr("src");
        ScenciInfo.ScenicZoneName = $("#scenicZoneName").val();
        ScenciInfo.ScenicZoneAdr = $("#scenicZoneAdr").val();
        ScenciInfo.ScenicZoneIntroduce = $("#scenciIntroduction").val();
        ScenciInfo.ScenicNotic = $("#traverNotice").val();
        ScenciInfo.Scenicphone = $("#scenicPhone").val();
        ScenciInfo.ScenicTime = $("#openTime").val();
        ScenciInfo.ScenicImg = $("#scenicimg").attr("src");

        $.post("/ModifyInformation/ChangeUserScenicInfo", { "userNameOld": userNameOld, "userInfo": objToJSONString(UserInfo), "scenicInfo": objToJSONString(ScenciInfo) }, function (data) {
            if (data == "true") {
                SetCookie("UserNameTemp", $("#userName").val());
                $("#succPrompt").show().fadeOut(3000);
            }
            else {
                alert("修改数据库失败");
            }
        });
}
function uploadheadimg() {
    $("#uploadheadimg").uploadify({
        'uploader': '../../Content/loadfile/uploadify.swf',
        'script': '/ScenicArea/Upload',
        'cancelImg': '../../Content/loadfile/cancel.png',
        'folder': 'UploadFile',
        'queueID': 'fileQueue',
        'buttonImg': '../../Content/loadfile/choscenicimg.png',
        'width': 117,
        'height': 40,
        'wmode': 'transparent',
        'auto': false,
        'multi': false,
        'onComplete': function (event, ID, fileObj, response, data) {
            $("#headimg").attr("src", response);

        }
    });
}
function uploadscenicimg() {
    $("#uploadscenicimg").uploadify({
        'uploader': '../../Content/loadfile/uploadify.swf',
        'script': '/ScenicArea/Upload',
        'cancelImg': '../../Content/loadfile/cancel.png',
        'folder': 'UploadFile',
        'queueID': 'fileQueue',
        'buttonImg': '../../Content/loadfile/choscenicimg.png',
        'width': 117,
        'height': 40,
        'wmode': 'transparent',
        'auto': false,
        'multi': false,
        'onComplete': function (event, ID, fileObj, response, data) {
            $("#scenicimg").attr("src", response);

        }
    });
}
function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

function SetCookie(name, value)//两个参数，一个是cookie的名子，一个是值
{
    var Days = 7; //此 cookie 将被保存 7 天
    var exp = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";path=/" + ";expires=" + exp.toGMTString();
}
function delCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";path=/" + ";expires=" + exp.toGMTString();
}
function objToJSONString(obj, filter) {
    return JSON.stringify(obj, filter);
}