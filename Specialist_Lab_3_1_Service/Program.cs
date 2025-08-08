using Specialist_Lab_3_1_Service.Context;

namespace Specialist_Lab_3_1_Service;
internal class Program
{
    internal static IConfiguration appConfig = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddSqlServer<AppDbContext>(appConfig.GetConnectionString("MsSql"));
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();
        
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Run();
    }
}