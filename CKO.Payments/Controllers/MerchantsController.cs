using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.Models.Merchants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CKO.Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IMerchantsService _merchantsService;

        public MerchantsController(IMerchantsService merchantsService)
        {
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
        public string Post([FromBody] RegisterMerchantModel model)
        {
            var newMerchant = new Merchant(model.Name, model.Email);
            newMerchant = _merchantsService.RegisterMerchant(newMerchant);

            return newMerchant.Secret;
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
