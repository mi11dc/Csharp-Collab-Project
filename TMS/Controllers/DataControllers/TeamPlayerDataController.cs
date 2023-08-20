using System;
using System.Collections.Generic;
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
    public class TeamPlayerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        RoleNames roles = new RoleNames();

        // GET: api/TeamPlayerData/ListTeamPlayers
        [HttpGet]
        public IHttpActionResult ListTeamPlayers(int id)
        {
            var users = db.UserDetails.ToList();
            List<TeamPlayerAssociation> teamPlayers = db.TeamPlayerAssociations.ToList();
            List<UserDetailDto> playerDtos = new List<UserDetailDto>();

            users.ForEach(p =>
            {
                if (teamPlayers.Count(x => (x.PlayerId == p.Id) && (x.TeamId == id)) > 0)
                //if (teamPlayers.Count(x => x.PlayerId == p.Id) == 0)
                //    if (teamPlayers.Count(x => x.TeamId == id) > 0)
                    playerDtos.Add(new UserDetailDto()
                    {
                        Id = p.Id,
                        FName = p.FName,
                        LName = p.LName
                    });
            });

            return Ok(playerDtos);
        }

        [HttpGet]
        public IHttpActionResult ListPlayers()
        {

            List<UserDetail> users = db.UserDetails.ToList();
            List<TeamPlayerAssociation> teamPlayers = db.TeamPlayerAssociations.ToList();
            List<UserDetailDto> playerDtos = new List<UserDetailDto>();

            users.ForEach((u) =>
            {
                if (u.User.Roles.FirstOrDefault().RoleId.Equals(roles.Player.Id))
                    if (teamPlayers.Count(x => x.PlayerId == u.Id) == 0)
                        playerDtos.Add(new UserDetailDto()
                        {
                            Id = u.Id,
                            FName = u.FName,
                            LName = u.LName,
                        });
            });

            return Ok(playerDtos);
        }

        //// GET: api/TeamPlayerData/ListTeamPlayersByTeamId
        //[HttpGet]
        //public IEnumerable<TeamPlayerDetailsWithPlayerDto> ListTeamPlayersByTeamId(int id)
        //{
        //    List<TeamPlayer> teamPlayers = db.TeamPlayers.ToList();
        //    List<TeamPlayerDetailsWithPlayerDto> playerDetails = new List<TeamPlayerDetailsWithPlayerDto>();

        //    teamPlayers = teamPlayers.Where(x =>
        //        (x.TeamId == id)
        //        && (x.ReleaseDate == null)
        //    ).ToList();

        //    if (teamPlayers != null && teamPlayers.Count > 0)
        //        teamPlayers.ForEach(tp => playerDetails.Add(new TeamPlayerDetailsWithPlayerDto()
        //        {
        //            Id = tp.Id,
        //            PlayerId = tp.PlayerId,
        //            PlayerName = String.Concat(tp.Players.FName, ' ', tp.Players.LName),
        //            TeamId = tp.TeamId,
        //            TeamName = tp.Teams.Name,
        //            JoinedDate = tp.JoinedDate,
        //            JoinedPrice = tp.JoinedPrice,
        //            ReleaseDate = (tp.ReleaseDate != null) ? (DateTime)tp.ReleaseDate : DateTime.MinValue,
        //            BasePrice = tp.Players.BasePrice,
        //            Country = tp.Players.Country,
        //            DOB = (DateTime)tp.Players.DOB,
        //            SDOB = tp.Players.DOB.ToString()
        //        }));

        //    return playerDetails;
        //}

        // GET: api/TeamPlayerData/ReleaseTeamPlayer/5
        [HttpGet]
        public IHttpActionResult ReleaseTeamPlayer(int id)
        {
            TeamPlayerAssociation teamPlayer = db.TeamPlayerAssociations.Find(id);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            db.TeamPlayerAssociations.Remove(teamPlayer);
            db.SaveChanges();

            return Ok(teamPlayer);
        }

        // GET: api/TeamPlayerData/FineTeamPlayer/5
        [ResponseType(typeof(TeamPlayerAssociation))]
        [HttpGet]
        public IHttpActionResult FineTeamPlayer(int id)
        {
            TeamPlayerAssociation teamPlayer = db.TeamPlayerAssociations.Where(x => x.PlayerId == id).FirstOrDefault();
            if (teamPlayer == null)
            {
                return NotFound();
            }

            TeamPlayerDto teamPlayerDto = new TeamPlayerDto()
            {
                Id = teamPlayer.Id,
                PlayerId = teamPlayer.PlayerId,
                PlayerName = String.Concat(teamPlayer.PlayerDetail.FName, ' ', teamPlayer.PlayerDetail.LName),
                TeamId = teamPlayer.TeamId,
                TeamName = teamPlayer.TeamDetail.Name,
                //JoinedDate = teamPlayer.JoinedDate,
                //JoinedPrice = teamPlayer.JoinedPrice,
                //ReleaseDate = (teamPlayer.ReleaseDate != null) ? (DateTime)teamPlayer.ReleaseDate : DateTime.MinValue
            };

            return Ok(teamPlayerDto);
        }

        // POST: api/TeamPlayerData/AddTeamPlayer
        [ResponseType(typeof(TeamPlayerAssociation))]
        [HttpPost]
        public IHttpActionResult AddTeamPlayer(TeamPlayerAssociation teamPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamPlayerAssociations.Add(teamPlayer);
            db.SaveChanges();

            return Ok(teamPlayer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool TeamPlayerExists(int id)
        //{
        //    return db.TeamPlayerAssociations.Count(e => e.Id == id) > 0;
        //}
    }
}
