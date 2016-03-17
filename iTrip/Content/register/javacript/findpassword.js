/// <reference path="../../../Scripts/jquery-1.4.4.js" />
/// <reference path="../../poshytip/jquery.poshytip.js" />

$(function () {
    ChangePwd();
    InputPrompt();
})
function ChangePwd() {
    var url = document.URL;
    var info = url.split('Register/FindPassword/')[1];
    if (info != null) {
        $(".FindPwd_Main").empty();
        var userName = info.split("itonu@")[0];
        var email = info.split("itonu@")[1];
        var pwd = info.split("itonu@")[2];
        $.post("/Register/ChangePwd", { "email": email, "pwd": pwd, "userName": userName }, function (result) {
            if (result == "true") {
                window.location = "/Login/Default";
            }
            else {
                alert("操作数据库出错");
            }

        });
    }
}
/*编辑者：吴国顺 功能：判断邮箱是否存在*/
function InputPrompt() {
    var result = new Array();
    for (var i = 0; i < 4; i++) {
        result[i] = false;
    }
        $("#emailAdr").poshytip({                  //邮箱提示
            className: 'tip-yellowsimple',
            content: '填写注册的邮箱，如：123@163.com',
            showOn: 'focus',
            alignTo: 'target',
            alignX: 'right',
            alignY: 'center',
            offsetX: 5
        });
        $("#emailAdr").poshytip({
            className: 'tip-yellowsimple',
            content: function () {
                var filter2 = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (trim($("#emailAdr").val()) == "") {
                    result[0] = false;
                    return "邮箱地址不能为空";
                }
                else if (!filter2.test($("#emailAdr").val())) {
                    return "邮箱格式不正确";
                }
                else {
                    if (IsEmailIn()) {
                        result[0] = true;
                        return "邮箱填写正确";

                    }
                    else {
                        result[0] = false;
                        return "该邮箱不存在,请重新填写";
                    }

                }
            },
            showOn: 'blur',
            alignTo: 'target',
            alignX: 'right',
            alignY: 'center',
            offsetX: 5
        });
        $("#userName").poshytip({                  //邮箱提示
            className: 'tip-yellowsimple',
            content: '填写注册的用户名',
            showOn: 'focus',
            alignTo: 'target',
            alignX: 'right',
            alignY: 'center',
            offsetX: 5
        });
        $("#userName").poshytip({
            className: 'tip-yellowsimple',
            content: function () {
                var filter = /^[a-z\d_\u4e00-\u9fa5]{4,16}/i;
                if (trim($("#userName").val()) == "") {
                    result[3] = false;
                    return "用户名不能为空";
                }
                else if (!filter.test($("#userName").val())) {
                    result[3] = false;
                    return "用户名格式错误";
                }
                else if (!IsUserNameIn()) {
                    result[3] = false;
                    return "该用户名不存在";
                }
                else if (!IsUserNameEmailMatch()) {
                    result[3] = false;
                    return "用户名和邮箱地址不匹配";
                }
                else {
                    result[3] = true;
                    return "用户名填写正确";
                }
            },
            showOn: 'blur',
            alignTo: 'target',
            alignX: 'right',
            alignY: 'center',
            offsetX: 5
        });
        $("#pwd").poshytip({                  //密码提示
            className: 'tip-yellowsimple',
            content: '新密码由6-16位半角字符（字母、数字、符号）组成',
            showOn: 'focus',
            alignTo: 'target',
            alignX: 'right',
            alignY: 'center',
            offsetX: 5
        });
        $("#pwd").poshytip({
            className: 'tip-yellowsimple',
            content: function () {
                var filter1 = /^[\@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{6,22}$/;
                if (trim($("#pwd").val()) == "") {
                    result[1] = false;
                    return "新密码不能为空";
                }
                else if (!filter1.test($("#pwd").val())) {
                    result[1] = false;
                    return "密码格式不正确";
                }
                else {
                    result[1] = true;
                    return "新密码填写正确";

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
                if (trim($("#confirmpwd").val()) == "") {
                    result[2] = false;
                    return "确认密码不能为空";
                }
                else if ($("#pwd").val() != $("#confirmpwd").val()) {
                    result[2] = false;
                    return "密码与确认密码不一致";
                }
                else {
                    result[2] = true;
                    return "确认密码正确";
                }
            },
            showOn: 'blur',
            alignTo: 'target',
            alignX: 'right',
            alignY: 'center',
            offsetX: 5
        });
        $("#confirmSend").click(function () {
            if (IsEmailIn() && result[1] == true && result[2] == true && result[3] == true) {
                $.post("/Register/SendEmail", { "email": trim($("#emailAdr").val()), "userName": trim($("#userName").val()), "pwd": trim($("#pwd").val()) }, function () {
                    SetCookie("email", $("#emailAdr").val());
                    $(".emailPrompt").show();
                    $(".emailPrompt").fadeOut(3000);
                });
            }
        });
}
function trim(ostr) {
    return ostr.replace(/^\s+|\s+$/g, "");
}

function IsEmailIn() {
    var emailIn = false;
    var emailAdr = $("#emailAdr").val();
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
function IsUserNameIn() {
    var userNameIn = false;
    var userName = $("#userName").val();
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
function IsUserNameEmailMatch()
{
  var match=false;
  var userName=$("#userName").val();
  var email=$("#emailAdr").val();
  $.ajax({
        url: "/Register/IsEmailNameMatch",
        data: { userName: userName,email:email },
        type: "post",
        async: false,
        success: function (result) {
            if (result == "true") {
                match = true;
            }
            else {
                match = false;
            }
        }
       });
  return match;
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