using System;
using System.Threading.Tasks;
using FitBitWebAuthenticator.FitBit;
using System.Diagnostics;
using Xamarin.Essentials;

namespace FitBitWebAuthenticator
{
    public class WebAuthenticatorHandler
    {
        public async Task<string> FetchFitBitCode()
        {
            string code = "";
            try
            {
                Uri callbackUrl = new Uri(FitBitConfiguration.Callback);

                Uri loginUrl = new Uri(FitBitServices.BuildAuthenticationUrl());

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
            Debug.WriteLine($"FetchFitBitCode : {code}");
            return code;
        } 
    }

    
}
