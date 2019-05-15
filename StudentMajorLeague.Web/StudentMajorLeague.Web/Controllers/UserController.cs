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
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentMajorLeague.Web.Controllers
{
    [RoutePrefix("api/User")]
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
        [Route("Index")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("Student Major League");
        }

        [AllowAnonymous]
        [Route("Registration")]
        [HttpPost]
        public async Task<IHttpActionResult> Registration(UserRegistrationRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"{model.Name} {model.Surname} {model.Email} {model.Password} {model.Birthday.ToString()} {model.LeagueId}");
            }

            try
            {
                var isUserExist = await userReadService.IsUserExistAsync(model.Email);

                if (isUserExist)
                {
                    return BadRequest("User is already registered");
                }
                else
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

                    return Ok(new { token, newUser.Id, newUser.RoleId });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [AllowAnonymous]
        [Route("Authorization")]
        [HttpPost]
        public async Task<IHttpActionResult> Authorization(AuthorizationRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = new StringBuilder();

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Append($"{error.ErrorMessage}\n");
                    }
                }

                return BadRequest(errors.ToString());
            }

            try
            {
                var user = await userReadService.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    return BadRequest("User is not registered");
                }
                else
                {
                    var hashPassword = userWriteService.HashPassword(model.Password);

                    if (hashPassword == user.Password)
                    {
                        var role = user.Role;

                        var identity = SignIn(user.Email, user.Role.Value, user.Id.ToString());

                        var token = GetUserToken(identity);

                        return Ok(new { token, user.Id, user.RoleId });
                    }
                    else
                    {
                        return BadRequest("IncorrectPassword");
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{userId:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUser(int userId)
        {
            try
            {
                var user = await userReadService.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return BadRequest("User is not registered");
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

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var result = new List<UserResponseModel>();

                var users = await userReadService.GetAllAsync();

                foreach(var user in users)
                {
                    result.Add(new UserResponseModel
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
                    });
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IHttpActionResult> Update(UserRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = new StringBuilder();

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Append($"{error.ErrorMessage}\n");
                    }
                }

                return BadRequest(errors.ToString());
            }

            try
            {
                var userById = await userReadService.GetUserByIdAsync(model.Id);
                var userByEmail = await userReadService.GetUserByEmailAsync(model.Email);

                if (userById == null || (userByEmail != null && userById.Id != userByEmail.Id))
                {
                    return BadRequest("User is not registered");
                }
                else
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

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("Delete/{userId:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> Delete(int userId)
        {
            try
            {
                //remove chain 1:1
                await chainWriteService.RemoveChainAsync(userId);

                //remove user
                await userWriteService.RemoveUserAsync(userId);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("History/{userId:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserHistory(int userId)
        {
            try
            {
                var user = await userReadService.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return BadRequest("User is not registered");
                }
                else
                {
                    var result = await chainReadService.GetBlocksInChainAsync(userId);

                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
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