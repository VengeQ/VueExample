using Microsoft.AspNetCore.Mvc;
using Security;
using System.Security.Claims;

namespace QuizesApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthenticateController(ITokenService tokenService) { _tokenService = tokenService; }

        [HttpPost(Name = "Autorize")]
        public async Task<IActionResult> Autorize(UserCredentials user)
        {
            var claims = new Dictionary<string, object>{ [ClaimTypes.Name] = user.Username };
            var token = _tokenService.GenerateToken(claims);

            return Ok(new { Id = user.Username, token });
        }

        public class UserCredentials
        {
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
    }
}
