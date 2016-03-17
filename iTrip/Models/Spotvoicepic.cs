using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class Spotvoicepic
    {
        public int ScenicZoneID { get; set; }//景区ＩＤ
        public int ScenicSpotID { get; set; }//景点ID
        public string ScenicSpotName { get; set; }//景点名称
        public string ScenicSpotIntroduce { get; set; }//景区介绍
        public string StorePath { get; set; }//存储路径
        public string PicturePath { get; set; }//图片路径
    }
}