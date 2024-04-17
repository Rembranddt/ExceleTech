using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ExceleTech.Domain.Interfaces.Services;
using ExceleTech.Domain.Entities;
using ExceleTech.Infrastructure.Cache;
using ExceleTech.Domain.Interfaces.Repositories;
using ExceleTech.Infrastructure.Repositories;
using ExceleTech.Application.Responses.ProductResponses;
using Quartz;
using ExceleTech.Infrastructure.BackgroundJobs.UpdateProductStatistics;

namespace ExceleTech.Infrastructure.Dependency_Injection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<EFContext>((dbContextConfig) =>
            {
                dbContextConfig.UseNpgsql(configuration.GetConnectionString("NpgDocker"));

            });
            services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetConnectionString("Cache"));
            services.AddScoped<ICacheService<User>, CacheService<User>>();
            services.AddScoped<ICacheService<string>, CacheService<string>>();
            services.AddScoped<ICacheService<SearchProductsResponse>, CacheService<SearchProductsResponse>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.InitRepositories();
            services.SetUpQuartz();
        }
        public static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IBasketRepository, BasketRepository>()
                    .AddScoped<IProductRepository, ProductRepository>()
                    .AddScoped<IOrderRepository, OrderRepository>();

        }
        public static void SetUpQuartz(this IServiceCollection services)
        {
            var updateProductJobKey = JobKey.Create(nameof(UpdateProductStatisticsJob));
            services.AddQuartz();   

           
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<UpdateProductStatisticsJobConfiguration>();
        }

    }
        

}
