using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _5204_Passion_Project_n01442368_v2.Models;
using System.Diagnostics;

namespace _5204_Passion_Project_n01442368_v2.Controllers
{
    public class FilmDataController : ApiController
    {
        private PassionProjectv2DbContext db = new PassionProjectv2DbContext();

        ///<summary>
        /// This will return a list of film in the database
        ///</summary>
        /// <returns>
        /// A list of film including FilmID, brand name, film series, box speed
        /// </returns>
        /// <example>
        /// GET: api/FilmData/GetFilms 
        /// </example>
        [ResponseType(typeof(IEnumerable<FilmDto>))]
        public IHttpActionResult GetFilms()
        {
            List<Film> Films = db.Films.ToList();
            List<FilmDto> FilmDtos = new List<FilmDto> { };

            //Information to be displayed
            foreach (var Film in Films)
            {
                FilmDto NewFilm = new FilmDto
                {
                    FilmID = Film.FilmID,
                    BrandName = Film.BrandName,
                    FilmSeries = Film.FilmSeries,
                    BoxSpeed = Film.BoxSpeed
                };
                FilmDtos.Add(NewFilm);
            }
            return Ok(FilmDtos);
        }

        /// <summary>
        /// This will return a specific film information searched by filmID
        /// </summary>
        /// <param name="id">FilmID</param>
        /// <returns>
        /// The specific film information including filmID, brand name, film series, box speed
        /// </returns>
        /// <example>
        /// GET: api/FilmData/FindFilm/5 
        /// </example>
        [HttpGet]
        [ResponseType(typeof(FilmDto))]
        public IHttpActionResult FindFilm(int id)
        {
            Film Film = db.Films.Find(id);
            if (Film == null)
            {
                return NotFound();
            }

            //Information to be displayed
            FilmDto FilmDto = new FilmDto
            {
                FilmID = Film.FilmID,
                BrandName = Film.BrandName,
                FilmSeries = Film.FilmSeries,
                BoxSpeed = Film.BoxSpeed
            };

            return Ok(FilmDto);
        }

        /// <summary>
        /// This will update a specific film information in the database
        /// </summary>
        /// <param name="id">FilmID</param>
        /// <param name="film">A film object</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/FilmData/UpdateFilm/5 
        /// </example>
        /// FORM DATA: Film JSON Object
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateFilm(int id, [FromBody] Film film)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != film.FilmID)
            {
                return BadRequest();
            }

            db.Entry(film).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

        /// <summary>
        /// This will add a new film information to the database
        /// </summary>
        /// <param name="film">A film object</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/FilmData/AddFilm
        /// </example>
        /// FORM DATA: Film JSON Object
        [HttpPost]
        [ResponseType(typeof(Film))]
        public IHttpActionResult AddFilm([FromBody] Film film)
        {
            //Debug.WriteLine(film);
            //This will validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Films.Add(film);
            db.SaveChanges();

            //Debug.WriteLine(film.FilmID);
            return Ok(film.FilmID);
        }

        /// <summary>
        /// This will delete a specific film data from the database
        /// </summary>
        /// <param name="id">FilmID</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/Film/DeleteFilm/5
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Film))]
        public IHttpActionResult DeleteFilm(int id)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return NotFound();
            }

            db.Films.Remove(film);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This will check if a specific film exists in the system. Internal use only
        /// </summary>
        /// <param name="id">FilmID</param>
        /// <returns>It will return true if a film exists. It will return false, if not.</returns>
        private bool FilmExists(int id)
        {
            return db.Films.Count(e => e.FilmID == id) > 0;
        }
    }
}