using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Repositories.RoleRepository;
using StudentMajorLeague.Web.Repositories.UserRepository;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentMajorLeague.Web.Services.UserService
{
    public class UserWriteService : IUserWriteService
    {
        private IUserWriteRepository userWriteRepository;
        private IUserReadRepository userReadRepository;

        private IRoleReadRepository roleReadRepository;

        public UserWriteService(
            IUserWriteRepository userWriteRepository,
            IUserReadRepository userReadRepository,
            IRoleReadRepository roleReadRepository)
        {
            this.userWriteRepository = userWriteRepository;
            this.userReadRepository = userReadRepository;

            this.roleReadRepository = roleReadRepository;
        }

        public async Task<User> RegistrationAsync(User model)
        {
            var studentRole = await roleReadRepository.GetStudentRoleAsync();

            model.RoleId = studentRole.Id;

            return await userWriteRepository.AddAsync(model);
        }

        public async Task<User> UpdateUserAsync(User model)
        {
            //update user
            var user = await userReadRepository.GetByIdAsync(model.Id);

            user.Email = model.Email;

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Birthday = model.Birthday;
            user.Education = model.Education;
            user.Phone = model.Phone;
            user.City = model.City;
            user.LeagueId = model.LeagueId;

            user.Weight = model.Weight;
            user.Height = model.Height;

            await userWriteRepository.UpdateAsync(user);

            return user;
        }

        public async Task<User> SetAdminRoleAsync(int userId)
        {
            var user = await userReadRepository.GetByIdAsync(userId);
            var adminRole = await roleReadRepository.GetAdminRoleAsync();

            user.RoleId = adminRole.Id;

            await userWriteRepository.UpdateAsync(user);

            return user;
        }

        public async Task<User> SetStudentRoleAsync(int userId)
        {
            var user = await userReadRepository.GetByIdAsync(userId);
            var adminRole = await roleReadRepository.GetStudentRoleAsync();

            user.RoleId = adminRole.Id;

            await userWriteRepository.UpdateAsync(user);

            return user;
        }

        public async Task RemoveUserAsync(int userId)
        {
            var user = await userReadRepository.GetByIdAsync(userId);

            await userWriteRepository.RemoveAsync(user);
        }

        public string HashPassword(string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);

            var CSP = new MD5CryptoServiceProvider();

            var byteHash = CSP.ComputeHash(bytes);

            var hash = new StringBuilder();

            foreach (byte b in byteHash)
            {
                hash.Append(string.Format("{0:x2}", b));
            }

            return hash.ToString();
        }
    }
}