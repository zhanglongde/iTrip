/// <reference path="../../../Scripts/jquery-1.4.4.js" />

$(function () {
    $(".a_scenic_announce").css("background", "url(../../Content/themes/image/shouqie.png)  no-repeat scroll -1px -47px  transparent");
    $(".a_scenic_announce").css("display", "block");
    $(".a_scenic_announce").css("color", "#739242");
    prompt();
    getScenicInfo();
    loadAnnountionsByPaging(getCookie("scenicZoneId"), 1);
    paging(getCookie("scenicZoneId"));
    //发布一条公告 ,并绑定发布公告信息单击事件
    AddAnnountion(getCookie("scenicZoneId"));
    //获得更多的三个月内公告
    getMore3MonthAnnountions(getCookie("scenicZoneId"));
});

//编辑者：张龙德 功能：获得更多的三个月内公告
function getMore3MonthAnnountions(scenicSpotID) {
    var address = '/ScenicAnnountion/getMore3MonthAnnountions/' + scenicSpotID;
    $("#3Month_annoution_title_Div").more({
        'address': address,
        'amount': '3',
        'template': '.ul_notice',
        'trigger': '.get_more'   //触发加载更多记录的class属性
    });
}

//绑定可输入字数事件
function prompt() {
    $("#annountionTitle").keyup(
      function () {
          var title = $("#annountionTitle").val();
          var titleLength = title.length;
          var remain = 100 - titleLength;
          $("#wordAmountID").text(remain);
          // $("#annountionTitle").unbind("keyup");
      });

    $("#annountionContent").keyup(
      function () {
          var title = $("#annountionContent").val();
          var titleLength = title.length;
          var remain = 500 - titleLength;
          $("#wordAmountID").text(remain);
          // $("#annountionContent").unbind("keyup");
      });
}
function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

//编辑者：张龙德 功能：分页初始化，并绑定点击页面事件
function paging(scenicSpotID) {
    var pageAmount = 0;
    $.post("/ScenicAnnountion/getPageAmount", { scenicSpotID: scenicSpotID }, function (data) {
        pageAmount = parseInt(data);
        var disPlay = 0;
        disPlay = parseInt(pageAmount / 2 + 1);
        $("#demo").paginate({
            count: pageAmount,
            start: 1,
            display: disPlay,
            border: true,
            border_color: '#BEF8B8',
            text_color: '#79B5E3',
            background_color: '#E3F2E1',
            border_hover_color: '#68BA64',
            text_hover_color: '#2573AF',
            background_hover_color: '#CAE6C6',
            images: false,
            mouse: 'press',
            onChange: function (page) {
                $("#scenicAnnounttionsDiv").empty();
                loadAnnountionsByPaging(scenicSpotID, page);
            }
        });
    });
}
//载入所有公告
function loadAllAnnountions(scenicSpotID) {
    $.post("/ScenicAnnountion/loadAllAnnountions", { scenicSpotID: scenicSpotID },
     function (datas, status) {
         if (status != "success") { alert("载入公告信息失败"); return; }
         else {
             for (var i = 0; i < datas.length; i++) {
                 var Annountion = datas[datas.length-i-1];
                 Annountion.DecTime = ChangeDateFormat(Annountion.DecTime);
                 prependSingleAnnountion(Annountion.DecID, Annountion.Title, Annountion.DecContent, Annountion.DecTime);
             }
         }
     }, "json");
}

//载入某一页的所有公告信息
function loadAnnountionsByPaging(scenicSpotID, page) {
    $.post("/ScenicAnnountion/loadAnnountionsBypagings", { scenicSpotID: scenicSpotID, page: page },
     function (datas, status) {
         if (status != "success") { alert("载入公告信息失败"); return; }
         else {
             for (var i = 0; i < datas.length; i++) {
                 var Annountion = datas[datas.length-i-1];
                 Annountion.DecTime = ChangeDateFormat(Annountion.DecTime);
                 prependSingleAnnountion(Annountion.DecID, Annountion.Title, Annountion.DecContent, Annountion.DecTime);
             }
         }
     }, "json");
 }
//获取景区信息
function getScenicInfo() {
    if (getCookie("UserNameTemp") != null) {
        $("#userName").text(getCookie("UserNameTemp"));
        $("#scenicName").text(getCookie("scenicZoneName"));
    }
}
//获得总页数
function getPageAmount() {
    $.post("/ScenicAnnountion/getPageAmount", function (data) {
        return data;
    });
}

//载入一周内公告
function loadWeekAnnountions(scenicSpotID) {
    $.post("/ScenicAnnountion/loadWeekAnnountions", { scenicSpotID: scenicSpotID },
     function (datas, status) {
         if (status != "success") { alert("载入公告信息失败"); return; }
         else {
             for (var i = 0; i < datas.length; i++) {
                 var Annountion = datas[i];
                 Annountion.DecTime = ChangeDateFormat(Annountion.DecTime);
                 prependSingleAnnountionTitle("weekTitle", Annountion.DecID, Annountion.Title);
             }
         }
     }, "json");
}

//载入1个月内公告
function loadMonthAnnountions(scenicSpotID) {
    $.post("/ScenicAnnountion/loadMonthAnnountions", { scenicSpotID: scenicSpotID },
     function (datas, status) {
         if (status != "success") { alert("载入公告信息失败"); return; }
         else {
             for (var i = 0; i < datas.length; i++) {
                 var Annountion = datas[i];
                 Annountion.DecTime = ChangeDateFormat(Annountion.DecTime);
                 prependSingleAnnountionTitle("monthTitle", Annountion.DecID, Annountion.Title);
             }
         }
     }, "json");
}

//载入3个月内公告
function load3MonthAnnountions(scenicSpotID) {
    $.post("/ScenicAnnountion/loadThreeMonthAnnountions", { scenicSpotID: scenicSpotID },
     function (datas, status) {
         if (status != "success") { alert("载入公告信息失败"); return; }
         else {
             for (var i = 0; i < datas.length; i++) {
                 var Annountion = datas[i];
                 Annountion.DecTime = ChangeDateFormat(Annountion.DecTime);
                 prependSingleAnnountionTitle("threemonthTitle", Annountion.DecID, Annountion.Title);
             }
         }
     }, "json");
}
//利用AJAX在界面中直接添加公告标题
function prependSingleAnnountionTitle(type, AnnountionDecID, AnnountionTitle) {
    $('#' + type).prepend(
     '<li id=' + AnnountionDecID + '> <a>' + AnnountionTitle + '</a></li>'
     );
}

//利用AJAX在界面中直接添加公告标题、公告内容和公告时间
function prependSingleAnnountion(AnnountionDecID, AnnountionTitle, AnnountionDecContent, AnnountionDate) {
    var AnnountionTitleID = AnnountionDecID + "1";
    var AnnountionContentID = AnnountionDecID + "2";
    var AnnountionDateID = AnnountionDecID + "3";
    var AnnountionRevise = AnnountionDecID + "4";
    var AnnountionDelete = AnnountionDecID + "5";
    //     AnnountionDate = ChangeDate(AnnountionDate);
    $("#scenicAnnounttionsDiv").prepend(
         '<div class="content" id=' + AnnountionDecID + '>' + '<span id=' + AnnountionTitleID + '>' + AnnountionTitle + '</span>' +
           '<p id=' + AnnountionContentID + '> ' + AnnountionDecContent + ' </p> <span class="submitTime">发布时间:</span> <span class="annountionDate" dispaly="inline" id=' + AnnountionDateID + '>' + AnnountionDate + '</span>' +
            '<a class="annChange" id=' + AnnountionRevise + '>修改</a> <a class="annDelete" id=' + AnnountionDelete + '>删除</a>  ' +
            '</div>'
);
    //绑定删除按钮，当触发单击时间时，执行删除操作
    $('#' + AnnountionDelete).click(function () {
        DeleteAnnountion(AnnountionDecID);
    });
    //绑定修改按钮，点击修改按钮，修改一条公告
    $('#' + AnnountionRevise).click(function () {
        if ($("#reviseAnnountion").css("display") != "none") {
            $("#reviseAnnountion").unbind("click");
        }
        //alert($("#reviseAnnountion").css("display"));
        $("#addAnnountion").hide();
        $("#reviseAnnountion").show();
        $.post("/ScenicAnnountion/queryContentByDecId", { DecId: AnnountionDecID }, function (datas, status) {
            if (status != "success") {
                alert("载入公告信息失败"); return;
            }
            else {
                var Annountion = datas;
                $("#annountionTitle").val(Annountion.Title);
                $("#annountionContent").val(Annountion.DecContent);
                $(window).scrollTop($(window).height() - 400);
            }
        }, "json");
        confirmReviseAnnountion(AnnountionDecID, AnnountionContentID, AnnountionTitleID, AnnountionDateID);
    });
}

//编辑者：张龙德 功能：点击确定按钮，修改一条公告
function confirmReviseAnnountion(AnnountionDecID, AnnountionDecContentID, AnnountionTitleID, AnnountionDateID) {
    $("#reviseAnnountion").click(function () {
        $("#reviseAnnountion").css("display", "inline");
        $("#addAnnountion").css("display", "none");
        var Title = $("#annountionTitle").val(); //读取修改后的公告标题
        var DecContent = $("#annountionContent").val(); //读取修改后的公告内容

        Title = filter(Title);
        DecContent = filter(DecContent);
        if (Title == "") {
            alert("公告标题不能为空！"); return;
        }
        else {
            if (DecContent == "") {
                alert("公告内容不能为空！"); return;
            }
            else {
                if (
               Title.length > 100) {
                    alert("公告标题不能超过100个字！"); return;
                }
                else {
                    if (DecContent.length > 500) {
                        alert("公告内容不能超过500个字！"); return;
                    }
                    else {
                        $("#reviseAnnountion").unbind("click");
                        $.post("/ScenicAnnountion/reviseAnnountion", { decContent: DecContent, title: Title, annountionDecID: AnnountionDecID }, function (data) {
                            if (data == "false") {
                                alert("请求失败");
                            }
                            else {//                
                                var ReviseDate = new Date();
                                ReviseDate = changeToRequiredTime(ReviseDate);
                                $('#' + AnnountionTitleID).text(Title);
                                $('#' + AnnountionDecContentID).text(DecContent);
                                $('#' + AnnountionDateID).text(ReviseDate);
                                //修改完清空
                                $("#annountionTitle").val("");
                                $("#annountionContent").val("");
                                $("#wordAmountID").text(100);
                                $("#revisesuccPic").show();
                                $("#revisesuccPic").fadeOut(1500);
                                $("#reviseAnnountion").css("display", "none");
                                $("#addAnnountion").css("display", "block");
                            }
                        });

                    }

                }
            }
        }

    });
}
//编辑者：张龙德 功能：发布一条公告 
function AddAnnountion(scenicSpotID) {
    $("#addAnnountion").click(function () {//点击添加公告按钮
        var AnnountionTitle = $("#annountionTitle").val(); //从输入框中读取公告标题
        var AnnountionDecContent = $("#annountionContent").val(); //从输入框中读取公告内容
        AnnountionTitle = filter(AnnountionTitle);
        AnnountionDecContent = filter(AnnountionDecContent);
        if (AnnountionTitle == "") {
            alert("公告标题不能为空！"); return;
        }
        else {
            if (AnnountionDecContent == "") {
                alert("公告内容不能为空！"); return;
            }
            else {
                if (
               AnnountionTitle.length > 100) {
                    alert("公告标题不能超过100个字！"); return;
                }
                else {
                    if (AnnountionDecContent.length > 500) {
                        alert("公告内容不能超过500个字！"); return;
                    }
                    else {
                        $.post("/ScenicAnnountion/issueAnnounce", { scenicSpotID: scenicSpotID, decContent: AnnountionDecContent, title: AnnountionTitle }, function (data, status) {
                            var AnnountionDecID = data;
                            var AnnountionDate = new Date();
                            AnnountionDate = changeToRequiredTime(AnnountionDate);
                            prependSingleAnnountion(AnnountionDecID, AnnountionTitle, AnnountionDecContent, AnnountionDate);
                            //修改完清空
                            $("#annountionTitle").val("");
                            $("#annountionContent").val("");
                            $("#wordAmountID").text(100);
                            $("#succPic").show();
                            $("#succPic").fadeOut(1500);
                        });
                    }

                }
            }
        }

    });
}
function filter(destination) {
    //用js实现过滤script
    var str = destination.replace(/<script.*?>.*?<\/script>/ig, '');

    var re = /<html>|select|insert|delete|update|count|drop|from|exec/gi;
    str = str.replace(re, function (sMatch) {
        return sMatch.replace(/./g, "");
    });

    // 过滤特殊字符 ,校验所有输入域是否含有特殊符号 
    //    var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）&mdash;—|{}【】‘；：”“'。，、？]")
    //    var rs = "";
    //    for (var i = 0; i < str.length; i++) {
    //        rs = rs + str.substr(i, 1).replace(pattern, '');
    //    } 

    return str;
}


//删除一条公告 
function DeleteAnnountion(AnnountionDecID) {
    if (confirm("确认删除公告信息")) {
        $.post("/ScenicAnnountion/deleteAnnontion", { AnnountionDecID: AnnountionDecID }, function (data) {
            if (data == "false") {
                alert("请求失败");
            }
            else {//                alert("请求成功");
                $("#" + AnnountionDecID).remove();
            }
        });
    }

}

//将时间数据反序列化
/// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
function ChangeDateFormat(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    }
    else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }
    var date = new Date(parseInt(jsondate, 10));
    return changeToRequiredTime(date); //获得当前时间，并转化为规定格式
}

//获得当前时间，并转化为规定格式
function changeToRequiredTime(date) {
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var Hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var Minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var Second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    return date.getFullYear() + "-" + month + "-" + currentDate + " " + Hour + ":" + Minute + ":" + Second;
}