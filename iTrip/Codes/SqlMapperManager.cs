using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;

namespace iTrip.Codes
{
    public class SqlMapperManager
    {
        private Hashtable mMapperColl = Hashtable.Synchronized(new Hashtable());
        private static SqlMapperManager mInstance = null;

        /// <summary>
        /// Mapper管理对象的单例
        /// </summary>
        public static SqlMapperManager Instance
        {
            get 
            {
                if (mInstance == null)
                    mInstance = new SqlMapperManager();
                return mInstance;
            }
        }

        /// <summary>
        /// 获得默认的Mapper
        /// </summary>
        public ISqlMapper DefaultMapper
        {
            get 
            {
                return this.Get("default");
            }
        }

        private SqlMapperManager()
        {
        }

        /// <summary>
        /// 判Mapper集合中是否包含设置的Key
        /// </summary>
        /// <param name="key">待检查的Key</param>
        /// <returns>存在与否</returns>
        public bool ContainKey(string key)
        {
            return this.mMapperColl.ContainsKey(key);
        }

        /// <summary>
        /// 添加新的SQLMapper到集合中
        /// </summary>
        /// <param name="key">Mapper的Key</param>
        /// <param name="mapper">SQL Mapper</param>
        /// <returns>添加到集合中的Mapper</returns>
        public ISqlMapper Add(string key, ISqlMapper mapper)
        {
            if (this.mMapperColl.ContainsKey(key))
                return this.mMapperColl[key] as SqlMapper;
            this.mMapperColl.Add(key, mapper);
            return mapper;
        }

        public ISqlMapper Add(string key, string connectionString, object noUse)
        {
            return Add(key, connectionString, String.Empty);
        }

        /// <summary>
        /// 添加新的SQLMapper到集合中
        /// </summary>
        /// <param name="key">Mapper的Key</param>
        /// <param name="connectionString">数据库的连接字符串</param>
        /// <param name="sqlMapFile">SqlMap.config文件</param>
        /// <returns>添加到集合中的Mapper</returns>
        public ISqlMapper Add(string key, string connectionString, string sqlMapFile)
        {
            try
            {
                DomSqlMapBuilder builder = new DomSqlMapBuilder();
                if (!String.IsNullOrEmpty(connectionString))
                {
                    NameValueCollection values = new NameValueCollection();
                    values.Add("connectionString", connectionString);
                    builder.Properties = values;
                }
                ISqlMapper mapper = null;
                if (String.IsNullOrEmpty(sqlMapFile))
                    mapper = builder.Configure();
                else
                    mapper = builder.Configure(sqlMapFile);
                Add(key, mapper);
                return mapper;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加新的SQLMapper到集合中
        /// </summary>
        /// <param name="key">Mapper的Key</param>
        /// <param name="sqlMapFile">SqlMap.config文件</param>
        /// <returns>添加到集合中的Mapper</returns>
        public ISqlMapper Add(string key, string sqlMapFile)
        {
            return Add(key, String.Empty, sqlMapFile);
        }

        /// <summary>
        /// 得到指定键的Mapper
        /// </summary>
        /// <param name="key">给定的键</param>
        /// <returns>集合中的Mapper</returns>
        public ISqlMapper Get(string key = "default")
        {
            //如果传入的Key的值为"default"的话,获取Mapper默认的实例,并将其添加到集合中
            if (key == "default")
            {
                if (this.mMapperColl.ContainsKey("default"))
                    return this.mMapperColl[key] as ISqlMapper;
                ISqlMapper mapper = Mapper.Instance();
                if(!this.mMapperColl.ContainsKey("default"))
                    this.mMapperColl.Add(key, mapper);
                return mapper;
            }
            else
            {
                //获取指定键的Mapper
                if (this.mMapperColl.ContainsKey(key))
                    return this.mMapperColl[key] as ISqlMapper;
                //不存在则返回空
                else
                    return null;
            }

        }
        /// <summary>
        /// 移除指定Key的Mapper
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (this.mMapperColl.ContainsKey(key))
            {
                ISqlMapper mapper = Get(key);
                mapper = null;
                this.mMapperColl.Remove(key);
            }
        }

        /// <summary>
        /// 清空所有的Mapper
        /// </summary>
        public void RemoveAll()
        {
            foreach (string key in this.mMapperColl.Keys)
            {
                ISqlMapper mapper = Get(key);
                mapper = null;
            }
            this.mMapperColl.Clear();
        }
    }
}
