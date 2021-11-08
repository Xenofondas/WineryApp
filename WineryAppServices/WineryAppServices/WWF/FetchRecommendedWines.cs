using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using WineryAppServices.Models;
using System.Threading.Tasks;
using System.Threading;

namespace WineryAppServices.WWF
{
    //This code is responsible for fetcing the recommended wines
    //from the database
    public sealed class FetchRecommendedWines : CodeActivity
    {
        // Define an activity input argument of List of ids
        public InArgument<List<int>> WineIDs { get; set; }

        // Define an activity output argument of List of wines
        public OutArgument<List<Wine>> OutList { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.

        private WineryAppServicesContext db = new WineryAppServicesContext();        
        
        protected override void Execute(CodeActivityContext context)
        {                      

            // Obtain the runtime value of the recList input argument
            var IdsList = context.GetValue(this.WineIDs);

            //The final list of recommended wines
            var WinesList = new List<Wine>();

            foreach (int id in IdsList)
            {
                Task<Wine> findAsyncTask = db.Wines.FindAsync(id);
                Task.Run(() => findAsyncTask).Wait();
                
                WinesList.Add(findAsyncTask.Result);
            }

            context.SetValue(OutList, WinesList);
        }
    }
}
