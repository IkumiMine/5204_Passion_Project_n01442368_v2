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
    public class LensController : Controller
    {
        //Http Client is the proper way to connect to a webAPI
        private readonly JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static LensController()
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

        // GET: Lens/List
        public ActionResult List()
        {
            string url = "lensdata/getlenses";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<LensDto> SelectedLenses = response.Content.ReadAsAsync<IEnumerable<LensDto>>().Result;
                return View(SelectedLenses);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Lens/Details/5
        public ActionResult Details(int id)
        {
            string url = "lensdata/findlens/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.?
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into lens data transfer object
                LensDto SelectedLens = response.Content.ReadAsAsync<LensDto>().Result;
                return View(SelectedLens);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Lens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lens/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Lens NewLensInfo)
        {
            Debug.WriteLine(NewLensInfo.LensID);
            string url = "lensdata/addlens";
            Debug.WriteLine(jss.Serialize(NewLensInfo));
            HttpContent content = new StringContent(jss.Serialize(NewLensInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                int lensid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = lensid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Lens/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "lensdata/findlens/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into lens data transfer object
                LensDto SelectedLens = response.Content.ReadAsAsync<LensDto>().Result;
                return View(SelectedLens);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Lens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Lens NewLensInfo)
        {
            Debug.WriteLine(NewLensInfo.LensID);
            string url = "lensdata/updatelens/" + id;
            Debug.WriteLine(jss.Serialize(NewLensInfo));
            HttpContent content = new StringContent(jss.Serialize(NewLensInfo));
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

        // GET: Lens/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "lensdata/findlens/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into lens data transfer object
                LensDto SelectedLens = response.Content.ReadAsAsync<LensDto>().Result;
                return View(SelectedLens);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Lens/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "lensdata/deletelens/" + id;
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
