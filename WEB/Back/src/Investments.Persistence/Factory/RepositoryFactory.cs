// using System;
// using Investments.Persistence;
// using Investments.Persistence.Contexts;
// using Investments.Persistence.Contracts;
// using MongoDB.Driver;

// public class RepositoryPersistFactory : IRepositoryPersistFactory
// {
//     private readonly InvestmentsContext _context;
//     private readonly IMongoDatabase _mongoDatabase;

//     public RepositoryPersistFactory(InvestmentsContext context = null, IMongoDatabase mongoDatabase = null)
//     {
//         _context = context;
//         _mongoDatabase = mongoDatabase;
//     }

//     public T CreateRepository<T>() where T : class, IRepositoryPersist
//     {
//         if (typeof(T) == typeof(DetailedFundPersist))
//         {
//             return new DetailedFundPersist(_context, _mongoDatabase) as T;
//         }
//         if (typeof(T) == typeof(DetailedStocksPersist))
//         {
//             return new DetailedStocksPersist(_context) as T;
//         }
//         // Adicione mais repositórios conforme necessário...

//         throw new InvalidOperationException($"Repository of type {typeof(T).Name} is not registered.");
//     }

// }
