using CKO.Payments.BL.Exceptions.Tokens;
using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.Models.Merchants;
using CKO.Payments.Models.Response;
using CKO.Payments.Models.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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

        // POST api/<MerchantsController>
        [HttpPost]
        [Route("Generate")]
        public ResponseModel Post([FromBody] GenerateAuthTokenModel model)
        {
            try
            {
                var merchant = _merchantsService.GetMerchantFromEmail(model.Email);

                if (merchant.IsSecretValid(model.Secret))
                {
                    var token = _securityService.GenerateAuthToken(merchant.Name, merchant.Email);
                    return ResponseModel.GetSuccessResponse(token);
                }
                else
                {
                    return ResponseModel.GetErrorResponse(StatusCodes.Status400BadRequest, "Supplied secret is not valid, please check and try again");
                }
            }
            catch (Exception exc)
            {
                return ResponseModel.GetErrorResponse(exc);
            }
        }
    }
}
