using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using System.Data;
using System.Data.SqlClient;
namespace iTrip.Codes.DAL
{
    public class ScenicAnnountionDal
    {
        /// <summary>
        ///  编辑者：张龙德 功能：通过用户名在用户表中获取景区ID
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int getScenicSpotIdByUsernameDAL(string userName)
        {
            SqlParameter[] prams = new SqlParameter[]{
            new SqlParameter("@userName",SqlDbType.NVarChar,50)
            };
            prams[0].Value = userName;
            DataTable dt = SQLHelper.QueryBySqlText("SELECT scenicZoneID from tb_user where userName=@userName ", prams);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        /// <summary>
        /// 编辑者：张龙德 功能：添加公告信息
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string AddAnnountionDal(ScenicAnnountionModel su)
        {

            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicZoneID",SqlDbType.Int),
                new SqlParameter("@decContent",SqlDbType.NVarChar,500),
                new SqlParameter("@decTime",SqlDbType.DateTime),
                new SqlParameter("@title",SqlDbType.NVarChar,100),
            };
            prams[0].Value = su.ScenicZoneID;
            prams[1].Value = su.DecContent;
            prams[2].Value = su.DecTime;
            prams[3].Value = su.Title;
            DataTable dt = SQLHelper.QueryBySqlProc("AddAnnountionPro", prams);
            return dt.Rows[0][0].ToString();
        }
        /// <summary>
        /// 编辑者：张龙德   功能：选择所有公告信息
        /// </summary>
        /// <returns>ScenicAnnountionModel公告信息实体</returns>
        public static List<ScenicAnnountionModel> selectAllAnnountionsDAL()
        {
            DataTable dt = SQLHelper.QueryBySqlText("SELECT * FROM tb_declaration ");
            List<ScenicAnnountionModel> sams = new List<ScenicAnnountionModel>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime DecTime = Convert.ToDateTime(row[3]);
                string Title = Convert.ToString(row[4]);
                sams.Add(new ScenicAnnountionModel
                {
                    DecID = Convert.ToInt32(row[0]),
                    ScenicZoneID = Convert.ToInt32(row[1]),
                    DecContent = Convert.ToString(row[2]),
                    DecTime = Convert.ToDateTime(row[3]),
                    Title = Convert.ToString(row[4])
                });
            }
            return sams;
        }
        /// <summary>
        /// 编辑者：张龙德   功能：根据页码选择公告信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> select4AnnountionByPagingDAL(int scenicSpotID, int page)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int),
               new SqlParameter("@firthTerm",SqlDbType.Int),
               new SqlParameter("@lastTerm",SqlDbType.Int)
            };
            int firthTerm = (page - 1) * 4 + 1;
            int lastTerm = page * 4;
            prams[0].Value = scenicSpotID;
            prams[1].Value = firthTerm;
            prams[2].Value = lastTerm;
            DataTable dt = SQLHelper.QueryBySqlText(@"select decId,scenicZoneID,decContent,decTime,title from(select *,ROW_NUMBER() OVER(ORDER BY decId desc ) AS RowNumber 
                                     from tb_declaration where scenicZoneID=@scenicSpotID)as abstract_table 
                                     where RowNumber BETWEEN @firthTerm and @lastTerm ", prams);
            List<ScenicAnnountionModel> sams = new List<ScenicAnnountionModel>();
            foreach (DataRow row in dt.Rows)
            {
                sams.Add(new ScenicAnnountionModel
                {
                    DecID = Convert.ToInt32(row[0]),
                    ScenicZoneID = Convert.ToInt32(row[1]),
                    DecContent = Convert.ToString(row[2]),
                    DecTime = Convert.ToDateTime(row[3]),
                    Title = Convert.ToString(row[4])
                });
            }
            return sams;
        }
        /// <summary>
        /// 编辑者：张龙德   功能：获得公告的页数
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <returns></returns>
        public static int getPageAmountDAL(int scenicSpotID)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int)
            };
            prams[0].Value = scenicSpotID;
            DataTable dt = SQLHelper.QueryBySqlText(" select count(*)from  tb_declaration  where scenicZoneID=@scenicSpotID", prams);
            int pageAmount = Convert.ToInt32(dt.Rows[0][0]) / 4;
            int surplus = Convert.ToInt32(dt.Rows[0][0]) % 4;
            if (surplus != 0)
            {
                pageAmount++;
            }
            return pageAmount;
        }
        /// <summary>
        /// 编辑者：张龙德   功能：选择一周内公告信息
        /// </summary>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> selectWeekAnnountionAnnountionsDAL(int scenicSpotID)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int),
            };
            prams[0].Value = scenicSpotID;
            DataTable dt = SQLHelper.QueryBySqlText(@"SELECT  top 3  *  FROM   dbo.tb_declaration  
            WHERE  ( decTime   > DateAdd(d,-7,GetDate())) and (scenicZoneID=10)", prams);
            List<ScenicAnnountionModel> sams = new List<ScenicAnnountionModel>();
            foreach (DataRow row in dt.Rows)
            {
                sams.Add(new ScenicAnnountionModel
                {
                    DecID = Convert.ToInt32(row[0]),
                    ScenicZoneID = Convert.ToInt32(row[1]),
                    DecContent = Convert.ToString(row[2]),
                    DecTime = Convert.ToDateTime(row[3]),
                    Title = Convert.ToString(row[4])
                });
            }
            return sams;
        }
        /// <summary>
        /// 编辑者：张龙德   功能：获得更多一周内公告信息
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <param name="last"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> getMore3MonthAnnountionAnnountionsDAL(int scenicSpotID, int last, int count)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int),
                new SqlParameter("@last",SqlDbType.Int),
                new SqlParameter("@count",SqlDbType.Int)
            };
            prams[0].Value = scenicSpotID;
            prams[1].Value = last;
            prams[2].Value = count;
            DataTable dt = SQLHelper.QueryBySqlText(@"SELECT  decId,scenicZoneID,decContent,decTime,title  from 
            (select ROW_NUMBER() over(order by decId desc) as RowNumber,* FROM   dbo.tb_declaration 
            WHERE  (datediff(month,decTime,getdate())<=3) and (scenicZoneID=@scenicSpotID))A 
             where(RowNumber between @last+1 and @last+@count)", prams);
            List<ScenicAnnountionModel> sams = new List<ScenicAnnountionModel>();
            foreach (DataRow row in dt.Rows)
            { //( decTime   > DateAdd(d,-7,GetDate()))
                sams.Add(new ScenicAnnountionModel
                {
                    DecID = Convert.ToInt32(row[0]),
                    ScenicZoneID = Convert.ToInt32(row[1]),
                    DecContent = Convert.ToString(row[2]),
                    DecTime = Convert.ToDateTime(row[3]),
                    Title = getFitWordLength(Convert.ToString(row[4]))
                });
            }


            return sams;
        }
        public static int bytelenght(string str)
        {
            byte[] bytestr = System.Text.Encoding.Unicode.GetBytes(str);
            int j = 0;
            for (int i = 0; i < bytestr.GetLength(0); i++)
            {
                if (i % 2 == 0)
                {
                    j++;
                }
                else
                {
                    if (bytestr[i] > 0)
                    {
                        j++;
                    }
                }
            }
            return j;
        }
        public static string getFitWordLength(string title)
        {
            if (bytelenght(title) > 30)
            {
                return title.Substring(0, 13) + "......";
            }
            else
            {
                return title;
            }
        }
        /// <summary>
        ///  编辑者：张龙德   功能：选择一个月内公告信息
        /// </summary>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> selectMonthAnnountionAnnountionsDAL(int scenicSpotID)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int)
            };
            prams[0].Value = scenicSpotID;
            DataTable dt = SQLHelper.QueryBySqlText("select top 3 * from dbo.tb_declaration where (datediff(month,decTime,getdate())<=1) and (scenicZoneID=@scenicSpotID)", prams);
            List<ScenicAnnountionModel> sams = new List<ScenicAnnountionModel>();
            foreach (DataRow row in dt.Rows)
            {
                sams.Add(new ScenicAnnountionModel
                {
                    DecID = Convert.ToInt32(row[0]),
                    ScenicZoneID = Convert.ToInt32(row[1]),
                    DecContent = Convert.ToString(row[2]),
                    DecTime = Convert.ToDateTime(row[3]),
                    Title = Convert.ToString(row[4])
                });
            }
            return sams;

        }
        /// <summary>
        /// 编辑者：张龙德   功能：选择三个月内公告信息
        /// </summary>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> select3MonthAnnountionAnnountionsDAL(int scenicSpotID)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int)
            };
            prams[0].Value = scenicSpotID;
            DataTable dt = SQLHelper.QueryBySqlText("select top 3 * from tb_declaration where (datediff(month,decTime,getdate())<=3) and (scenicZoneID=@scenicSpotID) ", prams);
            List<ScenicAnnountionModel> sams = new List<ScenicAnnountionModel>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime DecTime = Convert.ToDateTime(row[3]);
                string Title = Convert.ToString(row[4]);
                sams.Add(new ScenicAnnountionModel
                {
                    DecID = Convert.ToInt32(row[0]),
                    ScenicZoneID = Convert.ToInt32(row[1]),
                    DecContent = Convert.ToString(row[2]),
                    DecTime = Convert.ToDateTime(row[3]),
                    Title = Convert.ToString(row[4])
                });
            }
            return sams;
        }
        /// <summary>
        /// 编辑者：张龙德   功能：删除公告信息
        /// </summary>
        /// <param name="title">公告标题</param>
        /// <returns></returns>
        public static int deleteAnnontionDAL(int AnnountionDecID)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@AnnountionDecID",SqlDbType.Int)
            };
            prams[0].Value = Convert.ToInt32(AnnountionDecID);
            bool result = SQLHelper.ExcuteNonQueryBySqlText("Delete From tb_declaration WHERE decId=@AnnountionDecID", prams);
            if (result == true)
                return 1;
            return 0;
        }
        /// <summary>
        /// 编辑者：张龙德   功能：修改公告信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool ReviseAnnontionDAL(ScenicAnnountionModel sam, string annountionDecID)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@title",SqlDbType.NVarChar,100),
                new SqlParameter("@decContent",SqlDbType.NVarChar,500),
                new SqlParameter("@decTime",SqlDbType.DateTime),
                new SqlParameter("@annountionDecID",SqlDbType.Int)
            };
            prams[0].Value = sam.Title.Trim();
            prams[1].Value = sam.DecContent.Trim();
            prams[2].Value = sam.DecTime;
            prams[3].Value = Convert.ToInt32(annountionDecID.Trim());
            bool result = SQLHelper.ExcuteNonQueryBySqlText("update tb_declaration set  title=@title ,decContent=@decContent , decTime=@decTime  where decId=@annountionDecID", prams);
            return result;
        }
        /// <summary>
        /// 编辑者：张龙德   功能：传入公告ID，获得公告信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static ScenicAnnountionModel queryContentByDecIdDAL(string DecId)
        {

            SqlParameter[] prams = new SqlParameter[] { 
               new SqlParameter("@DecId",SqlDbType.Int) 
            };
            prams[0].Value = Convert.ToInt32(DecId);     
            DataTable dt = SQLHelper.QueryBySqlText("SELECT *  FROM  tb_declaration WHERE  decId=@DecId ", prams);
            
            ScenicAnnountionModel sams = new ScenicAnnountionModel();
            sams.DecID= Convert.ToInt32(dt.Rows[0][0]);
            sams.ScenicZoneID = Convert.ToInt32(dt.Rows[0][1]);
            sams.DecContent = Convert.ToString(dt.Rows[0][2]);
            sams.DecTime = Convert.ToDateTime(dt.Rows[0][3]);
            sams.Title = Convert.ToString(dt.Rows[0][4]);
         
            return sams;
        }

    }
}