using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _5204_Passion_Project_n01442368_v2.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;


namespace _5204_Passion_Project_n01442368_v2.Controllers
{
    public class FilmController : Controller
    {
        //Http Client is the proper way to connect to a webAPI
        private readonly JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static FilmController()
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

        // GET: Film/List
        public ActionResult List()
        {
            string url = "filmdata/getfilms";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<FilmDto> SelectedFilms = response.Content.ReadAsAsync<IEnumerable<FilmDto>>().Result;
                return View(SelectedFilms);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Film/Details/5
        public ActionResult Details(int id)
        {
            string url = "filmdata/findfilm/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.?
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into film data transfer object
                FilmDto SelectedFilm = response.Content.ReadAsAsync<FilmDto>().Result;
                return View(SelectedFilm);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Film/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Film/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Film FilmInfo)
        {
            Debug.WriteLine(FilmInfo.FilmID);
            string url = "filmdata/addfilm";
            Debug.WriteLine(jss.Serialize(FilmInfo));
            HttpContent content = new StringContent(jss.Serialize(FilmInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                int filmid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = filmid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Film/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "filmdata/findfilm/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into film data transfer object
                FilmDto SelectedFilm = response.Content.ReadAsAsync<FilmDto>().Result;
                return View(SelectedFilm);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Film/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Film FilmInfo)
        {
            Debug.WriteLine(FilmInfo.FilmSeries);
            string url = "filmdata/updatefilm/"+id;
            Debug.WriteLine(jss.Serialize(FilmInfo));
            HttpContent content = new StringContent(jss.Serialize(FilmInfo));
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

        // GET: Film/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "filmdata/findfilm/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into film data transfer object
                FilmDto SelectedFilm = response.Content.ReadAsAsync<FilmDto>().Result;
                return View(SelectedFilm);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Film/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "filmdata/deletefilm/" + id;
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
