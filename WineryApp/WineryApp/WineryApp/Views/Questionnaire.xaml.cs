using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using WineryApp.Models;
using Plugin.RestClient;
using System.Diagnostics;
using WineryApp.ViewModels;

namespace WineryApp.Views
{
    //The backing code for the questionaire View
    public partial class Questionnaire : ContentPage
    {
        public Questionnaire()
        {
            InitializeComponent();
            //Create binding between the QuestionaireView and the QuestionairViewModel
            BindingContext = new QuestionairViewModel();            
        }

        protected override async void OnAppearing()
        {
            var bb = await QuestionairViewModel.Create();
            this.BindingContext = bb;
        }
    }
}
