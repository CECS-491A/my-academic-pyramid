using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using SecurityLayer.Authorization;

namespace KFC.SIT.WebAPI.Utility
{
    public static class SecurityContextBuilder
    {
        public static SecurityContext CreateSecurityContext(HttpRequestHeaders headers)
        {
            string token;
            if (!headers.Contains("Authorization"))
            {
                return null;
            }
            try
            {
                string[] parts = headers.GetValues("Authorization").First().Split(' ');
                if (parts.Length != 2 || parts[0] != "Bearer")
                {
                    return null;
                }
                token = parts[1];
            }
            catch (InvalidOperationException) // Catch when Token header has no value.
            {
                return null;
            }
            return new SecurityContext(token);
        }
    }
}