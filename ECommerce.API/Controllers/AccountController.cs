using ECommerce.Data.Dtos;
using ECommerce.Entity.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BuggyController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //check user has existed in database

            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user is null) return GetUnAuthorizedErrorRequest();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return GetUnAuthorizedErrorRequest();

            return new UserDto
            {
                Email = user.Email,
                Token = "This will be a token",
                DisplayName = user.DisplayName
            };

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                ProfileImageUrl = "",
                UserName = registerDto.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return GetBadRequest();

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = "This will be a token",
                Email = user.Email
            };
        }
    }
}
