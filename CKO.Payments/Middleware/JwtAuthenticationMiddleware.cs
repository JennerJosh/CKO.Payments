using CKO.Payments.BL.Models;
using CKO.Payments.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CKO.Payments.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;

        public JwtAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration, ISecurityService securityService)
        {
            _next = next;
            _configuration = configuration;
            _securityService = securityService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["x-api-token"].FirstOrDefault();
        
            if (token != null)
                ValidateAndAddToContext(context, token);

            await _next(context);
        }

        private void ValidateAndAddToContext(HttpContext context, string token)
        {
            try
            {
                if (_securityService.IsTokenValid(token, out SecurityToken securityToken))
                {
                    var jwtToken = (JwtSecurityToken)securityToken;

                    var name = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                    var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                    context.Items["Merchant"] = new Merchant(name, email);
                }

            }
            catch
            {
                // If error occurs during validation we can assume token is not valid
            }
        }
    }
}
