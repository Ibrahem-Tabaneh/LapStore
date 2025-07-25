using Bl.IRepository;
using Microsoft.AspNetCore.Mvc;
using WebLap.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Domin.Entity;
using Microsoft.AspNetCore.Identity;
using Bl.ViewModel;
namespace WebLap.Controllers
{
    public class OrderController : Controller
    {
        public IServicesProduct ServicesProduct { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public IServicesOrder ServicesOrder { get; }
        public IServicesOrderItem ServicesOrderItem { get; }

        public OrderController(IServicesProduct servicesProduct,
            UserManager <ApplicationUser> userManager,
            IServicesOrder servicesOrder,
             IServicesOrderItem servicesOrderItem)
        {
            ServicesProduct = servicesProduct;
            UserManager = userManager;
            ServicesOrder = servicesOrder;
            ServicesOrderItem = servicesOrderItem;
        }
        public IActionResult Cart()
        {
            ShoppingCart cart = new ShoppingCart();
            if (HttpContext.Request.Cookies["Cart"]!= null)
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
                cart.Total=cart.LstItems.Sum(x => x.Total);

                return View(cart);

            }

            else
            {
                return RedirectToAction("EmptyCart");
            }

        }

        public IActionResult EmptyCart()
        {
            return View();
        }

        public IActionResult AddToCart(int? ProductId)
        {
            var product = ServicesProduct.GetVwProductById((int)ProductId);
            ShoppingCart cart;
            
            var sessionCart = HttpContext.Request.Cookies["Cart"];

            if (string.IsNullOrEmpty(sessionCart))
            {
                cart = new ShoppingCart();
            }
            else
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
            }

           var ItemInList=cart.LstItems.Where(x=>x.ProductId==ProductId).FirstOrDefault();
            
            if(ItemInList!=null)
            {
                ItemInList.Qty++;
                ItemInList.Total+= ItemInList.ProductPrice;
            }
            else
            {
                cart.LstItems.Add(new ShoppingCartItem
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductImg = product.ProductImg,
                    Qty = 1,
                    Total = product.ProductPrice
                });
            }
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Cart");
        }
    
        public IActionResult RemoveFromCart(int? ProductId)
        {
            ShoppingCart cart;
            var sessionCart = HttpContext.Request.Cookies["Cart"];

            cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
           
           var delProduct= cart.LstItems.Where(x=>x.ProductId== ProductId).FirstOrDefault();
            cart.LstItems.Remove(delProduct);

            if (cart.LstItems.Count == 0)
            {
                // حذف الكوكيز إذا كانت السلة فارغة
                HttpContext.Response.Cookies.Delete("Cart");
                return RedirectToAction("EmptyCart");
            }
            else
            {
                HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            }



            return RedirectToAction("Cart");
        }

     
        [Authorize]
        public async Task<IActionResult> OrderSuccess()
        {
            var sessionCart = HttpContext.Request.Cookies["Cart"];
            ShoppingCart cart= new ShoppingCart();

            if(sessionCart != null)
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
                await SaveOrder(cart);

                // حذف الكوكيز بعد تنفيذ الطلب
                HttpContext.Response.Cookies.Delete("Cart");

                return RedirectToAction("MyOrder");

            }
            return NotFound();
        }

        [Authorize]
        public async Task SaveOrder(ShoppingCart shoppingCart)
        {
            Order order = new Order();
            List<OrderItem> items = new List<OrderItem>();
            var user=await UserManager.GetUserAsync(User);
           
            if(shoppingCart != null)
            {
                //order
                order.StartDate=DateTime.Now;
                order.EndDate=DateTime.Now.AddDays(7);
                order.TotalPrice = shoppingCart.LstItems.Sum(x=>x.Total);
                order.CustomerId= user.Id;

                //orderitems
                foreach (var item in shoppingCart.LstItems)
                {
                    items.Add(new OrderItem
                    {
                        ProductImg = item.ProductImg,
                        ProductName = item.ProductName,
                        ProductPrice = item.ProductPrice,
                        Qty = item.Qty,
                        ProductId = item.ProductId,
                        
                    });
                }

              await  ServicesOrder.Save(order,items);
            }


        }

        [Authorize]
        public async Task<IActionResult> MyOrder()
        {


            var userId = UserManager.GetUserId(User); // أو: User.FindFirstValue(ClaimTypes.NameIdentifier)

            var orders =ServicesOrder.GetAll(userId);
            List<OrderViewModel> list = new List<OrderViewModel>();
            if (orders.Count > 0)
            {


                
                var user = await UserManager.FindByIdAsync(userId);


                foreach (var order in orders)
                {
                    list.Add(new OrderViewModel
                    {
                        OrderId = order.Id,
                        OrderDate = order.StartDate,
                        EndDate = order.EndDate,
                        TotalPrice = order.TotalPrice,
                        FirstName = user.Fname,
                        LastName = user.Lname,
                    });


                }

                return View(list);
            }
            else
            {
                return View(list);
            }
        }

        [Authorize]
        public IActionResult DetailsOrder(int id)
        {
            if(id != 0)
            {
                var items = new List<OrderItem>();
                items= ServicesOrderItem.GetAllById(id);

                return View(items);
            }

            return NotFound();
        }
    }
}
