using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WineryApp.Views
{
    //The facebook button view
    public partial class LoginButton : ContentPage
    {
        public LoginButton()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            var loginImage = new Image { Aspect = Aspect.AspectFit };

            loginImage.Source = Device.OnPlatform(
            iOS: ImageSource.FromFile("Images/facebook.jpg"),
            Android: ImageSource.FromFile("facebook.jpeg"),
            WinPhone: ImageSource.FromFile("facebook.jpeg"));

            LoginButtonGrid.Children.Add(loginImage,1,1);
        }
        //Event handle triggerd when the user taps the login button(grid)
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Navigate to the login page
            await Navigation.PushModalAsync(new LoginPage());          

        }        
        //Disable back button
        protected override bool OnBackButtonPressed() 
        {
            return true;
        }

        
    }
}
