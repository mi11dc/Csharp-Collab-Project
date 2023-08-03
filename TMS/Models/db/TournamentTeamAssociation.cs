using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TournamentTeamAssociation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foriegn Key
        public int TournamentId { get; set; }
        [ForeignKey("TournamentId")]
        public virtual Tournament TournamentDetail { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team TeamDetail { get; set; }
    }
}