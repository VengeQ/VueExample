using Domain.Services.Quizes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizesApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizesController : ControllerBase
    {

        private readonly ILogger<QuizesController> _logger;
        private readonly IQuizService _quizService;

        public QuizesController(ILogger<QuizesController> logger, IQuizService quizService)
        {
            _logger = logger;
            _quizService = quizService;
        }

        [HttpGet("{id:int}", Name = "GetQuiz")]
        [Authorize]
        public async Task<IActionResult> GetQuiz(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var quiz = await _quizService.Get(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        [HttpGet(Name = "Quizes")]
        [Authorize]
        public async Task<IActionResult> GetQuizes()
        {
            var quizes = await _quizService.Get();

            return Ok(quizes);
        }
    }
}
