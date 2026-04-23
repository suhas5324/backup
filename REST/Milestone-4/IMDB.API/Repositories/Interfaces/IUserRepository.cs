using IMDB_WebApplication.Models.DBModels;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetByEmail(string normalizedEmail);
        void Create(User user);
    }
}
