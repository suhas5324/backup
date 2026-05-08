using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
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

        public Task<User> GetByEmailAsync(string normalizedEmail)
        {
            const string query = @"SELECT id
	,username
	,email
	,normalizedemail
	,passwordhash
FROM foundation.users
WHERE normalizedemail = @NormalizedEmail";

            return GetAsync(query, new { NormalizedEmail = normalizedEmail });
        }

        public async Task CreateAsync(User user)
        {
            const string query = @"INSERT INTO foundation.users (
	username
	,email
	,normalizedemail
	,passwordhash
	)
VALUES (
	@UserName
	,@Email
	,@NormalizedEmail
	,@PasswordHash
	)";

            await ExecuteAsync(query, new
            {
                user.UserName,
                user.Email,
                user.NormalizedEmail,
                user.PasswordHash
            });
        }
    }
}
