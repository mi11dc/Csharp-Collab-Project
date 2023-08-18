using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMS.Models;

namespace TMS.Controllers
{
    public class TournamentVenuesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VenueData/listVenues
        [HttpGet]
        [ResponseType(typeof(TournamentVenueAssociationDto))]
        public IHttpActionResult ListTournamentVenues()
        {
            List<TournamentVenueAssociation> TournamentVenues = db.TournamentVenueAssociations.ToList();
            List<TournamentVenueAssociationDto> TournamentVenueDtos = new List<TournamentVenueAssociationDto>();

            TournamentVenues.ForEach(s => TournamentVenueDtos.Add(new TournamentVenueAssociationDto()
            {
                Id = s.Id,
                VenueId = s.VenueId,
                RentedPrice= s.RentedPrice,
                TournamentDetail= s.TournamentDetail,
                TournamentId = s.TournamentId,
                VenueDetail = s.VenueDetail,
            }));

            return Ok(TournamentVenueDtos);
        }
    }
}
