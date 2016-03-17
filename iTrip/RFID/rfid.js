/// <reference path="../Scripts/jquery-1.4.4.js" />
$(function () {
    $("#open").click(function () {
        if ($("#choseCom").val() != "") {
            $.post("/RFID/StartRfid", { "portNo": $("#choseCom").val() }, function (data) {
                alert(data);
            });
        }
        else {
            alert("先选择串口号");
        }
    });
    $("#close").click(function () {
        $.post("/RFID/CloesRFID", function () {
            alert("关闭成功");
        });
    });
    $("#stopReader").click(function () {
        $.post("/RFID/StopReader", function (data) {
            alert(data);
        });
    });
    $("#openMulReadCard").click(function () {
        $.post("/RFID/MultiReadMod", function () {

        });
    });
    setInterval(function () {
        $.post("/RFID/MultiReadRFID", function (data) {

        });
    }, 1000);
})