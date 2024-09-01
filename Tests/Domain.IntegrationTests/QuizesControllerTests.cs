using Domain.Quizes;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using QuizesApp.Server;
using QuizesApp.Server.Controllers;

namespace Domain.IntegrationTests
{
    [SingleThreaded, NonParallelizable]
    public class QuizesControllerTests : ApplicationTestFactory<Startup>
    {
        private ILogger<QuizesController> _logger = new DummyLogger<QuizesController>();
        private readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);
        private static readonly string _connectionString = 
            $"Host=localhost;Port=5455;Database=quizes_{Guid.NewGuid()};Username=postgres;Password=postgresPW";

        public QuizesControllerTests() : base(_connectionString)
        {
        }

        [Test]
        public async Task not_found()
        {
            var response = await HttpClient.GetAsync(new Uri("api/Quizes/3", UriKind.Relative));    

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test, Sequential]
        public async Task bad_request([Values(-1_000_000, -1, 0)] int id)
        {
            var uri = new Uri($"api/Quizes/{id}", UriKind.Relative);

            var response = await HttpClient.GetAsync(uri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test, Sequential]
        public async Task return_quiz([Values(1, 2)] int id)
        {
            var response = await HttpClient.GetAsync(new Uri($"api/Quizes/{id}", UriKind.Relative));

            var result = await response.Content.ReadFromJsonAsync<Quiz>(_jsonSerializerOptions);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result!.Id, Is.EqualTo(id));
            });
        }
    }
}
