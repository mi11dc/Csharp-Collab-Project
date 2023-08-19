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
        public string sDateTime { get; set; }
        public int Team1Id { get; set; }
        public string Team1 { get; set; }
        public int Team2Id { get; set; }
        public string Team2 { get; set; }
        public int TournamentVenueId { get; set; }
        public TournamentVenueAssociation TournamentVenue { get; set; }
        public List<TeamDto> Teams { get; set; }
        public List<TournamentVenueAssociationDto> TournamentVenues { get; set; }
        
    }
}