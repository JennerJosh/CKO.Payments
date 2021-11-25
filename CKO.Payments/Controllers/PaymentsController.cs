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
using CKO.Payments.Models.Payments;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CKO.Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public PaymentsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        // GET: api/<MerchantsController>
        //[HttpGet]
        //public ResponseModel Get()
        //{
        //    try
        //    {
        //        var merchant = (MerchantModel)HttpContext.Items["Merchant"];
        //        var model = _merchantsService.GetMerchantFromEmail(merchant.Email);

        //        // For security we don't want to reveal Merchant Secret
        //        // Last 4 characters of the Secret will be revealed
        //        // This is to allow Merchant to confirm similiarity to Secret they have on file
        //        model.MaskSecret();

        //        return ResponseModel.GetSuccessResponse(model);
        //    }
        //    catch (Exception exc)
        //    {
        //        return ResponseModel.GetErrorResponse(exc);
        //    }
        //}

        //POST api/<MerchantsController> 
        [HttpPost]
        [Route("intent")]
        public ResponseModel PaymentIntent([FromBody] PaymentIntentModel model)
        {
            try
            {
                var merchant = (MerchantModel)HttpContext.Items["Merchant"];

                if (model.IsSecretValid(merchant))
                {
                    var transaction = _transactionsService.AddTransactionStub(model.GetTransactionModel(merchant));
                    model.SetTransactionId(transaction.Id);

                    return ResponseModel.GetSuccessResponse(model);
                }
                else
                {
                    return ResponseModel.GetErrorResponse(StatusCodes.Status400BadRequest, "Invalid Merchant secret");
                }
            }
            catch (Exception exc)
            {
                return ResponseModel.GetErrorResponse(exc);
            }
        }
        
        [HttpPost]
        [Route("process")]
        public ResponseModel ProcessPayment([FromBody] ProcessPaymentModel model)
        {
            try
            {
                var merchant = (MerchantModel)HttpContext.Items["Merchant"];

                if (model.IsSecretValid(merchant))
                {
                    var response = _transactionsService.ProcessTransaction(model.GetTransactionModel(merchant));
                    return ResponseModel.GetSuccessResponse(response);
                }
                else
                {
                    return ResponseModel.GetErrorResponse(StatusCodes.Status400BadRequest, "Invalid request, please check and try again");
                }
            }
            catch (Exception exc)
            {
                return ResponseModel.GetErrorResponse(exc);
            }
        }

        [HttpPost]
        [Route("settle")]
        public ResponseModel SettlePayment([FromBody] ProcessPaymentModel model)
        {
            try
            {
                var merchant = (MerchantModel)HttpContext.Items["Merchant"];

                if (model.IsSecretValid(merchant))
                {
                    var response = _transactionsService.ProcessTransaction(model.GetTransactionModel(merchant));
                    return ResponseModel.GetSuccessResponse(response);
                }
                else
                {
                    return ResponseModel.GetErrorResponse(StatusCodes.Status400BadRequest, "Invalid request, please check and try again");
                }
            }
            catch (Exception exc)
            {
                return ResponseModel.GetErrorResponse(exc);
            }
        }
    }
}
