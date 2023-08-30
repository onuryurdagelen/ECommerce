using AutoMapper;
using ECommerce.Data.Abstracts;
using ECommerce.Data.Dtos;
using ECommerce.Data.Extensions;
using ECommerce.Data.Response;
using ECommerce.Entity.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BuggyController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            //first way
            string email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?
                .Value;

            AppUser user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            //second way
            //string email = User.FindFirstValue(ClaimTypes.Email);

            UserDto userDto = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                PictureUrl = user.ProfileImageUrl,
                DisplayName = user.DisplayName
            };

            return Ok(new ApiResponse<UserDto>(true, "success", userDto));
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<IActionResult> GetUserAddress()
        {
            AppUser user = await _userManager.FindByUserClaimsPrincipleWithAddress(User);

            return Ok(new ApiResponse<AddressDto>(true, "success", _mapper.Map<Address, AddressDto>(user.Address)));
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDto address)
        {
            AppUser user = await _userManager.FindByUserClaimsPrincipleWithAddress(HttpContext.User);
            user.Address = _mapper.Map<AddressDto,Address>(address);

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) 
                return Ok(new ApiResponse<AddressDto>( true,"success", _mapper.Map<Address,AddressDto>(user.Address)));

            return GetBadRequest("Problem updating the user");
        }

        [HttpGet("emailexists")]
        public async Task<bool> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            //check user has existed in database

            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user is null) return GetUnAuthorizedErrorRequest();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return GetUnAuthorizedErrorRequest();

            UserDto userDto = new UserDto
            {
                Email = user.Email,
                PictureUrl = user.ProfileImageUrl,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };

            return Ok(new ApiResponse<UserDto>(true, "success", userDto));

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(CheckEmailExistsAsync(registerDto.Email).Result)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[] {"Email Address is in use"}
                });
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                ProfileImageUrl = "",
                UserName = registerDto.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return GetBadRequest();

            UserDto userDto = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };

            return Ok(new ApiResponse<UserDto>(true, "success", userDto));
        }
    }
}
