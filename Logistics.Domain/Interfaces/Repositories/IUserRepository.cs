using Logistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> ValidateLoginAsync(string userName, string password);
    }
}
