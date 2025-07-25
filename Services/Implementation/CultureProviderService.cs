using Microsoft.AspNetCore.Localization;
using System.Net;

namespace Multiple_Desk.Services.Implementation
{
    public class CultureProviderService : IRequestCultureProvider
    {
        public int Order => 0; // Set the order of provider execution

        public async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var culture = await GetCurrentCulture(httpContext);
            return new ProviderCultureResult(culture);
        }

        private async Task<string> GetCurrentCulture(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine("Request IP: " + ipAddress);
            // _logger.LogInformation($"Request IP:{ipAddress}");
            if (string.IsNullOrEmpty(ipAddress) || IPAddress.IsLoopback(IPAddress.Parse(ipAddress)))
            {
                return "en-US";
            }

            var isNepaliIp = await IsNepaliIP(ipAddress);
            return isNepaliIp ? "ne-NP" : "en-US";
        }

        private async Task<bool> IsNepaliIP(string ipAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var response = await client.GetStringAsync($"https://ipapi.co/{ipAddress}/country/");
                    Console.WriteLine("ipchck response: " + response);
                    //  _logger.LogInformation("Ip check result:{response}", response);
                    return response.Trim().Equals("NP", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch
            {
               // _logger.LogWarning("IPApi could not be called!");
                return false;
            }
        }
    }
}