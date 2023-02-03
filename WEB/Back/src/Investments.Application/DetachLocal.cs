using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Application.Contracts;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Application
{
    public class DetachLocal
    {

        private readonly InvestmentsContext _context;

        public DetachLocal(InvestmentsContext context)
        {
            _context = context;
        }

        public virtual void Detach<T>(Func<T, bool> predicate) where T : class
        {

            dynamic local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();

            if(!local.IsNull())
            {
                _context.Entry(local).State = EntityState.Detached;
            }

        }
        
    }
}