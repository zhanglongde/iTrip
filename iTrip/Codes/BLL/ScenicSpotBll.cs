using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using iTrip.Codes.DAL;
using System.Data;

namespace iTrip.Codes.BLL
{
    public class ScenicSpotBll
    {
        ScenicSpotDal rd = new ScenicSpotDal();

        /// <summary>
        /// 编辑者：杨志君 功能：添加景区信息
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        public string AddScenicSpotBLL(string spotName, string spotIntroduce, int zoneID, string picPath, string sPath)
        {
            return rd.AddScenicSpotDAL(spotName, spotIntroduce, zoneID, picPath, sPath);
        }
        /// <summary>
        /// 编辑者：杨志君 功能：显示景区信息
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <returns></returns>
        public DataTable ModifyScenic(string scenicSpotID)
        {
            return rd.ModifyScenic(scenicSpotID);
        }
        /// <summary>
        /// / 编辑者：杨志君 功能：下面显示已有景点信息
        /// </summary>
        /// <param name="scenicid"></param>
        /// <returns></returns>
        public DataTable SpotshowBLL(string scenicid)
        {
            return rd.SpotshowDal(scenicid);
        }
        /// <summary>
        /// 编辑者：杨志君 功能：修改景区信息
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <param name="ScenicSpotName"></param>
        /// <param name="ScenicSpotIntroduce"></param>
        /// <param name="ScenicSpotNotic"></param>
        /// <returns></returns>
        public bool ScenicModifyinfo(int scenicSpotID, string ScenicSpotName, string ScenicSpotIntroduce, string picture, string sound)
        {

            return rd.ScenicModifyinfo(scenicSpotID, ScenicSpotName, ScenicSpotIntroduce, picture, sound);
        }
        /// <summary>
        /// 编辑者：杨志君 功能：删除景点信息
        /// </summary>
        /// <param name="scenicid"></param>
        /// <returns></returns>
        public bool ScenicDeleteBLL(int scenicspotid)
        {

            return rd.ScenicDeleteDal(scenicspotid);
        }
    }
}