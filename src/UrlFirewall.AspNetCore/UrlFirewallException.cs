using System;

namespace UrlFirewall.AspNetCore
{
    public class UrlFirewallException:Exception
    {
        public UrlFirewallException(string message,Exception innerExcetion):base(message,innerExcetion)
        {
            
        }

        public UrlFirewallException(string message) : base(message)
        {

        }
    }
}