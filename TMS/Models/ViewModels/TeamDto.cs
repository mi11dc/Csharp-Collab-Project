using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public int OwnerId { get; set; }

        public UserDetail OwnerDetail { get; set; }
    }
}