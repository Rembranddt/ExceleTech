using ExceleTech.API.Settings;
using ExceleTech.Domain.Options;
using ExceleTech.Infrastructure.Dependency_Injection;
using ExceleTech.Application.DependencyInjection;
using ExceleTech.API.Middlewares;
using Serilog;
using ExceleTech.API.Swagger;

namespace ExceleTech.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
            builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddJwtSettings(builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>());
            builder.Services.AddSwagger();
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));
          

            var app = builder.Build();
            app.AddMiddlewares();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Fontech Swagger v1.0");
                c.RoutePrefix = string.Empty;
            });
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
