using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using iTrip.Codes;

using iTrip.Models;
using iTrip.Codes.BLL;
namespace iTrip.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        UserModel um = new UserModel();
        LoginBll lb = new LoginBll();
        public ActionResult Default()
        {
            return View();
        }
        [HttpPost]
        public string login()
        {

            string result = "true";
            um.UserName = Request.Form["UserName"];
            um.Password = Request.Form["Pwd"];
            if (lb.CheckUserBll(um)==false)
                result = "false";
            return result;
        }
        //根据手机号或者邮箱地址获取用户名
        [HttpPost]
        public string GetUserNameByEmailOrTel()
        {
           string content = Request.Form["content"].ToString();
           string  userName= lb.GetUserNameByEmailOrTelBll(content);
           return userName;
        }
    }
}
