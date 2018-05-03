using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UrlFirewall.AspNetCore
{
    public class DefaultUrlFirewallValidator: IUrlFirewallValidator
    {
        private readonly ILogger<DefaultUrlFirewallValidator> _logger;
        public UrlFirewallOptions Options { get; set; }

        public DefaultUrlFirewallValidator(ILogger<DefaultUrlFirewallValidator> logger,IOptions<UrlFirewallOptions> options)
        {
            _logger = logger;
            Options = options.Value;

        }

        public bool Validate(string url,string method)
        {
            string valUrl = url;
            if (valUrl.Length > 1 && valUrl.Last() == '/')
            {
                valUrl = valUrl.Substring(0, valUrl.Length - 1);
            }

            var rule = Options.StandardRuleList.FirstOrDefault(m => m.Url == valUrl);

            if (rule == null)
            {
                foreach (var item in Options.RegexRuleList)
                {
                    if (Regex.IsMatch(url, item.Url, RegexOptions.IgnoreCase))
                    {
                        rule = item;
                        break;
                    }
                }
            }

            if (Options.RuleType == UrlFirewallRuleType.Black)
            {
                if (rule == null)
                {
                    return true;
                }
                else
                {
                    //in balck list,next step is match method.

                    if (rule.Method == "all")
                    {
                        return false;
                    }
                    else if (rule.Method == method)
                    {
                        //if path & method are matched,not allow this request.
                        return false;
                    }
                    else
                    {
                        // if path & method are not matched,allow this request.
                        return true;
                    }
                }

                
            }
            else
            {
                if (rule == null)
                {
                    return false;
                }
                else
                {
                    //in white list,next step is match method.

                    if (rule.Method == "all")
                    {
                        return true;
                    }
                    else if (rule.Method == method)
                    {
                        //if path & method are matched,allow this request.
                        return true;
                    }
                    else
                    {
                        // if path & method are not matched,not allow this request.
                        return false;
                    }
                }
            }

            
            
        }
    }
}