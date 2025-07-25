using Bl.IRepository;
using Domin.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OsController : Controller
    {
        private readonly IServicesOs servicesOs;

        public OsController(IServicesOs servicesOs)
        {
            this.servicesOs = servicesOs;
        }

        public IActionResult Edit(int? OsId)
        {
            if(OsId == null||OsId==0)
            return View(new Os());
            else
            {
                var Os = servicesOs.GetById((int)OsId);
                return View(Os);
            }
        }
        public IActionResult List()
        {
            var Oss = servicesOs.GetAll();
            return View(Oss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Os Os)
        {
            if(!ModelState.IsValid) 
            return View("Edit",Os);

            if(servicesOs.Save(Os))
                return RedirectToAction("List");

            return RedirectToAction("List");

        }
        public IActionResult Delete(int? OsId)
        {
            if(OsId==null||OsId==0)
                return NotFound();

            else
            {
               if(servicesOs.Delete((int)OsId))
                    return RedirectToAction("List");

               else return NotFound();
            }
        }
    }
}
