using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VueExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        public AuthenticateController() { }

        [HttpPost(Name = "Autorize")]
        public async Task<IActionResult> Autorize(UserCredentials user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
            var jwt = new JwtSecurityToken(
            claims: claims,
            issuer: "issuer",
            audience: "audience",
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VERY_SECRET_BEATIFUL_KEY_ISJAFWJAFJ")), SecurityAlgorithms.HmacSha256));

            return Ok(new { Id = user.Username, Token = new JwtSecurityTokenHandler().WriteToken(jwt) });
        }

        public class UserCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
