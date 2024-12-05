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
    public class EnderecoUsuarioPersist : IEnderecoUsuarioPersist
    {

        private readonly InvestmentsContext _context;

        public EnderecoUsuarioPersist(InvestmentsContext context)
        {
            _context = context;
        }

        public async Task<EnderecoUsuario> GetAddressUser(string username)
        {
            return await _context.EnderecoUsuarios.Where(x=> x.User.UserName == username)
                                                  .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAddressUser(EnderecoUsuario enderecoUsuario)
        {
            await _context.AddAsync(enderecoUsuario);
            return await Task.FromResult(Convert.ToBoolean(await _context.SaveChangesAsync()));
        }

        public async Task<bool> UpdateAddressUser(EnderecoUsuario enderecoUsuario)
        {
            _context.Update(enderecoUsuario);
            return await Task.FromResult(Convert.ToBoolean(await _context.SaveChangesAsync()));
        }
    }
}