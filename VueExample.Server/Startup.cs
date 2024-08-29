using Domain.Repository.Quizes;
using Domain.Services.Quizes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VueExample.Server;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "issuer",
                ValidAudience = "audience",
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes("VERY_SECRET_BEATIFUL_KEY_ISJAFWJAFJ")),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization();            // добавление сервисов авторизации

        services.AddTransient<IQuizService, QuizService>();
        services.AddTransient<IQuizAdminService, QuizAdminService>();
        services.AddTransient<IQuizRepository, QuizRepository>();

        //services.AddOptions<QuizContextOptions>()
        //    .Bind(Configuration.GetSection("QuizContextOptions"));


        services.Configure<QuizContextOptions>(
            Configuration.GetSection("QuizContextOptions")
        );

        //IOptions<QuizContextOptions> contextOptions = Options.Create(new QuizContextOptions
        //{
        //    ConnectionString = @"Host=localhost;Port=5455;Database=quizes;Username=postgres;Password=postgresPW",
        //});
        //services.AddSingleton(contextOptions);

        services.AddTransient<QuizContext>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();   // добавление middleware аутентификации 
        app.UseAuthorization();   // добавление middleware авторизации 

        app.UseEndpoints(app =>
        {
            // app.MapDefaultControllerRoute();
            app.MapControllers();

            app.MapFallbackToFile("/index.html");
        });
    }
}

