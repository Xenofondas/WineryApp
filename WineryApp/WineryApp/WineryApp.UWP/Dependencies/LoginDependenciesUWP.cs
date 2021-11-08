using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using WineryApp.DependencyServices;
using WineryApp.UWP.Dependencies;
using WineryApp.Views;
using Xamarin.Forms;

[assembly:Dependency(typeof(LoginDependenciesUWP))]

namespace WineryApp.UWP.Dependencies
{
    public class LoginDependenciesUWP : ILoginHelpers
    {
        //Check if the user is already logged in
        public bool  IsLoggedIn()
        {
            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id=1079798992114538"
                + "&scope=email,public_profile&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

            var Filter = new HttpBaseProtocolFilter();
            var cookieManager = Filter.CookieManager;
            //Check if there is cookies stored before
            HttpCookieCollection Cookies = cookieManager.GetCookies(new Uri(apiRequest));
            HttpCookieCollection Cookies1 = cookieManager.GetCookies(new Uri("http://www.facebook.com"));

            var count = 0;

            foreach (HttpCookie cookie in Cookies)
            {
                count++;
            }

            if (count > 0)
            {
                
                return true;
            }
            else
            {
                return false;
            }            
            
        }
        //Log the user out 
        public void LogOut()
        {
            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id=1079798992114538"
                + "&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";            

            var Filter = new HttpBaseProtocolFilter();
            var cookieManager = Filter.CookieManager;
            HttpCookieCollection Cookies = cookieManager.GetCookies(new Uri(apiRequest));
            //Delete all cookies related to the users login
            foreach (HttpCookie cookie in Cookies)
            {
                cookieManager.DeleteCookie(cookie);
            }            
        }
        //Return the UserId
        public async Task<string> GetCurrentUser()
        {
            var UserId = await LoadTextAsync("CurrentUser.txt");

            return UserId;
        }
        //Read from file
        public async Task<string> LoadTextAsync(string filename)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(filename);
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            Debug.WriteLine(text);
            return text;
        }
    }
}
