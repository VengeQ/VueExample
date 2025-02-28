﻿using Domain.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using QuizesApp.Server;
using Security;
using System.Security.Claims;

namespace Domain.IntegrationTests
{
    /// <summary>
    /// Фабрика для развертывания интеграционных тестов для викторин
    /// </summary>
    public class ApplicationTestFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected WebApplicationFactory<Startup> Factory { get; private set; } = null!;

        protected HttpClient HttpClient { get; set; } = null!;

        private readonly string _connectionString = null!;
        [ThreadStatic] private static string? t_token;

        public ApplicationTestFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + t_token);
            base.ConfigureClient(client);
        }

        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            Factory = new ApplicationTestFactory<Startup>(_connectionString);
            using var _scope = Factory.Services.CreateScope();
            var context = _scope.ServiceProvider.GetRequiredService<TestApplicationContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var tokenService = _scope.ServiceProvider.GetRequiredService<ITokenService>();
            var testClaims = new Dictionary<string, object>() { [ClaimTypes.Name] = "Testuser" };
            t_token = tokenService.GenerateToken(testClaims);
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            using var _scope = Factory.Services.CreateScope();
            var context = _scope.ServiceProvider.GetRequiredService<TestApplicationContext>();
            context.Database.EnsureDeleted();
            context.Dispose();
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
                services.AddTransient<TestApplicationContext>();

                IOptions<ApplicationContextOptions> contextOptions = Options.Create(new ApplicationContextOptions
                {
                    ConnectionString = _connectionString
                });
                services.AddSingleton(contextOptions);
            });
        }
    }
}