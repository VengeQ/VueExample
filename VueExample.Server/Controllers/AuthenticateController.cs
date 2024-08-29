using Microsoft.AspNetCore.Mvc;

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
            return Ok(new { Id = 1, Token = "testtoken"});
        }

        public class UserCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
