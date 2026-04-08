using IMDB_WebApplication.Models.DBModels;
using IMDB_WebApplication.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string normalizedEmail);
        Task CreateAsync(User user);
    }
}
