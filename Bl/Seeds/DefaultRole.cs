using Domin.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Seeds
{
    public class DefaultRole
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Helper.Roles.Customer.ToString()));

        }
    }
}
