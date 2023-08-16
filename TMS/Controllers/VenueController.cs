using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TMS.Models;

namespace TMS.Controllers
{
    public class VenueController : Controller
    {
        string APIURL = "VenueData/";
        private APICall api = new APICall();
        private General general = new General();

        // GET: Venue/List
        public ActionResult Index(string Search)
        {

            string url = APIURL +  "ListVenues";
            HttpResponseMessage response = api.Get(url);
            List<VenueDto> venues = new List<VenueDto>();

            if (response.StatusCode == HttpStatusCode.OK)
                venues = response.Content.ReadAsAsync<IEnumerable<VenueDto>>().Result.ToList();

            return View(venues);
        }

        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            string url = APIURL + "FindVenue/" + id;

            HttpResponseMessage response = api.Get(url);
            VenueDto selectedVenue = new VenueDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;

            ViewData["title"] = "Venue Details";
            return View(selectedVenue);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: Venue/New
        public ActionResult Create()
        {
            ViewData["title"] = "Create Venue";
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
        public ActionResult Create(VenueDto venueDto)
        {
            try
            {
                string url = APIURL + "AddVenue";

                Venue venue = new Venue()
                {
                    Name = venueDto.Name,
                    Id = venueDto.Id,
                    Country = venueDto.Country,
                    BasePrice = venueDto.BasePrice,
                    Location = venueDto.Location
                };

                HttpResponseMessage response = api.Post(url, venue);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("/");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Venue/Edit/5
        public ActionResult Edit(int id)
        {
            string url = APIURL + "FindVenue/" + id;

            HttpResponseMessage response = api.Get(url);
            VenueDto selectedVenue = new VenueDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;

            ViewData["title"] = "Venue Edit";
            return View(selectedVenue);

        }


        // POST: Venue/Update/5
        [HttpPost]
        public ActionResult Update(int id, VenueDto venueDto)
        {
            try 
            {
                string url = APIURL + "UpdateVenue/" + id;

                Venue venue = new Venue()
                {
                    Name = venueDto.Name,
                    Id = venueDto.Id,
                    Country = venueDto.Country,
                    BasePrice = venueDto.BasePrice,
                    Location = venueDto.Location
                };

                HttpResponseMessage response = api.Post(url, venue);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Venue/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = APIURL + "FindVenue/" + id;

            HttpResponseMessage response = api.Get(url);
            VenueDto selectedVenue = new VenueDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;

            ViewData["title"] = "Delete Venue";
            return View(selectedVenue);
        }


        // POST: Venue/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                string url = APIURL + "DeleteVenue/" + id;
                Object obj = new Object();

                HttpResponseMessage response = api.Post(url, obj);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("/");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
