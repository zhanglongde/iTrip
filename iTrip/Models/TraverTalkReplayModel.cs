using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class TraverTalkReplayModel
    {
        public int DecID { get; set; }//留言ID
        public int ReplyID { get; set; }//回复ID
        public string ReplyContent { get; set; }//回复内容
        public string ReplyTime { get; set; }//回复时间
    }

}