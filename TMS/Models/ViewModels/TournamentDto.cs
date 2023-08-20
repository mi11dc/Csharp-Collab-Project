using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string sStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string sEndDate { get; set; }
        public int OrganizerId { get; set; }
        public UserDetail OrganizerDetails { get; set; }
    }
}