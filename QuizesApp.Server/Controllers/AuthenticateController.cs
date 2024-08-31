using Microsoft.AspNetCore.Mvc;
using Security;
using System.Security.Claims;
using Utils;

namespace QuizesApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController(ITokenService tokenService, IUsersService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Authorize(UserCredentials credentials)
        {
            try
            {
                var user = await userService.Autorize(credentials.Username, credentials.Password);

                var claims = new Dictionary<string, object> { [ClaimTypes.Name] = credentials.Username };
                var token = tokenService.GenerateToken(claims);
                return Ok(new { Id = credentials.Username, token });
            }
            catch (AutorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        public class UserCredentials
        {
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
    }
}
