using Domain.Repository;
using Domain.Repository.Quizes;
using Domain.Services.Quizes;

namespace VueExample.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<IQuizService, QuizService>();
            services.AddTransient<IQuizAdminService, QuizAdminService>();
            services.AddTransient<IQuizRepository, QuizRepository>();
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

            //app.UseAuthorization();

            app.UseEndpoints(app => {
               // app.MapDefaultControllerRoute();
                app.MapControllers();
                
                app.MapFallbackToFile("/index.html");
            });

            //app.Run();
        }
    }
}
