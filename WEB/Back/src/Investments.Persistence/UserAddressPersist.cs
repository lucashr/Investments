using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Persistence
{
    public class UserAddressPersist : RepositoryPersist, IUserAddressPersist
    {

        private readonly InvestmentsContext _context;
        private readonly IMongoCollection<UserAddress> _userAddressCollection;

        public UserAddressPersist(InvestmentsContext context = null, 
                                  IMongoDatabase database = null): base(context)
        {
            _context = context;
            _userAddressCollection = database?.GetCollection<UserAddress>("UserAddresses");
        }

        public async Task<UserAddress> GetAddressUser(string username)
        {
            if(_context != null)
            {
                return await _context.UserAddresses.Where(x => x.User.UserName == username)
                                                .AsNoTracking().FirstOrDefaultAsync();
            }
            else if (_userAddressCollection != null)
            {
                return await _userAddressCollection.Find(x => x.User.UserName == username)
                                                .FirstOrDefaultAsync();
            }

            return new UserAddress();
        }

        public async Task<bool> SaveAddressUser(UserAddress enderecoUsuario)
        {
            if (_context != null)
            {
                await _context.AddAsync(enderecoUsuario);
                return await Task.FromResult(Convert.ToBoolean(await _context.SaveChangesAsync()));
            }
            else if (_userAddressCollection != null)
            {
                await _userAddressCollection.InsertOneAsync(enderecoUsuario);
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAddressUser(UserAddress enderecoUsuario)
        {
            if (_context != null) {
                _context.Update(enderecoUsuario);
                return await Task.FromResult(Convert.ToBoolean(await _context.SaveChangesAsync()));
            }
            else if (_userAddressCollection != null)
            {
                var filter = Builders<UserAddress>.Filter.Eq(x => x.User.UserName, enderecoUsuario.User.UserName);
                await _userAddressCollection.ReplaceOneAsync(filter, enderecoUsuario);
                return true;
            }

            return false;

        }
    }
}