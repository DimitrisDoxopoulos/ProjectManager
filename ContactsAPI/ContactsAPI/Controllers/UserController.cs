using AutoMapper;
using ContactsAPI.DTO;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationService _applicationService;
        private readonly IMapper? _mapper;

        
        public UserController(
            IApplicationService applicationService, IMapper mapper, IConfiguration configuration
        ) : base()
        {
            _applicationService = applicationService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/api/users/login")]
        public async Task<IActionResult> LoginUser(UserLoginDTO request)
        {
            try
            {
                var user = await _applicationService.UserService.LoginUserAsync(request);
                if (user == null)
                {
                    return NotFound();
                }
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, request.Username)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new()
                {
                    AllowRefresh = true,
                    IsPersistent = request.keepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/users/signup")]
        public async Task<IActionResult> SignUpUser(UserDTO request)
        {
            try
            {
                await _applicationService.UserService.SignUpUserAsync(request);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO request)
        {
            try
            {
                var user = await _applicationService.UserService.GetUserByUsernameAsync(request.Username!);
                var updatedUser = await _applicationService.UserService.UpdateUserAccountInfoAsync(request, user!.Id);
                return Ok(updatedUser);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/users/finduser")]
        public async Task<IActionResult> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _applicationService.UserService.GetUserByUsernameAsync(username);
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("/api/users/change-password")]
        public async Task<IActionResult> ChangePasswordAsync(PasswordUpdateDTO request)
        {
            try
            {
                var user = await _applicationService.UserService.ChangePasswordAsync(request);
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
