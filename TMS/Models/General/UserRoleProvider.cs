using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace TMS.Models
{
    public class UserRoleProvider
    {
        public List<DropdownViewModelForRole> GetAllRoles(string[] ExcludeRoles)
        {
            List<DropdownViewModelForRole> listRoles = new List<DropdownViewModelForRole>();

            string[] arr = new string[] { RoleNames.Admin, RoleNames.Owner, 
                RoleNames.Organizer, RoleNames.Agent, RoleNames.Player };

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
    }
}