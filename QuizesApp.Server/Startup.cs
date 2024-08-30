using Domain.Repository.Quizes;
using Domain.Services.Quizes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Security;
using System.Text;

namespace QuizesApp.Server;

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

        ConfigureSecurityContext(services);

        services.AddTransient<ITokenService, TokenService>();

        services.AddScoped<IQuizService, QuizService>();
        services.AddScoped<IQuizAdminService, QuizAdminService>();
        services.AddScoped<IQuizRepository, QuizRepository>();

        var quizContextSection = Configuration.GetSection("QuizContextOptions") ?? throw new ApplicationConfigurationException("Invalid database context options");
        services.Configure<QuizContextOptions>(quizContextSection);

        services.AddScoped<QuizContext>();
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

    private void ConfigureSecurityContext(IServiceCollection services)
    {
        var section = Configuration.GetSection("TokenOptions");
        var tokenOptions = section.Get<TokenOptions>() ?? throw new ApplicationConfigurationException("Invalid token options");
        services.Configure<TokenOptions>(section);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization();
    }
}