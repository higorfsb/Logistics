using Logistics.Domain.Dto.User;
using Logistics.Domain.Interfaces.Builders;
using System.Security.Claims;

namespace Logistics.Domain.Builders.User
{
    public class LoginResponseBuilder : ILoginResponseBuilder
    {
        public string BuildAccessToken(string accessToken)
        {
            return accessToken;
        }

        public UserTokenResponse BuildUserToken(IEnumerable<Claim> claims)
        {
            return new UserTokenResponse
            {
                Claims = claims.Select(c => new UserClaimsResponse { Type = c.Type, Value = c.Value })
            };
        }
    }
}
