using Bl.IRepository;
using Domin.Entity;
using Domin.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IServicesSlider servicesSlider;

        public SliderController(IServicesSlider servicesSlider)
        {
            this.servicesSlider = servicesSlider;
        }

        public IActionResult Edit(int? SliderId)
        {
            if(SliderId == null||SliderId==0)
            return View(new Slider());
            else
            {
                var Slider = servicesSlider.GetById((int)SliderId);
                return View(Slider);
            }
        }
        public IActionResult List()
        {
            var Sliders = servicesSlider.GetAll();
            return View(Sliders);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Slider Slider, List<IFormFile> Files)
        {
            //create
            if (Slider.Id == null || Slider.Id == 0)
            {
                if (Files.Count == 0 || Files == null)
                {
                    ModelState.AddModelError("SliderImg", ResourceData.SliderImg);
                }

                if (!ModelState.IsValid)
                {
                    return View("Edit", Slider);
                }
                else
                {
                    Slider.SliderImg = await Helper.UploadImage(Files, "SliderImg");
                    if (servicesSlider.Save(Slider))
                    {
                        return RedirectToAction("List");
                    }
                    return RedirectToAction("List");
                }
            }

            //update
            else
            {
                if (Files == null || Files.Count == 0)
                {
                    // الحصول على الصورة القديمة إذا لم يتم تحميل صورة جديدة
                    var oldslider = servicesSlider.GetById(Slider.Id);
                    if (oldslider != null && !string.IsNullOrEmpty(oldslider.SliderImg))
                    {
                        Slider.SliderImg = oldslider.SliderImg; // احتفظ بالصورة القديمة
                    }
                }
                else
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Uploads/SliderImg/" + Slider.SliderImg);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // تحميل صورة جديدة
                    Slider.SliderImg = await Helper.UploadImage(Files, "SliderImg");
                }

                if (servicesSlider.Save(Slider))
                {
                    return RedirectToAction("List");
                }
            }

            return RedirectToAction("List");
        }
        public IActionResult Delete(int? SliderId)
        {
            if(SliderId==null||SliderId==0)
                return NotFound();

            else
            {
               if(servicesSlider.Delete((int)SliderId))
                    return RedirectToAction("List");

               else return NotFound();
            }
        }
    }
}
