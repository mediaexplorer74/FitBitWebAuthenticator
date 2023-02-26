using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FitBitWebAuthenticator.FirBit;
using FitBitWebAuthenticator.Views;
using Xamarin.Forms;

namespace FitBitWebAuthenticator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Title = "Login";
        }

        void Login_Button_Clicked(Object sender, EventArgs e)
        {
            StartFitbitAthenticationAsync();
        }

        async Task StartFitbitAthenticationAsync()
        {
            WebAuthenticatorHandler authenticator = new WebAuthenticatorHandler();

            string _fitbitCode = await authenticator.FetchFitbitCode();

            if (!string.IsNullOrEmpty(_fitbitCode))
            {
                Models.FitBitResponseModel _tokenResponse = 
                    await new FitbitAPIService().CallTokenAPIAsync(_fitbitCode);

                if (!string.IsNullOrEmpty(_tokenResponse.access_token))
                {
                    Console.WriteLine($"Fitbit access token : {_tokenResponse.access_token}");

                    FitbitConfiguration.TokenResponse = _tokenResponse;

                    this.Navigation.PushAsync(new UserDetailsPage());
                }
                else
                    Debug.WriteLine("Error fetching token!!");
            }
            else
                Debug.WriteLine("Login failed !!");

        }

    }
}
