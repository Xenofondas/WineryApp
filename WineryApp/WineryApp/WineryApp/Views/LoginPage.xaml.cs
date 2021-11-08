using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WineryApp.Views
{
    //This is an empty content page wich will be custom rendered in each platform separately. 
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //Check what platform is used 
            if (Device.OS == TargetPlatform.Windows)
            {
                var customWebView = new CustomWebViewUWP
                {                    
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };                           

                Content = customWebView; 
            }
        }        
    }
}
