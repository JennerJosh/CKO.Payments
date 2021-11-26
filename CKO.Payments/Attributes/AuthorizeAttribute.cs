using CKO.Payments.BL.Models.Merchants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CKO.Payments.Attributes
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(x => x is IAllowAnonymous))
                return;

            var merchant = (MerchantModel)context.HttpContext.Items["Merchant"];
            if (merchant == null)
            {
                context.Result = new JsonResult(new { message = "User is not authorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
