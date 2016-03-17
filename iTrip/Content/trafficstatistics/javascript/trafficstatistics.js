/// <reference path="../../../Scripts/jquery-1.4.4.js" />
/// <reference path="../../jquery-ui-moreInfo/javascript/jquery.more.js" />

$(function () {
    $(".a_scenic_traffic").css("background", "url(../../Content/themes/image/shouqie.png)  no-repeat scroll -1px -47px  transparent");
    $(".a_scenic_traffic").css("display", "block");
    $(".a_scenic_traffic").css("color", "#739242");
    getScenicInfo();
    GetScenicSpotInfo();
    ChangeDate();
});

function ChangeDate() {
    createSelect(1);
    $("#tYEAR").change(function () { //改变日期
        createSelect();
    });
    $("#tMON").change(function () {
        createSelect();
    });
}
//获取景区信息
function getScenicInfo() {
    if (getCookie("UserNameTemp") != null) {
        $("#userName").text(getCookie("UserNameTemp"));
        $("#scenicName").text(getCookie("scenicZoneName"));
    }
}

//获取景点信息
function GetScenicSpotInfo() {
    if (getCookie("scenicZoneId") != null) {
        $("#scenicSpotName_list").more({
            'address': '/TrafficStatistics/GetScenicSpotInfo/' + getCookie("scenicZoneId"),
            'amount': '4',
            'template': '.ul_list',
            'trigger': '.get_more'   //触发加载更多记录的class属性
        });
        $(".ScenicSpotName").live("click", function () {
            var scenicSpotId = $(this).next().text();
        });
    }
}

function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

function createSelect(ActionFlag) {
    var selYear = document.getElementById("tYEAR");
    var selMonth = document.getElementById("tMON");
    var selDay = document.getElementById("tDAY");
    var dt = new Date();

    if (ActionFlag == 1) {
        MinYear = dt.getFullYear() - 150;
        MaxYear = dt.getFullYear();

        for (var i = MaxYear; i >= MinYear; i--) {
            var op = document.createElement("OPTION");
            op.value = i;
            op.innerHTML = i;
            selYear.appendChild(op);
        }
        selYear.selectedIndex = 0;

        for (var i1 = 1; i1 < 13; i1++) {
            var op1 = document.createElement("OPTION");
            op1.value = i1;
            op1.innerHTML = i1;
            selMonth.appendChild(op1);
        }
        selMonth.selectedIndex = dt.getMonth();
    }

    var date = new Date(selYear.value, selMonth.value, 0);
    var daysInMonth = date.getDate();
    selDay.options.length = 0;

    for (var i2 = 1; i2 <= daysInMonth; i2++) {
        var op2 = document.createElement("OPTION");
        op2.value = i2;
        op2.innerHTML = i2;
        selDay.appendChild(op2);
    }

    selDay.selectedIndex = dt.getDate() - 1;
}