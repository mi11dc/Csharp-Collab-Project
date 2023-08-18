using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMS.Models;

namespace TMS.Controllers
{
    public class TeamDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TeamData/ListTeams
        [HttpGet]
        [ResponseType(typeof(TeamDto))]
        public IHttpActionResult ListTeams()
        {

            List<Team> Team = db.Teams.ToList();
            List<TeamDto> TeamDtos = new List<TeamDto>();

            Team.ForEach(s => TeamDtos.Add(new TeamDto()
            {
                Id = s.Id,
                Name = s.Name,
                Country = s.Country,
                OwnerId= s.OwnerId,
                OwnerDetail = s.UserDetails,
            }));

            return Ok(TeamDtos);
        }
        // GET: api/TeamData/FindTeam/5

        [ResponseType(typeof(TeamDto))]
        [HttpGet]
        public IHttpActionResult FindTeam(int Id)
        {
            Team Team = db.Teams.Find(Id);
            TeamDto TeamDto = new TeamDto()
            {
                Id = Id,
                Name = Team.Name,
                Country = Team.Country,
                OwnerId = Team.OwnerId,
                OwnerDetail = Team.UserDetails,
            };
            if (Team == null)
            {
                return NotFound();
            }

            return Ok(TeamDto);
        }

        // PUT: api/TeamData/UpdateTeam/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeam(int Id, Team Team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Id != Team.Id)
            {
                return BadRequest();
            }

            db.Entry(Team).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/TeamData/AddTeam
        [ResponseType(typeof(Team))]
        public IHttpActionResult AddTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { Id = team.Id }, team);
        }

        // DELETE: api/TeamData/DeleteTeam/5
        [ResponseType(typeof(Team))]
        [HttpPost]
        public IHttpActionResult DeleteTeam(int Id)
        {
            Team team = db.Teams.Find(Id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int Id)
        {
            return db.Teams.Count(e => e.Id == Id) > 0;
        }
    }
}