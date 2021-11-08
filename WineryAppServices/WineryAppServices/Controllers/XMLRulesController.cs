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
    public class XMLRulesController : ApiController
    {
        private WineryAppServicesContext db = new WineryAppServicesContext();

        // GET: api/XMLRules
        public IQueryable<XMLRule> GetXMLRules()
        {
            return db.XMLRules;
        }

        // GET: api/XMLRules/5
        [ResponseType(typeof(XMLRule))]
        public async Task<IHttpActionResult> GetXMLRule(int id)
        {
            XMLRule xMLRule = await db.XMLRules.FindAsync(id);
            if (xMLRule == null)
            {
                return NotFound();
            }

            return Ok(xMLRule);
        }

        // PUT: api/XMLRules/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutXMLRule(int id, XMLRule xMLRule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != xMLRule.Id)
            {
                return BadRequest();
            }

            db.Entry(xMLRule).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!XMLRuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/XMLRules
        [ResponseType(typeof(XMLRule))]
        public async Task<IHttpActionResult> PostXMLRule(XMLRule xMLRule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.XMLRules.Add(xMLRule);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = xMLRule.Id }, xMLRule);
        }

        // DELETE: api/XMLRules/5
        [ResponseType(typeof(XMLRule))]
        public async Task<IHttpActionResult> DeleteXMLRule(int id)
        {
            XMLRule xMLRule = await db.XMLRules.FindAsync(id);
            if (xMLRule == null)
            {
                return NotFound();
            }

            db.XMLRules.Remove(xMLRule);
            await db.SaveChangesAsync();

            return Ok(xMLRule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool XMLRuleExists(int id)
        {
            return db.XMLRules.Count(e => e.Id == id) > 0;
        }
    }
}