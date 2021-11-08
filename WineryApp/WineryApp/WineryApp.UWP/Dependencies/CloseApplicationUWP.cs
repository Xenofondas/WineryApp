using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using WineryApp.DependencyServices;
using WineryApp.UWP.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplicationUWP))]

namespace WineryApp.UWP.Dependencies
{
    public class CloseApplicationUWP : IExitApplication
    {
        //Force application to exit
        public void ForceCloseApplication()
        {            
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}
