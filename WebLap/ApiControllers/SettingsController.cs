using Bl.IRepository;
using Domin.Entity;
using Microsoft.AspNetCore.Mvc;
using WebLap.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebLap.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IServicesSetting servicesSetting;

        public SettingsController(IServicesSetting servicesSetting)
        {
            this.servicesSetting = servicesSetting;
        }
        // GET: api/<SettingController>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse=new ApiResponse();
            try
            {
                oApiResponse.Data = servicesSetting.GetSetting();
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";

                return oApiResponse;

            }catch (Exception ex)
            {
                oApiResponse.Data =null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "502";

                return oApiResponse;
            }
        }

        // GET api/<SettingController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = servicesSetting.GetById(id);
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

        // POST api/<SettingController>
        [HttpPost]
        public ApiResponse Post([FromBody] Setting setting)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                servicesSetting.Save(setting);

                oApiResponse.Data ="done";
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
