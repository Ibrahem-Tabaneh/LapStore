using Bl.IRepository;
using Domin.Entity;
using Microsoft.AspNetCore.Mvc;
using WebLap.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLap.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IServicesProduct servicesProduct;

        public ProductApiController(IServicesProduct servicesProduct)
        {
            this.servicesProduct = servicesProduct;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public ApiResponse GetAllProduct()
        {
            ApiResponse oApiResponse = new ApiResponse();
            oApiResponse.Data = servicesProduct.GetAll();
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";

            return oApiResponse;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ApiResponse GetById(int id)
        {
            ApiResponse oApiResponse=new ApiResponse();
            oApiResponse.Data = servicesProduct.GetById(id);
            oApiResponse.Errors = null;
            oApiResponse.StatusCode = "200";

            return oApiResponse;
        }

        // POST api/<ProductController>
        [HttpPost]
        public ApiResponse Save([FromBody] Product product)
        {
            ApiResponse oApiResponse = new ApiResponse();

            try
            {
                servicesProduct.Save(product);

                oApiResponse.Data = "done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";

                return oApiResponse;
            }
            catch (Exception ex)
            {

                oApiResponse.Data =null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";

                return oApiResponse;
            }
        }

    
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();

            try
            {
                servicesProduct.Delete(id);

                oApiResponse.Data = "done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";

                return oApiResponse;
            }
            catch (Exception ex)
            {

                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";

                return oApiResponse;
            }
            
        }

       
    }
}
