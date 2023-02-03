using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Investments.Persistence
{
    public class GeneralPersist : IGeneralPersist
    {

        private readonly InvestmentsContext _context;

        public GeneralPersist(InvestmentsContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void DetachLocal<T>(Func<T, bool> predicate) where T : class
        {

            var local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();

            if(local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

        }

        public void AddRange<T>(T[] entity) where T : class
        {
            foreach (var item in entity)
            {
                _context.Add(item);
            }
            
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.AddRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}