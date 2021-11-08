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
    /// Question Operations
    /// </summary>
    public class QuestionsController : ApiController
    {
        //Initialize an Database instance
        private WineryAppServicesContext db = new WineryAppServicesContext();

        // GET: api/Questions
        //Returns an IQueryable of all questions stored in the database
        /// <summary>
        /// Returns all questions
        /// </summary>
        /// <returns></returns>
        public IQueryable<Questions> GetQuestions()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Questions.Include(a=>a.AnswersList);
        }

        // GET: api/Questions/5
        //Returns an question by its id
        /// <summary>
        /// Return answer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Questions))]
        public async Task<IHttpActionResult> GetQuestions(int id)
        {
            Questions questions = await db.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions.AnswersList);
        }

        // POST: api/Questions
        //Insert a question into Database
        /// <summary>
        /// Insert question
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        [ResponseType(typeof(Questions))]
        public async Task<IHttpActionResult> PostQuestions(Questions questions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Questions.Add(questions);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = questions.QuestionID }, questions);
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionsExists(int id)
        {
            return db.Questions.Count(e => e.QuestionID == id) > 0;
        }
    }
}