using Domain.Quizes;
using Domain.Repository.Quizes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using VueExample.Server;
using VueExample.Server.Controllers;

namespace Domain.IntegrationTests
{
    public class QuizControllerTests
    {

        ILogger<QuizesController> _logger = new DummyLogger<QuizesController>();
        WebApplicationFactory<Startup> _factory;
        HttpClient _httpClient;
        JsonSerializerOptions _jsonSerializerOptions;

        [SetUp]
        public void SetUp()
        {       
            _httpClient = _factory.CreateClient();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _jsonSerializerOptions = new(JsonSerializerDefaults.Web);
            _logger = new DummyLogger<QuizesController>();
            _factory = new QuizesTestFactory<Startup>();
            using var _scope = _factory.Services.CreateScope();
            var quizContext = _scope.ServiceProvider.GetRequiredService<QuizContext>();
            quizContext.Database.EnsureDeleted();
            quizContext.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            using var _scope = _factory.Services.CreateScope();
            var quizContext = _scope.ServiceProvider.GetRequiredService<QuizContext>();
            quizContext.Database.EnsureDeleted();
            quizContext.Dispose();
            _factory?.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient?.Dispose();      
        }

        [Test]
        public async Task not_found()
        {
            var response = await _httpClient.GetAsync(new Uri("api/Quizes/2", UriKind.Relative));    

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test, Sequential]
        public async Task bad_request([Values(-1_000_000, -1, 0)] int id)
        {
            var uri = new Uri($"api/Quizes/{id}", UriKind.Relative);

            var response = await _httpClient.GetAsync(uri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task return_quiz()
        {
            var response = await _httpClient.GetAsync(new Uri("api/Quizes/1", UriKind.Relative));

            var result = await response.Content.ReadFromJsonAsync<Quiz>(_jsonSerializerOptions);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result!.Id, Is.EqualTo(1));
            });
        }
    }
}
