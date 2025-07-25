using Bl.IRepository;
using Bl.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebLap.Models;

namespace WebLap.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesSlider servicesSlider;
        private readonly IServicesProduct servicesProduct;
        private readonly IServicesPage servicesPage;

        public HomeController(IServicesSlider servicesSlider,
            IServicesProduct servicesProduct
            ,IServicesPage servicesPage)
        {
            this.servicesSlider = servicesSlider;
            this.servicesProduct = servicesProduct;
            this.servicesPage = servicesPage;
        }

        public IActionResult Index()
        {
            HomePage homePage = new HomePage();
            homePage.VwProducts=servicesProduct.GetAllProductData();
            homePage.NewVwProducts=servicesProduct.NewProductData();
            homePage.DeliveryVwProducts=servicesProduct.DeliverProductData();
            homePage.Sliders = servicesSlider.GetAll();
            return View(homePage);
        }

     
        public IActionResult Page(int? pageId)
        {
            var Page = servicesPage.GetById((int)pageId);
            return View(Page);
        }
    }
}
