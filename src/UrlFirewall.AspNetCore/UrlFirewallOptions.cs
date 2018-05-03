using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace UrlFirewall.AspNetCore
{
    public class UrlFirewallOptions
    {
        /// <summary>
        /// White List or Black List.Default is Black.
        /// </summary>
        public UrlFirewallRuleType RuleType { get; set; } = UrlFirewallRuleType.Black;

        /// <summary>
        /// Standard Rule.String complete matching
        /// </summary>
        public List<UrFirewalllRule> StandardRuleList { get; set; }=new List<UrFirewalllRule>();

        /// <summary>
        /// Regex Rule.Regex matching.The two rule list is set up to speed up the matching.
        /// </summary>
        public List<UrFirewalllRule> RegexRuleList { get; set; }=new List<UrFirewalllRule>();

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

        public void SetRuleList(IConfigurationSection section)
        {
            if (section == null)
            {
                throw new ArgumentException(nameof(section));
            }

            var list = section.Get<List<UrFirewalllRule>>();

            if (list == null)
            {
                throw new UrlFirewallException("The section key is invalid.");
            }

            foreach (var t in list)
            {
                t.Method = t.Method.ToLower();

                //Whether it is a special rule or not
                if (t.Url.Contains("/*") || t.Url.Contains("/?"))
                {
                    //convert to regex 
                    t.Url = t.Url.Replace("/*", "/.*").Replace("/?", "/.?").ToLower();
                    RegexRuleList.Add(t);
                }
                else
                {
                    t.Url = t.Url.ToLower();
                    StandardRuleList.Add(t);
                }
            }
        }
    }
}