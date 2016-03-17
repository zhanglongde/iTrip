using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.IO;
using System.Configuration;

public class SQLHelper
    {
        //与SQLServer数据库的连接字符串
        //string filepath = HttpContext.Current.Server.MapPath("/");
        //private static FileStream fs = new FileStream(filepath + "\\db.txt", FileMode.Open);
        //private static StreamReader sr = new StreamReader(fs);
        //private static string sqlConString = sr.ReadLine();
        //public static SqlConnection connection = new SqlConnection(sqlConString);
        //private static readonly string sqlConString = @"Data Source=hello-pc;Initial Catalog=tongyou;Persist Security Info=True;User ID=sa;Password=102901";
        private static readonly string sqlConString = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public static SqlConnection connection = new SqlConnection(sqlConString);

        //string filepath = HttpContext.Current.Server.MapPath("/");
        #region   传入参数并且转换为SqlParameter类型
        /// <summary>
        /// 转换参数
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param></param>
        /// <param name="Size">参数大小</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// 初始化参数值
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;
            return param;
        }
        #endregion        
        
        /// <summary>
        /// 准备SqlCommand对象,该对象默认的执行方式为Sql语句，若要执行存储过程，则在调用该函数后需将
        /// SqlCommand的CommandType改成StoredProcedure
        /// </summary>
        /// <param name="conn">SqlCommand所对应的连接</param>
        /// <param name="cmd">需要准备的SqlCommand对象</param>
        /// <param name="tran">SqlCommand对象所对应的事务</param>
        /// <param name="sqlText">SqlCommand所要执行的Sql语句或存储过程名</param>
        /// <param name="prams">SqlCommand所需要的参数</param>
        private static void PrepareCommand(SqlConnection conn, SqlCommand cmd, SqlTransaction tran, string sqlText, SqlParameter[] prams)
        {
            cmd.Connection = conn;
            if (tran != null)
            {
                cmd.Transaction = tran;
            }
            cmd.CommandText = sqlText;
            if (prams != null)
            {
                foreach (SqlParameter p in prams)
                {
                    if (p != null)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// 通过SQL语句进行查询
        /// </summary>
        /// <param name="sqlText">要执行的查询语句</param>
        /// <param name="prams">该查询语句所需要的参数</param>
        /// <returns>返回查询的数据表</returns>
        public static DataTable QueryBySqlText(string sqlText, SqlParameter[] prams)
        {
            using (SqlConnection conn = new SqlConnection(sqlConString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(conn, cmd, null, sqlText, prams);
                    SqlDataReader sdr = null;
                    try
                    {
                        conn.Open();
                        sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }

                    DataTable dt = ConvertSqlDataReaderToDataTable(sdr);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 通过SQL语句进行查询
        /// </summary>
        /// <param name="sqlText">要执行的查询语句</param>        
        /// <returns>返回查询的数据表</returns>
        public static DataTable QueryBySqlText(string sqlText)
        {
            using (SqlConnection conn = new SqlConnection(sqlConString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(conn, cmd, null, sqlText, null);
                    SqlDataReader sdr = null;
                    try
                    {
                        conn.Open();
                        sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }

                    DataTable dt = ConvertSqlDataReaderToDataTable(sdr);
                    return dt;
                }
            }
        }


        /// <summary>
        /// 通过SQL存储过程进行查询
        /// </summary>
        /// <param name="sqlProc">要执行的存储过程名</param>
        /// <param name="prams">该查询语句所需要的参数</param>
        /// <returns>返回查询的数据集</returns>
        public static DataTable QueryBySqlProc(string sqlProc, SqlParameter[] prams)
        {
            using (SqlConnection conn = new SqlConnection(sqlConString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(conn, cmd, null, sqlProc, prams);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = null;
                    try
                    {
                        conn.Open();
                        sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }

                    DataTable dt = ConvertSqlDataReaderToDataTable(sdr);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 将SqlDataReader转换成DataTable
        /// </summary>
        /// <param name="sdr">要转换的SqlDataReader</param>
        /// <returns>返回转换后的DataTable</returns>
        public static DataTable ConvertSqlDataReaderToDataTable(SqlDataReader sdr)
        {
            if (sdr == null)
            {
                return null;
            }
            DataTable dt = new DataTable();
            int fieldCount = sdr.FieldCount;
            for (int intCounter = 0; intCounter < fieldCount; ++intCounter)
            {
                dt.Columns.Add(sdr.GetName(intCounter), sdr.GetFieldType(intCounter));
            }

            object[] objValues = new object[fieldCount];
            dt.BeginLoadData();
            while (sdr.Read())
            {
                sdr.GetValues(objValues);
                dt.LoadDataRow(objValues, true);
            }
            sdr.Close();
            dt.EndLoadData();
            return dt;
        }

        /// <summary>
        /// 通过Sql语句执行非查询操作
        /// </summary>
        /// <param name="sqlText">要执行的非查询SQL语句</param>
        /// <param name="prams">参数</param>
        /// <returns>若执行成功，则返回true,否则返回false</returns>
        public static bool ExcuteNonQueryBySqlText(string sqlText, SqlParameter[] prams)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(sqlConString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(conn, cmd, null, sqlText, prams);
                    try
                    {
                        conn.Open();
                        result = cmd.ExecuteNonQuery() > 0 ? true : false;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 当执行存储过程查询操作需要传回output参数时候用该函数
        /// </summary>
        /// <param name="sqlProc">存储过程名称</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <param name="prams">存储过程所需要参数</param>
        /// <returns>DataTable</returns>
        public static DataTable ExcuteQueryWithOutputParam(string sqlProc, SqlCommand cmd, SqlParameter[] prams)
        {
            using (SqlConnection conn = new SqlConnection(sqlConString))
            {
                PrepareCommand(conn, cmd, null, sqlProc, prams);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader sdr = null;
                try
                {
                    conn.Open();
                    sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }

                DataTable dt = ConvertSqlDataReaderToDataTable(sdr);
                return dt;

            }
        }




        /// <summary>
        /// 通过存储过程执行非查询操作，存储过程如果返回0，则表示存储过程执行成功，否则即失败
        /// </summary>
        /// <param name="sqlProc">存储过程名</param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        public static bool ExcuteNonQueryBySqlProc(string sqlProc, SqlParameter[] prams)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(sqlConString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(conn, cmd, null, sqlProc, prams);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter p = new SqlParameter("@returnValue", SqlDbType.Int);
                    p.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(p);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        int returnValue = int.Parse(cmd.Parameters["@returnValue"].Value.ToString());
                        result = returnValue > 0 ? false : true;

                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
            return result;
        }




        /// <summary>
        /// 通过存储过程执行非查询操作，存储过程如果返回0，则表示存储过程执行成功，否则即失败。当需要
        /// 输出参数的时候调用此函数
        /// </summary>
        /// <param name="sqlProc">存储过程名</param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        public static bool ExcuteNonQueryBySqlProcWithOutputPram(string sqlProc, SqlCommand cmd, SqlParameter[] prams)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(sqlConString))
            {

                PrepareCommand(conn, cmd, null, sqlProc, prams);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p = new SqlParameter("@returnValue", SqlDbType.Int);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int returnValue = int.Parse(cmd.Parameters["@returnValue"].Value.ToString());
                    result = returnValue > 0 ? false : true;

                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }

            }
            return result;
        }
    }

