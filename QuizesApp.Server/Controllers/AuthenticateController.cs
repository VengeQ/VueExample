using Microsoft.AspNetCore.Mvc;
using Security;
using System.Security.Claims;

namespace QuizesApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController(ITokenService tokenService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Authorize(UserCredentials user)
        {
            var claims = new Dictionary<string, object> { [ClaimTypes.Name] = user.Username };
            var token = tokenService.GenerateToken(claims);

            return Ok(new { Id = user.Username, token });

            //return Unauthorized("Something wrong");
        }

        public class UserCredentials
        {
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
    }
}
