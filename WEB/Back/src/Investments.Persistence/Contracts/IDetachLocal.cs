using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Persistence.Contracts
{
    public interface IDetachLocal
    {
        virtual void DetachLocal<T>(Func<T, bool> predicate) where T : class{}
    }
}