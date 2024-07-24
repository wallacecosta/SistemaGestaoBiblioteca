using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Infrastructure.Context;

namespace SistemaGestaoBiblioteca.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AppConnection"),
                    opt =>
                    {
                        opt.MigrationsHistoryTable("__AppMigrationsHistory", "dbo");
                        opt.CommandTimeout(60);
                    }
                );
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Scan(scan =>
            {
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            return services;
        }
    }
}
