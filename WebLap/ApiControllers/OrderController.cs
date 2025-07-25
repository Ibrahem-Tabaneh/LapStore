using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebLap.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLap.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       
        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] UpdateCartRequest req)
        {
            ShoppingCart cart;
            var sessionCart = HttpContext.Request.Cookies["Cart"];


            cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);

            var UpdateProduct = cart.LstItems.Where(x => x.ProductId == req.ProductId).FirstOrDefault();
            UpdateProduct.Qty=req.Qty;
            UpdateProduct.Total=UpdateProduct.ProductPrice*req.Qty;

            cart.Total = cart.LstItems.Sum(x => x.Total);


            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

        }

    }
}
