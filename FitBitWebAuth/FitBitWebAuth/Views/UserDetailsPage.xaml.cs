using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using FitBitWebAuthenticator.FitBit;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;
using FitBitWebAuthenticator.Models;

namespace FitBitWebAuthenticator.Views
{
    public partial class UserDetailsPage : ContentPage
    {
        //Models.FitBitResponseModel FitBitAccessToken;

        public UserDetailsPage()
        {
            this.InitializeComponent();

            this.Title = "User Details";
            
            //FitBitAccessToken = FitBitConfiguration.TokenResponse;
            Debug.WriteLine("FitBit Access token: " + FitBitConfiguration.TokenResponse);
            TBox1.Text = "FitBit Access token: " + FitBitConfiguration.TokenResponse;

            //FetchUserActivity();
            //FetchUserProfile();
            TBox1.Text = TBox1.Text + " " 
                + $"Welcome! Press the Resume refresh button to refresh all resume.";

        }

        void Logout_Button_Clicked(Object sender, EventArgs e)
        {
            //TODO: Pending to implement this
            // Basically what you will need to revoke tokens
        }

        void FetchUserProfile()
        {
            Models.FitBitResponseModel _tokenResponse = FitBitConfiguration.TokenResponse;
            if (!string.IsNullOrEmpty(_tokenResponse.access_token) 
                && !string.IsNullOrEmpty(_tokenResponse.user_id))
                new FitBitAPIService().FetchUserProfileAsync();
        }

        void FetchUserActivity()
        {
            var _tokenResponse = FitBitConfiguration.TokenResponse;
            var todayDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(_tokenResponse.access_token) 
                && !string.IsNullOrEmpty(_tokenResponse.user_id))
                new FitBitAPIService().FetchUserActivityForDateAsync(
                    _tokenResponse.user_id, todayDate);
        }


        // ResumeUpdateButtonClick
        private async void ResumeUpdateButtonClick(object sender, EventArgs e)
        {
            //var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            // https://api.hh.ru/resumes/mine

            // - A -
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.hh.ru");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer",
                    FitBitConfiguration.TokenResponse.access_token);
                client.DefaultRequestHeaders.UserAgent.ParseAdd
                (
                    $"FitBitExplorer/1 (me@nm.ru)"
                );

                // 1 get resume
                string content = "";

                try
                {
                    content = await client.GetStringAsync($"/resumes/mine");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.Message);
                    TBox1.Text = "Error: bad user access token (app access token used).";
           
                    Debug.WriteLine("Exception: " + ex.Message);
                    return;
                }

                Debug.WriteLine($"Resume content: {content}");
                TBox1.Text = $"Resume content: {content}";

                ResumeList Resumes = JsonConvert.DeserializeObject<ResumeList>(content);

                Debug.WriteLine($"Resumes count: {Resumes.Items.Count}");
                TBox1.Text = TBox1.Text  + " " + $"Resume count: {Resumes.Items.Count}";

                foreach (ResumeItem Resume in Resumes.Items)
                {
                    // Delay 2s
                    //Thread.Sleep(2000);

                    ResumeRefreshAsync(Resume.Id);
                }

            }//using...


        }//ResumeUpdateButtonClick end



        async void ResumeRefreshAsync(string ResumeId)
        {
            // - A -
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.hh.ru");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer",
                    FitBitConfiguration.TokenResponse.access_token);
                client.DefaultRequestHeaders.UserAgent.ParseAdd
                (
                    $"FitBitExplorer/1 (me@nm.ru)"
                );

                // 1 get resume
                string content = "";

                try
                {
                    content = await client.GetStringAsync($"/resumes/{ResumeId}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.Message);
                    TBox1.Text = TBox1.Text + " " 
                        + "Error: bad user access token (app access token used).";
                   
                    Debug.WriteLine("Exception: " + ex.Message);
                    return;
                }

                Debug.WriteLine($"Resume content: {content}");
                TBox1.Text = TBox1.Text + " " + $"Resume content: {content}";

                DateTime nextPublishAt = 
                    JObject.Parse(content)["next_publish_at"].ToObject<DateTime>();

                Debug.WriteLine($"Time to update the resume: {nextPublishAt}");
                TBox1.Text = TBox1.Text + " "
                    + $"Time to update the resume: {nextPublishAt}";


                // 2 Try to update resume

                // Delay 2s
                //Thread.Sleep(2000);

                HttpResponseMessage updateResponse =
                    await client.PostAsync($"/resumes/{ResumeId}/publish", 
                    new StringContent(""));

                string contents = await updateResponse.Content.ReadAsStringAsync();

                Debug.WriteLine($"Update Response: {contents}");
                TBox1.Text = TBox1.Text + " " + $"Update Response: {contents}";

            }
        }

    }
}
