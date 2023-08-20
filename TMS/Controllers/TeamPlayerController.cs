using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TMS.Models;

namespace TMS.Controllers
{
    public class TeamPlayerController : Controller
    {
        ApplicationUser appUser;
        RoleNames roles = new RoleNames();
        string curUserRole;
        UserRoleProvider urp = new UserRoleProvider();
        private APICall api = new APICall();
        private General general = new General();

        string APIURL = "TeamPlayerData/";
        string APIURL1 = "UserDetailsData/";

        public TeamPlayerController()
        {
            appUser = api.getCurrentUser();
            if (!String.IsNullOrEmpty(appUser.Id))
            {
                curUserRole = urp.getRoleFromId(appUser.Roles.FirstOrDefault().RoleId);
            }
        }

        // GET: TeamPlayer
        public ActionResult Index()
        {
            return View();
        }

        // GET: TeamPlayer/ReleasePlayer/5
        public ActionResult AddTeamPlayerToTeam(int id)
        {
            TeamPlayerDto teamPlayerDto = new TeamPlayerDto();
            teamPlayerDto.TeamId = id;

            string url = APIURL + "ListPlayers";

            HttpResponseMessage response = api.Get(url);

            if (response.StatusCode == HttpStatusCode.OK)
                teamPlayerDto.LstPlayers = response.Content.ReadAsAsync<IEnumerable<UserDetailDto>>().Result.ToList();

            ViewData["title"] = "Team Details";
            return View(teamPlayerDto);
        }

        [HttpPost]
        public ActionResult AddTeamPlayerToTeam(TeamPlayerDto teamPlayerDto)
        {
            TeamPlayerAssociation teamPLayer = new TeamPlayerAssociation()
            {
                PlayerId = teamPlayerDto.PlayerId,
                //JoinedDate = DateTime.Now,
                //JoinedPrice = teamPlayerDto.JoinedPrice,
                TeamId = teamPlayerDto.TeamId
            };

            string url = APIURL + "AddTeamPlayer/";
            HttpResponseMessage response = api.Post(url, teamPLayer);

            ViewData["title"] = "Team Details";

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(String.Concat("Details/", teamPlayerDto.TeamId), "Team");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: TeamPlayer/ReleasePlayer/5
        public ActionResult ReleasePlayer(int id)
        {
            string url = APIURL + "FineTeamPlayer/" + id;
            HttpResponseMessage response = api.Get(url);

            TeamPlayerDto selectedTeamPlayer = new TeamPlayerDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedTeamPlayer = response.Content.ReadAsAsync<TeamPlayerDto>().Result;

            ViewData["title"] = "Team Details";

            return View(selectedTeamPlayer);
        }

        // POST: TeamPlayer/ReleasePlayer/5
        [HttpPost]
        public ActionResult ReleaseTeamPlayer(int id, int teamId)
        {
            string url = APIURL + "ReleaseTeamPlayer/" + id;
            HttpResponseMessage response = api.Get(url);

            ViewData["title"] = "Team Details";

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(String.Concat("Details/", teamId), "Team");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}