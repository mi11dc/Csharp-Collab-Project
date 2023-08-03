using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class AgentVenuesAssosiation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foriegn Key
        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public virtual Venue VenueDetail { get; set; }
        public int AgentId { get; set; }
        [ForeignKey("AgentId")]
        public virtual UserDetail AgentDetail { get; set; }
    }
}