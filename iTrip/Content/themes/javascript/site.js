/// <reference path="../../../Scripts/jquery-1.4.4.js" />
$(function () {
    $(".menu_op").click(function () {
        $(this).css("background", "url(../../Content/themes/image/shouqie.png)  no-repeat scroll 0 -35px  transparent");
    }
    );
    if (getCookie("UserNameTemp") != null) {

        $("#aUserName").text(getCookie("UserNameTemp"));
        $(".login").css("display", "none");
        $(".register").css("display", "none");
        $.post("/ScenicArea/getScenicZoneInfo", { userName: getCookie("UserNameTemp") }, function (scenicZoneInfo) {
            SetCookie("scenicZoneId", scenicZoneInfo[0]);
            SetCookie("scenicZoneName", scenicZoneInfo[1]);
        }, "json");
    }
    else {
        $(".exit-login").css("display", "none");
        $(".user-info").css("display", "none");
        var page = document.URL.split("/")[3];
        page = page.toLowerCase();
        if (page != "homepage" && page != "register") {
            alert("请先登录");
            window.location = "../Login/Default";
        }
    }
    $(".exit-login").click(function () {
        delCookie("UserNameTemp");
        delCookie("UserName");
        delCookie("scenicZoneId");
        delCookie("scenicZoneName");
        window.location = "../Login/Default";
    });
    backToTop();
    $("object").hide();
    load();
})
function load() {
    var page = document.URL.split("/")[3];
    page = page.toLowerCase();
    if (page == "travertalk" || page == "scenicarea" || page == "scenicannountion" || page == "trafficstatistics") {
        var myDate = new Date();
        $("#month").text(myDate.getMonth() + 1 + "/");
        $("#day").text(myDate.getDate());
        var week = $("#week");
        if (myDate.getDay() == "0") {
            week.text("周天");
        }
        else if (myDate.getDay() == "1") {
            week.text("周一");
        }
        else if (myDate.getDay() == "2") {
            week.text("周二");
        }
        else if (myDate.getDay() == "3") {
            week.text("周三");
        }
        else if (myDate.getDay() == "4") {
            week.text("周四");
        }
        else if (myDate.getDay() == "5") {
            week.text("周五");
        }
        else if (myDate.getDay() == "6") {
            week.text("周六");
        }
        var userName = getCookie("UserNameTemp");
        $.post("/Register/IsUserSignInToday", { "userName": userName }, function (result) {
            if (result == "true") {
                $("#siginIn").text("已签到");
            }
        })
        $.post("/Register/GetUserSignInDayNum", { "userName": userName }, function (dayCount) {
            $("#dayCount").text(dayCount);
        });

    }
}
function change() {
    if ($("#siginIn").text() == "签到") {
        document.getElementById('siginIn').innerHTML = "已签到";
        var dayCount = document.getElementById('dayCount').innerHTML;
        document.getElementById('dayCount').innerHTML = parseInt(dayCount) + 1;
        var userName = getCookie("UserNameTemp");
        var ipAdr = remote_ip_info.start;
        var city = remote_ip_info.city;
        $.post("/Register/UserSignIn", { "userName": userName, "ipAdr": ipAdr, "city": city }, function (result) {
            if (result == "true")
            { }
            else {
                alert("操作数据库失败");
            }
        });
    }

}
function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;

}
function delCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";path=/" + ";expires=" + exp.toGMTString();
}

function SetCookie(name, value)//两个参数，一个是cookie的名子，一个是值
{
    var Days = 7; //此 cookie 将被保存 7 天
    var exp = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";path=/" + ";expires=" + exp.toGMTString();
}

function backToTop() {
    var $backToTopTxt = "返回顶部", $backToTopEle = $('<div class="backToTop"></div>').appendTo($("body"))
        .text($backToTopTxt).attr("title", $backToTopTxt).click(function () {
            $("html, body").animate({ scrollTop: 0 }, 120);
        }), $backToTopFun = function () {
            var st = $(document).scrollTop(), winh = $(window).height();
            (st > 0) ? $backToTopEle.show() : $backToTopEle.hide();
            //IE6下的定位
            if (!window.XMLHttpRequest) {
                $backToTopEle.css("top", st + winh - 166);
            }
        };
    $(window).bind("scroll", $backToTopFun);
    $(function () { $backToTopFun(); });
}