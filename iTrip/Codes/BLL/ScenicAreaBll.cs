using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using iTrip.Codes.DAL;

namespace iTrip.Codes.BLL
{
    public class ScenicAreaBll
    {
        ScenicAreaDal rd = new ScenicAreaDal();
        // <summary>
        /// 编辑者：杨志君 功能：添加景区信息
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        public bool AddScenicAreaBLL(ScenicAreaModel um,string userName)
        {
            return rd.AddScenicAreaDAL(um,userName);
        }

        public string[] GetScenicZoneInfoBll(string userName)
        {
            return rd.GetScenicZoneInfoDal(userName);
        }
    }
}