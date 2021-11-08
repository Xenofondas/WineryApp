using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WineryAppServices.Models
{
    //This class represent the design of the datadbase and initialize it.
    public class WineryAppServicesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WineryAppServicesContext() : base("name=WineryAppServicesContext")
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Questions> Questions { get; set; }

        public DbSet<Answers> Answers { get; set; }

        public System.Data.Entity.DbSet<WineryAppServices.Models.UserResponses> UserResponses { get; set; }

        public System.Data.Entity.DbSet<WineryAppServices.Models.Wine> Wines { get; set; }

        public System.Data.Entity.DbSet<WineryAppServices.Models.XMLRule> XMLRules { get; set; }
    }
}
