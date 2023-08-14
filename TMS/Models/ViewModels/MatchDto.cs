using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class MatchDto
    {        
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public string TournamentVenue { get; set; }
        
    }
}