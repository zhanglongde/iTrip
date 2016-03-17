using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using iTrip.Codes.DAL;
using iTrip.Models;

namespace iTrip.Codes.BLL
{
    public class ModifyInformationBll
    {
        ModifyInformationDal mid = new ModifyInformationDal();
        /// <summary>
        /// 获取用户信息和景区信息  编辑者：吴国顺
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public DataTable GetUserScenicZoneInfoBll(string userName)
        {
            return mid.GetUserScenicZoneInfoDal(userName);
        }
        /// <summary>
        /// 功能：获取用户信息 编辑者：吴国顺
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public DataTable GetUserInfoBll(string userName)
        {
            return mid.GetUserInfoDal(userName);
        }
        /// <summary>
        /// 功能：获取景区信息 编辑者：吴国顺
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable GetScenicZoneInfoBll(string userName)
        {
            return mid.GetScenicZoneInfoDal(userName);
        }
        public bool ChangeUserScenicInfoBll(string userNameOld, UserModel um, ScenicAreaModel sam)
        {
            return mid.ChangeUserScenicInfoDal(userNameOld,um,sam);
        }

    }
}