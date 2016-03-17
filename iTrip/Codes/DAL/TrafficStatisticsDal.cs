using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTrip.Models;
using System.Data;
using System.Data.SqlClient;
namespace iTrip.Codes.DAL
{
    public class TrafficStatisticsDal
    {
        public bool AddUserNumDal(string cardId)
        {
            SqlParameter[] prams = new SqlParameter[] { 
               new SqlParameter("@epcId",SqlDbType.VarChar,200),
               new SqlParameter("@enter_time",SqlDbType.DateTime)
            };
            prams[0].Value = cardId;
            prams[1].Value = DateTime.Now;
            return SQLHelper.ExcuteNonQueryBySqlText(@"insert into dbo.tb_infoStatistics(enter_time,scenicSpotID, epcId) values(@enter_time,1,@epcId)", prams);
        }

        public bool CheckHaveReadCardInTimeDal(string cardId)
        {
            SqlParameter[] prams = new SqlParameter[] { 
               new SqlParameter("@epcId",SqlDbType.VarChar,200),
            };
            prams[0].Value = cardId;
            DataTable dt = SQLHelper.QueryBySqlProc("CheckHaveReadCardInTime", prams);
            if (dt.Rows[0][0].ToString() == "1")
                return false;
            else
                return true;
        }
        #region
        /// <summary>
        /// 编辑者：王仕浩 功能：读取数据库，获得当时景点人数
        /// </summary>
        /// <returns></returns>
        public int getdata(int scenicSpotID)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int)
            };
            prams[0].Value = scenicSpotID;
            DataTable d = SQLHelper.QueryBySqlText(@"select COUNT(*) from dbo.tb_infoStatistics
            where  scenicSpotID=@scenicSpotID and DATEDIFF(SECOND,enter_time,GETDATE())<20", prams);
            int count = 0;//记录景点人数
            if (d.Rows[0][0].ToString() != "0")
                count = (int)d.Rows[0][0];
            return count;
            //DataTable dt = new DataTable();
            //dt.TableName = "dt";
            ////为表增加两个列，列名分别为x和y 
            //dt.Columns.Add("Count", typeof(int));
            //DataRow dr = dt.NewRow();
            //dr["Count"] =count;
            //dt.Rows.Add(dr);
            //string result =ConvertDataTableToJson(dt);
            //return result;
        }
        #endregion
        /// <summary>
        ///  编辑者：王仕浩 功能：读取数据库，获得景点历史人数
        /// </summary>
        /// <param name="scenicSpotID"></param>
        /// <returns></returns>
        public int[] Hgetdata(int scenicSpotID, string searchCondition)
        {
            SqlParameter[] prams = new SqlParameter[] { 
                new SqlParameter("@scenicSpotID",SqlDbType.Int),
                new SqlParameter("@year",SqlDbType.Int),
                new SqlParameter("@month",SqlDbType.Int),
                new SqlParameter("@day",SqlDbType.Int)
            };
            prams[0].Value = scenicSpotID;
            //DateTime currentTime = DateTime.Now;
            //prams[1].Value = currentTime.Year;
            DataTable d = new DataTable();
            int condition = 0;
            if (searchCondition.Length == 4)//年查询
            {
                condition = 1;
                prams[1].Value = Convert.ToInt32(searchCondition);
                prams[2].Value = 0;
                prams[3].Value = 0;
                d = SQLHelper.QueryBySqlText(@"select DATEPART(M,enter_time)month, count(*) num from dbo.tb_infoStatistics
                                  where DATEPART(YY,enter_time)=@year and scenicSpotID=@scenicSpotID 
                                  group by DATEPART(M,enter_time) order by DATEPART(M,enter_time)", prams);
            }
            else if (searchCondition.Length == 6)//月查询
            {
                condition = 2;
                prams[1].Value = Convert.ToInt32(searchCondition.Substring(0, 4));
                prams[2].Value = Convert.ToInt32(searchCondition.Substring(4, 2));
                prams[3].Value = 0;
                d = SQLHelper.QueryBySqlText(@"select DATEPART(d,enter_time)day, count(*) num from dbo.tb_infoStatistics
                       where DATEPART(YY,enter_time)=@year and scenicSpotID=@scenicSpotID and DATEPART(m,enter_time)=@month
                        group by DATEPART(d,enter_time) order by DATEPART(d,enter_time)", prams);
            }
            else if (searchCondition.Length == 8)//日查询
            {
                condition = 3;
                prams[1].Value = Convert.ToInt32(searchCondition.Substring(0, 4));
                prams[2].Value = Convert.ToInt32(searchCondition.Substring(4, 2));
                prams[3].Value = Convert.ToInt32(searchCondition.Substring(6, 2));
                d = SQLHelper.QueryBySqlText(@"select DATEPART(HH,enter_time)hour , count(*) num from dbo.tb_infoStatistics
                       where DATEPART(YY,enter_time)=@year and scenicSpotID=@scenicSpotID 
                       and DATEPART(m,enter_time)=@month and DATEPART(d,enter_time)=@day
                       group by DATEPART(HH,enter_time) order by DATEPART(HH,enter_time)", prams);
            }
            else //异常处理
            {
            }


            if (condition == 1)//年搜索
            {
                int[] count = new int[12];//记录某月景点人数
                int month = 0;
                int next = 0;  //记录哪个月有记录
                //获取今年每个月的客流量,并记录在count数组中
                for (int i = 0; i < 12; i++)
                {
                    if (d.Rows.Count <= i)
                        break;
                    month = (int)d.Rows[i][0];
                    for (int j = next; j < month; )
                    {
                        if (month != (j + 1))
                        {
                            count[j] = 0;
                            j++;
                        }
                        else
                        {
                            count[j] = (int)d.Rows[i][1];
                            next = j + 1;
                            break;
                        }
                    }
                }
                return count;
            }
            else if (condition == 2)//月搜索
            {
                int[] count = new int[10];
                int[] cday = new int[31];//记录某日景点人数
                int day = 0;//记录日期
                int next = 0;  //记录哪个月有记录
                //获取今年每日的客流量,并记录在count数组中
                for (int i = 0; i < 31; i++)
                {
                    if (d.Rows.Count <= i)
                        break;
                    day = (int)d.Rows[i][0];
                    for (int j = next; j < day; )
                    {
                        if (day != (j + 1))
                        {
                            cday[j] = 0;
                            j++;
                        }
                        else
                        {
                            cday[j] = (int)d.Rows[i][1];
                            next = j + 1;
                            break;
                        }
                    }
                }
                for (int k = 0; k < 10; k++)//把31天分成10份
                {
                    if (k == 9)
                    {
                        count[9] = cday[27] + cday[28] + cday[29] + cday[30];
                    }
                    else
                    {
                        for (int l = k * 3; l < (k + 1) * 3; l++)
                        {
                            count[k] += cday[l];
                        }
                    }
                }
                return count;
            }
            else if (condition == 3)//日搜索
            {
                int[] count = new int[13];
                int[] chour = new int[24];//记录小时景点人数
                int hour = 0;//记录日期
                int next = 0;  //记录哪个月有记录
                //获取今年每日的客流量,并记录在count数组中
                for (int i = 0; i < 24; i++)
                {
                    if (d.Rows.Count <= i)
                        break;
                    hour = (int)d.Rows[i][0];
                    for (int j = next; j <= hour; )
                    {
                        if (hour != j)
                        {
                            chour[j] = 0;
                            j++;
                        }
                        else
                        {
                            chour[j] = (int)d.Rows[i][1];
                            next = j + 1;
                            break;
                        }
                    }
                }
                //把每天24小时分成13份
                int l = 7;
                for (int k = 0; k < 13; k++)
                {
                    if (k == 12)
                    {
                        for (int s = 0; s < 7; s++)
                        {
                            count[12] += chour[s];
                        }
                        for (int e = 19; e < 24; e++)
                        {
                            count[12] += chour[e];
                        }
                    }
                    else
                    {
                        count[k] = chour[l];
                        l++;
                    }
                }
                return count;
            }
            else //异常处理
            {
                int[] count = new int[12];
                return count;
            }



        }
        /// <summary>
        /// 编辑者：吴国顺 功能：获取从第last条开始后count条的景点信息
        /// </summary>
        /// <param name="scenicSpotID">景区ID</param>
        /// <param name="last"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataTable GetScenicSpotInfoDal(int scenicZoneID,int last,int count)
        {

            SqlParameter[] prams = new SqlParameter[]
            {
                new SqlParameter("@scenicZoneID",SqlDbType.Int),
                new SqlParameter("@last",SqlDbType.Int),
                new SqlParameter("@count",SqlDbType.Int)
            };
            prams[0].Value = scenicZoneID;
            prams[1].Value = last;
            prams[2].Value = count;
            DataTable dt = new DataTable();
            dt = SQLHelper.QueryBySqlText(@"select * from 
                                         (select ROW_NUMBER() over(order by scenicSpotID) as RowNumber,* from dbo.tb_scenicSpot)A
                                           where (RowNumber between @last+1 and @last+@count) and scenicZoneID=@scenicZoneID", prams);
            return dt;
        }
    }
}