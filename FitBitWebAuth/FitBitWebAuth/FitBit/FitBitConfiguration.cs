using System;
using FitBitWebAuthenticator.Models;

namespace FitBitWebAuthenticator.FitBit
{
    public class FitBitConfiguration
    {
        public const string ClientId        
            = "2396QC";                         
        public const string ClientSecret    
            = "67de02d269cd6095aebadc998f10a55f"; 

        public const string Callback = 
           //                "https://" +
           //"app-settings.fitbitdevelopercontent.com/simple-redirect.html";
           "com.vipxam.fitbit-12345://callback";
        public const string CallbackEscaped =
        //           "app-settings.fitbitdevelopercontent.com%3Fsimple-redirect.html";
        //"https%3A%2F%2Fapp-settings.fitbitdevelopercontent.com%3Fsimple-redirect.html";
        "com.vipxam.fitbit-12345%3A%2F%2Fcallback";

        public const string CallbackScheme =
          //"app-settings.fitbitdevelopercontent.com";
          "com.vipxam.fitbit-12345";

        public const string Auth2Url = "https://www.fitbit.com/oauth2/authorize?";

        public const string ScopesEscaped = "activity+profile";//"activity%20profile";
        public const string ExpireIn = "604800";

        // Token
        public const string TokenApiUri = "https://api.fitbit.com/oauth2/token";
        public const string GrantType = "authorization_code";

        //values from APIs
        public static string AuthorizationBearer { get; set; }

        public static FitBitResponseModel TokenResponse { get; set; }

    }
}
