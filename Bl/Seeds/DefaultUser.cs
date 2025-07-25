using Bl.ViewModel;
using Domin.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Seeds
{
    public class DefaultUser
    {
        public static async Task SeedUserAsync (UserManager<ApplicationUser> userManager)
        {
            var Defaultuser = new ApplicationUser
            {
                Fname=Helper.Fname,
                Lname=Helper.Lname,
                Email=Helper.Email,
                UserName=Helper.UserName,
                EmailConfirmed=true,                

            };

            var user = await userManager.FindByEmailAsync(Defaultuser.Email);
            
            if (user == null)
            {
                var result=await userManager.CreateAsync(Defaultuser,Helper.password);
                if (result.Succeeded)
                {
                  await  userManager.AddToRoleAsync(Defaultuser,Helper.Roles.Admin.ToString());
                }
            }
        }
    }
}
