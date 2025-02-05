using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Persistence.Contracts
{
    public interface IRepositoryPersistFactory
    {
        T CreateRepository<T>() where T : class, IRepositoryPersist;
    }
}