using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using iTrip.Models;


namespace iTrip.Codes.DAL
{
    public class LoginDal
    {
        /// <summary>
        /// 编辑者：唐超 功能：判断用户是否注册并激活
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        public bool CheckUserDal(UserModel um)
        {
            bool result = false;
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@userName",SqlDbType.VarChar,50),
                new SqlParameter("@password",SqlDbType.VarChar,50),
            };
            prams[0].Value = um.UserName;
            prams[1].Value = um.Password;
            DataTable dt = SQLHelper.QueryBySqlText("select count(*) from tb_user where (userName=@userName or email=@userName or mobilephone=@userName) and password=@password and userState='已激活'", prams);
            if (dt.Rows[0][0].ToString() != "0")
                result = true;
            return result;
        }
        /// <summary>
        /// 功能：根据邮箱或者手机号获取用户名 编辑者：唐超
        /// </summary>
        /// <param name="content">手机号或者邮箱</param>
        /// <returns>用户名</returns>
        public string GetUserNameByEmailOrTelDal(string content)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@content",SqlDbType.VarChar,50)
            };
            prams[0].Value = content;
            DataTable dt = SQLHelper.QueryBySqlText(@"select userName from dbo.tb_user
                    where email=@content or mobilephone=@content", prams);
            return dt.Rows[0]["userName"].ToString();
        }
    }
}