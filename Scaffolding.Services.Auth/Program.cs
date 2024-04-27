using Scaffolding.Core.Interfaces;
using Scaffolding.Core.Services;
using Scaffolding.Core.Repositories;
using Scaffolding.Dbcontext.Interfaces;
using Scaffolding.Dbcontext.Dbcontext;
using System.Configuration;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuración de la configuración
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("SqliteConnection");

        // Add services to the container.
        builder.Services.AddScoped<IDatabaseConnection, SqliteDatabaseConnection>(provider =>
        {           
            return new SqliteDatabaseConnection(connectionString);
        });

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}