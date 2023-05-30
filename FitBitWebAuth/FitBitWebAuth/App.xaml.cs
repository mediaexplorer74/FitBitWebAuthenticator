using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitBitWebAuthenticator
{
    public partial class App : Application
    {
        public static string AccessToken = "";
        public static bool UserLogined = false;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
