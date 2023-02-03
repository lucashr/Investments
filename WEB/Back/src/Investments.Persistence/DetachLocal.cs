using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class DetachLocal : IDetachLocal
    {

        private readonly InvestmentsContext _context;

        public DetachLocal(InvestmentsContext context)
        {
            _context = context;
        }
        
        void IDetachLocal.DetachLocal<T>(Func<T, bool> predicate)
        {
            dynamic local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();

            if(!local.IsNull())
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}