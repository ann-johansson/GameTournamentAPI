
using GameTournamentAPI.Data;
using GameTournamentAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace GameTournamentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get the connection string from configuration (appsettings.json)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Tell Entity Framework to use SQL Server with the provided connection string
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<TournamentsService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Exposes: /openapi/v1.json
                app.MapOpenApi();

                // Exposes Swagger UI: /swagger
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
                    options.RoutePrefix = "swagger";
                });
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
