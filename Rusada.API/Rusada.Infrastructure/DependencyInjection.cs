using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rusada.Core.Common.Interfaces;

namespace Rusada.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationPersistenceServices(this IServiceCollection serviceCollection,
            IConfiguration configuration, IHostEnvironment environment)
        {
            serviceCollection.AddDbContext<IRusadaDbContext, RusadaDbContext>(opBuilder =>
            {
                opBuilder.UseSqlServer(configuration.GetConnectionString("RusadaDb"), option =>
                {
                    option.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);

                });


                if (environment.IsDevelopment())
                {
                    opBuilder.EnableDetailedErrors();
                    opBuilder.EnableSensitiveDataLogging();
                    opBuilder.LogTo(Console.WriteLine);
                }

            });

            return serviceCollection;
        }
    }
}
