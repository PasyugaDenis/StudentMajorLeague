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

        public Task<User> GetUserByIdAsync(int id)
        {
            var result = userReadRepository.GetByIdAsync(id);

            return result;
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            var result = userReadRepository.GetByEmailAsync(email);

            return result;
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            var result = await userReadRepository.GetByEmailAsync(email);

            return result != null;
        }

        public async Task<User[]> GetAllAsync()
        {
            var result = await userReadRepository.GetAllAsync();

            return result;
        }
    }
}