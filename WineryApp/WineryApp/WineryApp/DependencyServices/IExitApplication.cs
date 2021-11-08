using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineryApp.DependencyServices
{
    //Declare an interface for dependency injection
    public interface IExitApplication
    {
        //This method is responsible for force exiting the application
        void ForceCloseApplication();
    }
}
