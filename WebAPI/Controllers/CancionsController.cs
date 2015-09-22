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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CancionsController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Cancions
        public IQueryable<Cancion> GetCancions()
        {
            return db.Cancions;
        }

        // GET: api/Cancions/5
        [ResponseType(typeof(Cancion))]
        public async Task<IHttpActionResult> GetCancion(int id)
        {
            Cancion cancion = await db.Cancions.FindAsync(id);
            if (cancion == null)
            {
                return NotFound();
            }

            return Ok(cancion);
        }

        // PUT: api/Cancions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCancion(int id, Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cancion.Id)
            {
                return BadRequest();
            }

            db.Entry(cancion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancionExists(id))
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

        // POST: api/Cancions
        [ResponseType(typeof(Cancion))]
        public async Task<IHttpActionResult> PostCancion(Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cancions.Add(cancion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cancion.Id }, cancion);
        }

        // DELETE: api/Cancions/5
        [ResponseType(typeof(Cancion))]
        public async Task<IHttpActionResult> DeleteCancion(int id)
        {
            Cancion cancion = await db.Cancions.FindAsync(id);
            if (cancion == null)
            {
                return NotFound();
            }

            db.Cancions.Remove(cancion);
            await db.SaveChangesAsync();

            return Ok(cancion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CancionExists(int id)
        {
            return db.Cancions.Count(e => e.Id == id) > 0;
        }
    }
}