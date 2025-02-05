// using System;
// using System.Threading.Tasks;

// namespace Investments.Persistence.Contracts
// {
//     public interface IGeneralPersist : IRepository
//     {
//         void Add<T>(T entity) where T: class;
//         void AddRange<T>(T[] entity) where T: class;
//         void Update<T>(T entity) where T: class;
//         void Delete<T>(T entity) where T: class;
//         void DeleteRange<T>(T[] entity) where T: class;
//         Task<bool> SaveChangesAsync();
//     }
// }