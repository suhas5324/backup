using IMDB.API;
using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace IMDB_WebApplication.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<ConnectionString> options)
            : base(options.Value.IMDB)
        {
        }

        public User GetByEmail(string normalizedEmail)
        {
            const string query = @"SELECT id
	,username
	,email
	,normalizedemail
	,passwordhash
FROM foundation.users
WHERE normalizedemail = @NormalizedEmail";

            return Get(query, new { NormalizedEmail = normalizedEmail });
        }

        public void Create(User user)
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

            Create(query, new
            {
                user.UserName,
                user.Email,
                user.NormalizedEmail,
                user.PasswordHash
            });
        }
    }
}
