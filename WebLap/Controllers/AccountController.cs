using Bl.ViewModel;
using Domin.Entity;
using Domin.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebLap.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager
            ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

       
        public IActionResult Login(string? returnUrl)
        {
            LoginViewModel vm = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           var isUser= await userManager.FindByEmailAsync(model.Email);
           if (isUser == null) 
           ModelState.AddModelError("Email", ResourceData.EmailNotExist);

           if (!ModelState.IsValid)
            {
                return View("Login",model);
            }

            var result = await signInManager.PasswordSignInAsync(model.Email,model.Password,true,true);
            if (result.Succeeded)
            {
                return string.IsNullOrEmpty(model.ReturnUrl) ? Redirect("~/") : Redirect("/Order/OrderSuccess");
            }
            else
            {
                ModelState.AddModelError("Password", "Invalid password.");
            }

            return View("Login",model);
        }
        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(RegisterViewModel model)
        {
           if(!ModelState.IsValid) { return View("Register", model); }

            var user = new ApplicationUser
            {
                Email = model.Email,
                Fname = model.Fname,
                Lname = model.Lname,
                UserName = model.Email
            };

            var result=await userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                var result2 = await userManager.AddToRoleAsync(user,Helper.Roles.Customer.ToString());
                if(result2.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return RedirectToAction("Register");
        }
        
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/");
        }
     
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
