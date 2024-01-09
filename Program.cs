using Microsoft.EntityFrameworkCore;
using SportLeader.Application.SportsLeader;
using SportLeader.Infra.DB;
using SportLeader.Repository;
using static SportLeader.Application.SportsLeader.SportLeaderService;
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
            builder.Services.AddScoped<LeaderRepository>();
            builder.Services.AddScoped<ISportLeaderService, SportLeaderService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            app.Run();
        }
    }
}
