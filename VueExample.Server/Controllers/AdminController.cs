using Domain.Services.Quizes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VueExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private IQuizAdminService _quizAdminService;
        public AdminController(IQuizAdminService quizAdminService) { _quizAdminService = quizAdminService; }

        [HttpGet("Version", Name = "GetVersion")]
        public async Task<IActionResult> GetVersion()
        {
            var result = await _quizAdminService.GetVersion();

            return Ok(result);
        }
    }
}
