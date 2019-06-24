using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.UserRepository
{
    public interface IUserReadRepository
    {
        Task<User> GetByIdAsync(int id);

        Task<User> GetByEmailAsync(string email);

        Task<User[]> GetAllAsync();
    }
}
