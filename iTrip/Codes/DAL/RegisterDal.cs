using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using System.Data;
using System.Data.SqlClient;

namespace iTrip.Codes.DAL
{
    public class RegisterDal
    {
        /// <summary>
        /// 编辑者：唐超 功能：判断用户名是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool IsUserNameInDal(string UserName)
        {
            bool result = false;
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = UserName;
            DataTable dt = SQLHelper.QueryBySqlText("select count(*) from tb_user where userName=@username",prams);
            if (dt.Rows[0][0].ToString()=="1")
                result = true;
            return result;
        }
        /// <summary>
        /// 编辑者：唐超 功能：添加用户信息
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        public bool AddUserInfoDal(UserModel um)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50),
                new SqlParameter("@password",SqlDbType.VarChar,50),
                new SqlParameter("@email",SqlDbType.VarChar,50),
                new SqlParameter("@mobilePhone",SqlDbType.VarChar,50),
                new SqlParameter("@regTime",SqlDbType.DateTime),
                new SqlParameter("@userState",SqlDbType.NVarChar,5)
            };
            prams[0].Value = um.UserName;
            prams[1].Value = um.Password;
            prams[2].Value = um.Email;
            prams[3].Value = um.Mobilephone;
            prams[4].Value = um.Reg_time;
            prams[5].Value = um.userState;
            return SQLHelper.ExcuteNonQueryBySqlText("insert into tb_user(userName,password,email, mobilephone,reg_time,userState) values(@userName,@password,@email,@mobilePhone,@regTime,@userState)",prams);
        }
        /// <summary>
        /// 编辑者：唐超 功能：激活用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public bool ActiveUserStateDal(string UserName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = UserName;
            return !SQLHelper.ExcuteNonQueryBySqlText("update tb_user set userState='已激活' where userName=@userName", prams);
        }
        /// <summary>
        /// 功能：判断邮箱是否存在 编辑者：唐超
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns>True or False</returns>
        public bool IsEmailInDal(string email)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@email",SqlDbType.VarChar,50)
            };
            prams[0].Value = email;
            DataTable dt = SQLHelper.QueryBySqlText("select count(*) from tb_user where email=@email", prams);
            if (dt.Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 功能：找回密码 编辑者：唐超
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="pwd">新密码</param>
        /// <returns></returns>
        public bool ChangePwdDal(string email,string pwd,string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@email",SqlDbType.VarChar,50),
                new SqlParameter("@pwd",SqlDbType.VarChar,50),
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = email;
            prams[1].Value = pwd;
            prams[2].Value = userName;
            return SQLHelper.ExcuteNonQueryBySqlText(@"update dbo.tb_user
            set password=@pwd where email=@email and userName=@userName", prams);
        }

        /// <summary>
        /// 功能：用户名邮箱是否匹配 编辑者：唐超
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">邮箱地址</param>
        /// <returns>匹配与否</returns>
        public bool IsEmailNameMatchDal(string userName, string email)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50),
                new SqlParameter("@email",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            prams[1].Value = email;
            DataTable dt = SQLHelper.QueryBySqlText(@"select COUNT(*) from dbo.tb_user where userName=@userName and email=@email", prams);
            if (dt.Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 功能：判断景区名是否存在 编辑者：唐超
        /// </summary>
        /// <param name="scenicZoneName">景区名</param>
        /// <returns></returns>
        public bool IsScenicZoneNameInDal(string scenicZoneName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicZoneName",SqlDbType.NVarChar,50)
            };
            prams[0].Value = scenicZoneName;
            DataTable dt = SQLHelper.QueryBySqlText(@"select COUNT(*) from dbo.tb_scenicZone
                         where scenicZoneName=@scenicZoneName",prams);
            if (dt.Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 功能：判断手机号是否注册过 编辑者：唐超
        /// </summary>
        /// <param name="telNumber">手机号</param>
        /// <returns></returns>
        public bool IsTelInDal(string telNumber)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@telNumber",SqlDbType.VarChar,50)
            };
            prams[0].Value = telNumber;
            DataTable dt = SQLHelper.QueryBySqlText(@"select COUNT(*) from dbo.tb_user where mobilephone=@telNumber", prams);
            if (dt.Rows[0][0].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 用户签到功能
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="ipAdr">ip地址</param>
        /// <returns></returns>
        public bool UserSignInDal(string userName, string ipAdr,string city)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50),
                new SqlParameter("@ipAdr",SqlDbType.VarChar,50),
                new SqlParameter("@signInTime",SqlDbType.DateTime),
                new SqlParameter("@city",SqlDbType.NVarChar,30)
            };
            prams[0].Value = userName;
            prams[1].Value = ipAdr;
            prams[2].Value = DateTime.Now;
            prams[3].Value = city;
            return SQLHelper.ExcuteNonQueryBySqlText(@"insert into dbo.tb_signIn(userName,IpAddress,signInTime,city)
                                                values(@userName,@ipAdr,@signInTime,@city)",prams);
        }
        /// <summary>
        /// 获取用户签到次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public string GetUserSignInDayNumDal(string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            DataTable dt = SQLHelper.QueryBySqlText(@"select COUNT(*) from dbo.tb_signIn where userName=@userName", prams);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// 判断用户今天是否签到
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool IsUserSignInTodayDal(string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            DataTable dt = SQLHelper.QueryBySqlText(@"select DATEDIFF(DAY,(select top 1 signInTime  from dbo.tb_signIn where userName=@userName order by signInID desc),GETDATE())", prams);
            if (dt.Rows[0][0].ToString() == "0")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 功能：判断用户是否激活  编辑者：唐超
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool IsUserHaveActiveDal(string userName)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50)
            };
            prams[0].Value = userName;
            DataTable dt = SQLHelper.QueryBySqlText(@"select userState from dbo.tb_user
                                  where userName=@userName",prams);
            if (dt.Rows[0][0].ToString() == "已激活")
                return true;
            else
                return false;
        }

    }
}