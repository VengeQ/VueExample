using Domain;
using Microsoft.AspNetCore.Mvc;

namespace VueExample.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController: ControllerBase
    {

        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public Task<IEnumerable<Quiz>> Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            return Task.FromResult<IEnumerable<Quiz>>(null);
        }
    }
}
