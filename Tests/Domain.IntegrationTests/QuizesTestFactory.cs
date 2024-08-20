using Domain.Repository.Quizes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Domain.IntegrationTests
{
    public class QuizesTestFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<QuizContext>();

                IOptions<QuizContextOptions> contextOptions = Options.Create(new QuizContextOptions
                {
                    ConnectionString = @"Host=localhost;Port=5455;Database=quizes_test;Username=postgres;Password=postgresPW",
                });
                services.AddSingleton(contextOptions);

            });
        }
    }
}
