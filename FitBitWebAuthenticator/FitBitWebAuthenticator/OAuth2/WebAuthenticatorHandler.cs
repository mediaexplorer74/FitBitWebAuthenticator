using System;
using Xamarin.Essentials;
using System.Threading.Tasks;
using FitBitWebAuthenticator.FirBit;
using System.Diagnostics;

namespace FitBitWebAuthenticator
{
    public class WebAuthenticatorHandler
    {
        public async Task<string> FetchFitbitCode()
        {
            string code = "";
            try
            {
                Uri callbackUrl = new Uri(FitbitConfiguration.Callback);

                Uri loginUrl = new Uri(FitbitServices.BuildAuthenticationUrl());

                WebAuthenticatorResult authenticationResult = 
                    await WebAuthenticator.AuthenticateAsync(loginUrl, callbackUrl);

                code = authenticationResult.Properties["code"];
                code = code.Replace("#_=_", "");

            }
            catch (TaskCanceledException ex1)
            {
                Debug.WriteLine("[ex] TaskCanceledException: " + ex1.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] Exception: " + ex.Message);
            }
            Debug.WriteLine($"FetchFitbitCode : {code}");
            return code;
        } 
    }

    
}
