using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineryApp.Models
{
    //This model class respresent the Wine entity
    class Wine
    {
        public int WineId { get; set; }
        //Wine's title
        public string Title { get; set; }
        //Wine's variety
        public string Variety { get; set; }
        //Wines description
        public string Description { get; set; }
        //The production year of the wine
        public int ProductionYear { get; set; }
        //The wine's price in euros
        public decimal Price { get; set; }
        //Wine image
        public string WineImage { get; set; }
    }
}
