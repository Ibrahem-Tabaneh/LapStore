using Bl.IRepository;
using Domin.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TypeeController : Controller
    {
        private readonly IServicesTypee servicesTypee;

        public TypeeController(IServicesTypee servicesTypee)
        {
            this.servicesTypee = servicesTypee;
        }

        public IActionResult Edit(int? TypeeId)
        {
            if(TypeeId == null||TypeeId==0)
            return View(new Typee());
            else
            {
                var Typee = servicesTypee.GetById((int)TypeeId);
                return View(Typee);
            }
        }
        public IActionResult List()
        {
            var Typees = servicesTypee.GetAll();
            return View(Typees);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Typee Typee)
        {
            if(!ModelState.IsValid) 
            return View("Edit",Typee);

            if(servicesTypee.Save(Typee))
                return RedirectToAction("List");

            return RedirectToAction("List");

        }
        public IActionResult Delete(int? TypeeId)
        {
            if(TypeeId==null||TypeeId==0)
                return NotFound();

            else
            {
               if(servicesTypee.Delete((int)TypeeId))
                    return RedirectToAction("List");

               else return NotFound();
            }
        }
    }
}
