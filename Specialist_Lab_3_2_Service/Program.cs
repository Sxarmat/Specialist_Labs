using Specialist_Lab_3_2_Service.Context;
using Specialist_Lab_3_2_Service.Services;

namespace Specialist_Lab_3_2_Service;

internal class Program
{
    internal static IConfiguration appConfig = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSqlServer<AppDbContext>(appConfig.GetConnectionString("MsSql"));
        builder.Services.AddGrpc();

        var app = builder.Build();

        app.MapGrpcService<CourseApiService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}