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
    public class LensDataController : ApiController
    {
        private PassionProjectv2DbContext db = new PassionProjectv2DbContext();

        /// <summary>
        /// This will return a list of lens in the database
        /// </summary>
        /// <returns>
        /// A list of lens including lensID, lens info
        /// </returns>
        /// <example>
        /// GET: api/LensData/GetLenses
        /// </example>
        [ResponseType(typeof(IEnumerable<LensDto>))]
        public IHttpActionResult GetLenses()
        {
            List<Lens> Lenses = db.Lenses.ToList();
            List<LensDto> LensDtos = new List<LensDto> { };

            //Information to be displayed
            foreach (var Lens in Lenses)
            {
                LensDto NewLens = new LensDto
                {
                    LensID = Lens.LensID,
                    BrandName = Lens.BrandName,
                    LensInfo = Lens.LensInfo
                };
                LensDtos.Add(NewLens);
            }
            return Ok(LensDtos);
        }

        /// <summary>
        /// This will return a specific lens information searched by lensID
        /// </summary>
        /// <param name="id">LensID</param>
        /// <returns>
        /// A specific lens information including lensID, lens info
        /// </returns>
        /// <example>
        /// GET: api/LensData/FindLens/5
        /// </example>
        [HttpGet]
        [ResponseType(typeof(LensDto))]
        public IHttpActionResult FindLens(int id)
        {
            Lens Lens = db.Lenses.Find(id);
            if (Lens == null)
            {
                return NotFound();
            }

            //Information to be displayed
            LensDto LensDto = new LensDto
            {
                LensID = Lens.LensID,
                BrandName = Lens.BrandName,
                LensInfo = Lens.LensInfo
            };

            return Ok(LensDto);
        }

        /// <summary>
        /// This will update a specific lens information in the database
        /// </summary>
        /// <param name="id">LensID</param>
        /// <param name="lens">A lens object</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/LensData/UpdateLens/5 
        /// </example>
        /// FORM DATA: Lens JSON Object
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateLens(int id, [FromBody] Lens lens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lens.LensID)
            {
                return BadRequest();
            }

            db.Entry(lens).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LensExists(id))
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
        /// This will add a new lens information to the database
        /// </summary>
        /// <param name="lens">LensID</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/LensData/AddLens 
        /// </example>
        /// FORM DATA: Lens JSON Object
        [HttpPost]
        [ResponseType(typeof(Lens))]
        public IHttpActionResult AddLens([FromBody] Lens lens)
        {
            //Debug.WriteLine(lens);
            //This will validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lenses.Add(lens);
            db.SaveChanges();

            //Debug.WriteLine(lens.LensID);
            return Ok(lens.LensID);
        }

        /// <summary>
        /// This will delete a specific lens data from the database
        /// </summary>
        /// <param name="id">LensID</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/Lens/DeleteLens/5
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Lens))]
        public IHttpActionResult DeleteLens(int id)
        {
            Lens lens = db.Lenses.Find(id);
            if (lens == null)
            {
                return NotFound();
            }

            db.Lenses.Remove(lens);
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
        /// This will check if a specific lens exists in the system. Internal use only
        /// </summary>
        /// <param name="id">LensID</param>
        /// <returns>It will return true if a lens exists. It will return false, if not.</returns>
        private bool LensExists(int id)
        {
            return db.Lenses.Count(e => e.LensID == id) > 0;
        }
    }
}