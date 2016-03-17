using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using iTrip.Models;

namespace iTrip.Codes.DAL
{
    public class ModifyInformationDal
    {
        /// <summary>
        /// 获取用户信息和景区信息  编辑者：吴国顺
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public DataTable GetUserScenicZoneInfoDal(string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            return SQLHelper.QueryBySqlText(@"select userName, mobilephone, email, password,scenicZoneName, scenicZoneIntroduce, 
                                             recommandPath, scenicZoneAdr, scenicZoneTime, scenicZoneNotice, scenicZonePhone from dbo.tb_user,dbo.tb_scenicZone
                                             where userName=@userName and dbo.tb_scenicZone.scenicZoneID=(select scenicZoneID from dbo.tb_user where 
                                             userName=@userName)", prams);
        }
        /// <summary>
        /// 功能：获取用户信息 编辑者：吴国顺
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public DataTable GetUserInfoDal(string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            return SQLHelper.QueryBySqlText("select userName, mobilephone, email,headAdr,password from dbo.tb_user where userName=@userName", prams);
        }
        /// <summary>
        /// 功能：获取景区信息 编辑者：吴国顺
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public DataTable GetScenicZoneInfoDal(string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            return SQLHelper.QueryBySqlText(@"select scenicZoneName, scenicZoneIntroduce, 
                                             scenicZoneAdr, scenicZoneTime, scenicZoneNotice, scenicZonePhone,picturePath 
                                             from dbo.tb_scenicZone,dbo.tb_picture
                                              where dbo.tb_scenicZone.scenicZoneID=(select scenicZoneID from dbo.tb_user where 
                                              userName=@userName) and dbo.tb_scenicZone.scenicZoneID=dbo.tb_picture.scenicZoneID", prams);
        }

        /// <summary>
        /// 功能：修改用户景区信息 编辑者：吴国顺
        /// </summary>
        /// <param name="userNameOld">旧的用户名</param>
        /// <param name="um">用户类</param>
        /// <param name="sam"></param>
        /// <returns></returns>
        public bool ChangeUserScenicInfoDal(string userNameOld, UserModel um, ScenicAreaModel sam)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userNameOld",SqlDbType.VarChar,50),
                new SqlParameter("@userNameNew",SqlDbType.VarChar,50),
                new SqlParameter("@password",SqlDbType.VarChar,50),
                new SqlParameter("@email",SqlDbType.VarChar,50),

                new SqlParameter("@mobilephone",SqlDbType.VarChar,50),
                new SqlParameter("@scenicZoneName",SqlDbType.VarChar,50),
                new SqlParameter("@scenicZoneIntroduce",SqlDbType.Text),
                new SqlParameter("@scenicZoneAdr",SqlDbType.VarChar,100),
                new SqlParameter("@scenicZoneTime",SqlDbType.VarChar,50),
                new SqlParameter("@scenicZoneNotice",SqlDbType.VarChar,500),
                new SqlParameter("@scenicZonePhone",SqlDbType.VarChar,50),
                new SqlParameter("@headAdr",SqlDbType.VarChar,200),
                new SqlParameter("@scenicZoneImg",SqlDbType.VarChar,200)
            };
            prams[0].Value = userNameOld;
            prams[1].Value = um.UserName;
            prams[2].Value = um.Password;
            prams[3].Value = um.Email;
            prams[4].Value = um.Mobilephone;
            prams[5].Value = sam.ScenicZoneName;
            prams[6].Value = sam.ScenicZoneIntroduce;
            prams[7].Value = sam.ScenicZoneAdr;
            prams[8].Value = sam.ScenicTime;
            prams[9].Value = sam.ScenicNotic;
            prams[10].Value = sam.Scenicphone;
            prams[11].Value = um.HeadAdr;
            prams[12].Value = sam.ScenicImg;
            return SQLHelper.ExcuteNonQueryBySqlProc("ChangeUserScenicInfo", prams);
        }
    }
}