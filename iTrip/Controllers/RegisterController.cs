using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using iTrip.Codes;

using iTrip.Models;
using iTrip.Codes.BLL;
namespace iTrip.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        UserModel um = new UserModel();
        RegisterBll rb = new RegisterBll();
        ScenicAreaModel sa = new ScenicAreaModel();
        ScenicAreaBll sab = new ScenicAreaBll();
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult FindPassword()
        {
            return View();
        }
        public string ChangePwd()
        {
            string email = Request.Form["email"].ToString();
            string pwd=Request.Form["pwd"].ToString();
            string userName = Request.Form["userName"].ToString();
            if (rb.ChangePwdBll(email, pwd,userName))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        public void SendEmail()
        {
            string email = Request.Form["email"].ToString();
            string userName = Request.Form["userName"].ToString();
            string password = Request.Form["pwd"].ToString();
            MailAddress MessageForm = new MailAddress("958077497@qq.com");
            string MessageTo = email;
            string MessageSubject = "爱同游-修改密码";
            string M1 = "<a target='_blank'  href='http://localhost:1743/Register/FindPassword/";
            string M2 = userName+"itonu@"+email+"itonu@"+ password+ "'>点我，马上找回密码</a>";
            string MessageBody = M1 + M2;
            PublicMethod.Send(MessageForm, MessageTo, MessageSubject, MessageBody);
        }
        public string regist(string userName)
        {
            um.UserName = Request.Form["userName"];
            um.Password = Request.Form["password"];
            um.Email = Request.Form["email"];
            um.Mobilephone = Request.Form["mobilephone"];
            um.Reg_time = DateTime.Now;
            um.userState = "未激活";
            if (rb.AddUserInfoBll(um))
            {
                MailAddress MessageForm = new MailAddress("958077497@qq.com");
                string MessageTo = um.Email;
                string MessageSubject = "爱同游-注册激活";
                string M1 = "<a target='_blank'  href='http://localhost:1743/Register/Register/";
                string M2 = um.UserName+"'>点击我去激活账户</a>";
                string MessageBody = M1 + M2;
                PublicMethod.Send(MessageForm, MessageTo, MessageSubject, MessageBody);
                return "注册成功,已发送邮件至您的邮箱，请前去激活。";

            }
            else {
                return "数据插入数据库出错";
            }
            
        }
        /// <summary>
        /// 编辑者：杨志君 功能：添加景区信息
        /// </summary>
        /// <returns></returns>
        public string ScenicIntroduction()
        {
            sa.ScenicZoneName = Request.Form["areaName"];
            sa.ScenicZoneIntroduce = Request.Form["introduce"];
            sa.ScenicZoneAdr = Request.Form["address"];
            sa.Scenicphone = Request.Form["phone"];
            sa.ScenicTime = Request.Form["time"];
            sa.ScenicNotic = Request.Form["notice"];
            string userName = Request.Form["userName"];
            if (sab.AddScenicAreaBLL(sa,userName))
            {
                return "true";

            }
            else
            {
                return "false";
            }

        }
        //判断用户名是否存在
        public string IfUserNameExist()
        {
            string userName=Request.Form["userName"];
            if (rb.IsUserNameInBll(userName))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //激活用户
        public string ActivateUser(string userName)
        {
            if (rb.ActiveUserStateBll(userName))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        //判断邮箱地址是否存在
        public string IsEmailIn()
        {
            string email = Request.Form["email"].ToString();
            if (rb.IsEmailInBll(email))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //判断用户名邮箱是否匹配
        public string IsEmailNameMatch()
        {
           string userName=Request.Form["userName"].ToString();
            string email=Request.Form["email"].ToString();
            if (rb.IsEmailNameMatchBll(userName, email))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //判断景区名是否存在
        public string IsScenicZoneNameIn()
        {
            string scenicZoneName = Request.Form["scenicZoneName"].ToString();
            if (rb.IsScenicZoneNameInBll(scenicZoneName))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //判断手机号是否存在
        public string IsTelIn()
        {
            string telNumber = Request.Form["telNumber"].ToString();
            if (rb.IsTelInBll(telNumber))
            {
                return "true";
            }
            else
            {
                return "false";
            }
            
        }
        //获取用户签到次数
        public string GetUserSignInDayNum()
        {
            string userName = Request.Form["userName"].ToString();
            return rb.GetUserSignInDayNumBll(userName);
        }
        //用户签到
        public string UserSignIn()
        {
            string userName = Request.Form["userName"].ToString();
            string ipAdr = Request.Form["ipAdr"].ToString();
            string city = Request.Form["city"].ToString();
            if (rb.UserSignInBll(userName, ipAdr,city))
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        //判断用户今天是否签到
        public string IsUserSignInToday()
        {
            string userName = Request.Form["userName"].ToString();
            if (rb.IsUserSignInTodayBll(userName))
                return "true";
            else
                return "false";
         }
        //判断用户是否已经激活
        public string IsUserHaveActive()
        {
            string userName = Request.Form["userName"].ToString();
            if (rb.IsUserHaveActiveBll(userName))
                return "true";
            else
                return "false";
        }
        //internal string GenerateSalt()
        //{
        //    byte[] data = new byte[0x10];
        //    new RNGCryptoServiceProvider().GetBytes(data);
        //    return Convert.ToBase64String(data);
        //}
        //internal string EncodePassword(string pass, int passwordFormat, string salt)
        //{
        //    if (passwordFormat == 0)
        //    {
        //        return pass;
        //    }
        //    // 将密码和salt值转换成字节形式并连接起来
        //    byte[] bytes = Encoding.Unicode.GetBytes(pass);
        //    byte[] src = Convert.FromBase64String(salt);
        //    byte[] dst = new byte[src.Length + bytes.Length];
        //    byte[] inArray = null;
        //    Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        //    Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
        //    // 选择算法，对连接后的值进行散列
        //    if (passwordFormat == 1)
        //    {
        //        HashAlgorithm algorithm = HashAlgorithm.Create(Membership.HashAlgorithmType);
        //        if ((algorithm == null) && Membership.IsHashAlgorithmFromMembershipConfig)
        //        {
        //            RuntimeConfig.GetAppConfig().Membership.ThrowHashAlgorithmException();
        //        }
        //        inArray = algorithm.ComputeHash(dst);
        //    }
        //    else
        //    {
        //        inArray = this.EncryptPassword(dst);
        //    }
        //    // 以字符串形式返回散列值
            
        //    return Convert.ToBase64String(inArray);
        //}
    }
}
