using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Rusada.Core.Interface;
using Rusada.Core.Services;

namespace Rusada.Core
{
    // Register all depedancies belong to Core project 
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCoreDependencies(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IAircraftSightingService, AircraftSightingService>();

            return services;
        }
    }
}
