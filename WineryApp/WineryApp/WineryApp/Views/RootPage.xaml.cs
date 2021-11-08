using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineryApp.DependencyServices;
using Xamarin.Forms;

namespace WineryApp.Views
{
    //This is the backing code for the Root page of the application
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();
            
            //Check if the user is logged in
            if (!DependencyService.Get<ILoginHelpers>().IsLoggedIn())
            {
                Navigation.PushModalAsync(new LoginButton());
            }
            else {
                Detail = new NavigationPage(new StartPage());
            }
            //Handle button clicked events
            masterPage.QuestionaireButton.Clicked += QuestionaireButton_Clicked;

            masterPage.WinesButton.Clicked += WinesButton_Clicked;

            masterPage.RecomnendationButton.Clicked += RecomnendationButton_Clicked;

            masterPage.ShowProfileButton.Clicked += ShowProfileButton_Clicked;
        }

        private void ShowProfileButton_Clicked(object sender, EventArgs e)
        {
            //Set the detail page to Profile Page
            Detail = new NavigationPage(new ProfilePage());
            //Hide the master page
            IsPresented = false;
        }

        //Event handler for RecomnendationButton
        private void RecomnendationButton_Clicked(object sender, EventArgs e)
        {
            //Set the detail page to Recommendation
            Detail = new NavigationPage(new Recommendation());
            //Hide the master page
            IsPresented = false;
        }

        //Event handler for WinesButton
        private void WinesButton_Clicked(object sender, EventArgs e)
        {
            //Set the detail page to WinesListView
            Detail = new NavigationPage(new WinesListView());
            //Hide the master page
            IsPresented = false;
        }

        //Event handler for QuestionaireButton
        private void QuestionaireButton_Clicked(object sender, EventArgs e)
        {
            //Set the detail page to Questionnaire
            Detail = new NavigationPage(new Questionnaire());
            //Hide the master page        
            IsPresented = false;
        }        
    }
}
