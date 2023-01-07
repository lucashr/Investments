using System.Threading.Tasks;
using Investments.Test.Persistence.Contexts;
using Investments.Test.Persistence.Contracts;

namespace Investments.Test.Persistence
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

        public void AddRange<T>(T[] entity) where T : class
        {
            _context.AddRange(entity);
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