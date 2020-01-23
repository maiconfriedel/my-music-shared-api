using Microsoft.Extensions.DependencyInjection;
using MyMusicSharedBackend.Core.Interfaces.UseCases;
using MyMusicSharedBackend.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core
{
    /// <summary>
    /// Add the extensions of the Core to the Service Collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the services of the Core
        /// </summary>
        /// <param name="services">Service Collection that will the Core register to</param>
        /// <returns>Service Collection with the Core services into</returns>
        public static IServiceCollection AddMyMusicSharedBackendCore(this IServiceCollection services)
        {
            services.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();

            return services;
        }
    }
}