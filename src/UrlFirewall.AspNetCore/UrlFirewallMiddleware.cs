using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UrlFirewall.AspNetCore
{
    public class UrlFirewallMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUrlFirewallValidator _validator;
        private readonly ILogger<UrlFirewallMiddleware> _logger;

        public UrlFirewallMiddleware(RequestDelegate next,
            IUrlFirewallValidator validator,
            ILogger<UrlFirewallMiddleware> logger)
        {
            _next = next;
            _validator = validator;
            _logger = logger;
        }

        public Task Invoke(HttpContext context)
        {
            string path = context.Request.Path.ToString().ToLower();
            string method = context.Request.Method.ToLower();
            if (_validator.Validate(path, method))
            {
                _logger.LogInformation($"The path {path} valid.");
                return this._next(context);
            }
            else
            {
                _logger.LogInformation($"The path {path} invalid.");
                context.Response.StatusCode = (int) _validator.Options.StatusCode;
                return Task.CompletedTask;;
            }
        }
    }
}