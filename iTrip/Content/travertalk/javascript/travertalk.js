
/// <reference path="../../../Scripts/jquery-1.4.4.js" />
///// <reference path="../../poshytip/jquery.poshytip.js" />

$(function () {
    $(".a_traver_talk").css("background", "url(../../Content/themes/image/shouqie.png)  no-repeat scroll -1px -47px  transparent");
    $(".a_traver_talk").css("display", "block");
    $(".a_traver_talk").css("color", "#739242");
    getScenicZoneInfo();
    GetScenicSpotInfo();
    GetCommentInWeek();
    GetTraverTalk(1);
    GetCollectTraverTalk(1);
    GetHelpTraverTalk(1);
    deleteTraverTalk();
    collectComment();
    Page("普通", 1);
    Page("收藏", 2);
    Page("求助", 3);
});
//获取景区信息
function getScenicZoneInfo() {
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
        GetCommentByScenicSpotId();
    }
}

function GetCommentByScenicSpotId() {
    $(".ScenicSpotName").live("click",function () {
        var scenicSpotId = $(this).next().text();
        $("#con_three_1").hide();
        $("#traverTalkPage1").hide();
        $("#con_three_4").show();
        $("#con_three_4").empty();
        $.post("/TraverTalk/GetCommentByScenicSpotId", { "scenicSpotId": scenicSpotId }, function (commentInfos) {
            for (var i = 0; i < commentInfos.length; i++) {
                var commentInfo = commentInfos[i];
                AddTraverTalkDiv(4, commentInfo.CommentID, commentInfo.CmtContent, commentInfo.CmtTime, commentInfo.PicturePath, commentInfo.ScenicSpotName);
            }
        },"json");
    });
}
//获取留言信息
function GetTraverTalk(pageNum) {
    $.post("/TraverTalk/GetScenicZoneComment", { "scenicZoneId": getCookie("scenicZoneId"), "pageNum": pageNum, "comMark": "普通" }, function (TraverTalkInfos) {
        for (var i = 0; i < TraverTalkInfos.length; i++) {
            var TraverTalkInfo = TraverTalkInfos[TraverTalkInfos.length - i - 1];
            AddTraverTalkDiv(1, TraverTalkInfo.CommentID, TraverTalkInfo.CmtContent, TraverTalkInfo.CmtTime, TraverTalkInfo.PicturePath, TraverTalkInfo.ScenicSpotName);
        }
    }, "json");
}
//获取一周内留言内容
function GetCommentInWeek() {
    $.post("/TraverTalk/GetCommentInWeek/" + getCookie("scenicZoneId"), { "last": 0, "amount": 5 }, function (commentInfos) {
        for (var i = 0; i < commentInfos.length; i++) {
            var commentInfo = commentInfos[i];
            $(".commentTitle_Week").append('<li><span>' + commentInfo.CmtContent + '<span><li>');
        }
    }, "json");
    $("#getMore_commentWeek").click(function () {
        if (getCookie("scenicZoneId") != null) {
            $("#con_three_1").hide();
            $("#traverTalkPage1").hide();
            $("#con_three_4").show();
            $("#con_three_4").empty();
            $.post("/TraverTalk/GetCommentInWeek/" + getCookie("scenicZoneId"), { "last": 0, "amount": 20 }, function (commentInfos) {
                for (var i = 0; i < commentInfos.length; i++) {
                    var commentInfo = commentInfos[i];
                    AddTraverTalkDiv(4, commentInfo.CommentID, commentInfo.CmtContent, commentInfo.CmtTime, commentInfo.PicturePath, commentInfo.ScenicSpotName);
                }
            }, "json");
        }
    });
}

//获取收藏留言信息
function GetCollectTraverTalk(pageNum) {
    $.post("/TraverTalk/GetScenicZoneComment", { "scenicZoneId": getCookie("scenicZoneId"), "pageNum": pageNum, "comMark": "收藏" }, function (TraverTalkInfos) {
        for (var i = 0; i < TraverTalkInfos.length; i++) {
            var TraverTalkInfo = TraverTalkInfos[TraverTalkInfos.length - i - 1];
            AddTraverTalkDiv(2, TraverTalkInfo.CommentID, TraverTalkInfo.CmtContent, TraverTalkInfo.CmtTime, TraverTalkInfo.PicturePath, TraverTalkInfo.ScenicSpotName);
        }
    }, "json");

}
//获取求助的游客信息
function GetHelpTraverTalk(pageNum) {
    $.post("/TraverTalk/GetScenicZoneComment", { "scenicZoneId": getCookie("scenicZoneId"), "pageNum": pageNum, "comMark": "求助" }, function (TraverTalkInfos) {
        for (var i = 0; i < TraverTalkInfos.length; i++) {
            var TraverTalkInfo = TraverTalkInfos[TraverTalkInfos.length - i - 1];
            AddTraverTalkDiv(3, TraverTalkInfo.CommentID, TraverTalkInfo.CmtContent, TraverTalkInfo.CmtTime, TraverTalkInfo.PicturePath, TraverTalkInfo.ScenicSpotName);
        }
    }, "json");
}
//动态添加留言div
function AddTraverTalkDiv(num, commentId, cmdContent, cmdTime, picturePath, ScenicSpotName) {
    $("#con_three_" + num).prepend('<div class="yk1">' +
                        '<div class="ly">' +
                            '<span>' + cmdContent + '</span>' +
                        '</div>' +
                        '<div class="vt">' +
                            '<div class="i">' +
                                '<img src="../../Content/travertalk/images/1.png" />' +
                            '</div>' +
                            '<div class="ws">' +
                                '<span>游客</span>' +
                            '</div>' +
                        '</div>' +
                        '<div class="bt" id=' + commentId + 'mk' + num + '>' +
                            '<span class="cmdTime">留言时间:' + set_time(cmdTime) + '</span>' +'<span class="scenicSpotName">('+ScenicSpotName+')</span>'+ '<a class="rb" id=' + commentId + 'rb' + num + '>回复</a> <a class="del">删除</a> <a class="col">' +
                                '收藏</a>' +
                        '</div>' +
                    '</div>');
    traverTalk_replay(commentId, num);
    AddReplayContent();
}

//回复留言div
function traverTalk_replay(commentId, num) {
    $("#" + commentId + "rb" + num).click(function () {
        var replayDiv = $("#" + commentId + "ttb" + num);
        if (replayDiv.css("display") == "block") {
            replayDiv.css("display", "none");
        }
        else if (replayDiv.css("display") == "none") {
            replayDiv.css("display", "block");
        }
        else {
            $("#" + commentId + "rb" + num).parent().after(
              '<div class="traverTalk_replay" id=' + commentId + 'ttb' + num + '>' +
                    '<textarea class="replay_content" id=' + commentId + 'rpc' + num + '></textarea>' +
                    '<a class="replay_btn"></a>' +
                     '</div>'
            );
            $.post("/TraverTalk/GetCommentReply", { "comId": commentId }, function (replyInfos) {
                for (var i = 0; i < replyInfos.length; i++) {
                    var replyInfo = replyInfos[i];
                    var replyTime = Date.parse(replyInfo.ReplyTime).toLocaleString();
                    AddReplyContentDiv(commentId, num, replyInfo.ReplyID, replyInfo.ReplyContent, set_time(replyInfo.ReplyTime));
                }
            }, "json");
            AddReplayContent(num);
        }
    });

}

//添加回复内容
function AddReplayContent(num) {
    $(".replay_btn").click(function () {
        var commendId = $(this).parent().attr("id");
        commendId = commendId.substring(0, commendId.length - 4);
        var replyContent = $("#" + commendId + "rpc" + num).val();
        if (trim(replyContent) == "") {
            alert("请填写回复内容");
        }
        else {
            var date = new Date();
            date = date.toLocaleDateString();
            $.post("/TraverTalk/AddCommentReplay", { "comId": commendId, "ReplayContent": replyContent }, function (replyId) {
                AddReplyContentDiv(commendId, num, replyId, replyContent, date);
            });
        }
    });
}

//添加回复内容div
function AddReplyContentDiv(commendId, num, replyId, replyContent, replyDate) {
    $("#" + commendId + 'ttb' + num + " a").after(
                '<div class="replyContent_Show">' +
                    '<div class="fl replyContent_userhead">' +
                         '<img src="../../Content/travertalk/images/userhead.png" />' +
                     '</div>' +
                     '<div class="fr replyContent_talk">' +
                       '<span class="replyContent_say">' + replyContent + '</span>' +
                       '<span class="">' + '(' + replyDate + ')' + '</span>' +
                     '</div>' +
                   '</div>'
            );
}
//删除留言
function deleteTraverTalk() {
    $(".del").live("click", function () {
        if (confirm("确认删除留言")) {
            var div = $(this).parent().parent();
            var comId = $(this).parent().attr("id");
            comId = comId.substring(0, comId.length - 3);
            $.post("/TraverTalk/DelComment", { "comId": comId }, function (result) {
                if (result == "true") {
                    alert("删除成功");
                    div.hide();

                }
                else {
                    alert("操作数据库失败");
                }
            });
        }
    }
    );
}

//收藏留言
function collectComment() {
    $(".col").live("click",function () {
        var comId = $(this).parent().attr("id");
        comId = comId.substring(0, comId.length - 3);
        $.post("/TraverTalk/CollectCommnet", { "comId": comId }, function (result) {
            if (result == "true") {
                alert("收藏成功");
            }
            else {
                alert("操作数据库失败");
            }
        });
    });
}
function set_time(date)    /**获取当前时间**/
{
    var year = "", month = "", day = "";
    date = new Date(Date.parse(date));
    year = date.getFullYear();
    month = date.getMonth() + 1;
    day = date.getDate();
    var timeNew = year + "年" + month + "月" + day + "日";
    return timeNew;
}
function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;

}




/*第一种形式 第二种形式 更换显示样式*/
function setTab(name, cursel, n) {
    for (i = 1; i <= n; i++) {
        var menu = document.getElementById(name + i);
        var con = document.getElementById("con_" + name + "_" + i);
        var page = document.getElementById("traverTalkPage" + i);
        menu.className = i == cursel ? "hover" : "";
        con.style.display = i == cursel ? "block" : "none";
        page.style.display = i != cursel ? "none" : "block";

    }
    $("#con_three_4").hide();
}

//分页
function Page(comMark, num) {
    var pagecount = 0;
    $.post("/TraverTalk/GetCommentNumByScenicZoneId", { "scenicZoneId": getCookie("scenicZoneId"), "comMark": comMark }, function (comNum) {
        if (parseInt(comNum) % 4 != 0) {
            pagecount = Math.floor(parseInt(comNum) / 4) + 1;
        }
        else {
            pagecount = Math.floor(parseInt(comNum) / 4);
        }
        if (comNum > 0) {
            $("#traverTalkPage" + num).paginate({
                count: pagecount,
                start: 1,
                display: Math.floor(pagecount / 2) + 1,
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
                    $("#con_three_" + num).empty();
                    if (num == '1') {
                        GetTraverTalk(page);
                    }
                    else if (num == '2') {
                        GetCollectTraverTalk(page);
                    }
                    else {
                        GetHelpTraverTalk(page);
                    }
                }
            });
            if (num > 1) {
                $("#traverTalkPage" + num).hide();
            }
        }
        else {
            $("#con_three_" + num).prepend('<span class="no_info">没有留言信息<span>');
        }
    });
}

/** 修剪字串前后的空格  */
function trim(s) {
    var count = s.length;
    var st = 0;       // start   
    var end = count - 1; // end   

    if (s == "") return s;
    while (st < count) {
        if (s.charAt(st) == " ")
            st++;
        else
            break;
    }
    while (end > st) {
        if (s.charAt(end) == " ")
            end--;
        else
            break;
    }
    return s.substring(st, end + 1);
}
//获取景区某个时间的留言