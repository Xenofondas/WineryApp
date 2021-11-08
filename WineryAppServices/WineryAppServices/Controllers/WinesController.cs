using CodeEffects.Rule.Core;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml;
using WineryAppServices.CodeEffectsRules;
using WineryAppServices.Models;
using WineryAppServices.WWF;

namespace WineryAppServices.Controllers
{    
    /// <summary>
    /// Wine Operations
    /// </summary>
    public class WinesController : ApiController
    {
        //Initialize an Database instance
        private WineryAppServicesContext db = new WineryAppServicesContext();

        // GET: api/Wines
        //Return a collection of all wines stored in the db
        /// <summary>
        /// Return all wines
        /// </summary>
        /// <returns></returns>
        public IQueryable<Wine> GetWines()
        {
            return db.Wines;
        }

        // GET: api/Wines/5
        //Return a specific wine
        /// <summary>
        /// Return specific wine
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Wine))]
        public async Task<IHttpActionResult> GetWine(int id)
        {
            Wine wine = await db.Wines.FindAsync(id);
            if (wine == null)
            {
                return NotFound();
            }
            return Ok(wine);
        }

        //Custom route prefix for calling this method
        /// <summary>
        /// Return Recommenden Wines for User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("api/Wines/recommendation/{UserId:int}")]
        [ActionName("Get")]
        //This method is responsible for creating the recomended list of wines
        public async Task<IHttpActionResult> GetRecommendedWines(int UserId)
        {
            //Create the workflow input
            var InputArguments = FetchUserResponses(UserId);    
            
            //Invoke the workflow
            var workflow = new WorkflowInvoker(new MainActivity2());
            var resultDictionary = workflow.Invoke(InputArguments);
            //Respresent a list of (recommended) wines
            var recommedation = resultDictionary["FinalWineList"];

            return Ok(recommedation);
        }        

        // POST: api/Wines
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="wine"></param>
        /// <returns></returns>
        [ResponseType(typeof(Wine))]
        public async Task<IHttpActionResult> PostWine(Wine wine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Wines.Add(wine);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = wine.WineId }, wine);
        }

        [Route("api/Wines/recommendationByRules/{UserId:int}")]
        [ActionName("Get")]
        //This method is responsible for creating the recomended list of wines
        public async Task<IHttpActionResult> GetRecommendedWinesByRules(int UserId)
        {
            List<Wine> FinalWinesList = new List<Wine>();

            int RuleTrggered = 0;

            UserData userFeedback = InitializeUserData(UserId);

            if (userFeedback == null)
            {
                return Ok("Something went wrong..");
            }
            //Load rules from db
            var RuleSet = db.XMLRules;            

            //Evaluate rules
            foreach (XMLRule rule in RuleSet)
            {
                Evaluator evaluator = new Evaluator(typeof(UserData), rule.xmlRule);

                bool success = evaluator.Evaluate(userFeedback); // Returns False
                if (success)
                {
                    RuleTrggered = rule.Id;
                    break;
                }

            }

            var WineIds  = userFeedback.RecommendedWinesList;

            foreach (int id in WineIds)
            {
                Wine wine = await db.Wines.FindAsync(id);
                FinalWinesList.Add(wine);
            }
            return Ok(FinalWinesList);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WineExists(int id)
        {
            return db.Wines.Count(e => e.WineId == id) > 0;
        }
        //This method creates the Iput dictionary for the workflow and return it.
        public Dictionary<string,Object> FetchUserResponses(int id)//Fetch the user feedback
        {
            //Return the collection of User Responses including question title and Answer text
            db.Configuration.LazyLoadingEnabled = false;//Disable lazy loading            
            var UserFeedback = db.UserResponses.Include(q => q.Question).Include(a => a.Answer).Where(u => u.User_Id == id);
            //Create a temporary list of 3 this(user_id, QuestionText , Answer_Text)
            var list = UserFeedback.Select(s => new {
                User_id = s.User_Id,
                QuestionText = s.Question.Title, // "Cast" IQueryeble to list
                AnswerText = s.Answer.AnswerText
            }).ToList();

            //Initialize the dictionary with its keys
            var InputArguments = new Dictionary<string, object>();                      
            var DictioneryKeys = new List<string>
            {
                "UserAgeRank",
                "EducationLevel",
                "HouseholdIncome",
                "WineRelation",
                "Ethnicbackground",
                "ConsumptionFrequency",
                "PriceRange"
            };
           
            //Iterate over List of Users Responses and feed the dictionary
            for ( int i = 0; i < list.Count; i++ ) //Review later
            {
                InputArguments.Add(DictioneryKeys[i], list[i].AnswerText);
            }
            return InputArguments;
        }

        public UserData InitializeUserData(int id)
        {
            UserData userData = new UserData();

            //Return the collection of User Responses including question title and Answer text
            db.Configuration.LazyLoadingEnabled = false;//Disable lazy loading            
            var UserFeedback = db.UserResponses.Include(q => q.Question).Include(a => a.Answer).Where(u => u.User_Id == id);
            //Create a temporary list of 3 this(user_id, QuestionText , Answer_Text)
            var list = UserFeedback.Select(s => new {
                User_id = s.User_Id,
                QuestionText = s.Question.Title,
                AnswerText = s.Answer.AnswerText
            }).ToList();

            if (list == null)
            {
                return null;
            }

            userData.UserAgeRank = list[0].AnswerText;
            userData.EducationLevel = list[1].AnswerText;
            userData.WineRelation = list[3].AnswerText;
            userData.Ethnicbackground = list[4].AnswerText;
            userData.ConsumptionFrequency = list[5].AnswerText;
            userData.PriceRange = list[6].AnswerText;

            return userData;            
        }

    }


}