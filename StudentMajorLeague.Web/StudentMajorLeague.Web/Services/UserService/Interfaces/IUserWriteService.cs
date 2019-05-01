using StudentMajorLeague.Web.Models.Entities;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.UserService
{
    public interface IUserWriteService
    {
        Task<User> RegistrationAsync(User model);

        Task<User> UpdateUserAsync(User model);

        Task DeleteUserAsync(int userId);

        string HashPassword(string password);
    }
}
