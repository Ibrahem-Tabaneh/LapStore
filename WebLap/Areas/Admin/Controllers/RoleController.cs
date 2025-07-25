using Bl.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult List()
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Roles = roleManager.Roles.OrderBy(x => x.Name).ToList();
            return View(roleViewModel);
        }
        public async Task<IActionResult> Edit(string? RoleId)
        {
            NewRole newRole = new NewRole();

            if(RoleId==null ||RoleId== Guid.Empty.ToString()) return View(newRole);

            else
            {
                IdentityRole Role = new IdentityRole();
                Role = await roleManager.FindByIdAsync(RoleId);
                newRole.Id = Role.Id;
                newRole.RoleName = Role.Name;
                return View(newRole);
            }

            
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(NewRole role)
        {
            if(!ModelState.IsValid) return View("Edit",role);
            else
            {
                var identityrloe = new IdentityRole
                {
                    Id = role.Id,
                    Name = role.RoleName
                };
                //create
                if(identityrloe.Id==null || role.Id == Guid.Empty.ToString())
                {
                    identityrloe.Id = Guid.NewGuid().ToString();

                    var result =await roleManager.CreateAsync(identityrloe);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("List");
                    }
                    return RedirectToAction("List");
                }
                //update
                else
                {
                    var oldrole = await roleManager.FindByIdAsync(identityrloe.Id);
                    if(oldrole!=null) 
                    {
                        oldrole.Name = role.RoleName;
                        var result= await roleManager.UpdateAsync(oldrole);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("List");
                        }
                        return RedirectToAction("List");

                    }
                    return RedirectToAction("List");

                }
            }
        }
        public async Task<IActionResult> Delete(string? RoleId)
        {
            IdentityRole role = new IdentityRole();

            role=await roleManager.FindByIdAsync(RoleId);

            var result= await roleManager.DeleteAsync(role);
            if(result.Succeeded)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}
