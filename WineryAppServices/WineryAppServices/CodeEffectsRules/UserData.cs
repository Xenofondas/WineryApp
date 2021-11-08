using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeEffects.Rule.Attributes;

namespace WineryAppServices.CodeEffectsRules
{
    //This class defines the source objects used for the rule engine
    public class UserData
    {
        public string UserAgeRank { get; set; }

        public string EducationLevel { get; set; }

        public string WineRelation { get; set; }

        public string Ethnicbackground { get; set; }

        public string ConsumptionFrequency { get; set; }

        public string PriceRange { get; set; }

        public List<int> RecommendedWinesList { get; set; }


        [Action(Description = "Create first recommended list for fisrt rule")]
        public void CreateFistRecommedation()
        {
            //Creating the list<ids> of recommended wines 
            this.RecommendedWinesList =new List<int>(){1,2,4,7 };
        }

        [Action(Description = "Create first recommended list for second rule")]
        public void CreateSecondRecommedation()
        {
            //Creating the list<ids> of recommended wines 
            this.RecommendedWinesList = new List<int>() { 3, 5, 6 };
        }

        [Action(Description = "Create first recommended list for third rule")]
        public void CreateThirdRecommedation()
        {
            //Creating the list<ids> of recommended wines 
            this.RecommendedWinesList = new List<int>() { 13, 15 };
        }

        [Action(Description = "Create first recommended list for fourth rule")]
        public void CreateFourthRecommedation()
        {
            //Creating the list<ids> of recommended wines 
            this.RecommendedWinesList = new List<int>() { 8, 9 };
        }
    }
}