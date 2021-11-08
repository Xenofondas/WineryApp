using Newtonsoft.Json;
using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WineryApp.Models;
using WineryApp.Views;
using Xamarin.Forms;

namespace WineryApp.ViewModels
{
    //This class is responsible for initilizing the survay. It fetches all the data
    //needed for the survay which then will be bound to the interface.
    //*The INotifyPropertyChanged is the interface which implements methods for 
    //handling changes for properties...
    public class QuestionairViewModel : INotifyPropertyChanged
    {
        //Bindible List of questions
        private List<Question> _QuestionsList; 
        public List<Question> QuestionsList
        {
            get { return _QuestionsList; }
            set {
                _QuestionsList = value;
                OnPropertyChanged();
            }

        }
        //Bindible List of Answers
        private List<Answers> _answersList; 
        public List<Answers> answersList
        {
            get { return _answersList; }
            set {
                _answersList = value;
                OnPropertyChanged();
            }
        }
        //Bindible bool Property for the Q&A listview
        private bool _isListVisible;
        public bool IsListVisible
        {
            get
            {
                return _isListVisible;
            }
            set
            {
                _isListVisible = value;
                OnPropertyChanged();
            }
        }
        //Bindable String property for showing survay messages
        private string _SurveyMessage; 
        public string SurveyMessage
        {
            get { return _SurveyMessage; }
            set
            {
                _SurveyMessage = value;
                OnPropertyChanged();
            }

        }
        //property for the question displyed
        public Question CurrentQuestion { get; set; }

        //Bindable string property for The question title 
        private string _QuestionTitle;
        public string QuestionTitle
        {
            get { return _QuestionTitle; }
            set
            {   _QuestionTitle = value;
                 OnPropertyChanged();
            }
        }
        //Bindible property for the button tilte
        private string _ButtonTitle;
        public string ButtonTitle
        {
            get { return _ButtonTitle; }
            set
            {
                _ButtonTitle = value;
                OnPropertyChanged();
            }
        }

        private bool busy = false;
        public bool IsBusy
        {
            get { return busy; }
            set
            {              
                busy = value;
                OnPropertyChanged();
            }
        }
        //Bindable property for hide/show the button
        private bool _ButtonVisibility = true;
        public bool ButtonVisibility
        {
            get { return _ButtonVisibility; }
            set
            {
                _ButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        //Bindable Selected answer property
        private Answers _SelectedAnswer; 
        public Answers SelectedAnswer
        {
            get { return _SelectedAnswer; }
            set
            {
                _SelectedAnswer = value;
                OnPropertyChanged();
            }
        }

        //Variable for iterating through the questions list
        public int ListIndexer; 

        public  int UserId { get; set; }

        public static async Task<QuestionairViewModel> Create()
        {

            var myClass = new QuestionairViewModel();

            await myClass.InitializeUser();

            await myClass.InitializeSurvay();
            return myClass;
        }

        //Constructor of QuestionairViewModel
        public QuestionairViewModel() 
        {

            //Task taskA = Task.Run(() => this.InitializeUser());
            //// Thesn fetch the recommendation.
            //Task continuation = taskA.ContinueWith(a => this.InitializeSurvay());
            IsBusy = true;
            ButtonTitle = "Next question";
            
        }
        
        public string _NullResponse;
        public string NullResponse
        {
            get { return _NullResponse; }
            set
            {
                _NullResponse = value;
                OnPropertyChanged();
            }
        }
        //This method grabs the runtime name of the property chaged and notify the interface
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //The command executed when the button is tapped
        public Command FetchNextQueationCommand
        {
            get {
                return new Command(() =>
                {                    
                    //Check if the user selected an answer
                    if (SelectedAnswer==null)
                    {                                      
                        NullResponse = "You didn't pick a choise!";                                              

                    }
                    else
                    {
                        NullResponse = null;
                        //Create an instance of the response given by the user 
                        var feedback = new UserResponses
                        {
                            User_Id = UserId,
                            Question_Id = SelectedAnswer.QuestionId,
                            Answer_Id = SelectedAnswer.AnswerId
                        };
                        //and post it to the database
                        Task.Run(() => this.SendFeedback(feedback)).Wait();
                        //Empty out selected answer for the next question 
                        SelectedAnswer = null;
                        //Initialize the next question
                        GetNextQuestion();
                    } 
                    
                });
            }
        }
        //Make an Http Post to the winneryapp web services
        private async void SendFeedback(UserResponses feedback)
        {
            var Client = new RestClient<UserResponses>();

            var CurrentUser = await Client.PostAsync(feedback, "UserResponses");
        }
        //This method get the list of questions to be answered.        
        private async Task InitializeSurvay()
        {            
            var Client = new RestClient<Question>();

            QuestionsList = await Client.GetAsync("Questions");

            //Check for previus responses by th current user
            var CurrentResponses = await CheckPreviusResponses(UserId);

            IsListVisible = true;
            //Hide activity indicator
            IsBusy = false;

            if (QuestionsList != null)
            {
                if (QuestionsList.Count > CurrentResponses.Count)// Check if questions are more than responses
                {
                    var bbb = UserId;
                    
                    SurveyMessage = "Please complete this survey in order to let the app learn about you!";

                    ListIndexer = CurrentResponses.Count; //Begin from the new question                    

                    CurrentQuestion = QuestionsList[ListIndexer]; //Initialize first question

                    QuestionTitle = CurrentQuestion.Title;

                    answersList = QuestionsList[ListIndexer].AnswersList;
                }
                else
                {
                    ListIndexer = 0;
                    ButtonVisibility = false;
                    QuestionTitle = "You have already completed the survey. Check our recommendation!! ";
                    IsListVisible = false;
                }
            }
            else
            {
                SurveyMessage = "Something went wrong...";
            } 
                
        }
        //Get the next question from the list of questions fetched before
        private void GetNextQuestion()
        {
            ListIndexer++;

            if (ListIndexer <= QuestionsList.Count() - 1)
            {
                //If is the last question change the title of button
                if (ListIndexer == QuestionsList.Count()) 
                {
                    ButtonTitle = "Finish";
                }
                CurrentQuestion = QuestionsList[ListIndexer];                

                QuestionTitle = CurrentQuestion.Title;

                answersList = QuestionsList[ListIndexer].AnswersList;
            }
            else if (ListIndexer == QuestionsList.Count)
            {
                //Changes the messages when the survay is completed
                SurveyMessage = "Thank you for your answers!";
                QuestionTitle = "Now you can check our wine recommendation in the recommedation tab.";
                ButtonVisibility = false;
                IsListVisible = false;
                

            }

        }
        //This methode call gets the user_id from the users table from the backend
        public async Task InitializeUser()
        {
            
            var fb_id = await App.GetUserID();

            if (fb_id != -1)
            {
                var Client = new RestClient<FacebookUserProfile>();

                var CurrentUser = await Client.GetAsync("Users/", fb_id);

                if(CurrentUser != null)
                {
                    UserId = CurrentUser.Id;
                }                
            }
        }
        //Check the current answers given by the user
        public async Task<List<UserResponses>> CheckPreviusResponses(int userId)//
        {
            var Client = new RestClient<UserResponses>();           

            var currentFeedback = await Client.GetAsync("UserResponses/"+ userId);           

            return currentFeedback;
        }       
    }
}
