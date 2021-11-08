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
    //This ViewModel sets the data for the list of all wines include in the backend database.
    class WinesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //This method grabs the runtime name of the property chaged and notify the interface
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Bindable List property of questions
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

        public static async Task<WinesViewModel> Init()
        {

            var myClass = new WinesViewModel();

            await myClass.InitializeWinesList();
            return myClass;
        }


        //WinesViewModel constructor
        public WinesViewModel()
        {
            //Task.Run(() => this.InitializeWinesList()).Wait();     
        }

        //Gets the list of wines from the backend
        private async Task InitializeWinesList()
        {
            var Client = new RestClient<Wine>();
            IsLoading = false;
            WinesList = await Client.GetAsync("Wines");                        
        }       
    }
}
