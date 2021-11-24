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
                new Claim("exp", DateTimeOffset.Now.AddHours(12).ToUnixTimeSeconds().ToString()),
                new Claim("iat", DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
            };

            payload.AddClaims(claims);

            var token = new JwtSecurityToken(header, payload);
            return tokenHandler.WriteToken(token);
        }

        public bool IsTokenValid(string token, string merchantSecret, out SecurityToken securityToken)
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
                    IssuerSigningKey = securityKey,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                securityToken = validatedToken;
            }
            catch
            {
                securityToken = null;
                return false;
            }

            return true;
        }
    }
}
