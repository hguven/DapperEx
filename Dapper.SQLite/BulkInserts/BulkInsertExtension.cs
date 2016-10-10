﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Dapper;

namespace Dapper.SQLite
{
    public static class BulkInsertExtension
    {
        public const int DefaultBatchSize = 1000;

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="context">当前DbContext</param>
        /// <param name="destinationTableName">要插入的表名称</param>
        /// <param name="reader">IDataReader</param>
        /// <param name="batchSize">每次插入的数量</param>
        /// <remarks>数据一致性,请开启事务</remarks>
        public static int BulkInsert(this SQLiteDbContext context, string destinationTableName, DataTable table,
            int batchSize = DefaultBatchSize)
        {
            var provider = new BulkInsertSQLiteProvider(context);
            var result =  provider.BulkInsert(destinationTableName,table, batchSize);
            return result;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="context">当前DbContext</param>
        /// <param name="destinationTableName">要插入的表名称</param>
        /// <param name="reader">IDataReader</param>
        /// <param name="batchSize">每次插入的数量</param>
        /// <remarks>数据一致性,请开启事务</remarks>
        public static int BulkInsert(this SQLiteDbContext context, string destinationTableName, IDataReader reader,
            int batchSize = DefaultBatchSize)
        {
            var provider = new BulkInsertSQLiteProvider(context);
            var result = provider.BulkInsert(destinationTableName,reader, batchSize);
            return result;
        }

        /// <summary>
        /// 批量插入,不建议使用List插入、性能会受一定影响
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="list">List列明必须与数据表列一致,严格大小写区分</param>
        /// <param name="batchSize"></param>
        public static int BulkInsert<T>(this SQLiteDbContext context, string destinationTableName, IList<T> list,
            int batchSize = DefaultBatchSize) where T : class
        {
            var provider = new BulkInsertSQLiteProvider(context);
            var result = provider.BulkInsert<T>(destinationTableName ,list , batchSize);
            return result;
        }

    }
}