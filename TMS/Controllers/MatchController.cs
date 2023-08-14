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
using TMS.Models.ViewModel;

namespace TMS.Controllers
{
    public class MatchController : Controller
    {
        string APIURL = "MatchData/";
        private APICall api = new APICall();
        private General general = new General();

        // GET: Match
        public ActionResult Index()
        {
            string url = APIURL + "Listmatches";
            HttpResponseMessage response = api.Get(url);
            List<MatchDto> matches = new List<MatchDto>();

            if (response.StatusCode == HttpStatusCode.OK)
                matches = response.Content.ReadAsAsync<IEnumerable<MatchDto>>().Result.ToList();

            return View(matches);
        }

        // GET: Match/Details/5
        public ActionResult Details(int id)
        {
            string url = APIURL + "FindMatch/" + id;

            HttpResponseMessage response = api.Get(url);
            MatchDto selectedMatch = new MatchDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedMatch = response.Content.ReadAsAsync<MatchDto>().Result;

            ViewData["title"] = "Match Details";
            return View(selectedMatch);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            ViewData["title"] = "Create Match";
            return View();
        }

        [HttpPost]
        public ActionResult Create(MatchDto matchDto)
        {
            try
            {
                string url = APIURL + "AddMatch";

                Match match = new Match()
                {
                    DateTime = DateTime.Now,
                    //Team1Id = matchDto.Team1
                };

                HttpResponseMessage response = api.Post(url, match);
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
    
        // GET: Match/Edit/5
        public ActionResult Edit(int id)
        {
            string url = APIURL + "FindMatch/" + id;

            HttpResponseMessage response = api.Get(url);
            MatchDto selectedMatch = new MatchDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedMatch = response.Content.ReadAsAsync<MatchDto>().Result;

            ViewData["title"] = "Match Update";
            return View(selectedMatch);
        }

        // POST: Match/Update/5
        [HttpPost]
        public ActionResult Update(int id, MatchDto matchDto)
        {
            try
            {
                string url = APIURL + "UpdateMatch/" + id;
                Match match = new Match()
                {
                    DateTime = DateTime.Now,
                    //Team1Id = matchDto.Team1
                };

                HttpResponseMessage response = api.Post(url, match);

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

        // GET: Match/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = APIURL + "FindMatch/" + id;

            HttpResponseMessage response = api.Get(url);
            MatchDto selectedMatch = new MatchDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedMatch = response.Content.ReadAsAsync<MatchDto>().Result;

            ViewData["title"] = "Match Update";
            return View(selectedMatch);
        }

        // POST: Match/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try 
            { 
                string url = APIURL + "DeleteMatch/" + id;
                Object obj = new Object();

                HttpResponseMessage response = api.Post(url, obj);

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
    }
}
