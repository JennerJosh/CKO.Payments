using Microsoft.IdentityModel.Tokens;

namespace CKO.Payments.BL.Services.Interfaces
{
    public interface ISecurityService
    {
        string GenerateAuthToken(string merchantName, string merchantEmail, string merchantSecret);
        bool IsTokenValid(string token, out SecurityToken securityToken);
    }
}