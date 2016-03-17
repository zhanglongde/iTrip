using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using iTrip.Codes.DAL;

namespace iTrip.Codes.BLL
{
    public class ScenicAnnountionBll
    {
        ScenicAnnountionDal saDAL = new ScenicAnnountionDal();
        /// <summary>
        /// 编辑者：张龙德 功能：通过用户名在用户表中获取景区ID
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static int getScenicSpotIdByUsernameBLL(string userName)
        {
            int scenicSpotID = iTrip.Codes.DAL.ScenicAnnountionDal.getScenicSpotIdByUsernameDAL(userName);
            return scenicSpotID;
        }
        /// <summary>
        /// 编辑者：张龙德 功能：添加公告信息
        /// </summary>
        /// <param name="um">公告信息</param>
        /// <returns></returns>
        public string AddAnnountionBll(ScenicAnnountionModel sa)
        {
            return saDAL.AddAnnountionDal(sa);
        }
        /// <summary>
        /// 编辑者：张龙德 功能：载入所有公告信息
        /// </summary>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> selectAllAnnountionsBLL()
        {
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.DAL.ScenicAnnountionDal.selectAllAnnountionsDAL();
            return ScenicAnnountionModels;
        }
        /// <summary>
        /// 编辑者：张龙德 功能：根据页码选择公告信息
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> select4AnnountionByPagingBLL(int scenicSpotID, int page)
        {
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.DAL.ScenicAnnountionDal.select4AnnountionByPagingDAL(scenicSpotID, page);
            return ScenicAnnountionModels;
        }
        /// <summary>
        /// 编辑者：张龙德 功能：获得总页码数
        /// </summary>
        /// <returns></returns>
        public static int getPageAmountBLL(int scenicSpotID)
        {
            return iTrip.Codes.DAL.ScenicAnnountionDal.getPageAmountDAL(scenicSpotID);
        }
        /// <summary>
        /// 编辑者：张龙德 功能：获得更多1周内公告信息
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <param name="last"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> getMore3MonthAnnountionsBLL(int scenicSpotID, int last, int count)
        {
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.DAL.ScenicAnnountionDal.getMore3MonthAnnountionAnnountionsDAL(scenicSpotID, last, count);
            return ScenicAnnountionModels;
        }
        /// <summary>
        /// 编辑者：张龙德 功能：载入1周内公告信息
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> selectWeekAnnountionsBLL(int scenicSpotID)
        {
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.DAL.ScenicAnnountionDal.selectWeekAnnountionAnnountionsDAL(scenicSpotID);
            return ScenicAnnountionModels;
        }
        /// <summary>
        /// 编辑者：张龙德 功能：载入1个月内公告信息
        /// </summary>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> selectMonthAnnountionsBLL(int scenicSpotID)
        {
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.DAL.ScenicAnnountionDal.selectMonthAnnountionAnnountionsDAL(scenicSpotID);
            return ScenicAnnountionModels;
        }
        /// <summary>
        /// 编辑者：张龙德 功能：载入3个月内公告信息
        /// </summary>
        /// <returns></returns>
        public static List<ScenicAnnountionModel> select3MonthAnnountionsBLL(int scenicSpotID)
        {
            List<ScenicAnnountionModel> ScenicAnnountionModels = iTrip.Codes.DAL.ScenicAnnountionDal.select3MonthAnnountionAnnountionsDAL(scenicSpotID);
            return ScenicAnnountionModels;
        }
        /// <summary>
        /// 编辑者：张龙德 功能：删除公告信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool deleteAnnontionBLL(int AnnountionDecID)
        {
            int result = iTrip.Codes.DAL.ScenicAnnountionDal.deleteAnnontionDAL(AnnountionDecID);
            if (result != 1)
            { //添加关注失败
                return false;
            }
            else
            { //添加关注成功
                return true;
            }
        }
        /// <summary>
        /// 编辑者：张龙德 功能：修改公告信息
        /// </summary>
        /// <param name="sam"></param>
        /// <param name="formerTitle"></param>
        /// <returns></returns>
        public static bool ReviseAnnontionBLL(ScenicAnnountionModel sam, string annountionDecID)
        {
            return iTrip.Codes.DAL.ScenicAnnountionDal.ReviseAnnontionDAL(sam, annountionDecID);
        }
        /// <summary>
        /// 编辑者：张龙德   功能：传入公告标题，获得公告内容
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static ScenicAnnountionModel queryContentByDecIdBLL(string title)
        {
            return iTrip.Codes.DAL.ScenicAnnountionDal.queryContentByDecIdDAL(title);
        }
    }
}