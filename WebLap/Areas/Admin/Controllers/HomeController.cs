using Bl.IRepository;
using Bl.IRepository.ServicesRepository;
using Bl.Migrations;
using Bl.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebLap.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IServicesProduct servicesProduct;
        private readonly IServicesCategory servicesCategory;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IServicesUser servicesUser;

        public HomeController(IServicesProduct servicesProduct,
            IServicesCategory servicesCategory,
            UserManager<ApplicationUser> userManager,
            IServicesUser servicesUser
            )
        {
            this.servicesProduct = servicesProduct;
            this.servicesCategory = servicesCategory;
            this.userManager = userManager;
            this.servicesUser = servicesUser;
        }


        public IActionResult Index()
        {
            AdminPageViewModel adminPage = new AdminPageViewModel();

            adminPage.CountProduct = servicesProduct.Count();
            adminPage.CountCategory = servicesCategory.Count();
            adminPage.CountUsers = userManager.Users.Count();
            adminPage.users=servicesUser.GetUsers();

           
            

            return View(adminPage);
        }
    }
}
