using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class RoleNames
    {
        public DropdownViewModelForRole Admin = new DropdownViewModelForRole()
        {
            Id = "1",
            Name = "Admin"
        };
        public DropdownViewModelForRole Agent = new DropdownViewModelForRole()
        {
            Id = "2",
            Name = "Agent"
        };
        public DropdownViewModelForRole Owner = new DropdownViewModelForRole()
        {
            Id = "3",
            Name = "Team Owner"
        };
        public DropdownViewModelForRole Organizer = new DropdownViewModelForRole()
        {
            Id = "4",
            Name = "Tournament Organizer"
        };
        public DropdownViewModelForRole Player = new DropdownViewModelForRole()
        {
            Id = "5",
            Name = "Player"
        };
    }
}