using Bl.IRepository;
using Bl.IRepository.ServicesRepository;
using Bl.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServicesProduct servicesProduct;

        public ProductController(IServicesProduct servicesProduct)
        {
            this.servicesProduct = servicesProduct;
        }

        public IActionResult ProductDetails(int ProductId)
        {
            VwProductDetails vwProductDetails = new VwProductDetails();
            vwProductDetails.Product = servicesProduct.GetVwProductById((int)ProductId);
            vwProductDetails.RelatedItems = servicesProduct.RealtedItems((int)ProductId);
            return View(vwProductDetails);
        }

    }
}
