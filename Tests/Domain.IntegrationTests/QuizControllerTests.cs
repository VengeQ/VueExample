using Domain.Quizes;
using Domain.Repository.Quizes;
using Domain.Services.Quizes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VueExample.Server;
using VueExample.Server.Controllers;

namespace Domain.IntegrationTests
{
    public class QuizControllerTests
    {

        ILogger<QuizesController> _logger = new DummyLogger<QuizesController>();
        WebApplicationFactory<Startup> _factory;
        HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _logger = new DummyLogger<QuizesController>();
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    //
                });
            });
            using (var _scope = _factory.Services.CreateScope())
            {
                var context = _scope.ServiceProvider.GetRequiredService<QuizContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }


            _httpClient = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient?.Dispose();
            _factory?.Dispose();
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

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
