using Android.Net.Sip;
using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WineryApp.DependencyServices;
using WineryApp.Models;
using WineryApp.Views;
using Xamarin.Forms;

namespace WineryApp
{
    public class App : Application
    {
        public App()
        {            
            //Set the main page of the app to be the RootPage
            MainPage = new RootPage();
        } 
             
        //Read the facebook_id from the settings file
        public static async Task<long> GetUserID()
        {
            try
            {
                //Ivoke the dependecy service for reading files in each platform
                var userId = await DependencyService.Get<ILoginHelpers>().GetCurrentUser();
                return Int64.Parse(userId); ;

            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine("[Data File Missing] {0}", e);
                return -1;
            }             
        }
        protected async override void OnStart()
        {
            // Handle when the app starts
           var Status = await CheckServerStatus();

            if (Status==false)
            {
                await App.Current.MainPage.DisplayAlert("Server is down... ", "Try again later", "Exit");
                DependencyService.Get<IExitApplication>().ForceCloseApplication();
            }           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        //Check backend statuss
        private async Task<bool> CheckServerStatus()
        {
            try
            {
                var Client = new RestClient<FacebookUserProfile>();

                var response = await Client.GetAsync("Users");
               
                return true;

            }     
            catch(Exception ex) {
                // do whatever you need to
                Debug.WriteLine("Error:" + ex.Message);
                return true;
            }
        }
    }
}
