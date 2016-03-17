using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using iTrip.Codes.DAL;

namespace iTrip.Codes.BLL
{
    public class LoginBll
    {
        LoginDal ld = new LoginDal();
        /// <summary>
        /// 编辑者：吴国顺 功能：判断用户是否注册并激活
        /// </summary>
        /// <param name="um">用户信息类</param>
        /// <returns></returns>
        public bool CheckUserBll(UserModel um)
        {
            return ld.CheckUserDal(um);
        }
        /// <summary>
        /// 功能：根据手机号或者邮箱地址获取用户名 编辑者：吴国顺
        /// </summary>
        /// <param name="content">手机号或者邮箱地址</param>
        /// <returns>用户名</returns>
        public string GetUserNameByEmailOrTelBll(string content)
        {
            return ld.GetUserNameByEmailOrTelDal(content);
        }
    }
}