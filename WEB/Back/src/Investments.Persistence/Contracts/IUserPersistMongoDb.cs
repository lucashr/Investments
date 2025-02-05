using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain;
using Investments.Domain.Identity;

namespace Investments.Persistence.Contracts
{
    public interface IUserPersistMongoDb : IRepositoryPersist
    {
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(int id);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<IList<string>> GetRolesAsync(string idUser);
    }
}