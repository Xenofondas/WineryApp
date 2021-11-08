using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WineryApp.Views;
using WineryApp.Droid.Renderers;
using WineryApp.Models;
using Newtonsoft.Json.Linq;
using Plugin.RestClient;
using System.Threading.Tasks;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace WineryApp.Droid.Renderers
{
    class LoginPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var activity = this.Context as Activity;
            //Initialize the authenticator
            var auth = new OAuth2Authenticator(
                clientId: "1079798992114538", // your OAuth2 client id
                scope: "email", // The scopes for the particular API you're accessing. The format for this will vary by API.
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), // the auth URL for the service
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")); // the redirect URL for the service

            activity.StartActivity(auth.GetUI(activity));
            //Handle when authentication is completes
            auth.Completed += LoginComplete;
        }

        public async void LoginComplete(object sender, AuthenticatorCompletedEventArgs e)
        {            
            if (!e.IsAuthenticated)
            {
                Console.WriteLine("Not Authorised");
                //Pop loginPage out of the modal stack
                await App.Current.MainPage.Navigation.PopModalAsync(); 
                return;
            }
            //Extract authentication data
            var accessToken = e.Account.Properties["access_token"].ToString();
            var expiresIn = Convert.ToDouble(e.Account.Properties["expires_in"]);
            var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

            // Now that we're logged in, make a OAuth2 request to get the user's id,name,etc.
            //Make request to the Facebook Api(Graph) for additional data
            var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=id,first_name,last_name,gender,picture"), null, e.Account);

            await request.GetResponseAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Console.WriteLine("Error: " + t.Exception.InnerException.Message);
                }
                else
                {
                    string json = t.Result.GetResponseText();                    
                    Console.WriteLine(json);
                }
            });

            AccountStore.Create().Save(e.Account, "Facebook");

            //StoreCredentials(e);

            Task.Run(() => this.StoreCredentials(e)).Wait();

            //Pop out LoginPage
            await App.Current.MainPage.Navigation.PopModalAsync();
            //Pop out LoginButtons
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        //Store User credentials to the backend database
        private async void StoreCredentials(AuthenticatorCompletedEventArgs auth)
        {

            var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/v2.7/me/?fields=first_name,last_name,email,gender,id"), null, auth.Account);

            try
            {
                var response = await request.GetResponseAsync();
                var obj = JObject.Parse(response.GetResponseText());

                //Extract data from the json object received by the Graph Api.*email problem at first attemp                

                var FbProfile = new FacebookUserProfile
                {
                    FbId = Convert.ToInt64(obj["id"].ToString()),
                    FirstName = obj["first_name"].ToString(),
                    LastName = obj["last_name"].ToString(),
                    UserEmail = obj["email"].ToString(),
                    Gender = obj["gender"].ToString()
                };

                SaveUserIdToFile(FbProfile.FbId.ToString());
                //Post data to the database
                RestClient<FacebookUserProfile> newUser = new RestClient<FacebookUserProfile>();
                var request1 = newUser.PostAsync(FbProfile, "Users");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error=>"+e.Message);
            }
            
        }
        //store facebook id to file
        protected void SaveUserIdToFile(string id)
        {            
            var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("UserId", id);
            prefEditor.Commit();
        }
    }
}