using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BaseRepository<T> where T : class
{
    protected readonly string _connectionString;
    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<int> CreateAsync(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(query, parameters);
    }

    public async Task<IList<T>> GetAllAsync(string query, object parameters=null)
    {
        using var connection = new SqlConnection(_connectionString);
        if(parameters == null)
        {
            var results = await connection.QueryAsync<T>(query);
            return results.ToList();
        }
        else
        {
            var results = await connection.QueryAsync<T>(query, parameters);
            return results.ToList();
        }
    }

    public async Task<T> GetAsync(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
    }

    public async Task<int> ExecuteAsync(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(query, parameters);
    }

    public async Task UpdateAsync(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(string query, object parameters)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(query, parameters);
    }
}
