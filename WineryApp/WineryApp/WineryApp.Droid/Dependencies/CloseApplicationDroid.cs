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
using WineryApp.DependencyServices;
using Xamarin.Forms;
using WineryApp.Droid.Dependencies;

[assembly: Dependency(typeof(CloseApplicationDroid))]

namespace WineryApp.Droid.Dependencies
{
    public class CloseApplicationDroid : IExitApplication
    {
        //Close the application
        public void ForceCloseApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }

    }
}