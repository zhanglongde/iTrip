using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using iTrip.Codes.DAL;
using System.Data;

namespace iTrip.Codes.BLL
{
    public class TrafficStatisticsBll
    {
        TrafficStatisticsDal tsd = new TrafficStatisticsDal();
        
        public bool AddUserNumBll(string cardId)
        {
            return tsd.AddUserNumDal(cardId);
        }
        public bool CheckHaveReadCardInTimeBll(string cardId)
        {
            return tsd.CheckHaveReadCardInTimeDal(cardId);
        }
        TrafficStatisticsDal ts = new TrafficStatisticsDal();
        /// <summary>
        /// 编辑者：王仕浩 功能：获取当前景点人数
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// 实时景点人数
        public int GetCount(int scenicSpotID)
        {
            return ts.getdata(scenicSpotID);
        }
        /// <summary>
        /// 编辑者：王仕浩 功能：获取历史景点人数
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <returns></returns>
        public int[] GetHCount(int scenicSpotID, string searchCondition)
        {
            int[] count;
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

            count = ts.Hgetdata(scenicSpotID, searchCondition);
            return count;
        }
        public DataTable GetScenicSpotInfoBll(int scenicZoneID, int last, int count)
        {
            return ts.GetScenicSpotInfoDal(scenicZoneID,last,count);
        }

    }
}