using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTrip.Codes.BLL;
using System.Data;
using iTrip.Models;
using System.Web.Script.Serialization;

namespace iTrip.Controllers
{
    public class TraverTalkController : Controller
    {
        //
        // GET: /TraverTalk/
        TraverTalkBll ttb = new TraverTalkBll();
        TraverTalkReplayModel ttrm = new TraverTalkReplayModel();
        public ActionResult TraverTalk()
        {
            return View();
        }
        [HttpPost]
        //获取景区留言
        public object GetScenicZoneComment()
        {
            int scenicZoneId = Convert.ToInt32(Request.Form["scenicZoneId"]);
            int pageNum = Convert.ToInt32(Request.Form["pageNum"]);
            string comMark = Request.Form["comMark"].ToString();
            DataTable dt = ttb.GetScenicZoneCommentBll(scenicZoneId,pageNum,comMark);
            List<TraverTalkModel> lttm = new List<TraverTalkModel>();
            for(int i=0;i<dt.Rows.Count;i++)
            {
               lttm.Add(new TraverTalkModel{
                  CommentID=Convert.ToInt32(dt.Rows[i]["commentID"]),
                  CmtContent = dt.Rows[i]["cmt_content"].ToString(),
                  PicturePath = dt.Rows[i]["picturePath"].ToString(),
                  CmtTime=dt.Rows[i]["cmtTime"].ToString(),
                  ScenicSpotName = dt.Rows[i]["scenicSpotName"].ToString()
               });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(lttm);
        }
        [HttpPost]
        public object GetCommentByScenicSpotId()
        {
            int scenicSpotId = Convert.ToInt32(Request.Form["scenicSpotId"]);
            DataTable dt = ttb.GetCommentByScenicSpotIdBll(scenicSpotId);
            List<TraverTalkModel> lttm = new List<TraverTalkModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lttm.Add(new TraverTalkModel
                {
                    CommentID = Convert.ToInt32(dt.Rows[i]["commentID"]),
                    CmtContent = dt.Rows[i]["cmt_content"].ToString(),
                    PicturePath = dt.Rows[i]["picturePath"].ToString(),
                    CmtTime = dt.Rows[i]["cmtTime"].ToString(),
                    ScenicSpotName = dt.Rows[i]["scenicSpotName"].ToString()
                });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(lttm);
        }
        [HttpPost]
        //获取一个星期内留言
        public object GetCommentInWeek(string id)
        {
            int last = Convert.ToInt32(Request.Form["last"]);
            int count = Convert.ToInt32(Request.Form["amount"]);
            DataTable dt = ttb.GetCommentInWeek(Convert.ToInt32(id),last,count);
            List<TraverTalkModel> lttm = new List<TraverTalkModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lttm.Add(new TraverTalkModel {
                    CommentID = Convert.ToInt32(dt.Rows[i]["commentID"]),
                    CmtContent = dt.Rows[i]["cmt_content"].ToString(),
                    PicturePath = dt.Rows[i]["picturePath"].ToString(),
                    CmtTime = dt.Rows[i]["cmtTime"].ToString(),
                    ScenicSpotName = dt.Rows[i]["scenicSpotName"].ToString()
                });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(lttm);
        }
        [HttpPost]
        //获取景区留言数量
        public int GetCommentNumByScenicZoneId()
        {
            int scenicZoneId = Convert.ToInt32(Request["scenicZoneId"]);
            string comMark = Request.Form["comMark"].ToString();
            return ttb.GetCommentNumByScenicZoneIdBll(scenicZoneId,comMark);
        }

        [HttpPost]
        //删除留言信息
        public string DelComment()
        {
            int comId = Convert.ToInt32(Request.Form["comId"]);
            if (ttb.DelCommentBll(comId) == true)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        [HttpPost]
        //收藏留言
        public string CollectCommnet()
        {
            int comId = Convert.ToInt32(Request.Form["comId"]);
            if (ttb.CollectCommentBll(comId) == true)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //添加留言回复
        public string AddCommentReplay()
        {
            ttrm.DecID = Convert.ToInt32(Request.Form["comId"]);
            ttrm.ReplyContent=Request.Form["ReplayContent"].ToString();
            return ttb.AddCommentReplayBll(ttrm);
        }
        //获取留言回复
        public object GetCommentReply()
        {
            int comId = Convert.ToInt32(Request.Form["comId"]);
            DataTable dt = ttb.GetCommentReplyBll(comId);
            List<TraverTalkReplayModel> ttrm = new List<TraverTalkReplayModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ttrm.Add(new TraverTalkReplayModel {
                    ReplyID = Convert.ToInt32(dt.Rows[i]["repId"]),
                    ReplyContent = dt.Rows[i]["replyContent"].ToString(),
                    ReplyTime = dt.Rows[i]["replyTime"].ToString()
                });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ttrm);
        }
    }
}
