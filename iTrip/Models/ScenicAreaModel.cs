using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class ScenicAreaModel
    {
        public int ScenicZoneID { get; set; }//景区ID
        public string ScenicZoneName { get; set; }//景区名
        public string ScenicZoneIntroduce { get; set; }//景区介绍
        public string ScenicTime { get; set; }//景区开放时间
        public string ScenicNotic { get; set; }//游客须知
        public string Scenicphone { get; set; }//景区联系方式
        public string RecommanPath { get; set; }//推荐路线地址
        public string ScenicZoneAdr { get; set; }//景区地址
        public string ScenicImg { get; set; }//景区图片
    }
}