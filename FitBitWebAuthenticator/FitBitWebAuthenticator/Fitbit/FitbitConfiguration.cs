using System;
using FitBitWebAuthenticator.Models;

namespace FitBitWebAuthenticator.FirBit
{
    public class FitbitConfiguration
    {
        public const string ClientId        = "xxxxx";                            //client_id
        public const string ClientSecret    = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";  //client_secret

        public const string Callback        = "com.vipxam.webauthenticator-12345://callback";
        public const string CallbackEscaped = "com.vipxam.webauthenticator-12345%3A%2F%2Fcallback";

        public const string CallbackScheme  = "com.vipxam.webauthenticator-12345";

        public const string Auth2Url = "https://www.fitbit.com/oauth2/authorize?";

        public const string ScopesEscaped = "activity%20profile";
        public const string ExpireIn = "604800";

        // Token
        public const string TokenApiUri = "https://api.fitbit.com/oauth2/token";
        public const string GrantType = "authorization_code";

        //values from APIs
        public static string AuthorizationBearer { get; set; }

        public static FitBitResponseModel TokenResponse { get; set; }

    }
}
