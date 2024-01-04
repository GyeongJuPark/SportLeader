using Microsoft.EntityFrameworkCore;
using SportLeader.Data;
using SportLeader.Services;
using System;

namespace SportLeader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SpotrsLeaderDBContext>(options =>
            {
                var _config = builder.Configuration;
                options.UseSqlServer(_config.GetConnectionString("MyAppConnection"));
            });

            builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
            builder.Services.AddScoped<ISportLeaderService, SportLeaderService>();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

            app.Run();
        }
    }
}
