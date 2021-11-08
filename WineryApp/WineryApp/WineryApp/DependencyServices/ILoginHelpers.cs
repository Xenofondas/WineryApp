using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineryApp.DependencyServices
{
    //This interface is used for dependency injection an declare the above methode 
    //for helping the login proccess
    public interface ILoginHelpers
    {
        //Check if a users is already logged in
        bool IsLoggedIn();
        //Log out the user
        void LogOut();
        //Read the settings for the user
        Task<string> GetCurrentUser();
    }
}
