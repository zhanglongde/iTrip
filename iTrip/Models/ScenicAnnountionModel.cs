using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class ScenicAnnountionModel
    {
        public int DecID { get; set; }//公告ID
        public int ScenicZoneID { get; set; }//景区ID
        public string DecContent { get; set; }//公告内容
        public DateTime DecTime { get; set; }//公告时间
        public string Title { get; set; }//公告标题
    }
}