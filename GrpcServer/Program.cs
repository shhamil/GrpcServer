using GrpcServer.DataContext;
using GrpcServer.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// чтение конфигурации из файла
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddGrpc();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql("Host=postgres;Port=5432;Database=grpcServDb;Username=shamil;Password=shamil1998"));

var app = builder.Build();

// считываем порт из конфигурации
int grpcPort = builder.Configuration.GetValue<int>("GRPCServer:Port");

var retries = 5;
var delay = TimeSpan.FromSeconds(2);

while (retries > 0)
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            await dbContext.Database.MigrateAsync();
        }
        retries = 0;
    }
    catch (Exception ex) when (retries > 0)
    {
        retries--;
        Console.WriteLine($"Failed to migrate database. Retrying in {delay.TotalSeconds} seconds. {retries} retries left.");
        await Task.Delay(delay);
    }
}

app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

// устанавливаем считанный порт при запуске приложения
await app.RunAsync($"http://0.0.0.0:{grpcPort}");