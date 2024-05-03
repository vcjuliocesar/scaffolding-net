using Scaffolding.Services.Auth.Contracts;
using Scaffolding.Services.Auth.Services;
using Scaffolding.Services.Auth.Repositories;
using Scaffolding.Services.Auth.Dbcontext;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Scaffolding.Services.Auth.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuraci�n de la configuraci�n
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("MySqlConnection");
        
        // Add services to the container.
        builder.Services.AddScoped<IDatabaseConnection, MySqlDatabaseConnection>(provider =>
        {           
            return new MySqlDatabaseConnection(connectionString);
        });

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Agregar el middleware de encriptaci�n y desencriptaci�n de contrase�as
        //builder.Services.AddTransient<PasswordEncryptionMiddleware>();

        var app = builder.Build();

        // Middleware personalizado para encriptar y desencriptar contrase�as
        //app.UseMiddleware<PasswordEncryptionMiddleware>();

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