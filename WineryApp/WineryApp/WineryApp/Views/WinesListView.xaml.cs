using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineryApp.ViewModels;
using Xamarin.Forms;

namespace WineryApp.Views
{
    //The backing code for the Wines View
    public partial class WinesListView : ContentPage
    {
        public WinesListView()
        {
            InitializeComponent();
            //Create binding between the Wines View and the WinesViewModel
            BindingContext = new WinesViewModel();
        }

        protected override async void OnAppearing()
        {
            var ViewModel = await WinesViewModel.Init();
            this.BindingContext = ViewModel;
        }
    }
}
