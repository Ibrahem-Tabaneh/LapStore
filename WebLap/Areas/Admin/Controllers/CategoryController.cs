using Bl.IRepository;
using Domin.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IServicesCategory servicesCategory;

        public CategoryController(IServicesCategory servicesCategory)
        {
            this.servicesCategory = servicesCategory;
        }

        public IActionResult Edit(int? CategoryId)
        {
            if(CategoryId == null||CategoryId==0)
            return View(new Category());
            else
            {
                var Category = servicesCategory.GetById((int)CategoryId);
                return View(Category);
            }
        }
        public IActionResult List()
        {
            var Categorys = servicesCategory.GetAll();
            return View(Categorys);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Category Category)
        {
            if(!ModelState.IsValid) 
            return View("Edit",Category);

            if(servicesCategory.Save(Category))
                return RedirectToAction("List");

            return RedirectToAction("List");

        }
        public IActionResult Delete(int? CategoryId)
        {
            if(CategoryId==null||CategoryId==0)
                return NotFound();

            else
            {
               if(servicesCategory.Delete((int)CategoryId))
                    return RedirectToAction("List");

               else return NotFound();
            }
        }
    }
}
