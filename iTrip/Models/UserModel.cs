using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrip.Models
{
    public class UserModel
    {
        public int UserID { get; set; } //用户ID
        public string UserName { get; set; }//用户名
        public string Mobilephone { get; set; }//电话号码
        public string Email { get; set; }//Email
        public string Password { get; set; }//密码
        public DateTime Reg_time { get; set; }//注册时间
        public string userState { get; set; }//用户状态
        public string HeadAdr { get; set; }//头像地址
    }
}