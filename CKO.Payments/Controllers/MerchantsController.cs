using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.Models.Merchants;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = CKO.Payments.Attributes.AuthorizeAttribute;
using CKO.Payments.BL.Exceptions.Merchants;
using Microsoft.AspNetCore.Http;
using CKO.Payments.Models.Response;
using System;
using Microsoft.AspNetCore.Authorization;

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
        public ResponseModel Get()
        {
            try
            {
                var merchant = (MerchantModel)HttpContext.Items["Merchant"];
                var model = _merchantsService.GetMerchantFromEmail(merchant.Email);

                // For security we don't want to reveal Merchant Secret
                // Last 4 characters of the Secret will be revealed
                // This is to allow Merchant to confirm similiarity to Secret they have on file
                model.MaskSecret();

                return ResponseModel.GetSuccessResponse(model);
            }
            catch (Exception exc)
            {
                return ResponseModel.GetErrorResponse(exc);
            }
        }

        //POST api/<MerchantsController> 
        [HttpPost]
        [AllowAnonymous]
        public ResponseModel Post([FromBody] RegisterMerchantModel model)
        {
            try
            {
                var newMerchant = new MerchantModel(model.Name, model.Email);
                newMerchant = _merchantsService.RegisterMerchant(newMerchant);

                // Return Merchant Secret for user to store
                return ResponseModel.GetSuccessResponse(newMerchant.Secret);
            }
            catch (Exception exc)
            {
                return ResponseModel.GetErrorResponse(exc);
            }
        }
    }
}
