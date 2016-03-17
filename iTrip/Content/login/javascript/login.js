
/// <reference path="../../../Scripts/jquery-1.4.4.js" />
/// <reference path="../../poshytip/jquery.poshytip.js" />

$(function () {
    AutoLoginInWeek();
    $("#UserName").click(function () {
        if ($("#UserName").val() == "用户名/邮箱/手机号") {
            $("#UserName").val("");
        }
    }).blur(function () {
        if ($("#UserName").val() == "") {
            $("#UserName").val("用户名/邮箱/手机号");
        }
    });
    $("#alogin").click(function () {
        InputPrompt();
    });
    KeyToLogin();
    $("object").hide();
})

function AutoLoginInWeek() {
    if (getCookie("UserName") != null) {
        $(".ct-login").empty();
        $(".ct-login").prepend("<h2>正在登陆...<h2>");
        $(".ct-login h2").css({ "font-size": "20px", "color": "#9E9392" });
        $(".ct-login h2").css("margin-top", "20px");
        SetCookie("UserNameTemp", getCookie("UserName"));
        window.location = "/HomePage/HomePage";
    }
}
function KeyToLogin() { //按下enter键登陆
    $("#UserName").keydown(function (event) {
        if (event.keyCode == 13) {
            InputPrompt();
        }
    });
    $("#Pwd").keydown(function (event) {
        if (event.keyCode == 13) {
            InputPrompt();
        }
    });
}
function InputPrompt() {
    var UserName = $("#UserName").val();
    var Password = $("#Pwd").val();
    $.post("/Login/login", { UserName: UserName, Pwd: Password }, function (result) {
        if (result == "false") {
            $(".false-prompt").css("display", "block");
        }
        else {
            var filter1 = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/; //邮箱格式
            var filter2 = /^1[3,5,8]{1}[0-9]{9}$/; //手机号格式
            if (filter1.test(UserName) || filter2.test(UserName)) { //手机或者邮箱登陆
                $.ajax({
                    url: "/Login/GetUserNameByEmailOrTel",
                    data: { "content": UserName },
                    type: "post",
                    async: false,
                    success: function (result) {
                        SetCookie("UserNameTemp", result);
                        if (document.getElementById("CkAutoLogin").checked == true) {
                            SetCookie("UserName", result);
                        }
                    }
                });
            }
            else {
                SetCookie("UserNameTemp", UserName);
                if (document.getElementById("CkAutoLogin").checked == true) {
                    SetCookie("UserName", UserName);
                }
            }
            $(".ct-login").empty();
            $(".ct-login").prepend("<div>正在登陆...</div>");
            $(".ct-login div").css({ "font-size": "20px", "color": "#9E9392" });
            $(".ct-login div").css("margin-top", "20px");
            window.location = "/HomePage/HomePage";
        }
    });
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

