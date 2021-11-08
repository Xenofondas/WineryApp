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
    /// Answer operations
    /// </summary>
    public class AnswersController : ApiController
    {
        private WineryAppServicesContext db = new WineryAppServicesContext();

        // GET: api/Answers
        //Returns a list of all answers
        /// <summary>
        /// Returns all answers
        /// </summary>
        /// <returns>Gelaw</returns>
        public IQueryable<Answers> GetAnswers()
        {
            return db.Answers;
        }

        // GET: api/Answers/5
        //Get an answer by id
        /// <summary>
        /// Return answer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Answers))]
        public async Task<IHttpActionResult> GetAnswers(int id)
        {
            Answers answers = await db.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }        

        // POST: api/Answers
        //Insert an answer into the database
        /// <summary>
        /// Post answer
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        [ResponseType(typeof(Answers))]
        public async Task<IHttpActionResult> PostAnswers(Answers answers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Answers.Add(answers);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = answers.AnswerId }, answers);
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswersExists(int id)
        {
            return db.Answers.Count(e => e.AnswerId == id) > 0;
        }
    }
}