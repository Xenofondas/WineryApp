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
using WineryApp.Droid.Dependencies;
using WineryApp.DependencyServices;
using Xamarin.Auth;
using Xamarin.Forms;
using WineryApp.Views;
using System.Threading.Tasks;

[assembly: Dependency(typeof(LoginDependenciesDroid))]

namespace WineryApp.Droid.Dependencies
{
    class LoginDependenciesDroid : ILoginHelpers
    {
        //Check if the account store has a "Facebook" entry
        public bool IsLoggedIn()
        {
            if (AccountStore.Create().FindAccountsForService("Facebook").Count() > 0)
                return true;
            else
                return false;
        }
        //Delete the "Facebook" entry from account store
        public void LogOut()
        {
            var account = AccountStore.Create().FindAccountsForService("Facebook").FirstOrDefault();

            if (account != null)
            {
                AccountStore.Create().Delete(account, "Facebook");
            }                          
        }
        //Get the facebook_id from the settings file
        public async Task<string> GetCurrentUser()//Pending changes
        {
            var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var UserId = prefs.GetString("UserId", null);           
            return UserId;
        }

    }
}