using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.UserRepository;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.UserService
{
    public class UserReadService : IUserReadService
    {
        private IUserReadRepository userReadRepository;

        public UserReadService(
            IUserReadRepository userReadRepository)
        {
            this.userReadRepository = userReadRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var result = await userReadRepository.GetByIdAsync(id);

            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var result = await userReadRepository.GetByEmailAsync(email);

            return result;
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            var result = await userReadRepository.GetByEmailAsync(email);

            return result != null;
        }
    }
}