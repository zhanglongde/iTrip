var chart;
var PostId = 1;
var ScenicSpotName = new Array(); // '软件学院'
//scenicSpotId=[1,2,3,4,5,6,7,8,9,10];
var scenicSpotId = new Array();
ScenicSpotName = ["软件学院", "音乐厅", "校团委", "青春剧场", "图书馆", "室内体育馆", "学生街", "网球场", "共青团广场", "文化街"];
var title = '实时客流量';
for (var i = 0; i < 10; i++) {
    scenicSpotId[i] = i + 1;
    //  alert(scenicSpotId[i]);
}
//window.onload=function(){
//    getIDs();
//    getNames();
//};
////获取景点id
//        function getIDs(){
//            $.post("/TrafficStatistics/GetScenicSpotID",function(result){
//              if(result=null)
//              {
//                alert("fail");
//              }
//              else{
//                      result = result.split("/");
//                        var size = result.length;
//                        scenicSpotId.length = size
//                        //alert(hnum.length);
//                        for (var i = 0; i < size; i++) {
//                            scenicSpotId[i] = parseInt(result[i]); //将字符串类型转换成int类型再保存到hnum中
//                            
//                        }
//                        
//              }

//        });
//        }
//        //获取景点名
//        function getNames(){
//              $.post("/TrafficStatistics/GetScenicSpotName",function(result){
//                  if(result=null)
//                  {
//                    alert("fail");
//                  }
//                  else{
//                           result = result.split("/");
//                            var size = result.length;
//                            ScenicSpotName.length = size
//                            //alert(hnum.length);
//                            for (var i = 0; i < size; i++) {
//                                ScenicSpotName[i] = parseInt(result[i]); //将字符串类型转换成int类型再保存到hnum中
//                            }
//                  }

//            });
//        }
$(function () {
    $(document).ready(function () {
        Highcharts.setOptions({
            global: {
                useUTC: false
            }
        });

        //获取点击景点的景点名和id并且更改highchart对应属性
        //        $(".ScenicSpotName").live("click", function () {
        //            scenicSpotId = $(this).next().text();
        //            ScenicSpotName = $(this).text();
        //            //title = ScenicSpotName + '实时客流量';
        //            //changeTitle();
        //        });


        //更改表格标题
        function changeTitle() {
            chart.setTitle({ text: title }); //更改实时图的标题
            //hchart.setTitle({ text: htitle }); //更改历史图的标题
        }


        //获取数据
        var num = 0;
        function getData() {
            $.post("/TrafficStatistics/Count", { scenicSpotId: PostId }, function (data) {
                if (num == null) {
                    alert("fail !");
                }
                else {
                    num = data;
                }
            });
            return num;
        }
        //end

        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'Real-time flow',
                type: 'spline',
                marginRight: 10,
                events: {
                    load: function () {
                        //获取所有的景点名和景点id
                        //                        getIDs();
                        //                        getNames();
                        //                        alert(scenicSpotId[0]);
                        //声明series的数据
                        //                        var data = new Array();         //先声明一维
                        //                        for (var i = 0; i < 5; i++) {          //一维长度为5
                        //                            datas[i] = new Array();    //在声明二维
                        //                            for (var j = 0; j < 5; j++) {      //二维长度为5
                        //                                datas[i][j] = 0;
                        //                            }
                        //                        }
                        var datax = new Array();
                        var datay = new Array();
                        var dataSeries = new Array();
                        var time = (new Date()).getTime();
                        //为datax,datay赋值
                        for (var i = -10; i < 0; i++) {
                            datax[i + 10] = time + i * 5000;
                            datay[i + 10] = 0;
                            dataSeries.push({
                                x: time + i * 1000,
                                y: 0
                            });
                        }
                        for (i = 0; i < 10; i++) {
                            this.addSeries({ name: ScenicSpotName[i], data: dataSeries });
                        }

                        // 更新chart每隔5秒
                        var series = new Array(); //记录所有Series
                        //var series = this.series[0];
                        for (i = 0; i < 10; i++) {
                            series[i] = this.series[i];
                        }
                        setInterval(function () {
                            for (i = 0; i < 10; i++) {
                                var x = (new Date()).getTime(); // current time
                                PostId = scenicSpotId[i];
                                //alert(scenicSpotId[i]);
                                var y = getData();
                                // alert(y);
                                series[i].addPoint([x, y], true, true);
                            }
                        }, 5000);
                    }
                }
            },
            title: {
                text: title
            },
            xAxis: {
                type: 'datetime',
                tickPixelInterval: 150
            },
            yAxis: {
                max: 11,
                min: -1,
                allowDecimals: false,
                title: {
                    text: '人数/个'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {  //移动到数据点时显示的提示格式设置
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                        Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                        Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: true ,//默认为true，绝对是否要在图表下显示说明/图例
                itemWidth :130,
                itemStyle: {
                    paddingBottom: '10px'
                }
            },
            exporting: {
                enabled: true
            },
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
                                    maincontentText: "x轴：" + Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '</p>' + "y轴：" + this.y,
                                    width: 200
                                });
                            }
                        }
                    },
                    marker: {
                        lineWidth: 1
                    }
                }
            }
            //                    series: [{
            //                        name: ScenicSpotName[0],
            //                        data: (function () {
            //                            // generate an array of random data
            //                            var data = [],
            //                                time = (new Date()).getTime(),
            //                                i;

            //                            for (i = -4; i <= 0; i++) {
            //                                data.push({
            //                                    x: time + i * 1000,
            //                                    y: 0
            //                                });
            //                            }
            //                            return data;
            //                        })()
            //                    }]
        });
    });



});





