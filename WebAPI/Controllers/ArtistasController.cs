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
    public class ArtistasController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Artistas
        public IQueryable<Artista> GetArtistas()
        {
            return db.Artistas;
        }

        // GET: api/Artistas/5
        [ResponseType(typeof(Artista))]
        public async Task<IHttpActionResult> GetArtista(int id)
        {
            Artista artista = await db.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }

            return Ok(artista);
        }

        // PUT: api/Artistas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArtista(int id, Artista artista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artista.Id)
            {
                return BadRequest();
            }

            db.Entry(artista).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistaExists(id))
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

        // POST: api/Artistas
        [ResponseType(typeof(Artista))]
        public async Task<IHttpActionResult> PostArtista(Artista artista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artistas.Add(artista);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = artista.Id }, artista);
        }

        // DELETE: api/Artistas/5
        [ResponseType(typeof(Artista))]
        public async Task<IHttpActionResult> DeleteArtista(int id)
        {
            Artista artista = await db.Artistas.FindAsync(id);
            if (artista == null)
            {
                return NotFound();
            }

            db.Artistas.Remove(artista);
            await db.SaveChangesAsync();

            return Ok(artista);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtistaExists(int id)
        {
            return db.Artistas.Count(e => e.Id == id) > 0;
        }
    }
}