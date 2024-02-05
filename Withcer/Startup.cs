using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Withcer.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Withcer.Models;

namespace Withcer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<Data>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Withcer")));

            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Withcer", Version = "v1" });
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostRatingService, PostRatingService>();
            //services.AddScoped<другое название интерфейса, сервис название>();
            services.AddScoped<IJwtService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var secretKey = configuration["Jwt:SecretKey"];
                return new JwtService(secretKey);
            });
            services.AddSingleton<ITokenService, TokenService>();
            // Добавьте другие сервисы, если необходимо
        }

        private void CheckDatabaseConnection(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<Data>())
            {
                try
                {
                    context.Database.OpenConnection();
                    context.Database.CloseConnection();
                    Console.WriteLine("Database connection successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error connecting to the database: " + ex.Message);
                }
            }
        }
       

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Withcer");
            });

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                CheckDatabaseConnection(serviceScope.ServiceProvider);
            }
        }
    }
}
