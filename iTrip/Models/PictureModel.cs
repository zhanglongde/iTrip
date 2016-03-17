using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class PictureModel
    {
        public int PictrueID { get; set; }//图片ID
        public int ScenicZoneID { get; set; }//景区ID
        public int ScenicSpotID { get; set; }//景点ID
        public string PicturePath { get; set; }//图片路径
    }
}