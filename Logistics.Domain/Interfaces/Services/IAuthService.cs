using Logistics.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string userName, string password);
    }
}
