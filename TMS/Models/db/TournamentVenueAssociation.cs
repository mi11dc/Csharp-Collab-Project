using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TournamentVenueAssociation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal RentedPrice { get; set; }

        // Foriegn Key
        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public virtual Venue VenueDetail { get; set; }
        public int TournamentId { get; set; }
        [ForeignKey("TournamentId")]
        public virtual Tournament TournamentDetail { get; set; }
    }
}