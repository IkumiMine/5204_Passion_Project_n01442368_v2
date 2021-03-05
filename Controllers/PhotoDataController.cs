using System;
using System.IO;
using System.Web;
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
using _5204_Passion_Project_n01442368_v2.Models.ViewModels;
using System.Diagnostics;


namespace _5204_Passion_Project_n01442368_v2.Controllers
{
    public class PhotoDataController : ApiController
    {
        private PassionProjectv2DbContext db = new PassionProjectv2DbContext();

        /// <summary>
        /// This will return a list of photos in the database. If the filmID is selected, it will return a list of photos associated with the filmID.
        /// </summary>
        /// <param name="filmid">FilmID</param>
        /// <returns>
        /// A list of photos with images.
        /// </returns>
        /// <example>
        /// GET: api/PhotoData/GetPhotos/{filmid?}
        /// </example>
        [HttpGet]
        [Route("api/PhotoData/GetPhotos/{filmid?}")]
        [ResponseType(typeof(IEnumerable<PhotoDto>))]
        public IHttpActionResult GetPhotos(int filmid = 0)
        {
            List<Photo> Photos = db.Photos.ToList();

            //Filter result if filmID is set
            if (filmid != 0)
            {
                Photos = Photos.Where(p => p.FilmID == Convert.ToInt32(filmid)).ToList();

            }

            List<PhotoDto> PhotoDtos = new List<PhotoDto> { };

            if (Photos == null)
            {
                return NotFound();
            }

            //Information which I would like to retrieve to the API
            foreach (var Photo in Photos)
            {
            PhotoDto NewPhoto = new PhotoDto
                {
                    PhotoID = Photo.PhotoID,
                    ISO = Photo.ISO,
                    Aperture = Photo.Aperture,
                    ShutterSpeed = Photo.ShutterSpeed,
                    FocalLength_mm = Photo.FocalLength_mm,
                    Title = Photo.Title,
                    Description = Photo.Description,
                    DateTaken = Photo.DateTaken,
                    IsPhoto = Photo.IsPhoto,
                    PhotoExtension = Photo.PhotoExtension,
                    FilmID = Photo.FilmID,
                    LensID = Photo.LensID
                };
                PhotoDtos.Add(NewPhoto);
            }
            return Ok(PhotoDtos);
        }

        /// <summary>
        /// This will find a film informaion that is included in a specific photo infomation searched by photoID
        /// </summary>
        /// <param name="id">PhotoID</param>
        /// <returns>
        /// A film information including filmID, brand name, film series, box speed
        /// </returns>
        /// <example>
        /// GET: api/PhotoData/GetFilmForPhoto/5 
        /// </example>
        [HttpGet]
        [ResponseType(typeof(FilmDto))]
        public IHttpActionResult GetFilmForPhoto(int id)
        {
            //SELECET * FROM Films JOIN Photos ON F.FILMID = P.FILMID
            Film Film = db.Films.Where(f => f.Photos.Any(p => p.PhotoID == id)).FirstOrDefault();
            if (Film == null)
            {
                return NotFound();
            }

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
        /// This will find a lens informaion that is included in a specific photo infomation searched by photoID
        /// </summary>
        /// <param name="id">PhotoID</param>
        /// <returns>
        /// A lens information including lensID, lens info
        /// </returns>
        /// <example>
        /// GET: api/PhotoData/GetLensForPhoto/5 
        /// </example>
        [HttpGet]
        [ResponseType(typeof(LensDto))]
        public IHttpActionResult GetLensForPhoto(int id)
        {
            Lens Lens = db.Lenses.Where(l => l.Photos.Any(p => p.PhotoID == id)).FirstOrDefault();

            if (Lens == null)
            {
                return NotFound();
            }

            LensDto LensDto = new LensDto
            {
                LensID = Lens.LensID,
                BrandName = Lens.BrandName,
                LensInfo = Lens.LensInfo
            };
            return Ok(LensDto);
        }

        /// <summary>
        /// This will return a specific photo information searched by photoID
        /// </summary>
        /// <param name="id">PhotoID</param>
        /// <returns>
        /// A specific photo information including photoID, ISO, aperture, shutter speed, focal length(mm), title, description, date taken, if a photo file is uploaded(boolean), a photo file extension if it exists, filmID, lensID
        /// </returns>
        /// <example>
        /// GET: api/PhotoData/FindPhoto/5 
        /// </example>
        [HttpGet]
        [ResponseType(typeof(PhotoDto))]
        public IHttpActionResult FindPhoto(int id)
        {
            Photo Photo = db.Photos.Find(id);
            if (Photo == null)
            {
                return NotFound();
            }

            //A Friendly object format
            //Information I would like to display
            PhotoDto PhotoDto = new PhotoDto
            {
                PhotoID = Photo.PhotoID,
                ISO = Photo.ISO,
                Aperture = Photo.Aperture,
                ShutterSpeed = Photo.ShutterSpeed,
                FocalLength_mm = Photo.FocalLength_mm,
                Title = Photo.Title,
                Description = Photo.Description,
                DateTaken = Photo.DateTaken,
                IsPhoto = Photo.IsPhoto,
                PhotoExtension = Photo.PhotoExtension,
                FilmID = Photo.FilmID,
                LensID = Photo.LensID
            };

            return Ok(PhotoDto);
        }

        /// <summary>
        /// This will update a specific photo information in the database
        /// </summary>
        /// <param name="id">PhotoID</param>
        /// <param name="photo">A photo object</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/PhotoData/UpdatePhoto/5
        /// </example>
        /// FORM DATA: Photo JSON Object
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdatePhoto(int id, [FromBody] Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photo.PhotoID)
            {
                return BadRequest();
            }

            db.Entry(photo).State = EntityState.Modified;
            //Photo updata is handled by another method
            db.Entry(photo).Property(p => p.IsPhoto).IsModified = false;
            db.Entry(photo).Property(p => p.PhotoExtension).IsModified = false;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
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
        /// This will update a specific photo information in the database
        /// </summary>
        /// <param name="id">PhotoID</param>
        /// <param name="lens">A photo object</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/PhotoData/UpdatePhoto/5 
        /// </example>
        /// FORM DATA: Photo JSON Object
        [HttpPost]
        public IHttpActionResult UpdatePhotoFile(int id)
        {
            bool isphoto = false;
            string photoextension;
            if (Request.Content.IsMimeMultipartContent())
            {
                Debug.WriteLine("Received multipart form data");

                int numfiles = HttpContext.Current.Request.Files.Count;
                Debug.WriteLine("Files Received: " + numfiles);

                //Check if a file is posted
                if(numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
                {
                    var PhotoFile = HttpContext.Current.Request.Files[0];
                    //Check if the file is empty
                    if(PhotoFile.ContentLength > 0)
                    {
                        var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                        var extension = Path.GetExtension(PhotoFile.FileName).Substring(1);
                        //Check the extension of the file
                        if (valtypes.Contains(extension))
                        {
                            try
                            {
                                //File name is the id of the photo file
                                string filename = id + "." + extension;

                                //Get a direct file path to ~/Content/Photos/{id}.{extension}
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Photos/"), filename);

                                //Save the file
                                PhotoFile.SaveAs(path);

                                //If these are all successful then we can set these fields
                                isphoto = true;
                                photoextension = extension;

                                //Update the photo isfile and photoextension fields in the database
                                Photo SelectedPhoto = db.Photos.Find(id);
                                SelectedPhoto.IsPhoto = isphoto;
                                SelectedPhoto.PhotoExtension = photoextension;
                                db.Entry(SelectedPhoto).State = EntityState.Modified;

                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("A photo file was nor saved successfully.");
                                Debug.WriteLine("Exception:" + ex);
                            }
                        }

                    }
                    
                }
            }

            return Ok();
        }

        /// <summary>
        /// This will add a new photo information to the database
        /// </summary>
        /// <param name="photo">PhotoID</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/PhotoData/AddPhoto 
        /// </example>
        /// FORM DATA: Photo JSON Object
        [HttpPost]
        [ResponseType(typeof(Photo))]
        public IHttpActionResult AddPhoto([FromBody] Photo photo)
        {
            //This will validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                //Debug.WriteLine(photo);
                return BadRequest(ModelState);
            }

            db.Photos.Add(photo);
            db.SaveChanges();

            //Debug.WriteLine(photo.PhotoID);
            return Ok(photo.PhotoID);
        }

        /// <summary>
        /// This will delete a specific photo information from the database
        /// </summary>
        /// <param name="id">PhotoID</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/Photo/DeletePhoto/5
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Photo))]
        public IHttpActionResult DeletePhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return NotFound();
            }

            db.Photos.Remove(photo);
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
        /// This will check if a specific photo exists in the system. Internal use only
        /// </summary>
        /// <param name="id">PhotoID</param>
        /// <returns>It will return true if a photo exists. It will return false, if not.</returns>
        private bool PhotoExists(int id)
        {
            return db.Photos.Count(e => e.PhotoID == id) > 0;
        }
    }
}