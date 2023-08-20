using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TeamPlayerDto
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public UserDetailDto Player { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public TeamDto Team { get; set; }

        public List<UserDetailDto> LstPlayers { get; set; }
    }
}