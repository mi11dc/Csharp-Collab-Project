using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models.ViewModel
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public int OwnerId { get; set; }

        public string UserDetails { get; set; }
    }
}