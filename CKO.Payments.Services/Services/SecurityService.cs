using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using CKO.Payments.BL.Services.Interfaces;

namespace CKO.Payments.BL.Services
{
    public class SecurityService : ISecurityService
    {
        private const string TOKEN_SECRET = "X]TDHxkMM$`t^c/tC`l[f}3uie,QD,7}}rG]*M2bZZLKuT+6V`D?P]Sh$JV^5*q";
        private const int TOKEN_LIFETIME = 24;

        /// <summary>
        /// Generate new JWT token for users to authenticate requests into Gateway
        /// </summary>
        /// <param name="merchantName">Name of the Merchant</param>
        /// <param name="merchantEmail">Email of the Merchant</param>
        /// <returns>Returns a new token string</returns>
        public string GenerateAuthToken(string merchantName, string merchantEmail, string merchantSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = new SymmetricSecurityKey(Encoding.Default.GetBytes(TOKEN_SECRET));
            var header = new JwtHeader(new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest));
            var payload = new JwtPayload();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, merchantSecret),
                new Claim(ClaimTypes.Name, merchantName),
                new Claim(ClaimTypes.Email, merchantEmail),
                new Claim("exp", DateTimeOffset.Now.AddHours(TOKEN_LIFETIME).ToUnixTimeSeconds().ToString()),
                new Claim("iat", DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
            };

            payload.AddClaims(claims);

            var token = new JwtSecurityToken(header, payload);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Validates whether a supplied token is valid
        /// </summary>
        /// <param name="token">JWT token that was previously generated</param>
        /// <param name="securityToken">Validated Security token containing claims</param>
        /// <returns></returns>
        public bool IsTokenValid(string token, out SecurityToken securityToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(TOKEN_SECRET));
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
