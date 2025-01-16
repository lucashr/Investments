using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class UserAddressPersist : IUserAddressPersist
    {

        private readonly InvestmentsContext _context;

        public UserAddressPersist(InvestmentsContext context)
        {
            _context = context;
        }

        public async Task<UserAddress> GetAddressUser(string username)
        {
            return await _context.UserAddresses.Where(x=> x.User.UserName == username)
                                                .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAddressUser(UserAddress enderecoUsuario)
        {
            await _context.AddAsync(enderecoUsuario);
            return await Task.FromResult(Convert.ToBoolean(await _context.SaveChangesAsync()));
        }

        public async Task<bool> UpdateAddressUser(UserAddress enderecoUsuario)
        {
            _context.Update(enderecoUsuario);
            return await Task.FromResult(Convert.ToBoolean(await _context.SaveChangesAsync()));
        }
    }
}