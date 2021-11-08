using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineryApp.ViewModels;
using Xamarin.Forms;

namespace WineryApp.Views
{
    //The initial detail page for the app.
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            var bb = await ProfileViewModel.Init();
            this.BindingContext = bb;
        }
    }
}
