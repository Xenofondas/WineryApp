using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WineryApp.UWP.Renderers;
using WineryApp.Views;
using Xamarin.Forms.Platform.UWP;
using Windows.Web.Http.Filters;
using Windows.Web.Http;
using Newtonsoft.Json;
using WineryApp.Models;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
using Plugin.RestClient;
using Windows.Storage;

[assembly: ExportRenderer(typeof(CustomWebViewUWP), typeof(LoginPageRenderer))]

namespace WineryApp.UWP.Renderers
{
    //Custom renederer for the login web view
    class LoginPageRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {

                var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id=1079798992114538"
                + "&scope=email,public_profile&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

                var customWebView = Element as CustomWebViewUWP;

                Control.Source = new Uri(apiRequest);
                //Handle when user typed his creds
                customWebView.Navigated += IsNavigated;
            }
        }
        
        private async void IsNavigated(object sender, WebNavigatedEventArgs e)
        {
            //**Check previus cookies**
            if (e.Url.Contains("access_denied") || !e.Url.Contains("access_token"))
            {
                return;
            }
            //String variable for facebook access token
            var accessToken = ExtractAccessTokenFromUrl(e.Url);
            if (accessToken != null)
            {
                StoreCredentials(accessToken);
            }
            //Pop out LoginPage    
            await WineryApp.App.Current.MainPage.Navigation.PopModalAsync();
            //Pop out LoginButton     
            await WineryApp.App.Current.MainPage.Navigation.PopModalAsync();
        }
        //Pasre url to extract access_token
        private string ExtractAccessTokenFromUrl(string url) 
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;

        }

        //Call the graph api for user data
        private async void StoreCredentials(string accessToken)
        {
            var requestUrl = new Uri(
                "https://graph.facebook.com/v2.7/me/?fields=first_name,last_name,email,gender,id&access_token="
                + accessToken);

            var httpClient = new System.Net.Http.HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);
            
            var obj = JObject.Parse(userJson);

            //Extract data from the json object received by the Graph Api.
            var FbProfile = new FacebookUserProfile
            {
                FbId = Convert.ToInt64(obj["id"].ToString()),
                FirstName = obj["first_name"].ToString(),
                LastName = obj["last_name"].ToString(),
                UserEmail = obj["email"].ToString(),
                Gender = obj["gender"].ToString()
            };

            //Store user in the backend database            
            RestClient<FacebookUserProfile> newUser = new RestClient<FacebookUserProfile>();

            var request = newUser.PostAsync(FbProfile,"Users");            

            await SaveTextAsync("CurrentUser.txt",FbProfile.FbId.ToString());//Save currentUser's id to local file 
            
        }
        //Save user Facebook id to local settings file
        public async Task SaveTextAsync(string filename, string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, text);            
        }
    }
}
