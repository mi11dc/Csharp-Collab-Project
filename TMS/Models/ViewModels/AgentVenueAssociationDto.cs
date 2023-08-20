using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class AgentVenueAssociationDto
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public virtual Venue VenueDetail { get; set; }
        public int AgentId { get; set; }
        public virtual UserDetail AgentDetail { get; set; }
    }
}