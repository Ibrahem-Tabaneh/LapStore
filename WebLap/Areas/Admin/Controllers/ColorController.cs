using Bl.IRepository;
using Domin.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IServicesColor servicesColor;

        public ColorController(IServicesColor servicesColor)
        {
            this.servicesColor = servicesColor;
        }

        public IActionResult Edit(int? ColorId)
        {
            if(ColorId == null||ColorId==0)
            return View(new Color());
            else
            {
                var color = servicesColor.GetById((int)ColorId);
                return View(color);
            }
        }
        public IActionResult List()
        {
            var colors = servicesColor.GetAll();
            return View(colors);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Color color)
        {
            if(!ModelState.IsValid) 
            return View("Edit",color);

            if(servicesColor.Save(color))
                return RedirectToAction("List");

            return RedirectToAction("List");

        }
        public IActionResult Delete(int? ColorId)
        {
            if(ColorId==null||ColorId==0)
                return NotFound();

            else
            {
               if(servicesColor.Delete((int)ColorId))
                    return RedirectToAction("List");

               else return NotFound();
            }
        }
    }
}
