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
    //This class descibes the content of the master page
    public partial class MasterPage : ContentPage
    {
        public StackLayout mainLayout { get { return masterLayout; } }
        //The Questionaire button of the master page
        public Button QuestionaireButton { get { return Pbutton; } }
        //The wine button of the master page
        public Button WinesButton { get { return WinesListButton; } }
        //The Recommendation button of the master page
        public Button RecomnendationButton {get { return RecButton; } }
        //The buuton exposes profile
        public Button ShowProfileButton { get { return ProfileButton; } }
        public MasterPage()
        {
            InitializeComponent();                    
        }
       
        //Event handler for the Logout button
        private void LogoutButton(object sender, EventArgs e)
        {
            var NavStack = App.Current.MainPage.Navigation.NavigationStack.Count;
            //Invoke the dependecy service for loging out the user
            DependencyService.Get<ILoginHelpers>().LogOut();
            Navigation.PushModalAsync(new LoginButton());
        }        
    }
}
