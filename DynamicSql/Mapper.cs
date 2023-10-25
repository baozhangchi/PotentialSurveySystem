using System.Collections.Generic;

namespace DynamicSql
{
    public class Mapper: ISqlMapper
    {

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