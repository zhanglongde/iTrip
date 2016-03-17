using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using System.Data;
using System.Data.SqlClient;

namespace iTrip.Codes.DAL
{
    public class TraverTalkDal
    {
        /// <summary>
        /// 功能：获取景区留言 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicZoneId"></param>
        /// <returns></returns>
        public DataTable GetScenicZoneCommentDal(int scenicZoneId, int pageNum, string comMark)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicZoneID",SqlDbType.Int), 
                new SqlParameter("@pageNum",SqlDbType.Int),
                new SqlParameter("@comMark",SqlDbType.NVarChar,2)
            };
            prams[0].Value = scenicZoneId;
            prams[1].Value = (pageNum - 1) * 4 + 1;
            prams[2].Value = comMark;
            if (comMark == "普通")
            {
                return SQLHelper.QueryBySqlText(@"select commentID, VisitorID, cmt_content,A.scenicSpotID,picturePath,cmtTime,comMark,scenicSpotName from (select ROW_NUMBER() over(order by commentID desc) as 
                                            RowNumber,* from dbo.tb_comment where scenicZoneID=@scenicZoneID)A,dbo.tb_scenicSpot
                                            where (RowNumber between @pageNum and @pageNum+3) and A.scenicSpotID=dbo.tb_scenicSpot.scenicSpotID", prams);
            }
            else
            {
                return SQLHelper.QueryBySqlText(@"select commentID, VisitorID, cmt_content,A.scenicSpotID,picturePath,cmtTime,comMark,scenicSpotName from 
                                         (select ROW_NUMBER() over(order by commentID desc) as RowNumber,* from dbo.tb_comment where
                                             scenicZoneID=@scenicZoneID and comMark=@comMark)A,dbo.tb_scenicSpot
                                           where (RowNumber between @pageNum and @pageNum+3) and A.scenicSpotID=dbo.tb_scenicSpot.scenicSpotID", prams);
            }

        }


        /// <summary>
        /// 功能：获取七天内留言 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicZoneId">景区ID</param>
        /// <param name="last">留言开始位置</param>
        /// <param name="count">留言数量</param>
        /// <returns>留言内容</returns>
        public DataTable GetCommentInWeekDal(int scenicZoneId, int last, int count)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicZoneID",SqlDbType.Int), 
                new SqlParameter("@last",SqlDbType.Int),
                new SqlParameter("@count",SqlDbType.Int)
            };
            prams[0].Value = scenicZoneId;
            prams[1].Value = last;
            prams[2].Value = count;
            return SQLHelper.QueryBySqlText(@"select commentID, VisitorID, cmt_content,A.scenicSpotID,picturePath,cmtTime,comMark,scenicSpotName from 
                                         (select ROW_NUMBER() over(order by commentID) as RowNumber,* from dbo.tb_comment where scenicZoneID=@scenicZoneID)A,
                                           dbo.tb_scenicSpot where (RowNumber between @last+1 and @last+@count)and A.scenicSpotID=dbo.tb_scenicSpot.scenicSpotID and DATEDIFF(DAY,cmtTime,GETDATE())>=7", prams);
        }
        /// <summary>
        /// 功能：获取留言总数 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicZoneId"></param>
        /// <returns></returns>
        public int GetCommentNumByScenicZoneIdDal(int scenicZoneId, string comMark)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicZoneID",SqlDbType.Int),
                new SqlParameter("@comMark",SqlDbType.NVarChar,2)
            };
            prams[0].Value = scenicZoneId;
            prams[1].Value = comMark;
            DataTable dt = new DataTable();
            if (comMark == "普通")
            {
                dt = SQLHelper.QueryBySqlText("select count(*) from dbo.tb_comment where scenicZoneID=@scenicZoneID", prams);
            }
            else
            {
                dt = SQLHelper.QueryBySqlText("select count(*) from dbo.tb_comment where scenicZoneID=@scenicZoneID and comMark=@comMark", prams);
            }
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 功能：删除留言 编辑者：吴国顺
        /// </summary>
        /// <param name="comId">留言ID</param>
        /// <returns></returns>
        public bool DelCommentDal(int comId)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@commentID",SqlDbType.Int),     
            };
            prams[0].Value = comId;
            return SQLHelper.ExcuteNonQueryBySqlText("delete from dbo.tb_comment where commentID=@commentID", prams);
        }

        /// <summary>
        /// 功能：收藏留言 编辑者：吴国顺
        /// </summary>
        /// <param name="comId"></param>
        /// <returns></returns>
        public bool CollectCommentDal(int comId)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@commentID",SqlDbType.Int),     
            };
            prams[0].Value = comId;
            return SQLHelper.ExcuteNonQueryBySqlText("update dbo.tb_comment set comMark='收藏' where commentID=@commentID", prams);
        }

        /// <summary>
        /// 功能：添加留言回复 编辑者：吴国顺
        /// </summary>
        /// <param name="ttm">留言回复类</param>
        /// <returns></returns>
        public string AddCommentReplayDal(TraverTalkReplayModel ttrm)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@comID",SqlDbType.Int),
                new SqlParameter("@replyContent",SqlDbType.VarChar,500),
                new SqlParameter("@replyTime",SqlDbType.DateTime)
            };
            prams[0].Value = ttrm.DecID;
            prams[1].Value = ttrm.ReplyContent;
            prams[2].Value = DateTime.Now;
            DataTable dt = SQLHelper.QueryBySqlProc("AddCommentReply", prams);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// 功能：获取留言回复信息  编辑者：吴国顺
        /// </summary>
        /// <param name="comId">留言ID</param>
        /// <returns>留言回复内容</returns>
        public DataTable GetCommentReplyDal(int comId)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@comID",SqlDbType.Int)
            };
            prams[0].Value = comId;
            return SQLHelper.QueryBySqlText("select * from dbo.tb_reply where commentID=@comId", prams);
        }

        /// <summary>
        /// 功能：获取景点留言 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicSpotId">景点ID</param>
        /// <returns>某景点留言</returns>
        public DataTable GetCommentByScenicSpotIdDal(int scenicSpotId)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicSpotId",SqlDbType.Int)
            };
            prams[0].Value = scenicSpotId;
            return SQLHelper.QueryBySqlText(@"select commentID, VisitorID, cmt_content,dbo.tb_comment.scenicSpotID,
                                picturePath,cmtTime,comMark,scenicSpotName from dbo.tb_comment,dbo.tb_scenicSpot
                                 where dbo.tb_comment.scenicSpotID=@scenicSpotId and dbo.tb_comment.scenicSpotID=dbo.tb_scenicSpot.scenicSpotID", prams);
        }
    }
}