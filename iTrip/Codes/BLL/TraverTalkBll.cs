using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using iTrip.Codes;
using iTrip.Codes.DAL;
using System.Data;


namespace iTrip.Codes.BLL
{
    public class TraverTalkBll
    {
        TraverTalkDal ttd = new TraverTalkDal();
        /// <summary>
        /// 功能：获取景区留言 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicZoneId"></param>
        /// <returns></returns>
        public DataTable GetScenicZoneCommentBll(int scenicZoneId,int pageNum,string comMark)
        {
            return ttd.GetScenicZoneCommentDal(scenicZoneId,pageNum,comMark);
        }

        /// <summary>
        /// 功能：获取景区留言数量 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicZoneId"></param>
        /// <returns></returns>
        public int GetCommentNumByScenicZoneIdBll(int scenicZoneId,string comMark)
        {
            return ttd.GetCommentNumByScenicZoneIdDal(scenicZoneId,comMark);
        }

        /// <summary>
        ///功能： 删除留言 编辑者：吴国顺
        /// </summary>
        /// <param name="comId">留言ID</param>
        /// <returns></returns>
        public bool DelCommentBll(int comId)
        {
            return ttd.DelCommentDal(comId);
        }

        /// <summary>
        /// 功能：收藏留言 编辑者：吴国顺
        /// </summary>
        /// <param name="comId">留言ID</param>
        /// <returns></returns>
        public bool CollectCommentBll(int comId)
        {
            return ttd.CollectCommentDal(comId);
        }

        /// <summary>
        /// 功能：添加留言回复  编辑者：吴国顺
        /// </summary>
        /// <param name="ttm">留言回复类</param>
        /// <returns></returns>
        public string AddCommentReplayBll(TraverTalkReplayModel ttrm)
        {
            return ttd.AddCommentReplayDal(ttrm);
        }

        /// <summary>
        /// 功能：获取留言回复信息 编辑者：吴国顺
        /// </summary>
        /// <param name="comId">留言ID</param>
        /// <returns>留言回复信息</returns>
        public DataTable GetCommentReplyBll(int comId)
        {
            return ttd.GetCommentReplyDal(comId);
        }

        /// <summary>
        /// 功能：获取七天内留言 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicZoneId">景区ID</param>
        /// <param name="last">留言开始位置</param>
        /// <param name="count">留言数量</param>
        /// <returns>留言内容</returns>
        public DataTable GetCommentInWeek(int scenicZoneId, int last, int count)
        {
            return ttd.GetCommentInWeekDal(scenicZoneId,last,count);
        }

        // <summary>
        /// 功能：获取景点留言 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicSpotId">景点ID</param>
        /// <returns>某景点留言</returns>
        public DataTable GetCommentByScenicSpotIdBll(int scenicSpotId)
        {
            return ttd.GetCommentByScenicSpotIdDal(scenicSpotId);
        }
    }
}