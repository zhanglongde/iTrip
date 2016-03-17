using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using System.Data;
using System.Data.SqlClient;

namespace iTrip.Codes.DAL
{
    public class ScenicSpotDal
    {

        /// <summary>
        /// 编辑者：杨志君 功能：添加景点信息
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        public string AddScenicSpotDAL(string spotName, string spotIntroduce, int zoneID, string picPath, string sPath)
        {

            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotName",SqlDbType.VarChar,50),
                new SqlParameter("@scenicSpotIntroduce",SqlDbType.Text),
                //new SqlParameter ("@recommandPath",SqlDbType.VarChar,50),
                new SqlParameter("@scenicZoneID",SqlDbType.Int),
               new SqlParameter("@picturePath",SqlDbType.VarChar,200),
               new SqlParameter("@voicePath",SqlDbType.VarChar,200)
          
               
            };
            prams[0].Value = spotName;
            prams[1].Value = spotIntroduce;

            prams[2].Value = zoneID;
            prams[3].Value = picPath;
            prams[4].Value = sPath;
            DataTable dt = SQLHelper.QueryBySqlProc("spot", prams);
            return dt.Rows[0][0].ToString();
            //return SQLHelper.ExcuteNonQueryBySqlText("INSERT INTO tb_scenicSpot(scenicSpotName, scenicSpotIntroduce, scenicZoneID,picture)VALUES   (@scenicSpotName,@scenicSpotIntroduce,@scenicZoneID,@picture)", prams);
        }
        /// <summary>
        /// 编辑者：杨志君 功能：显示景点信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable ModifyScenic(string scenicSpotID)
        {

            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicSpotID",SqlDbType.Int),
                //new SqlParameter("@conent",SqlDbType.VarChar,50),
                //new SqlParameter("@nowtime",SqlDbType.DateTime),      
            };



            prams[0].Value = scenicSpotID;
            //prams[1].Value = conent;
            //prams[2].Value = DateTime.Now;
            DataTable dt = new DataTable();
            dt = SQLHelper.QueryBySqlProc("Showspot", prams);
            return dt;
        }
        /// <summary>
        /// 编辑者：杨志君 功能：下面显示已有景点信息
        /// </summary>
        /// <param name="scenicid"></param>
        /// <returns></returns>
        public DataTable SpotshowDal(string scenicid)
        {
            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicZoneID",SqlDbType.Int),    
            };
            prams[0].Value = Convert.ToInt32(scenicid);
            DataTable dt = new DataTable();
            dt = SQLHelper.QueryBySqlProc("AllSpot", prams);
            return dt;

        }
        /// <summary>
        /// 编辑者：杨志君 功能：修改景点信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="counts"></param>
        /// <returns></returns>
        public bool ScenicModifyinfo(int scenicSpotID, string ScenicSpotName, string ScenicSpotIntroduce, string picture, string sound)
        {
            bool result = false;
            SqlParameter[] prams = new SqlParameter[] { 
                  new SqlParameter("@scenicSpotID",SqlDbType.Int),
                new SqlParameter("@scenicSpotName",SqlDbType.VarChar,50),
                new SqlParameter("@scenicSpotIntroduce",SqlDbType.Text),
                //new SqlParameter ("@recommandPath",SqlDbType.VarChar,50),
                //new SqlParameter("@scenicZoneID",SqlDbType.VarChar,100),
               
            new SqlParameter("@picturePath",SqlDbType.VarChar,200),
            new SqlParameter("@voicePath",SqlDbType.VarChar,200)
               
            };
            prams[0].Value = scenicSpotID;
            prams[1].Value = ScenicSpotName;
            prams[2].Value = ScenicSpotIntroduce;
            prams[3].Value = picture;
            prams[4].Value = sound;
            result = SQLHelper.ExcuteNonQueryBySqlProc("modifyspot ", prams);
            return result;
        }

        /// <summary>
        /// 编辑者：杨志君 功能：删除景点信息
        /// </summary>
        /// <param name="nowt"></param>
        /// <returns></returns>
        public bool ScenicDeleteDal(int scenicspotid)
        {

            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicSpotID",SqlDbType.Int),   
            };
            prams[0].Value = scenicspotid;
            return SQLHelper.ExcuteNonQueryBySqlProc("deletespot", prams);
        }
    }
}