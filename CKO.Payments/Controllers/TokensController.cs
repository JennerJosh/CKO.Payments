using CKO.Payments.BL.Exceptions.Tokens;
using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.Models.Merchants;
using CKO.Payments.Models.Tokens;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CKO.Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMerchantsService _merchantsService;

        public TokensController(ISecurityService securityService, IMerchantsService merchantsService)
        {
            _securityService = securityService;
            _merchantsService = merchantsService;
        }

        //// GET: api/<MerchantsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<MerchantsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<MerchantsController>
        [HttpPost]
        [Route("Generate")]
        public string Post([FromBody] GenerateAuthTokenModel model)
        {
            var merchant = _merchantsService.GetMerchantFromEmail(model.Email);

            if (merchant.IsSecretValid(model.Secret))
                return _securityService.GenerateAuthToken(merchant.Name, merchant.Email, model.Secret);

            throw new InvalidMerchantSecretException("Supplied secret is not valid, please check and try again");
        }

        //// PUT api/<MerchantsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MerchantsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
