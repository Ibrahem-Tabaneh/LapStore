using Bl.IRepository;
using Bl.ViewModel;
using Domin.Entity;
using Domin.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IServicesProduct servicesProduct;
        private readonly IServicesColor servicesColor;
        private readonly IServicesOs servicesOs;
        private readonly IServicesTypee servicesTypee;
        private readonly IServicesCategory servicesCategory;

        public ProductController(IServicesProduct servicesProduct
            ,IServicesColor servicesColor
            ,IServicesOs servicesOs 
            ,IServicesTypee servicesTypee
            ,IServicesCategory servicesCategory)
        {
            this.servicesProduct = servicesProduct;
            this.servicesColor = servicesColor;
            this.servicesOs = servicesOs;
            this.servicesTypee = servicesTypee;
            this.servicesCategory = servicesCategory;
        }

        public IActionResult Edit(int? ProductId)
        {
            ViewBag.lstCategories=servicesCategory.GetAll();
            ViewBag.lstItemTypes = servicesTypee.GetAll();
            ViewBag.lstOs = servicesOs.GetAll();
            ViewBag.lstcolors=servicesColor.GetAll();

            if (ProductId == null||ProductId==0)
            return View(new Product());
            else
            {
                var Product = servicesProduct.GetById((int)ProductId);
                return View(Product);
            }
        }
        public IActionResult List()
        {
            var Products = servicesProduct.GetAllProductData();
            return View(Products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Product Product, List<IFormFile> Files)
        {
            //create
            if (Product.Id == null || Product.Id == 0)
            {
                if (Files.Count == 0 || Files == null)
                {
                    ModelState.AddModelError("ProductImg", ResourceData.ProductImg);
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.lstCategories = servicesCategory.GetAll();
                    ViewBag.lstItemTypes = servicesTypee.GetAll();
                    ViewBag.lstOs = servicesOs.GetAll();
                    ViewBag.lstcolors = servicesColor.GetAll();
                    return View("Edit", Product);
                }
                else
                {
                    Product.ProductImg = await Helper.UploadImage(Files, "ProductImg");
                    if (servicesProduct.Save(Product))
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
                    var oldProduct = servicesProduct.GetById(Product.Id);
                    if (oldProduct != null && !string.IsNullOrEmpty(oldProduct.ProductImg))
                    {
                        Product.ProductImg = oldProduct.ProductImg; // احتفظ بالصورة القديمة
                    }
                }
                else
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Uploads/ProductImg/" + Product.ProductImg);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // تحميل صورة جديدة
                    Product.ProductImg = await Helper.UploadImage(Files, "ProductImg");
                }

                if (servicesProduct.Save(Product))
                {
                    return RedirectToAction("List");
                }
            }

            return RedirectToAction("List");
        }
        public IActionResult Delete(int? ProductId)
        {
            if(ProductId==null||ProductId==0)
                return NotFound();

            else
            {
               if(servicesProduct.Delete((int)ProductId))
                    return RedirectToAction("List");

               else return NotFound();
            }
        }
         
    }
}
