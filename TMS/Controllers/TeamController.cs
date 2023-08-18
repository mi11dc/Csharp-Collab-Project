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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace TMS.Controllers
{
    public class TeamController : Controller
    {
        ApplicationUser appUser;
        RoleNames roles = new RoleNames();
        string curUserRole;
        UserRoleProvider urp = new UserRoleProvider();
        private APICall api = new APICall();
        private General general = new General();
        
        string APIURL = "TeamData/";
        string APIURL1 = "UserDetailsData/";

        public TeamController()
        {
            appUser = api.getCurrentUser();
            if (!String.IsNullOrEmpty(appUser.Id))
            {
                curUserRole = urp.getRoleFromId(appUser.Roles.FirstOrDefault().RoleId);
            }
        }

        // GET: Team/
        public ActionResult Index(string Search)
        {
            api.GetApplicationCookie();

            string url = APIURL + "ListTeams";
            string url1 = APIURL1 + "GetUserDetails/" + appUser.Id;
            List<TeamDto> teams = new List<TeamDto>();
            UserDetailDto userDetails = new UserDetailDto();

            HttpResponseMessage response = api.Get(url);
            HttpResponseMessage response1 = api.Get(url1);

            if (response1.StatusCode == HttpStatusCode.OK)
                userDetails = response1.Content.ReadAsAsync<UserDetailDto>().Result;

            if (response.StatusCode == HttpStatusCode.OK)
                teams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result.ToList();

            if (!String.IsNullOrEmpty(Search))
                teams = teams.Where(x =>
                    general.getLowerStringForSearch(x.Name).Contains(general.getLowerStringForSearch(Search)) ||
                    general.getLowerStringForSearch(x.Country).Contains(general.getLowerStringForSearch(Search))
                ).ToList();

            ViewData["title"] = "Team List";
            ViewData["search"] = Search;
            ViewData["user"] = userDetails;
            ViewData["role"] = curUserRole;

            return View(teams);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            api.GetApplicationCookie();
            string url = APIURL + "FindTeam/" + id;

            HttpResponseMessage response = api.Get(url);
            TeamDto selectedTeam = new TeamDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;

            ViewData["title"] = "Team Details";
            return View(selectedTeam);
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
                api.GetApplicationCookie();

                string url1 = APIURL1 + "GetUserDetails/" + appUser.Id;

                UserDetailDto userDetails = new UserDetailDto();
                HttpResponseMessage response1 = api.Get(url1);

                if (response1.StatusCode == HttpStatusCode.OK)
                    userDetails = response1.Content.ReadAsAsync<UserDetailDto>().Result;

                string url = APIURL + "AddTeam";
                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Country = teamDto.Country,
                    OwnerId = userDetails.Id
                };

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
            api.GetApplicationCookie();

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
                api.GetApplicationCookie();

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
            api.GetApplicationCookie();

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
                api.GetApplicationCookie();

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

        public ActionResult Error()
        {

            return View();
        }
    }
}
