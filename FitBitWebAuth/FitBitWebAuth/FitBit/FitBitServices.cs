using System;
using System.Text;

namespace FitBitWebAuthenticator.FitBit
{
    public class FitBitServices
    {
        public static string BuildAuthenticationUrl()
        {
            return $"{FitBitConfiguration.Auth2Url}" +
                $"response_type=code" +
                $"&client_id={FitBitConfiguration.ClientId}" +
                $"&state=2F" +
                $"&redirect_uri={FitBitConfiguration.CallbackEscaped}" +
                $"&scope={FitBitConfiguration.ScopesEscaped}" +
                $"&expires_in={FitBitConfiguration.ExpireIn}";
        }

        public static string Base64String()
        {
            var authString = FitBitConfiguration.ClientId + ":" + FitBitConfiguration.ClientSecret;
            var bytes = Encoding.UTF8.GetBytes(authString);
            var base64String = Convert.ToBase64String(bytes);

            return base64String;
        }
    }
}
