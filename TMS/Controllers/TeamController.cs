using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TMS.Models.ViewModel;
using TMS.Models;

namespace TMS.Controllers
{
    public class TeamController : Controller
    {
        string APIURL = "TeamData/";
        private APICall api = new APICall();
        private General general = new General();

        // GET: Team/
        public ActionResult Index(string Search)
        {

            string url = APIURL + "ListTeams";

            HttpResponseMessage response = api.Get(url);
            List<TeamDto> teams = new List<TeamDto>();

            if (response.StatusCode == HttpStatusCode.OK)
                teams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result.ToList();

            if (!String.IsNullOrEmpty(Search))
                teams = teams.Where(x =>
                    general.getLowerStringForSearch(x.Name).Contains(general.getLowerStringForSearch(Search))
                    //general.getLowerStringForSearch(x.UserDetails.).Contains(general.getLowerStringForSearch(Search)) ||
                    //general.getLowerStringForSearch(x.FormedOn.ToShortDateString()).Contains(general.getLowerStringForSearch(Search))
                ).ToList();

            ViewData["title"] = "Team List";
            ViewData["search"] = Search;

            return View(teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            string url = APIURL + "FindTeam/" + id;

            HttpResponseMessage response = api.Get(url);
            TeamDto selectedTeam = new TeamDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;

            ViewData["title"] = "Team Details";
            return View(selectedTeam);
        }
        
        public ActionResult Error()
        {

            return View();
        }
        
        // GET: Team/Create
        public ActionResult Create()
        {
            ViewData["title"] = "Create Team";
            return View();
        }   


        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamDto teamDto)
        {
            try
            {
                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Id = teamDto.Id,
                    Country = teamDto.Country,
                    OwnerId = teamDto.OwnerId
                };

                string url = APIURL + "AddTeam";
                HttpResponseMessage response = api.Post(url, team);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(String.Concat("/"));
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

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            string url = APIURL + "FindTeam/" + id;
            HttpResponseMessage response = api.Get(url);
            TeamDto selectedTeam = new TeamDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;

            ViewData["title"] = "Edit Team";

            return View(selectedTeam);
        }



        // POST: Team/Update/5
        [HttpPost]
        public ActionResult Update(int id, Team teamDto)
        {
            try
            {
                // TODO: Add update logic here
                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Id = teamDto.Id,
                    Country = teamDto.Country,
                    OwnerId = teamDto.OwnerId
                };

                string url = APIURL + "UpdateTeam/" + id;

                HttpResponseMessage response = api.Post(url, team);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(String.Concat("Details/", id));
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

        // GET: Team/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = APIURL + "FindTeam/" + id;
            HttpResponseMessage response = api.Get(url);
            TeamDto selectedTeam = new TeamDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;

            ViewData["title"] = "Edit Team";

            return View(selectedTeam);
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                string url = APIURL + "DeleteTeam/" + id;
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
