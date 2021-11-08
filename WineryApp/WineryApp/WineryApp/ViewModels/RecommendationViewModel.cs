using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WineryApp.Models;
using Xamarin.Forms;

namespace WineryApp.ViewModels
{
    //This viewmodel is responsible initializing the data for the Wines view.
    class RecommendationViewModel : INotifyPropertyChanged
    {
        //This static method creates asyncronoysly an instance of RecommendationViewModel
        public static async Task<RecommendationViewModel> Init()
        {
            var myClass = new RecommendationViewModel();

            await myClass.InitializeUser();

            await myClass.GetRecommendedWines("Wines/recommendation/");

            return myClass;
        }

        //Constructor
        public RecommendationViewModel()
        {
            SurveyMessage = false;
        }
        //The user id from the backed table Users
        public int UserId { get; set; }

        //Boolean variable to indicate whether the Survay is completed or not.
        public bool IsSurveyCompleted { get; set; }

        //Bindable List of questions
        private List<Wine> _WinesList; 
        public List<Wine> WinesList
        {
            get { return _WinesList; }
            set
            {
                _WinesList = value;
                OnPropertyChanged();
            }
        }

        //Used for activity indicator 
        private bool _IsLoading = true;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                OnPropertyChanged();
            }
        }


        //Used for Switch control
        private bool _isOn = false;
        public bool isOn
        {
            get { return _isOn; }
            set
            {
                _isOn = value;
                if (isOn == true)
                {
                    IsLoading = true;
                    Task.Run(() => this.GetRecommendedWines("Wines/recommendationByRules/"));
                }
                else
                {
                    IsLoading = true;
                    Task.Run(() => this.GetRecommendedWines("Wines/recommendation/"));
                }       
                OnPropertyChanged();
            }
        }

        //Bindable boolean property for hide/show the Survey message
        private bool _SurveyMessage = false;
        public bool SurveyMessage
        {
            get { return _SurveyMessage; }
            set
            {
                _SurveyMessage = value;
                OnPropertyChanged();
            }
        }
        //Then event returned when a property is changed
        public event PropertyChangedEventHandler PropertyChanged;
        //This method grabs the runtime name of the property chaged and notify the interface
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //An asyncronous task for geting the users id
        public async Task InitializeUser()
        {
            SurveyMessage = false;

            var fb_id = await App.GetUserID();

            if (fb_id != -1) //-1 when fb_id wasnt found on settings file
            {
                var Client = new RestClient<FacebookUserProfile>();

                var CurrentUser = await Client.GetAsync("Users/", fb_id);

                if (CurrentUser != null)
                {
                    UserId = CurrentUser.Id;
                }
                //Check if user has completed the survay
                IsSurveyCompleted = await CheckUserFeedback(UserId); 
            }
        }

        //This method gets the recomended wines from the backend 
        private async Task GetRecommendedWines(string Route)
        {
            if (IsSurveyCompleted == true)
            {
                var Client = new RestClient<Wine>();

                var List = await Client.GetAsync(Route + UserId);

                //Set the bindable property WinesList to the list came from server
                WinesList = List;
            }
            else {
                SurveyMessage = true;
            }

            IsLoading = false;
        }
        //Before getting the recomendation check if the user has completed the survay 
        public async Task<bool> CheckUserFeedback(int userId)
        {
            var Client = new RestClient<UserResponses>();

            var currentFeedback = await Client.GetAsync("UserResponses/" + userId);

            if (currentFeedback.Count == 0)
            {
                return false;
            }
            else {
                return true;
            }            
        }
    }
}
