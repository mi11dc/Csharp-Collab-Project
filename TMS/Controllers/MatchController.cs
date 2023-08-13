using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TMS.Models;
using TMS.Models.ViewModel;

namespace TMS.Controllers
{
    public class MatchController : Controller
    {
        private static readonly HttpClient client;
        private ApplicationDbContext db = new ApplicationDbContext();
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static MatchController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44395/api/");
        }
        // GET: Match/List
        public ActionResult List()
        {
            string url = "MatchData/listmatches";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The Response is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<MatchDto> matches = response.Content.ReadAsAsync<IEnumerable<MatchDto>>().Result;
            Debug.WriteLine("Number of Matches received: ");
            Debug.WriteLine(matches.Count());

            return View(matches);
        }

        // GET: Match/Details/5
        public ActionResult Details(int id)
        {
            //DetailsMatch ViewModel = new DetailsMatch();

            string url = "MatchData/findmatch/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("the respone code is ");
            Debug.WriteLine(response.StatusCode);

            MatchDto selectedMatch = response.Content.ReadAsAsync<MatchDto>().Result;
            Debug.WriteLine("matches : ");
            Debug.WriteLine(selectedMatch.Id);

            //ViewModel.SelectedMatch = selectedMatch;

            return View(selectedMatch);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Match match)
        {
            string url = "MatchData/addmatch";

            string jsonpayload = jss.Serialize(match);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }
    
        // GET: Match/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateMatch ViewModel = new UpdateMatch();

            //the existing Match information
            string url = "Matchdata/findMatch/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MatchDto SelectedMatch = response.Content.ReadAsAsync<MatchDto>().Result;
            ViewModel.SelectedMatch = SelectedMatch;

            // all Venue to choose from when updating this Match
            //the existing Match information
            url = "Venuedata/listVenues/";
            response = client.GetAsync(url).Result;
            IEnumerable<VenueDto> VenueOptions = response.Content.ReadAsAsync<IEnumerable<VenueDto>>().Result;

            ViewModel.VenueOptions = VenueOptions;

            return View(ViewModel);
        }

        // POST: Match/Update/5
        [HttpPost]
        public ActionResult Update(int id, Match Match)
        {

            string url = "Matchdata/updateMatch/" + id;
            string jsonpayload = jss.Serialize(Match);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Match/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Matchdata/findMatch/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MatchDto selectedMatch = response.Content.ReadAsAsync<MatchDto>().Result;
            return View(selectedMatch);
        }

        // POST: Match/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Matchdata/deleteMatch/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
