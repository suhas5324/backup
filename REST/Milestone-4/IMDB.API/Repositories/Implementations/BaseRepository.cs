using Dapper;
using IMDB.API;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

public class BaseRepository<T> where T : class
{
    protected readonly string _connectionString;
    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public int Create(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.ExecuteScalar<int>(query, parameters);
    }
    public IList<T> Get(string query)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<T>(query).ToList();
    }
    public IList<T> GetAll(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<T>(query, parameters).ToList();
    }
    public T Get(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QuerySingleOrDefault<T>(query, parameters);
    }
    public void Update(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(query, parameters);
    }
    public void Delete(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(query, parameters);
    }
}
