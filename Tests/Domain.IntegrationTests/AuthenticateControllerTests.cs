using Microsoft.Extensions.Logging;
using QuizesApp.Server;
using QuizesApp.Server.Controllers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Domain.IntegrationTests
{
    public class AuthenticateControllerTests : ApplicationTestFactory<Startup>
    {
        private ILogger<QuizesController> _logger = new DummyLogger<QuizesController>();
        private readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);
        private static readonly string _connectionString =
            $"Host=localhost;Port=5455;Database=quizes_{Guid.NewGuid()};Username=postgres;Password=postgresPW";
        private readonly Uri _authenticateUri = new("api/authenticate", UriKind.Relative);

        public AuthenticateControllerTests() : base(_connectionString)
        {
        }

        [Test]
        public async Task success()
        {
            var userCredentials = new { UserName = "test", Password = "test" };
            var content = JsonContent.Create(userCredentials);

            var response = await HttpClient.PostAsync(_authenticateUri, content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task not_exists_user()
        {
            var userCredentials = new { UserName = "not_exists_user", Password = "test" };
            var content = JsonContent.Create(userCredentials);

            var response = await HttpClient.PostAsync(_authenticateUri, content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task invalid_password()
        {
            var userCredentials = new { UserName = "test", Password = "invalid_password" };
            var content = JsonContent.Create(userCredentials);

            var response = await HttpClient.PostAsync(_authenticateUri, content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

    }
}
