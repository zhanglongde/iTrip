using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTrip.Codes.BLL;
using System.Data;
using iTrip.Models;
using System.Web.Script.Serialization;
using System.Text;
namespace iTrip.Controllers
{
    public class TrafficStatisticsController : Controller
    {
        //
        // GET: /TrafficStatistics/
        TrafficStatisticsBll ts = new TrafficStatisticsBll();
        TrafficStatisticsModel tm = new TrafficStatisticsModel();
        public ActionResult TrafficStatistics()
        {
            return View();
        }
        [HttpPost]
        public int Count()
        {
            int scenicSpotId = Convert.ToInt32(Request.Form["scenicSpotId"]);
            int count = ts.GetCount(scenicSpotId);
            return count;
        }
        /// <summary>
        /// 编辑者：王仕浩 功能：获取历史景点客流量数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public StringBuilder HCount()
        {
            int[] count;
            tm.ScenicSpotID = Convert.ToInt32(Request.Form["ScenicSpotID"]);
            string searchCondition = Convert.ToString(Request.Form["searchCondition"]);
            if (searchCondition.Length == 4)//判断搜索条件是年、月、日的哪一种来确定创建数组大小
            {
                count = new int[12];
            }
            else if (searchCondition.Length == 6)
            {
                count = new int[10];
            }
            else if (searchCondition.Length == 8)
            {
                count = new int[13];
            }
            count = ts.GetHCount(tm.ScenicSpotID, searchCondition);
            StringBuilder stringbuilder = new StringBuilder("");
            for (int i = 0; i < count.Length; i++)
            {
                stringbuilder = stringbuilder.Append(count[i]);
                if (i < (count.Length - 1))
                {
                    stringbuilder = stringbuilder.Append('/');
                }

            }
            return stringbuilder;
        }
        [HttpPost]
        public object GetScenicSpotInfo(string id)
        {
            int last =Convert.ToInt32(Request.Form["last"]);
            int count = Convert.ToInt32(Request.Form["amount"]);
            DataTable dt = ts.GetScenicSpotInfoBll(Convert.ToInt32(id),last,count);
            List<ScenicSpotModel> ltsm = new List<ScenicSpotModel>();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                ltsm.Add(new ScenicSpotModel { 
                   ScenicSpotID=Convert.ToInt32(dt.Rows[i]["scenicSpotID"]),
                   ScenicSpotName = dt.Rows[i]["scenicSpotName"].ToString(),
                   ScenicSpotIntroduce = dt.Rows[i]["scenicSpotIntroduce"].ToString()
                });
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(ltsm);
        }
    }
}