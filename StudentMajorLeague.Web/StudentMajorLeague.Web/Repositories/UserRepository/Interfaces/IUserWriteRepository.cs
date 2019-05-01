using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.UserRepository
{
    public interface IUserWriteRepository
    {
        Task<User> AddAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(User user);
    }
}
