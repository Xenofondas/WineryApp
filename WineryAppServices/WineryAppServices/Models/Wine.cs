using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WineryAppServices.Models
{
    //This model class respresent the Wine entity
    public class Wine
    {
        //Primary key for Wines table
        [Key]
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