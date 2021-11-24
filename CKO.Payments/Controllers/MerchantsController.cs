using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.Models.Merchants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = CKO.Payments.Attributes.AuthorizeAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CKO.Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MerchantsController : ControllerBase
    {
        private readonly IMerchantsService _merchantsService;

        public MerchantsController(IMerchantsService merchantsService)
        {
            _merchantsService = merchantsService;
        }

        // GET: api/<MerchantsController>
        [HttpGet]
        public Merchant Get()
        {
            var merchant = (Merchant)HttpContext.Items["Merchant"];
            return _merchantsService.GetMerchantFromEmail(merchant.Email);
        }

        //POST api/<MerchantsController>
        [HttpPost]
        [AllowAnonymous]
        public string Post([FromBody] RegisterMerchantModel model)
        {
            var newMerchant = new Merchant(model.Name, model.Email);
            newMerchant = _merchantsService.RegisterMerchant(newMerchant);

            return newMerchant.Secret;
        }
    }
}
