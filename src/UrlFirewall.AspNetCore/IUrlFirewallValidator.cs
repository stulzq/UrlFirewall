namespace UrlFirewall.AspNetCore
{
    public interface IUrlFirewallValidator
    {
        UrlFirewallOptions Options { get; set; }

        /// <summary>
        /// Validate url
        /// </summary>
        /// <param name="url">Request url</param>
        /// <param name="method">Request method</param>
        /// <returns>True:Allow this request.False:This request is not allowed</returns>
        bool Validate(string url,string method);
    }
}