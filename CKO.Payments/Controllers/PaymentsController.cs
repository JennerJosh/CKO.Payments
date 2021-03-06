using CKO.Payments.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = CKO.Payments.Attributes.AuthorizeAttribute;
using Microsoft.AspNetCore.Http;
using CKO.Payments.Models.Response;
using System;
using CKO.Payments.Models.Payments;
using System.Threading.Tasks;
using CKO.Payments.BL.Models.Merchants;

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

        [HttpGet]
        public ResponseModel Get()
        {
            try
            {
                var merchant = (MerchantModel)HttpContext.Items["Merchant"];
                var transactions = _transactionsService.GetMerchantTransactions(merchant.Id);

                return ResponseModel.GetSuccessResponse(transactions);
            }
            catch (Exception exc)
            {
                return ResponseModel.GetErrorResponse(exc);
            }
        }

        [HttpPost]
        [Route("Intent")]
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
        [Route("Process")]
        public async Task<ResponseModel> ProcessPayment([FromBody] ProcessPaymentModel model)
        {
            try
            {
                var merchant = (MerchantModel)HttpContext.Items["Merchant"];

                if (model.IsSecretValid(merchant))
                {
                    var response = await _transactionsService.ProcessTransactionAsync(model.GetTransactionModel(merchant));
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
        [Route("Settle")]
        public async Task<ResponseModel> SettlePayment([FromBody] ProcessPaymentModel model)
        {
            try
            {
                var merchant = (MerchantModel)HttpContext.Items["Merchant"];

                if (model.IsSecretValid(merchant))
                {
                    var response = await _transactionsService.SettleTransactionAsync(model.GetTransactionModel(merchant));
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
