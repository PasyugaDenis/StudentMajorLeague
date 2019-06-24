using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Repositories.UserRepository
{
    public interface IUserWriteRepository
    {
        Task<User> AddAsync(User model);

        Task UpdateAsync(User model);

        Task RemoveAsync(User model);
    }
}
