/// <reference path="../../../Scripts/jquery-1.4.4.js" />
/// <reference path="../../poshytip/jquery.poshytip.js" />

$(function () {
    InputPrompt();
    IsActive();
    $(".go-to-email").click(function () {
        var emailFormat = $(".email-adress").text();
        emailFormat = emailFormat.split('@')[1];
        emailFormat = emailFormat.split('.')[0];
        window.location = "http://mail." + emailFormat + ".com";
    });
})

/*编辑者：唐超  功能：激活用户*/
function IsActive() {
    var url = document.URL;
    var userName = url.split('Register/Register/')[1];
    if (userName != null) {
            $(".register_info").css("display", "none");
            $(".reg-success").css("display", "block");
            $(".register_pro_img").css("background", "url(../../Content/register/image/register_progressbar.png) no-repeat scroll -12px -127px transparent");
            SetCookie("UserNameTemp", userName);
            $.post("/Register/IsUserHaveActive", { userName: userName }, function (result) {
                if (result == "true") {
                }
                else {
                    $.post("/Register/ActivateUser", { username: userName }, function (data) {
                        if (data == "true") {
                        }
                    });
                }
            });
            setTimeout(function () {
                window.location = "/HomePage/HomePage";
            }, 1000);
    }
}


/*编辑者：唐超 功能：输入错误提示*/
function InputPrompt() {
    var result1 = false;
    var result2 = false;
    var result3 = false;
    var result4 = false;
    var result5 = false;
    var result6 = false;
    var result7 = false;
    var result8 = false;
    var result9 = false;
    var result10 = false;
    var result11 = false;
    var filter = /^[a-z\d_\u4e00-\u9fa5]{4,16}/i;
    $("#username").poshytip({                  //用户名提示
        className: 'tip-yellowsimple',
        content: '可输入4-16位，包含字母、数字、下划线、或者中文字符',
        showOn: 'focus',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#username").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result1 = false;
            if (trim($("#username").val()) == "") {
                return "用户名不能为空";
            }
            else if (!filter.test($("#username").val())) {
                return "用户名格式不正确";
            }
            else if (IsUserNameIn()) {
                return "此用户名已被注册过";
            }
            else {
                result1 = true;
                return "此用户名可用";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#password").poshytip({                  //密码提示
        className: 'tip-yellowsimple',
        content: '密码由6-16位半角字符（字母、数字、符号）组成',
        showOn: 'focus',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#password").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result2 = false;
            var filter1 = /^[\@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{6,22}$/;
            if (trim($("#password").val()) == "") {
                return "密码不能为空";
            }
            else if (!filter1.test($("#password").val())) {
                return "密码格式不正确";
            }
            else {
                result2 = true;
                return "此密码可用";

            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#confirmpwd").poshytip({                  //确认密码提示
        className: 'tip-yellowsimple',
        content: function () {
            result3 = false;
            if (trim($("#confirmpwd").val()) == "") {
                return "确认密码不能为空";
            }
            else if ($("#password").val() != $("#confirmpwd").val()) {
                return "密码与确认密码不一致";
            }
            else {
                result3 = true;
                return "确认密码正确";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#email").poshytip({                  //邮箱提示
        className: 'tip-yellowsimple',
        content: '填写常用邮箱，如：123@163.com',
        showOn: 'focus',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#email").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result4 = false;
            var filter2 = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (trim($("#email").val()) == "") {
                return "邮箱地址不能为空";
            }
            else if (!filter2.test($("#email").val())) {
                return "邮箱格式不正确";
            }
            else if (IsEmailIn()) {
                return "邮箱已被注册过";
            }
            else {
                result4 = true;
                return "此邮箱可用";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#mobilephone").poshytip({                  //手机提示
        className: 'tip-yellowsimple',
        content: '填写11位常用手机号，如：18950457531',
        showOn: 'focus',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#mobilephone").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result5 = false;
            var filter3 = /^1[3,5,8]{1}[0-9]{9}$/;
            if (trim($("#mobilephone").val()) == "") {
                return "手机号不能为空";
            }
            else if (!filter3.test(trim($("#mobilephone").val()))) {
                return "手机号格式不正确";
            }
            else if (IsTelIn()) {
                return "此手机号已注册过";
            }
            else {
                result5 = true;
                return "此手机号可用";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#scenic_name").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result6 = false;
            if (trim($("#scenic_name").val()) == "") {
                return "景区名不为空";
            }
            else if (IsScenicZoneNameIn()) {
                return "景区名已存在,请重新填写";
            }
            else {
                result6 = true;
                return "检查通过";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#scenic_intro").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result7 = false;
            if (trim($("#scenic_intro").val()) == "") {
                return "景区介绍不为空";
            }
            else {
                result7 = true;
                return "检查通过";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });

    $("#scenic_addr").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result8 = false;
            if (trim($("#scenic_addr").val()) == "") {
                return "景区地址不为空";
            }
            else {
                result8 = true;
                return "检查通过";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#scenic_phone").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result9 = false;
            if (trim($("#scenic_phone").val()) == "") {
                return "联系方式不为空";
            }
            else {
                result9 = true;
                return "检查通过";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#open_time").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result11 = false;
            if (trim($("#open_time").val()) == "") {
                return "开放时间不为空";
            }
            else {
                result11 = true;
                return "检查通过";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });
    $("#traver_notice").poshytip({
        className: 'tip-yellowsimple',
        content: function () {
            result10 = false;
            if (trim($("#traver_notice").val()) == "") {
                return "游客须知不为空";
            }
            else {
                result10 = true;
                return "检查通过";
            }
        },
        showOn: 'blur',
        alignTo: 'target',
        alignX: 'right',
        alignY: 'center',
        offsetX: 5
    });


    $("#regist").click(function () {
        if (result1 && result2 && result3 && result4 && result5 && result6 && result7 && result8 && result9 && result10 && result11) {
            /*注册用户信息*/
            var username = $("#username").val();
            var password = $("#password").val();
            var email = $("#email").val();
            var mobilephone = $("#mobilephone").val();
            $.ajax({
                url: "/Register/regist",
                data: { username: username, password: password, email: email, mobilephone: mobilephone },
                type: "post",
                async: false,
                success: function (result) {
                    
                }
            });
            //注册景区信息
            var areaName = $("#scenic_name").val();
            var introduce = $("#scenic_intro").val();
            var address = $("#scenic_addr").val();
            var phone = $("#scenic_phone").val();
            var time = $("#open_time").val();
            var notice = $("#traver_notice").val();
            $.post("/Register/ScenicIntroduction", { areaName: areaName, introduce: introduce, address: address, time: time, notice: notice, phone: phone, userName: username }, function (data) {
                if (data == "true") {
                    $(".register_info").css("display", "none");
                    $(".tip-yellowsimple").css("display", "none");
                    $(".register_pro_img").css("background", "url(../../Content/register/image/register_progressbar.png) no-repeat scroll -12px -67px transparent");
                    $(".email-adress").text($("#email").val());
                    $(".email-active").css("display", "block");
                    $(".register_content").css("height", "550px");
                }
            });

        }
    });
}
function trim(ostr) {
    return ostr.replace(/^\s+|\s+$/g, "");
}
function IsUserNameIn() {
    var userNameIn = false;
    var userName = $("#username").val();
    $.ajax({
        url: "/Register/IfUserNameExist",
        data: { userName: userName },
        type: "post",
        async: false,
        success: function (result) {
            if (result == "true") {
                userNameIn = true;
            }
            else {
                userNameIn = false;
            }
        }
    });
    return userNameIn;
}
function IsEmailIn() {
    var emailIn = false;
    var emailAdr = $("#email").val();
    $.ajax({
        url: "/Register/IsEmailIn",
        data: { email: emailAdr },
        type: "post",
        async: false,
        success: function (result) {
            if (result == "true") {
                emailIn = true;
            }
            else {
                emailIn = false;
            }
        }
    });
    return emailIn;
}
//判断景区名是否存在
function IsScenicZoneNameIn() {
    var scenicZoneName = $("#scenic_name").val();
    var ScenicZoneNameIn = false;
    $.ajax({
        url: "/Register/IsScenicZoneNameIn",
        data: { scenicZoneName: scenicZoneName },
        type: "post",
        async: false,
        success: function (result) {
            if (result == "true") {
                ScenicZoneNameIn = true;
            }
            else {
                ScenicZoneNameIn = false;
            }
        }
    });
    return ScenicZoneNameIn;
}
function IsTelIn() {
    var telIn = false;
    var telNumber = $("#mobilephone").val();
    $.ajax({
        url: "/Register/IsTelIn",
        data: { telNumber: telNumber },
        type: "post",
        async: false,
        success: function (result) {
            if (result == "true") {
                telIn = true;
            }
            else {
                telIn = false;
            }
        }
    });
    return telIn;
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

function getCookie(name)//取cookies函数
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

