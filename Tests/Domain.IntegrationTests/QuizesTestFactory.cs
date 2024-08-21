using Domain.Repository.Quizes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VueExample.Server;

namespace Domain.IntegrationTests
{
    public class QuizesTestFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected WebApplicationFactory<Startup> Factory { get; private set; }

        protected HttpClient HttpClient { get; set; } = null!;

        private readonly string _connectionString = null!;

        public QuizesTestFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            Factory = new QuizesTestFactory<Startup>(_connectionString);
            using var _scope = Factory.Services.CreateScope();
            var quizContext = _scope.ServiceProvider.GetRequiredService<QuizTestContext>();
            quizContext.Database.EnsureDeleted();
            quizContext.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            using var _scope = Factory.Services.CreateScope();
            var quizContext = _scope.ServiceProvider.GetRequiredService<QuizTestContext>();
            quizContext.Database.EnsureDeleted();
            quizContext.Dispose();
            Factory?.Dispose();
        }

        [SetUp]
        public void SetUpBase()
        {
            HttpClient = Factory.CreateClient();
        }

        [TearDown]
        public void TearDownBase()
        {
            HttpClient?.Dispose();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<QuizTestContext>();

                IOptions<QuizContextOptions> contextOptions = Options.Create(new QuizContextOptions
                {
                    ConnectionString = _connectionString
                });
                services.AddSingleton(contextOptions);
            });
        }
    }
}