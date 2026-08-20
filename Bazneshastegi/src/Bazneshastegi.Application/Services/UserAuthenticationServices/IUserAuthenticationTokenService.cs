using System.Security.Claims;

namespace Bazneshastegi.Application.Services.UserAuthenticationServices;
public interface IUserAuthenticationTokenService
{
    PrimitiveResult<string> GenerateToken(string id);
    PrimitiveResult<ClaimsPrincipal> ValidateToken(string token);
}
