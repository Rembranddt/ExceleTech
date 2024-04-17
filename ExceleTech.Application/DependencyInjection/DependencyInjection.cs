using System.Reflection;
using ExceleTech.Application.Behaviors;
using ExceleTech.Application.Services;
using ExceleTech.Domain.Interfaces.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExceleTech.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                options.AddOpenBehavior(typeof(RequestLoggingBehavior<,>));
                options.AddOpenBehavior(typeof(ExceptionHandlingBehavior<,>));
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));


            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); ;
            services.AddServices();
          
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}