using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using CKO.Payments.BL.Services.Interfaces;

namespace CKO.Payments.BL.Services
{
    public class SecurityService : ISecurityService
    {
        public string GenerateAuthToken(string merchantName, string merchantEmail, string merchantSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = new SymmetricSecurityKey(Encoding.Default.GetBytes(merchantSecret));
            var header = new JwtHeader(new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest));
            var payload = new JwtPayload();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, merchantName),
                new Claim(ClaimTypes.Email, merchantEmail),
                new Claim(ClaimTypes.Expiration, TimeSpan.FromDays(1).ToString(), ClaimValueTypes.DaytimeDuration),
            };

            payload.AddClaims(claims);

            var token = new JwtSecurityToken(header, payload);
            return tokenHandler.WriteToken(token);
        }

        public bool IsTokenValid(string token, string merchantSecret)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(merchantSecret));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
