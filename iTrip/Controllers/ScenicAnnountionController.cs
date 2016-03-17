using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTrip.Codes.BLL;
using iTrip.Models;
using iTrip.Codes;
using System.Web.Script.Serialization;

namespace iTrip.Controllers
{
    public class ScenicAnnountionController : Controller
    {
        //
        // GET: /TraverTalk/
        ScenicAnnountionModel saModel = new ScenicAnnountionModel();
        ScenicAnnountionBll saBLL = new ScenicAnnountionBll();
        public ActionResult ScenicAnnountion()
        {
            return View();
        }
        /// <summary>
        /// 编辑者：张龙德  功能:发布公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string issueAnnounce()
        {
            saModel.ScenicZoneID = Convert.ToInt32(Request.Form["scenicSpotID"]);
            saModel.DecContent = Request.Form["decContent"];
            saModel.DecTime = DateTime.Now;
            saModel.Title = Request.Form["title"];
            return saBLL.AddAnnountionBll(saModel);
        }
        /// <summary>
        /// 编辑者：张龙德 功能：通过用户名获得景区 ID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int getScenicSpotId()
        {
            string userName = Request["userName"];
            int ScenicSpotId = iTrip.Codes.BLL.ScenicAnnountionBll.getScenicSpotIdByUsernameBLL(userName);
            return ScenicSpotId;
        }
        /// <summary>
        /// 编辑者：张龙德  功能:载入所有公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Object loadAllAnnountions(string scenicSpotID)
        {
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.BLL.ScenicAnnountionBll.selectAllAnnountionsBLL();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ScenicAnnountionModels);
        }
        /// <summary>
        /// 编辑者：张龙德  功能:根据页码载入公告信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public Object loadAnnountionsBypagings()
        {
            int page = Convert.ToInt32(Request.Form["page"]);
            int scenicSpotID = Convert.ToInt32(Request.Form["scenicSpotID"]);
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.BLL.ScenicAnnountionBll.select4AnnountionByPagingBLL(scenicSpotID, page);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ScenicAnnountionModels);
        }
        /// <summary>
        /// 编辑者：张龙德  功能:获得页数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int getPageAmount()
        {
            int scenicSpotID = Convert.ToInt32(Request.Form["scenicSpotID"]);
            return iTrip.Codes.BLL.ScenicAnnountionBll.getPageAmountBLL(scenicSpotID);
        }
        /// <summary>
        /// 编辑者：张龙德  功能:获得更多一周内公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Object getMore3MonthAnnountions(string id)
        {
            int scenicSpotID = Convert.ToInt32(id);
            int last = Convert.ToInt32(Request.Form["last"]);
            int count = Convert.ToInt32(Request.Form["amount"]);
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.BLL.ScenicAnnountionBll.getMore3MonthAnnountionsBLL(scenicSpotID, last, count);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ScenicAnnountionModels);
        }
        /// <summary>
        /// 编辑者：张龙德  功能:载入一周内公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Object loadWeekAnnountions()
        {
            int scenicSpotID = Convert.ToInt32(Request.Form["scenicSpotID"]);
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.BLL.ScenicAnnountionBll.selectWeekAnnountionsBLL(scenicSpotID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ScenicAnnountionModels);
        }
        /// <summary>
        /// 编辑者：张龙德  功能:载入一个月内公告
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <returns></returns>
        [HttpPost]
        public Object loadMonthAnnountions()
        {
            int scenicSpotID = Convert.ToInt32(Request.Form["scenicSpotID"]);
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.BLL.ScenicAnnountionBll.selectMonthAnnountionsBLL(scenicSpotID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ScenicAnnountionModels);
        }
        /// <summary>
        /// 编辑者：张龙德  功能:载入三个月内公告
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <returns></returns>
        [HttpPost]
        public Object loadThreeMonthAnnountions()
        {
            int scenicSpotID = Convert.ToInt32(Request.Form["scenicSpotID"]);
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.BLL.ScenicAnnountionBll.select3MonthAnnountionsBLL(scenicSpotID);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ScenicAnnountionModels);
        }
        /// <summary>
        /// 编辑者：张龙德  功能:删除一条公告信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        public bool deleteAnnontion()
        {
            string DecIDString = Request.Form["AnnountionDecID"];
            int AnnountionDecID = Convert.ToInt32(DecIDString);
            bool data = iTrip.Codes.BLL.ScenicAnnountionBll.deleteAnnontionBLL(AnnountionDecID);
            return data;
        }
        /// <summary>
        /// 编辑者：张龙德  功能:修改一条公告信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool reviseAnnountion()
        {
            saModel.ScenicZoneID = 10;
            saModel.DecContent = Request.Form["decContent"];
            saModel.DecTime = DateTime.Now;
            saModel.Title = Request.Form["title"];
            string annountionDecID = Request.Form["annountionDecID"];
            bool data = iTrip.Codes.BLL.ScenicAnnountionBll.ReviseAnnontionBLL(saModel, annountionDecID);
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Object queryContentByDecId()
        {
            string DecId = Request.Form["DecId"].Trim().ToString();
            ScenicAnnountionModel ScenicAnnountionModels = iTrip.Codes.BLL.ScenicAnnountionBll.queryContentByDecIdBLL(DecId);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ScenicAnnountionModels);
        }
    }
}
