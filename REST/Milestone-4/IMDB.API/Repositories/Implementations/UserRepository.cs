using Dapper;
using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDB)
        {
        }

        public async Task<User> GetByEmailAsync(string normalizedEmail)
        {
            const string query = @"
                SELECT id, username, email, normalizedemail, passwordhash
                FROM foundation.users
                WHERE normalizedemail = @NormalizedEmail";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<User>(query, new { NormalizedEmail = normalizedEmail });
        }

        public async Task CreateAsync(User user)
        {
            const string query = @"
                INSERT INTO foundation.users
                    (username, email, normalizedemail, passwordhash)
                OUTPUT INSERTED.id
                VALUES
                    (@UserName, @Email, @NormalizedEmail, @PasswordHash)";

            using var connection = new SqlConnection(_connectionString);
            user.Id = await connection.ExecuteScalarAsync<int>(query, new
            {
                user.UserName,
                user.Email,
                user.NormalizedEmail,
                user.PasswordHash
            });
        }
    }
}
