using Infrastructure.Database.PostgresDb.Configurations;

using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var host = builder.Build();
host.Run();
