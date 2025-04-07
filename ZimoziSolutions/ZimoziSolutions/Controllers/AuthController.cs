using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZimoziSolutions.ApiModels.Tokens;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Core.Interfaces.Users;
using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Controllers
{
    public class AuthController : BaseApiController<AuthController>
    {
        private readonly IAuthCoreService _authCoreService;
        
        public AuthController(IAuthCoreService authCoreService)
        {
            _authCoreService = authCoreService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserModel request)
        {
            var user = await _authCoreService.RegisterAsync(request);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseModel>> Login(UserModel request)
        {
            var response = await _authCoreService.LoginAsync(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseModel>> RefreshToken(RefreshTokenRequestModel request)
        {
            var response = await _authCoreService.RefreshTokensAsync(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are and admin!");
        }
    }
}
