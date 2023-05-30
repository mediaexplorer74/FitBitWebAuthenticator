using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using FitBitWebAuthenticator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
//using RestSharp;

namespace FitBitWebAuthenticator.FitBit
{
    public class FitBitAPIService
    {
        public async Task<FitBitResponseModel> CallTokenAPIAsync(string code)
        {
            FitBitResponseModel tokenDetails = new FitBitResponseModel();
            try
            {
                RestClient client = new RestClient(FitBitConfiguration.TokenApiUri)
                {
                    //UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36",
                    Timeout = new TimeSpan(0, 0, 0, 0, -1)//-1
            };

                string _authorization = "Basic " + FitBitServices.Base64String();

                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", _authorization);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                request.AddParameter("grant_type", FitBitConfiguration.GrantType);
                request.AddParameter("client_id", FitBitConfiguration.ClientId);
                request.AddParameter("client_secret", FitBitConfiguration.ClientSecret);
                request.AddParameter("redirect_uri", FitBitConfiguration.Callback);
                request.AddParameter("code", code);

                IRestResponse response = await client.Execute(request);//.ExecuteAsync(request);
                Debug.WriteLine($"CallTokenAPI : {response.Content}");

                if (response.IsSuccess)//.IsSuccessful)
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    string _jsonResult = JsonConvert.DeserializeObject(response.Content).ToString();

                    tokenDetails = JsonConvert.DeserializeObject<FitBitResponseModel>(_jsonResult,
                        settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] RestClient Exception: " + ex.Message);
            }

            return tokenDetails;
        }

        private async Task APIRequestAsync(string requestUri)
        {

            RestClient client = new RestClient(requestUri)
            {
                //UserAgent = "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36"//,
                Timeout = new TimeSpan(0, 0, 0, 0, -1)//-1
            };

            var _accessToken = FitBitConfiguration.TokenResponse.access_token;

            var request = new RestRequest(Method.GET);

            var bearerToken = "Bearer " + _accessToken;
            request.AddHeader("Authorization", bearerToken);
            //request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36"); //RnD

            IRestResponse response = await client.Execute(request);//.ExecuteAsync
            Debug.WriteLine(response.Content);
        }

        public async Task FetchUserProfileAsync()
        {
            var requestUri = "https://api.fitbit.com/1/user/-/profile.json";

            APIRequestAsync(requestUri);

        }

        public async Task FetchUserActivityForDateAsync(string userId, string date)
        {
            var requestUri = "https://api.fitbit.com/1/user/" + userId + "/activities/date/"
                + date + ".json";

            APIRequestAsync(requestUri);
        }
    }
}
