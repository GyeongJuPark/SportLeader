using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportLeader.Application.SportsLeader;
using SportLeader.Application.SportsLeader.Validators;
using SportLeader.Controllers.Client.Request;
using SportLeader.Infra.DB;
using SportLeader.Repository;
using static SportLeader.Application.SportsLeader.SportLeaderService;
namespace SportLeader
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var corsPolicyName = "MyCorsPolicy";

      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddDbContext<SpotrsLeaderDBContext>(options =>
      {
        var _config = builder.Configuration;
        options.UseSqlServer(_config.GetConnectionString("MyAppConnection"));
      });

      builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
      builder.Services.AddScoped<LeaderRepository>();
      builder.Services.AddScoped<ISportLeaderService, SportLeaderService>();

      //SportLeaderValidator 
      builder.Services.AddScoped<IValidator<RegisterSportsLeaderRequest>, SportLeaderValidator>();
      

      builder.Services.AddAutoMapper(typeof(MappingProfile));

      builder.Services.AddCors(option =>
      {
        option.AddPolicy(name: corsPolicyName,
          policy =>
          {
            policy
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
          });
      });

      var app = builder.Build();

      app.UseStaticFiles();
      app.UseRouting();

      // Cors ¼³Á¤ 
      app.UseCors(corsPolicyName);

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
