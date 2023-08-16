using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace TMS.Models
{
    public class UserRoleProvider
    {
        RoleNames roles = new RoleNames();

        public List<DropdownViewModelForRole> GetAllRoles(string[] ExcludeRoles)
        {
            List<DropdownViewModelForRole> listRoles = new List<DropdownViewModelForRole>();

            string[] arr = new string[] { roles.Admin.Name, roles.Owner.Name, 
                roles.Organizer.Name, roles.Agent.Name, roles.Player.Name };

            arr.ToList().ForEach((role) => 
            {
                if (!ExcludeRoles.Contains(role))
                {
                    listRoles.Add(new DropdownViewModelForRole()
                    {
                        Id = role,
                        Name = role
                    });
                }
            });

            return listRoles;
        }

        public string getRoleFromId (string Id)
        {
            string roleName = "";

            if (roles.Admin.Id.Equals(Id))
            {
                roleName = roles.Admin.Name;
            } else if (roles.Agent.Id.Equals(Id))
            {
                roleName = roles.Agent.Name;
            } else if (roles.Owner.Id.Equals(Id))
            {
                roleName = roles.Owner.Name;
            } else if (roles.Organizer.Id.Equals(Id))
            {
                roleName = roles.Organizer.Name;
            } else if (roles.Player.Id.Equals(Id))
            {
                roleName = roles.Player.Name;
            }

            return roleName;
        }
    }
}