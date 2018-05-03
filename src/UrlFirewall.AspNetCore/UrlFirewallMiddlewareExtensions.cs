using Microsoft.AspNetCore.Builder;

namespace UrlFirewall.AspNetCore
{
    public static class UrlFirewallMiddlewareExtensions
    {
        public static IApplicationBuilder UseUrlFirewall(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UrlFirewallMiddleware>();
        }
    }
}