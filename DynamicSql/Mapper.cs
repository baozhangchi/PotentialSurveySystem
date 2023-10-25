#region

using System;
using System.Collections.Generic;

#endregion

namespace DynamicSql
{
    public class Mapper : ISqlMapper
    {
        public int Insert(string statementName, object item)
        {
            throw new NotImplementedException();
        }

        public int Update(string statementName, object item)
        {
            throw new NotImplementedException();
        }

        public int Delete(string statementName, object primaryKey)
        {
            throw new NotImplementedException();
        }

        public T QueryForObject<T>(string statementName, object primaryKey)
        {
            throw new NotImplementedException();
        }

        public IList<T> QueryForList<T>(string statementName, object parameterObject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成Sql语句
        /// </summary>
        /// <param name="sqlMap"></param>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string GenerateSql(SqlMap sqlMap, string id, object parameter)
        {
            var statement = sqlMap.Statements[id];
            if (statement == null)
            {
                return string.Empty;
            }

            var sql = statement.Content;
            if (!string.IsNullOrWhiteSpace(statement.Extends))
            {
                sql = $"{GenerateSql(sqlMap, statement.Extends, parameter)} {sql}";
            }


            throw new NotImplementedException();
        }
    }

    public interface ISqlMapper
    {
        int Insert(string statementName, object item);
        int Update(string statementName, object item);
        int Delete(string statementName, object primaryKey);
        T QueryForObject<T>(string statementName, object primaryKey);
        IList<T> QueryForList<T>(string statementName, object parameterObject);
    }
}