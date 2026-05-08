using IMDB_WebApplication.Models.DBModels;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string normalizedEmail);
        Task CreateAsync(User user);
    }
}
