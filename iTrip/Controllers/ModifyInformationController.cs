using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Web.Script.Serialization;
using iTrip.Codes.BLL;
using iTrip.Models;

namespace iTrip.Controllers
{
    public class ModifyInformationController : Controller
    {
        //
        // GET: /ModifyInformation/
        ModifyInformationBll mib=new ModifyInformationBll();
        public ActionResult ModifyInformation()
        {
            return View();
        }
        [HttpPost]
        public object GetUserInfo()
        {
            string userName=Request.Form["userName"];
            DataTable dt=mib.GetUserInfoBll(userName);
            List<UserModel> ltum = new List<UserModel>();
            ltum.Add(new UserModel { 
               UserName=dt.Rows[0]["userName"].ToString(),
               Password=dt.Rows[0]["password"].ToString(),
               Mobilephone = dt.Rows[0]["mobilephone"].ToString(),
               Email=dt.Rows[0]["email"].ToString(),
               HeadAdr=dt.Rows[0]["headAdr"].ToString()
            });
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ltum);
        }

        [HttpPost]
        public object GetScenicZoneInfo()
        {
            string userName = Request.Form["userName"];
            DataTable dt = mib.GetScenicZoneInfoBll(userName);
            List<ScenicAreaModel> ltsam = new List<ScenicAreaModel>();
            ltsam.Add(new ScenicAreaModel
            {
                ScenicZoneName = dt.Rows[0]["scenicZoneName"].ToString(),
                ScenicZoneIntroduce = dt.Rows[0]["scenicZoneIntroduce"].ToString(),
                ScenicZoneAdr = dt.Rows[0]["scenicZoneAdr"].ToString(),
                ScenicNotic = dt.Rows[0]["scenicZoneNotice"].ToString(),
                Scenicphone = dt.Rows[0]["scenicZonePhone"].ToString(),
                ScenicTime = dt.Rows[0]["scenicZoneTime"].ToString(),
                ScenicImg = dt.Rows[0]["picturePath"].ToString()
            });
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ltsam);
        }
        [HttpPost]
        public string ChangeUserScenicInfo()
        {   
            string userNameOld=Request.Form["userNameOld"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            UserModel um = jss.Deserialize<UserModel>(Request.Form["userInfo"]);
            ScenicAreaModel sam = jss.Deserialize<ScenicAreaModel>(Request.Form["scenicInfo"]);
            if (mib.ChangeUserScenicInfoBll(userNameOld, um, sam) == true)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
    }
}
