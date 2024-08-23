using Domain.Services.Quizes;
using Microsoft.AspNetCore.Mvc;

namespace VueExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IQuizAdminService quizAdminService) : ControllerBase
    {
        private readonly IQuizAdminService _quizAdminService = quizAdminService;

        [HttpGet("Version", Name = "GetVersion")]
        public async Task<IActionResult> GetVersion()
        {
            var result = await _quizAdminService.GetVersion();

            return Ok(result);
        }
    }
}
