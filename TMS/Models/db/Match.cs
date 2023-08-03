using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        // Foriegn Key
        public int Team1Id { get; set; }
        [ForeignKey("Team1Id")]
        public virtual Team TeamDetail1 { get; set; }
        public int Team2Id { get; set; }
        [ForeignKey("Team2Id")]
        public virtual Team TeamDetail2 { get; set; }
        public int TournamentVenueId { get; set; }
        [ForeignKey("TournamentVenueId")]
        public virtual TournamentVenueAssociation TournamentVenueDetail { get; set; }
    }
}