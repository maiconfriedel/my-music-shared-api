using Microsoft.Extensions.DependencyInjection;
using MyMusicSharedBackend.Core.Interfaces.Gateways;
using MyMusicSharedBackend.Infrastructure.EntityFramework;
using MyMusicSharedBackend.Infrastructure.Repositories;

namespace MyMusicSharedBackend.Infrastructure
{
    /// <summary>
    /// Add the extensions of the Infrastructure to the Service Collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the services of the Infrastructure
        /// </summary>
        /// <param name="services">Service Collection that will the Infrastructure register to</param>
        /// <returns>Service Collection with the Infrastructure services into</returns>
        public static IServiceCollection AddMyMusicSharedBackendInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<MyMusicSharedDbContext>();

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}