using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMS.Models;

namespace TMS.Controllers
{
    public class MatchDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [ResponseType(typeof(MatchDto))]
        public IEnumerable<MatchDto> ListMatches()
        {
            List<Match> Matches = db.Matches.ToList();
            List<MatchDto> MatchDtos = new List<MatchDto>();

            Matches.ForEach(a => MatchDtos.Add(new MatchDto()
            {
                Id = a.Id,
                DateTime = a.DateTime,
                Team1 = a.TeamDetail1.Name,
                Team2 = a.TeamDetail2.Name,
                TournamentVenue = a.TournamentVenueDetail.VenueDetail.Name,

            }));
            return MatchDtos;

        }

        // GET: api/MatchData/FindMatch/1
        [ResponseType(typeof(Match))]
        [HttpGet]
        public IHttpActionResult FindMatch(int id)
        {
            Match Match = db.Matches.Find(id);
            MatchDto MatchDto = new MatchDto()
            {
                Id = Match.Id,
                DateTime = Match.DateTime,
                Team1 = Match.TeamDetail1.Name,
                Team2 = Match.TeamDetail2.Name,
                TournamentVenue = Match.TournamentVenueDetail.VenueDetail.Name,
            };
            if (Match == null)
            {
                return NotFound();
            }

            return Ok(MatchDto);
        }



        // PUT: api/MatchData/updatematch/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMatch(int id, Match match)
        {
            Debug.WriteLine("I have reached the update match method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != match.Id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + match.Id);
                Debug.WriteLine("POST parameter" + match.Team1Id);
                Debug.WriteLine("POST parameter " + match.Team2Id);
                Debug.WriteLine("POST parameter " + match.TournamentVenueDetail);
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    Debug.WriteLine("Match not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/MatchData/AddMatch
        [ResponseType(typeof(Match))]
        public IHttpActionResult PostMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matches.Add(match);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = match.Id }, match);
        }

        // DELETE: api/MatchData/DeleteMatch/5
        [ResponseType(typeof(Match))]
        public IHttpActionResult DeleteMatch(int id)
        {
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            db.SaveChanges();

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int id)
        {
            return db.Matches.Count(e => e.Id == id) > 0;
        }
    }
}