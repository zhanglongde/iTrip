using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class TraverTalkModel
    {
        public int CommentID { get; set; }//留言ID
        public int ScenicSpotID{get;set;}//景点ID
        public string ScenicSpotName { get; set; }//景点名
        public string UserName { get; set; }//用户名
        public string CmtContent { get; set; }//留言内容
        public string PicturePath { get; set; }//图片地址
        public string CmtTime { get; set; }//发表时间
    }
}