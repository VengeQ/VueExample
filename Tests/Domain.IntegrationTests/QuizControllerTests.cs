using Domain.Quizes;
using Domain.Services.Quizes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
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
            _factory = new WebApplicationFactory<Startup>();
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
            var response = await _httpClient.GetAsync(new Uri("Quizes/2", UriKind.Relative));    

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test, Sequential]
        public async Task bad_request([Values(-1_000_000, -1, 0)] int id)
        {
            var uri = new Uri($"Quizes/{id}", UriKind.Relative);

            var response = await _httpClient.GetAsync(uri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
