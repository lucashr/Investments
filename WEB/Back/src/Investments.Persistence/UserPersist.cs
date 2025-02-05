using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Identity;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Persistence
{
    public class UserPersist : RepositoryPersist, IUserPersist
    {
        public InvestmentsContext _context { get; }
        public IMongoDatabase _mongoDatabase { get; }

        public UserPersist(InvestmentsContext context = null,
                           IMongoDatabase database = null) : base(context)
        {
            _context = context;
            _mongoDatabase = database;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            
            if(_context is null)
            {
                var usersCollection = _mongoDatabase.GetCollection<User>("Users");
                return await usersCollection.Find(user => user.Id == id.ToString()).FirstOrDefaultAsync();
            }

            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            if (_context is null)
            {
                var usersCollection = _mongoDatabase.GetCollection<User>("Users");
                return await usersCollection.Find(user => user.UserName == userName.ToLower()).FirstOrDefaultAsync();
            }

            return await _context.Users
                                 .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            if (_context is null)
            {
                var usersCollection = _mongoDatabase.GetCollection<User>("Users");
                return await usersCollection.Find(_ => true).ToListAsync();
            }
            return await _context.Users.ToListAsync();
        }

    }
}