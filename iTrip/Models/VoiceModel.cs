using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class VoiceModel
    {
        public int VoiceID { get; set; }//语音ID
        public int ScenicZoneID { get; set; }//景区ID
        public int ScenicSpotID { get; set; }//景点ID
        public string StorePath { get; set; }//存储路径
    }
}