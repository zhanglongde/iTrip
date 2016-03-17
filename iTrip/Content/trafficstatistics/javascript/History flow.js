var hchart;
var hnum = new Array();
var hScenicSpotName = '软件学院';
var hscenicSpotId = 1;
var htitle = hScenicSpotName + '历史客流量';
var searchCondition = (new Date()).getFullYear().toString();
$(document).ready(function () {
    //ajax设置成同步的
    $.ajaxSetup({
        async: false
    });
    //获取数据

    function hgetData() {
        $.post("/TrafficStatistics/HCount", { scenicSpotId: hscenicSpotId, searchCondition: searchCondition }, function (result) {
            if (result == null) {
                alert("fail !");
            }
            else {
                result = result.split("/");
                var size = result.length;
                hnum.length = size
                //alert(hnum.length);
                for (var i = 0; i < size; i++) {
                    hnum[i] = parseInt(result[i]); //将字符串类型转换成int类型再保存到hnum中
                }

            }
        });
        //alert(hnum);
        return hnum;
    }
    //end

    //获取点击景点的景点名和id并且更改highchart对应属性
    $(".ScenicSpotName").live("click", function () {
        hscenicSpotId = $(this).next().text();
        hScenicSpotName = $(this).text();
        htitle = hScenicSpotName + '实时客流量';
        changeTitle();
        searchCondition = (new Date()).getFullYear().toString();
        var d = new Array();
        d = hgetData();
        hchart.series[0].setData(d); //通过click事件重置表格数据
        hchart.xAxis[0].setCategories(['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']); //修改x轴categories值

    });


    //更改表格标题
    function changeTitle() {
        //chart.setTitle({ text: title }); //更改实时图的标题
        hchart.setTitle({ text: htitle }); //更改历史图的标题
    }
    //绑定页面上的ScenicSpotName，并为之添加click事件-重置表格数据
    //    $(".ScenicSpotName").live("click", function () {
    //        var d = new Array();
    //        d = hgetData();
    //        hchart.series[0].setData(d); //通过click事件重置表格数据
    //    });

    //规定历史流量加载条件
    $("#search").live("click", function () {
        selYear = document.getElementById("tYEAR");
        var selMonth = document.getElementById("tMON");
        var selDay = document.getElementById("tDAY");
        var selCondition = document.getElementById("Conditon");
        //获取选中的年月日以及搜索条件
        var year = selYear.options[selYear.selectedIndex].text;
        var month = selMonth.options[selMonth.selectedIndex].text;
        var day = selDay.options[selDay.selectedIndex].text;
        var condition = selCondition.options[selCondition.selectedIndex].text;
        //alert(year); alert(month); alert(day);alert(condition);
        //alert(Object.prototype.toString.apply(year)); //显示变量属性类型
        //alert(Object.prototype.toString.apply(searchCondition));
        //alert(searchCondition);
        if (condition == "年") {
            //alert(1);
            searchCondition = year;
            //alert(1);
            //hchart.xAxis[0].setCategories(['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']); //修改x轴categories值

        }
        else if (condition == "月") {
            // alert(2);
            if (month.length == 1) {
                month = '0' + month;
                //alert(month);
            }
            searchCondition = year + month;
            //alert(2);
            //hchart.xAxis[0].setCategories(['1-3号', '4-6号', '7-9号', '10-12号', '13-15号', '16-18号', '19-21号', '22-24号', '25-27号', '28-31号']); //修改x轴categories值
        }
        else if (condition == "日") {
            //alert(3);
            if (month.length == 1) {
                month = '0' + month;
                //alert(month);
            }
            if (day.length == 1) {
                day = '0' + day;
                //alert(day);
            }
            searchCondition = year + month + day;
            //alert(3);
            //hchart.xAxis[0].setCategories(['7-8点', '8-9点', '9-10点', '10-11点', '11-12点', '12-13点', '13-14点', '14-15点', '15-16点', '16-17点', '17-18点', '18-19点', '19-7点']); //修改x轴categories值
            //alert(searchCondition);
        }
        else {
            alert("error!");
        }
        //更改series数据
        var d = new Array();
        d = hgetData();
        //hchart.redraw();
        hchart.series[0].setData(d);
        if (condition == "年") {
            hchart.xAxis[0].setCategories(['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']); //修改x轴categories值

        }
        else if (condition == "月") {
            hchart.xAxis[0].setCategories(['1-3号', '4-6号', '7-9号', '10-12号', '13-15号', '16-18号', '19-21号', '22-24号', '25-27号', '28-31号']); //修改x轴categories值
        }
        else if (condition == "日") {
            hchart.xAxis[0].setCategories(['7-8点', '8-9点', '9-10点', '10-11点', '11-12点', '12-13点', '13-14点', '14-15点', '15-16点', '16-17点', '17-18点', '18-19点', '19-7点']); //修改x轴categories值
        }
        //hchart.xAxis[0].setCategories(['1-3号', '4-6号', '7-9号', '10-12号', '13-15号', '16-18号', '19-21号', '22-24号', '25-27号', '28-31号']); //修改x轴categories值




    });

    //highchart声明
    hchart = new Highcharts.Chart({
        chart: {
            renderTo: 'History flow',          //放置图表的容器
            defaultSeriesType: 'spline',
            events: {
                load: function () {
                    var hdata = new Array();
                    hdata = hgetData();
                    //alert(hdata);
                    this.addSeries({ name: hScenicSpotName, data: hdata });

                    //                    alert(hgetData());
                    //                    this.addSeries({ name: ScenicSpotName, data: (function () {
                    //                        var h = new Array();
                    //                        $.post("/TrafficStatistics/HCount", { scenicSpotId: scenicSpotId }, function (result) {
                    //                            if (result == null) {
                    //                                alert("fail !");
                    //                            }
                    //                            else {
                    //                                result = result.split("/");
                    //                                //alert(result);
                    //                                for (var i = 0; i < 12; i++) {
                    //                                    result[i] = parseInt(result[i]);
                    //                                }
                    //                                // alert(typeof(h));
                    //                            }
                    //                        });
                    //                        //console.log(h);
                    //                        return h;
                    //                    })()
                    //                    });
                }
            }
        },
        title: {
            text: htitle
        },
        subtitle: {
            text: '作者-123'
        },
        xAxis: {//X轴数据
            categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            labels: {
                rotation: 0, //字体倾斜
                align: 'right',
                style: { font: 'normal 13px 宋体' }
            }
        },
        yAxis: {//Y轴显示文字
            title: {
                text: '人数/个'
            }
        },
        tooltip: {  //提示
            enabled: true,
            formatter: function () {
                return '<b>' + this.x + '</b><br/>' + this.series.name + ': ' + Highcharts.numberFormat(this.y, 1);
            }
        },
        //        plotOptions: {
        //            line: {
        //                dataLabels: {
        //                    enabled: true
        //                },
        //                enableMouseTracking: true ,//是否显示提示
        //                series: {
        //                    cursor: 'pointer',
        //                    point: {
        //                        events: {
        //                            click: function () {
        //                                hs.htmlExpand(null, {
        //                                    pageOrigin: {
        //                                        x: this.pageX,
        //                                        y: this.pageY
        //                                    },
        //                                    headingText: this.series.name,
        //                                    maincontentText: this.category+ ':<br/> ' +
        //									this.y + ' visits',
        //                                    width: 200
        //                                });
        //                            }
        //                        }
        //                    },
        //                    marker: {
        //                        lineWidth: 1
        //                    }
        //                }
        //            }
        //        },
        plotOptions: {
            series: {
                cursor: 'pointer',
                point: {
                    events: {
                        click: function () {
                            hs.htmlExpand(null, {
                                pageOrigin: {
                                    x: this.pageX,
                                    y: this.pageY
                                },
                                headingText: this.series.name,
                                maincontentText: "x轴：" + this.category + "  " + "y轴：" + this.y,
                                width: 10
                            });
                        }
                    }
                },
                marker: {
                    lineWidth: 1
                }
            }
        },
        exporting: {
            enabled: true
        }
        //        series: [{
        //            name: ScenicSpotName,
        //            data: [null]
        //            data: [353, 574, 234, 347, 357, 753, 1002, 346, 658, 345, 565, 697]
        //        }]
    });

})

