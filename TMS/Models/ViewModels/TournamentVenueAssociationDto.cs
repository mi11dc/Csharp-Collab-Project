using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TournamentVenueAssociationDto
    {
        public int Id { get; set; }
        public decimal RentedPrice { get; set; }
        public int VenueId { get; set; }
        public Venue VenueDetail { get; set; }
        public int TournamentId { get; set; }
        public Tournament TournamentDetail { get; set; }
    }
}