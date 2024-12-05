using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Identity;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class UserPersist : GeneralPersist, IUserPersist
    {
        public InvestmentsContext _context { get; }

        public UserPersist(InvestmentsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var ccc = _context.DetailedFunds.Count();
            var xxxxx = _context.Users.Count();
            var tttt = await _context.Users
                                 .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
            return await _context.Users
                                 .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

    }
}