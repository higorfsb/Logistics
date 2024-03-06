using Logistics.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Builders
{
    public interface ILoginResponseBuilder
    {
        string BuildAccessToken(string accessToken);
        UserTokenResponse BuildUserToken(IEnumerable<Claim> claims);
    }
}
