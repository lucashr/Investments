using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Identity;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Persistence
{
    public class UserPersistMongoDb : RepositoryPersist, IUserPersistMongoDb
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ApplicationUser> _usersCollection;

        public UserPersistMongoDb(InvestmentsContext context = null,
                                  IMongoDatabase database = null) : base(context)
        {
            _mongoDatabase = database;
            _usersCollection = _mongoDatabase.GetCollection<ApplicationUser>("Users");
        }

        public async Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(user => user.Id, id.ToString());
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq(user => user.UserName, userName.ToLower());
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<IList<string>> GetRolesAsync(string idUser)
        {
            
            var ccc = _mongoDatabase.GetCollection<ApplicationUser>("Users");
            var UserRolesColl = _mongoDatabase.GetCollection<ApplicationUserRole>("UserRoles");
            List<ApplicationUser> users = await ccc.Find(_ => true).ToListAsync();
            List<ApplicationUserRole> userRolesss = await UserRolesColl.Find(_ => true).ToListAsync();
            // Buscar as associa��es de roles do usu�rio na cole��o ApplicationUserRole
            var filter = Builders<ApplicationUserRole>.Filter.Eq(ur => ur.UserId, idUser);
            var userRoles = await _mongoDatabase.GetCollection<ApplicationUserRole>("UserRoles")
                                                .Find(filter)
                                                .ToListAsync();

            // Obter os RoleIds de todos os documentos ApplicationUserRole associados ao usu�rio
            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            // Buscar as roles associadas a esses RoleIds na cole��o ApplicationRole
            var roleFilter = Builders<ApplicationRole>.Filter.In(role => role.Id, roleIds);
            var roles = await _mongoDatabase.GetCollection<ApplicationRole>("Roles")
                                            .Find(roleFilter)
                                            .ToListAsync();

            // Retornar os nomes das roles
            return roles.Select(role => role.Name).ToList();
        }

    }
}