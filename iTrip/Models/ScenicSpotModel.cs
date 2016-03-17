using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class ScenicSpotModel
    {
        public int ScenicZoneID { get; set; }//景区ＩＤ
        public int ScenicSpotID { get; set; }//景点ID
        public string ScenicSpotName { get; set; }//景点名称
        public string ScenicSpotIntroduce { get; set; }//景区介绍

    }
}