using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5204_Passion_Project_n01442368_v2.Models;
using _5204_Passion_Project_n01442368_v2.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace _5204_Passion_Project_n01442368_v2.Controllers
{
    public class PhotoController : Controller
    {
        //Http Client is the proper way to connect to a webAPI
        private readonly JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static PhotoController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44382/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
        }

        // GET: Photo/List
        public ActionResult List()
        {
            string url = "photodata/getphotos";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<PhotoDto> SelectedPhotos = response.Content.ReadAsAsync<IEnumerable<PhotoDto>>().Result;
                return View(SelectedPhotos);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Photo/Details/5
        public ActionResult Details(int id)
        {
            DetailPhoto ViewModel = new DetailPhoto();
            string url = "photodata/findphoto/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.?
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into photo data transfer object
                PhotoDto SelectedPhoto = response.Content.ReadAsAsync<PhotoDto>().Result;
                ViewModel.Photo = SelectedPhoto;

                //Get film data
                url = "photodata/getfilmforphoto/" + id;
                response = client.GetAsync(url).Result;
                FilmDto selectedFilm = response.Content.ReadAsAsync<FilmDto>().Result;
                ViewModel.Film = selectedFilm;

                //Get Lens data
                url = "photodata/getlensforphoto/" + id;
                response = client.GetAsync(url).Result;
                LensDto selectedLens = response.Content.ReadAsAsync<LensDto>().Result;
                ViewModel.Lens = selectedLens;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Photo/Create
        public ActionResult Create()
        {
            UpdatePhoto ViewModel = new UpdatePhoto();

            //Get film data and assign ViewModel.AllFilms
            string urlFilm = "filmdata/getfilms";
            HttpResponseMessage responseFilm = client.GetAsync(urlFilm).Result;
            IEnumerable<FilmDto> PotentialFilms = responseFilm.Content.ReadAsAsync<IEnumerable<FilmDto>>().Result;
            ViewModel.AllFilms = PotentialFilms;

            //Get lens data assign ViewModel.AllLenses
            string urlLens = "lensdata/getlenses";
            HttpResponseMessage responseLens = client.GetAsync(urlLens).Result;
            IEnumerable<LensDto> PotentialLenses = responseLens.Content.ReadAsAsync<IEnumerable<LensDto>>().Result;
            ViewModel.AllLenses = PotentialLenses;

            return View(ViewModel);
        }

        // POST: Photo/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Photo PhotoInfo)
        {
            //Debug.WriteLine(PhotoInfo);
            string url = "photodata/addphoto";
            //Debug.WriteLine(jss.Serialize(PhotoInfo));
            HttpContent content = new StringContent(jss.Serialize(PhotoInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                int photoid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = photoid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            UpdatePhoto ViewModel = new UpdatePhoto();
            string url = "photodata/findphoto/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            if (response.IsSuccessStatusCode)
            {
                //Put data into photo data transfer object
                PhotoDto SelectedPhoto = response.Content.ReadAsAsync<PhotoDto>().Result;
                ViewModel.Photo = SelectedPhoto;

                //Get Film data
                url = "filmdata/getfilms";
                response = client.GetAsync(url).Result;
                IEnumerable<FilmDto> PotentialFilms = response.Content.ReadAsAsync<IEnumerable<FilmDto>>().Result;
                ViewModel.AllFilms = PotentialFilms;

                //Get Lens data
                url = "lensdata/getLenses";
                response = client.GetAsync(url).Result;
                IEnumerable<LensDto> PotentialLenses = response.Content.ReadAsAsync<IEnumerable<LensDto>>().Result;
                ViewModel.AllLenses = PotentialLenses;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }

        // POST: Photo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Photo PhotoInfo)
        {
            string url = "photodata/updatephoto/" + id;
            Debug.WriteLine(jss.Serialize(PhotoInfo));
            HttpContent content = new StringContent(jss.Serialize(PhotoInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Photo/DeleteConfirm/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            DetailPhoto ViewModel = new DetailPhoto();
            string url = "photodata/findphoto/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            Debug.WriteLine(id);
            if (response.IsSuccessStatusCode)
            { 
                //Put data into photo data transfer object
                PhotoDto SelectedPhoto = response.Content.ReadAsAsync<PhotoDto>().Result;
                ViewModel.Photo = SelectedPhoto;

                //Get film data
                url = "photodata/getfilmforphoto/" + id;
                response = client.GetAsync(url).Result;
                FilmDto selectedFilm = response.Content.ReadAsAsync<FilmDto>().Result;
                ViewModel.Film = selectedFilm;

                //Get Lens data
                url = "photodata/getlensforphoto/" + id;
                response = client.GetAsync(url).Result;
                LensDto selectedLens = response.Content.ReadAsAsync<LensDto>().Result;
                ViewModel.Lens = selectedLens;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Photo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "photodata/deletephoto/" + id;
            //post body is empty
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
