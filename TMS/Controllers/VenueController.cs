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
        ApplicationUser appUser;
        RoleNames roles = new RoleNames();
        string curUserRole;
        UserRoleProvider urp = new UserRoleProvider();
        private APICall api = new APICall();
        private General general = new General();
        
        string APIURL = "VenueData/";
        string APIURL1 = "UserDetailsData/";
        string APIURL2 = "AgentVenueData/";

        public VenueController()
        {
            appUser = api.getCurrentUser();
            if (!String.IsNullOrEmpty(appUser.Id))
            {
                curUserRole = urp.getRoleFromId(appUser.Roles.FirstOrDefault().RoleId);
            }
        }

        // GET: Venue/List
        public ActionResult Index(string Search)
        {
            api.GetApplicationCookie();

            string url1 = APIURL1 + "GetUserDetails/" + appUser.Id;
            List<AgentVenueAssociationDto> agentVenuesFromTable = new List<AgentVenueAssociationDto>();
            UserDetailDto userDetails = new UserDetailDto();

            HttpResponseMessage response1 = api.Get(url1);

            if (response1.StatusCode == HttpStatusCode.OK)
                userDetails = response1.Content.ReadAsAsync<UserDetailDto>().Result;

            string url = APIURL2 + "ListAgentVenue/" + userDetails.Id;
            HttpResponseMessage response = api.Get(url);
            if (response.StatusCode == HttpStatusCode.OK)
                agentVenuesFromTable = response.Content.ReadAsAsync<IEnumerable<AgentVenueAssociationDto>>().Result.ToList();

            if (!String.IsNullOrEmpty(Search))
                agentVenuesFromTable = agentVenuesFromTable.Where(x =>
                    general.getLowerStringForSearch(x.VenueDetail.Name).Contains(general.getLowerStringForSearch(Search)) ||
                    general.getLowerStringForSearch(x.VenueDetail.Country).Contains(general.getLowerStringForSearch(Search))
                ).ToList();

            ViewData["title"] = "Team List";
            ViewData["search"] = Search;

            return View(agentVenuesFromTable);
        }

        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            api.GetApplicationCookie();
            string url = APIURL2 + "FindAgentVenue/" + id;

            HttpResponseMessage response = api.Get(url);
            AgentVenueAssociationDto selectedAgentVenue = new AgentVenueAssociationDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedAgentVenue = response.Content.ReadAsAsync<AgentVenueAssociationDto>().Result;

            ViewData["title"] = "Venue Details";
            return View(selectedAgentVenue);
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
                api.GetApplicationCookie();

                string url = APIURL + "AddVenue";
                string url1 = APIURL1 + "GetUserDetails/" + appUser.Id;
                string url2 = APIURL2 + "AddAgentVenue";

                UserDetailDto userDetails = new UserDetailDto();
                HttpResponseMessage response1 = api.Get(url1);

                if (response1.StatusCode == HttpStatusCode.OK)
                    userDetails = response1.Content.ReadAsAsync<UserDetailDto>().Result;

                Venue venue = new Venue()
                {
                    Name = venueDto.Name,
                    Country = venueDto.Country,
                    BasePrice = venueDto.BasePrice,
                    Location = venueDto.Location
                };

                HttpResponseMessage response = api.Post(url, venue);

                if (response.IsSuccessStatusCode)
                {
                    Venue createdVenue = response.Content.ReadAsAsync<Venue>().Result;

                    AgentVenuesAssosiation avAssosiation = new AgentVenuesAssosiation()
                    {
                        AgentId = userDetails.Id,
                        VenueId = createdVenue.Id
                    };


                    HttpResponseMessage response2 = api.Post(url2, avAssosiation);

                    if (response2.IsSuccessStatusCode)
                    {
                        return RedirectToAction("/");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
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
                api.GetApplicationCookie();

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

        // GET: Venue/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            api.GetApplicationCookie();
            string url = APIURL2 + "FindAgentVenue/" + id;

            HttpResponseMessage response = api.Get(url);
            AgentVenueAssociationDto selectedAgentVenue = new AgentVenueAssociationDto();

            if (response.StatusCode == HttpStatusCode.OK)
                selectedAgentVenue = response.Content.ReadAsAsync<AgentVenueAssociationDto>().Result;

            ViewData["title"] = "Delete Venue";
            return View(selectedAgentVenue);
        }


        // POST: Venue/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AgentVenueAssociationDto agentVenue)
        {
            try
            {
                // TODO: Add delete logic here
                string url = APIURL + "DeleteVenue/" + agentVenue.VenueId;
                string url1 = APIURL2 + "ReleaseAgentProperty/" + agentVenue.Id;
                Object obj = new Object();

                HttpResponseMessage response1 = api.Get(url1);

                if (response1.IsSuccessStatusCode)
                {
                    HttpResponseMessage response = api.Post(url, obj);
                    if (response1.IsSuccessStatusCode)
                    {
                        return RedirectToAction("/");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
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
