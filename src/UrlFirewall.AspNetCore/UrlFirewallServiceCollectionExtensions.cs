using System;
using Microsoft.Extensions.DependencyInjection;

namespace UrlFirewall.AspNetCore
{
    public static class UrlFirewallServiceCollectionExtensions
    {
        public static IServiceCollection AddUrlFirewall(this IServiceCollection services, Action<UrlFirewallOptions> options)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            services.AddOptions();
            services.Configure(options);
            services.AddSingleton<IUrlFirewallValidator, DefaultUrlFirewallValidator>();

            return services;
        }

        public static IServiceCollection AddUrlFirewallValidator<T>(this IServiceCollection services) where T : IUrlFirewallValidator
        {
            services.AddSingleton(typeof(IUrlFirewallValidator),typeof(T));
            return services;
        }
    }
}