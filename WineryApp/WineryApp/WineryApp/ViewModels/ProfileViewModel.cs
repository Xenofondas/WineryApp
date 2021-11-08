using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WineryApp.Models;

namespace WineryApp.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {

        public static async Task<ProfileViewModel> Init()
        {
            
            var myClass = new ProfileViewModel();
            
            await myClass.GetUserProfile();
            return myClass;
        }

        private ProfileViewModel()
        {
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //This method grabs the runtime name of the property chaged and notify the interface
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        private FacebookUserProfile _UserProfile;

        public FacebookUserProfile UserProfile
        {
            get { return _UserProfile; }
            set
            {
                _UserProfile = value;

                if (UserProfile == null)
                {
                    IsLoading = true;
                }
                else
                    IsLoading = false; 
                
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        

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


        public async Task GetUserProfile()
        {
            var fb_id = await App.GetUserID();

            if (fb_id != -1) //-1 when fb_id wasnt found on settings file
            {
                var Client = new RestClient<FacebookUserProfile>();
                IsLoading = true;
               
                var CurrentUser = await Client.GetAsync("Users/", fb_id);                
                this.UserProfile = CurrentUser;                
            }
        }
    }
}
