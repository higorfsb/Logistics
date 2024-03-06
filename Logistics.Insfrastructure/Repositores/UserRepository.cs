using Logistics.Domain.Entities;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Insfrastructure.Data;
using Logistics.Insfrastructure.Helpers.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Insfrastructure.Repositores
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LogisticsContext context) : base(context)
        {
        }

        public async Task<User> ValidateLoginAsync(string userName, string password)
        {
            return await _context.Users
                .Where(x => x.Password == Security.EncryptString(password) &&
                x.UserName == userName).FirstOrDefaultAsync();
        }
    }
}
