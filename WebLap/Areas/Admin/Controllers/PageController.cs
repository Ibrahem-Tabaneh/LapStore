using Bl.IRepository;
using Domin.Entity;
using Domin.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IServicesPage servicesPage;

        public PageController(IServicesPage servicesPage)
        {
            this.servicesPage = servicesPage;
        }

        public IActionResult Edit(int? PageId)
        {
            if(PageId == null||PageId==0)
            return View(new Page());
            else
            {
                var Page = servicesPage.GetById((int)PageId);
                return View(Page);
            }
        }
        public IActionResult List()
        {
            var Pages = servicesPage.GetAll();
            return View(Pages);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Page Page, List<IFormFile> Files)
        {
            //create
            if (Page.Id == null || Page.Id == 0)
            {
                if (Files.Count == 0 || Files == null)
                {
                    ModelState.AddModelError("PageImg", ResourceData.PageImg);
                }

                if (!ModelState.IsValid)
                {
                    return View("Edit", Page);
                }
                else
                {
                    Page.PageImg = await Helper.UploadImage(Files, "PageImg");
                    if (servicesPage.Save(Page))
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
                    var oldPage = servicesPage.GetById(Page.Id);
                    if (oldPage != null && !string.IsNullOrEmpty(oldPage.PageImg))
                    {
                        Page.PageImg = oldPage.PageImg; // احتفظ بالصورة القديمة
                    }
                }
                else
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Uploads/PageImg/" + Page.PageImg);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // تحميل صورة جديدة
                    Page.PageImg = await Helper.UploadImage(Files, "PageImg");
                }

                if (servicesPage.Save(Page))
                {
                    return RedirectToAction("List");
                }
            }

            return RedirectToAction("List");
        }
        public IActionResult Delete(int? PageId)
        {
            if(PageId==null||PageId==0)
                return NotFound();

            else
            {
               if(servicesPage.Delete((int)PageId))
                    return RedirectToAction("List");

               else return NotFound();
            }
        }
    }
}
