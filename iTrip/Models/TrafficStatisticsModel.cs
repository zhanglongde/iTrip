using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class TrafficStatisticsModel
    {
        public int InfoStatisticsID { get; set; }//信息统计ID
        public int ScenicSpotID { get; set; }//景点ID
        public int UserID { get; set; }//用户ID
        public DateTime EnterTime { get; set; }//进入时间
        public DateTime OutTime { get; set; }//出去时间
    }
}