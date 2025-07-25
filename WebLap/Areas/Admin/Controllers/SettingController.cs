using Bl.IRepository;
using Domin.Entity;
using Domin.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly IServicesSetting servicesSetting;

        public SettingController(IServicesSetting servicesSetting)
        {
            this.servicesSetting = servicesSetting;
        }

        public IActionResult Edit()
        {
            var setting=servicesSetting.GetSetting();
           return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Setting Setting, List<IFormFile> Files)
        {
            //create
            if (Setting.Id == null || Setting.Id == 0)
            {
                if (Files.Count == 0 || Files == null)
                {
                    ModelState.AddModelError("Logo", ResourceData.LogoImg);
                }

                if (!ModelState.IsValid)
                {
                    return View("Edit", Setting);
                }
                else
                {
                    Setting.Logo = await Helper.UploadImage(Files, "LogoImg");
                    if (servicesSetting.Save(Setting))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            //update
            else
            {
                if (Files == null || Files.Count == 0)
                {
                    // الحصول على الصورة القديمة إذا لم يتم تحميل صورة جديدة
                    var oldSetting = servicesSetting.GetById(Setting.Id);
                    if (oldSetting != null && !string.IsNullOrEmpty(oldSetting.Logo))
                    {
                        Setting.Logo = oldSetting.Logo; // احتفظ بالصورة القديمة
                    }
                }
                else
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Uploads/Logo/" + Setting.Logo);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // تحميل صورة جديدة
                    Setting.Logo = await Helper.UploadImage(Files, "LogoImg");
                }

                if (servicesSetting.Save(Setting))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
