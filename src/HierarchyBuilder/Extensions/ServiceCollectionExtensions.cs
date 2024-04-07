using HierarchyBuilder.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HierarchyBuilder.Extensions
{
    /// <summary>
    /// Service collection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the hierarchy builder to the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddHierarchyBuilder(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            services.Add(new ServiceDescriptor(typeof(IHierarchyTransformer), typeof(Transformer), serviceLifetime));
            return services;
        }
    }
}