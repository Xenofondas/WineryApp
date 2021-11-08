namespace WineryAppServices.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WineryAppServices.Models.WineryAppServicesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WineryAppServices.Models.WineryAppServicesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Wines.AddOrUpdate(
            //        w => w.WineId,
            //        new Wine
            //        {
            //            Title = "Aglianico Hadjimichalis",
            //            Variety = "Aglianico",
            //            ProductionYear = 2002,
            //            Description = "An ancient Greek grape variety,popular nowadays in Central and Southern Italy. One of the best wines we’ve ever tasted from the estate Hatzimichalis. Highly dynamic and rebellious in his youth, surprisingly balanced now after several years of aging. Reminiscent excellent examples related wines from the neighboring country, as Antinori-Tormaresca – Bocca di Lupo & Taurasi – Feudi di San Gregorio.",
            //            Price = 17,
            //            WineImage = "http://www.krasiagr.com/wp-content/uploads/2012/11/Aglianico2002.jpg"
            //        },
            //        new Wine
            //        {
            //            Title = "TAOS Parparousis",
            //            Variety = "Mavrodaphne",
            //            ProductionYear = 2005,
            //            Description = "The neglected for long dry version Mavrodaphne-a major player off-axis Nemea Naoussa-occurs with claims through the winery Parparoussis.A very serious winemaker gives us a graceful example of this.Color deep purple, almost impenetrable, with ruby highlights.Nose with complexity, where spring dark forest fruits, alternating with dried fruit,spices and intense suspicion botanical character.Where the suspicion becomes certainty, is the mouth,where the plum and black fruits pass the background almost completely as- desirable - the exquisite botanical character unfolds dominated by sage and eucalyptus close to the wet bay leaves.",
            //            Price = 21,
            //            WineImage = "http://www.krasiagr.com/wp-content/uploads/2012/10/TAOS_2005.jpg"
            //        },
            //        new Wine
            //        {
            //            Title = "Enstikto Grand Collection white Silva",
            //            Variety = "Chardonnay",
            //            ProductionYear = 2010,
            //            Description = "The enstikto white 2010, vinified using the pre-fermentation extraction and aged for 4 months in French oak barrels. Has moderate lemonoprasino color, elegant nose moderately aromatic intensity, pleasant aromas of citrus fruits, dried apricots, nectarines, passage acacia flowers and distinctive barrel, while not go unnoticed hints of dried herbs. In the mouth sealed the excellent cooperation between the two varieties as Chardonnay contributes to structure and depth and Vidiano adds aromatic complexity-which successfully carry out and nose-. Lemony acidity, creamy texture, alcohol 13.5% and elegant aromas of vanilla on the long finish. A wine aristocratic structure, honesty and important sense of balance.",
            //            Price = 10,
            //            WineImage = "http://www.krasiagr.com/wp-content/uploads/2012/10/TAOS_2005.jpg"
            //        }
            //    );
        }
    }
}
