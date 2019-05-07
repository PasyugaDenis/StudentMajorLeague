using Microsoft.IdentityModel.Tokens;
using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using StudentMajorLeague.Web.Models.Requests;
using StudentMajorLeague.Web.Models.Responses;
using StudentMajorLeague.Web.Services.ChainService;
using StudentMajorLeague.Web.Services.RoleService;
using StudentMajorLeague.Web.Services.UserService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentMajorLeague.Web.Controllers
{
    public class UserController : ApiController
    {
        private SMLConfiguration settings;

        private IUserReadService userReadService;
        private IUserWriteService userWriteService;

        private IChainReadService chainReadService;
        private IChainWriteService chainWriteService;

        private IRoleReadService roleReadService;

        public UserController(
            SMLConfiguration settings,
            IUserReadService userReadService,
            IUserWriteService userWriteService,
            IChainReadService chainReadService,
            IChainWriteService chainWriteService,
            IRoleReadService roleReadService)
        {
            this.settings = settings;

            this.userReadService = userReadService;
            this.userWriteService = userWriteService;

            this.chainReadService = chainReadService;
            this.chainWriteService = chainWriteService;

            this.roleReadService = roleReadService;
        }

        [AllowAnonymous]
        [HttpGet]
        public string Index()
        {
            return "Student Major League";
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Registration(UserRegistrationRequestModel model)
        {
            var isUserExist = await userReadService.IsUserExistAsync(model.Email);

            if (isUserExist)
            {
                return JsonResults.Error(401, "User is already registered");
            }
            else
            {
                try
                {
                    //registration
                    var chain = new Chain
                    {
                        CreatedOn = DateTime.UtcNow
                    };

                    var user = new User
                    {
                        Email = model.Email,
                        Password = userWriteService.HashPassword(model.Password),
                        Name = model.Name,
                        Surname = model.Surname,
                        Birthday = model.Birthday,
                        LeagueId = model.LeagueId,
                        RegistrationDate = DateTime.UtcNow,
                        Chain = chain
                    };

                    var newUser = await userWriteService.RegistrationAsync(user);

                    //add history
                    var maxId = await chainReadService.GetMaxBlocksIdAsync();

                    var block = new HistoryBlock
                    {
                        Id = ++maxId,
                        Changes = "User registration",
                        AuthorId = newUser.Id,
                        CreatedOn = DateTime.UtcNow.Date
                    };

                    block.Hash = block.HashValues();

                    var newBlock = await chainWriteService.AddHistoryBlockToChainAsync(newUser.Chain.Id, block);

                    //sign in
                    var identity = SignIn(newUser.Email, newUser.Role.Value, newUser.Id.ToString());

                    var token = GetUserToken(identity);

                    return JsonResults.Success(new { token, newUser.Id, newUser.RoleId });
                }
                catch (Exception ex)
                {
                    return JsonResults.Error(400, ex.Message);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Authorization(AuthorizationRequestModel model)
        {
            var user = await userReadService.GetUserByEmailAsync(model.Email);

            if (user == null)
            {
                return JsonResults.Error(401, "User is not registered");
            }
            else
            {
                var hashPassword = userWriteService.HashPassword(model.Password);

                if (hashPassword == user.Password)
                {
                    var role = user.Role;

                    var identity = SignIn(user.Email, user.Role.Value, user.Id.ToString());

                    var token = GetUserToken(identity);

                    return JsonResults.Success(new { token, user.Id, user.RoleId });
                }
                else
                {
                    return JsonResults.Error(402, "IncorrectPassword");
                }
            }
        }

        [HttpGet]
        public async Task<object> GetUser(int userId)
        {
            var user = await userReadService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return JsonResults.Error(401, "User is not registered");
            }
            else
            {
                var result = new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    RegistrationDate = user.RegistrationDate,
                    RoleId = user.RoleId,
                    Name = user.Name,
                    Surname = user.Surname,
                    Birthday = user.Birthday.Value,
                    Education = user.Education,
                    Phone = user.Phone,
                    City = user.City,
                    LeagueId = user.LeagueId,
                    Height = user.Height,
                    Weight = user.Weight
                };

                return JsonResults.Success(result);
            }
        }

        [HttpPost]
        public async Task<object> Update(UserRequestModel model)
        {
            var isUserExist = await userReadService.IsUserExistAsync(model.Email);

            if (!isUserExist)
            {
                return JsonResults.Error(401, "User is not registered");
            }
            else
            {
                try
                {
                    var user = new User
                    {
                        Id = model.Id,
                        Email = model.Email,
                        Name = model.Name,
                        Surname = model.Surname,
                        Birthday = model.Birthday,
                        Education = model.Education,
                        Phone = model.Phone,
                        City = model.City,
                        Weight = model.Weight,
                        Height = model.Height,
                        LeagueId = model.LeagueId
                    };

                    var updatedUser = await userWriteService.UpdateUserAsync(user);

                    //add history
                    var maxId = await chainReadService.GetMaxBlocksIdAsync();

                    var block = new HistoryBlock
                    {
                        Id = ++maxId,
                        Changes = $"Update user. Height = {user.Height}, weight = {user.Weight}. Date: {DateTime.UtcNow.ToShortDateString()}",
                        AuthorId = updatedUser.Id,
                        CreatedOn = DateTime.UtcNow.Date
                    };

                    block.Hash = block.HashValues();

                    var newBlock = await chainWriteService.AddHistoryBlockToChainAsync(updatedUser.Id, block);

                    return JsonResults.Success();
                }
                catch (Exception ex)
                {
                    return JsonResults.Error(400, ex.Message);
                }
            }
        }

        [HttpGet]
        public async Task<object> Delete(int userId)
        {
            try
            {
                //remove chain 1:1
                await chainWriteService.RemoveChainAsync(userId);

                //remove user
                await userWriteService.RemoveUserAsync(userId);

                return JsonResults.Success();
            }
            catch (Exception ex)
            {
                return JsonResults.Error(400, ex.Message);
            }
        }

        //Utills
        private ClaimsIdentity SignIn(string email, string role, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim("Name", email),
                new Claim("Role", role),
                new Claim("UserId", userId)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private string GetUserToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: settings.SiteUrl,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}