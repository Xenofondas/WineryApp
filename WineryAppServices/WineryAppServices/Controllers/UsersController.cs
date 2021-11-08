using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WineryAppServices.Models;
using WineryAppServices.WWF;

namespace WineryAppServices.Controllers
{
    /// <summary>
    /// User operations
    /// </summary>
    public class UsersController : ApiController
    {
        //Initialize an Database instance
        private WineryAppServicesContext db = new WineryAppServicesContext();

        // GET: api/Users
        //Return all users stored in the database
        /// <summary>
        /// Return all Users
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        //Return a user by his id
        /// <summary>
        /// Return specific User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(long id)
        {
            //User user = await db.Users.FindAsync(id);
            User myUser = db.Users.SingleOrDefault(u => u.FbId == id);
            if (myUser == null)
            {
                return NotFound();
            }            

            return Ok(myUser);
        }       

        // POST: api/Users
        //Insert a user into the database
        /// <summary>
        /// Insert user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            //Check if data give correspond to the User model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = db.Users.Count(a => a.FbId == user.FbId);

            if (existingUser == 0)
            {
                db.Users.Add(user);
            }            
            
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);            
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}