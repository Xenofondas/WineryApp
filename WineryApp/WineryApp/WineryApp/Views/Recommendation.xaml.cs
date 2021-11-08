using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineryApp.ViewModels;
using Xamarin.Forms;

namespace WineryApp.Views
{
    //The backing code for Recommendation View
    public partial class Recommendation : ContentPage
    {        public Recommendation()
        {
            InitializeComponent();            
        }

        //Create binding between the Recommendation View and the RecommendationViewModel
        //using the factory patern
        protected override async void OnAppearing()
        {
            var ViewModel = await RecommendationViewModel.Init();
            this.BindingContext = ViewModel;
        }
    }
}
