using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using iTrip.Codes.DAL;

namespace iTrip.Codes.BLL
{
    public class RegisterBll
    {
        RegisterDal rd = new RegisterDal();
        /// <summary>
        /// 编辑者：吴国顺 功能：判断用户名是否存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public bool IsUserNameInBll(string UserName)
        {
            return rd.IsUserNameInDal(UserName);
        }

        /// <summary>
        /// 编辑者：吴国顺 功能：添加用户信息
        /// </summary>
        /// <param name="um">用户信息类</param>
        /// <returns></returns>
        public bool AddUserInfoBll(UserModel um)
        {
            return rd.AddUserInfoDal(um);
        }

        /// <summary>
        /// 编辑者：吴国顺 功能：激活用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool ActiveUserStateBll(string UserName)
        {
            return rd.ActiveUserStateDal(UserName);
        }

        /// <summary>
        /// 功能：判断email是否存在 编辑者：吴国顺
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailInBll(string email)
        {
            return rd.IsEmailInDal(email);
        }
        /// <summary>
        /// 功能：找回密码 编辑者：吴国顺
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="pwd">新密码</param>
        /// <returns></returns>
        public bool ChangePwdBll(string email, string pwd,string userName)
        {
            return rd.ChangePwdDal(email,pwd,userName);
        }

        /// <summary>
        /// 功能：用户名和email是否匹配 编辑者：吴国顺
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="email">email地址</param>
        /// <returns>匹配与否</returns>
        public bool IsEmailNameMatchBll(string userName, string email)
        {
            return rd.IsEmailNameMatchDal(userName, email);
        }

        /// <summary>
        /// 功能：判断景区名是否存在 编辑者：吴国顺
        /// </summary>
        /// <param name="scenicZoneName">景区名</param>
        /// <returns></returns>
        public bool IsScenicZoneNameInBll(string scenicZoneName)
        {
            return rd.IsScenicZoneNameInDal(scenicZoneName);
        }

        /// <summary>
        /// 功能：判断手机号是否注册过 编辑者：吴国顺
        /// </summary>
        /// <param name="telNumber">手机号码</param>
        /// <returns></returns>
        public bool IsTelInBll(string telNumber)
        {
            return rd.IsTelInDal(telNumber);
        }

        /// <summary>
        /// 用户签到
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="ipAdr">ip地址</param>
        /// <returns></returns>
        public bool UserSignInBll(string userName, string ipAdr,string city)
        {
            return rd.UserSignInDal(userName,ipAdr,city);
        }
        /// <summary>
        /// 获取用户签到次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public string GetUserSignInDayNumBll(string userName)
        {
            return rd.GetUserSignInDayNumDal(userName);
        }
        /// <summary>
        /// 功能：判断用户今天是否签到 编辑者：吴国顺
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool IsUserSignInTodayBll(string userName)
        {
            return rd.IsUserSignInTodayDal(userName);
        }
        public bool IsUserHaveActiveBll(string userName)
        {
            return rd.IsUserHaveActiveDal(userName);
        }
    }
}