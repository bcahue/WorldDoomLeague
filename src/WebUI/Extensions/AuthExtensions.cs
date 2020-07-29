using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WorldDoomLeague.Api.Extensions
{
    public static class AuthExtensions
    {
        public static async Task<string[]> GetExternalProvidersAsync(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

            var authSchemes = (from scheme in await schemes.GetRequestHandlerSchemesAsync()
                    where !string.IsNullOrEmpty(scheme.DisplayName)
                    select scheme).ToArray();

            List<string> authStrings = new List<string>();

            foreach (var scheme in authSchemes)
            {
                authStrings.Add(scheme.DisplayName);
            }
            return authStrings.ToArray();
        }

        public static async Task<bool> IsProviderSupportedAsync(this HttpContext context, string provider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return (from scheme in await context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>().GetRequestHandlerSchemesAsync()
                    where string.Equals(scheme.Name, provider, StringComparison.OrdinalIgnoreCase)
                    select scheme).Any();
        }
    }
}
