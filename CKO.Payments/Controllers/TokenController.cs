using CKO.Payments.BL.Services.Interfaces;
using CKO.Payments.Models.Response;
using CKO.Payments.Models.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CKO.Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMerchantsService _merchantsService;

        public TokenController(ISecurityService securityService, IMerchantsService merchantsService)
        {
            _securityService = securityService;
            _merchantsService = merchantsService;
        }

        [HttpPost]
        [Route("Generate")]
        public ResponseModel Post([FromBody] GenerateAuthTokenModel model)
        {
            try
            {
                var merchant = _merchantsService.GetMerchantFromEmail(model.Email);

                if (merchant.IsSecretValid(model.Secret))
                {
                    var token = _securityService.GenerateAuthToken(merchant.Id, merchant.Name, merchant.Email, merchant.Secret);
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
