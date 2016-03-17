using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using System.Data;
using System.Data.SqlClient;
namespace iTrip.Codes.DAL
{
    public class ScenicAreaDal
    {
        /// <summary>
        /// 编辑者：杨志君 功能：添加景区信息
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        public bool AddScenicAreaDAL(ScenicAreaModel um,string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicZoneName",SqlDbType.VarChar,50),
                new SqlParameter("@scenicZoneIntroduce",SqlDbType.Text),
                new SqlParameter("@scenicZoneAdr",SqlDbType.VarChar,100),
                new SqlParameter("@scenicZoneTime",SqlDbType.VarChar,50),
                new SqlParameter("@scenicZoneNotice",SqlDbType.VarChar,500),
                new SqlParameter("@scenicZonePhone",SqlDbType.VarChar,50),
                new SqlParameter("@userName",SqlDbType.VarChar,50)
               
            };
            prams[0].Value = um.ScenicZoneName;
            prams[1].Value = um.ScenicZoneIntroduce;
            prams[2].Value = um.ScenicZoneAdr;
            prams[3].Value = um.ScenicTime;
            prams[4].Value = um.ScenicNotic;
            prams[5].Value = um.Scenicphone;
            prams[6].Value = userName;
            return SQLHelper.ExcuteNonQueryBySqlProc("InsertScenicAreaInfo", prams);
        }

        public string[] GetScenicZoneInfoDal(string userName)
        {
            string[] scenicZoneInfo=new string[2];
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            DataTable dt = SQLHelper.QueryBySqlText("select scenicZoneID,scenicZoneName from dbo.tb_scenicZone where scenicZoneID=(select scenicZoneID from dbo.tb_user where userName=@userName)", prams);
            scenicZoneInfo[0]=dt.Rows[0][0].ToString();
            scenicZoneInfo[1] = dt.Rows[0][1].ToString();
            return scenicZoneInfo;
        }
    }
}