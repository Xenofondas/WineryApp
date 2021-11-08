using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WineryAppServices.Models;

namespace WineryAppServices.Controllers
{
    /// <summary>
    /// UserResponses Operations
    /// </summary>
    public class UserResponsesController : ApiController
    {
        //Initialize an Database instance
        private WineryAppServicesContext db = new WineryAppServicesContext();

        // GET: api/UserResponses
        //Return reponses foe all users including question title and answer text
        /// <summary>
        /// Return all UserResponses
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserResponses> GetUserResponses()
        {
            //For less data query
            db.Configuration.LazyLoadingEnabled = false;//Disable lazy loading
            return db.UserResponses.Include(q => q.Question).Include(a => a.Answer);
        }

        // GET: api/UserResponses/5
        //Return response for a specific user
        /// <summary>
        /// Return UserResponse for specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(UserResponses))]
        public IQueryable<UserResponses> GetUserResponses(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;//Disable lazy loading
            return db.UserResponses.Include(q => q.Question).Include(a => a.Answer).Where(u => u.User_Id == id);
        }              

        // POST: api/UserResponses
        //Insert responses for a user into database
        /// <summary>
        /// Insert UserResponses
        /// </summary>
        /// <param name="userResponses"></param>
        /// <returns></returns>
        [ResponseType(typeof(UserResponses))]
        public async Task<IHttpActionResult> PostUserResponses(UserResponses userResponses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserResponses.Add(userResponses);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userResponses.Id }, userResponses);
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserResponsesExists(int id)
        {
            return db.UserResponses.Count(e => e.Id == id) > 0;
        }
    }
}