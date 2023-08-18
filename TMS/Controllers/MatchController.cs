using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TMS.Models;

namespace TMS.Controllers
{
    public class MatchController : Controller
    {
        string APIURL = "MatchData/";
        string APIURL1 = "TeamData/";
        string APIURL2 = "TournamentVenuesData/";
        private APICall api = new APICall();
        private General general = new General();

        private void GetApplicationCookie()
        {
            HttpClient client = api.getClient();
            string token = "";
            //HTTP client is set up to be reused, otherwise it will exhaust server resources.
            //This is a bit dangerous because a previously authenticated cookie could be cached for
            //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //collect token as it is submitted to the controller
            //use it to pass along to the WebAPI.
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }

        // GET: Match
        public ActionResult Index(string Search)
        {
            string url = APIURL + "Listmatches";
            HttpResponseMessage response = api.Get(url);
            List<MatchDto> matches = new List<MatchDto>();

            if (response.StatusCode == HttpStatusCode.OK)
                matches = response.Content.ReadAsAsync<IEnumerable<MatchDto>>().Result.ToList();

            if (!String.IsNullOrEmpty(Search))
                matches = matches.Where(x =>
                    general.getLowerStringForSearch(String.Concat(x.Team1, " vs ", x.Team2))
                    .Contains(general.getLowerStringForSearch(Search)) ||
                    general.getLowerStringForSearch(x.TournamentVenue.VenueDetail.Name)
                    .Contains(general.getLowerStringForSearch(Search))
                ).ToList();

            ViewData["search"] = Search;

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
            MatchDto matchDto = new MatchDto();

            string url = APIURL1 + "ListTeams";
            string url1 = APIURL2 + "ListTournamentVenues";

            HttpResponseMessage response = api.Get(url);

            if (response.StatusCode == HttpStatusCode.OK)
                matchDto.Teams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result.ToList();

            HttpResponseMessage response1 = api.Get(url1);

            if (response1.StatusCode == HttpStatusCode.OK)
                matchDto.TournamentVenues = response1.Content.ReadAsAsync<IEnumerable<TournamentVenueAssociationDto>>().Result.ToList();

            ViewData["title"] = "Create Match";
            return View(matchDto);
        }

        [HttpPost]
        public ActionResult Create(MatchDto matchDto)
        {
            try
            {
                string url = APIURL + "AddMatch";

                Match match = new Match()
                {
                    DateTime = matchDto.DateTime,
                    Team1Id = matchDto.Team1Id,
                    Team2Id = matchDto.Team2Id,
                    TournamentVenueId = matchDto.TournamentVenueId
                };

                HttpResponseMessage response = api.Post(url, match);
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
    
        // GET: Match/Edit/5
        public ActionResult Edit(int id)
        {
            MatchDto selectedMatch = new MatchDto();
            string url = APIURL + "FindMatch/" + id;
            string url1 = APIURL1 + "ListTeams";
            string url2 = APIURL2 + "ListTournamentVenues";

            HttpResponseMessage response = api.Get(url);
            if (response.StatusCode == HttpStatusCode.OK)
                selectedMatch = response.Content.ReadAsAsync<MatchDto>().Result;

            HttpResponseMessage response1 = api.Get(url1);
            if (response1.StatusCode == HttpStatusCode.OK)
                selectedMatch.Teams = response1.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result.ToList();

            HttpResponseMessage response2 = api.Get(url2);
            if (response2.StatusCode == HttpStatusCode.OK)
                selectedMatch.TournamentVenues = response2.Content.ReadAsAsync<IEnumerable<TournamentVenueAssociationDto>>().Result.ToList();

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
                    Id= id,
                    DateTime = matchDto.DateTime,
                    Team1Id = matchDto.Team1Id,
                    Team2Id = matchDto.Team2Id,
                    TournamentVenueId = matchDto.TournamentVenueId
                };

                HttpResponseMessage response = api.Post(url, match);

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
