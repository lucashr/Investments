using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investments.Domain.Models;
using Investments.Persistence.Contexts;
using Investments.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Investments.Persistence
{
    public class RepositoryPersist : IRepositoryPersist
    {
        private readonly InvestmentsContext _context;
        private readonly IMongoDatabase _mongoDatabase;

        public RepositoryPersist(InvestmentsContext context = null, IMongoDatabase mongoDatabase = null)
        {
            _context = context;
            _mongoDatabase = mongoDatabase;
        }

        // Adiciona uma entidade no banco
        public void Add<T>(T entity) where T : class
        {
            if (_context != null) // SQLite
            {
                _context.Add(entity);
            }
            else if (_mongoDatabase != null) // MongoDB
            {
                var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
                collection.InsertOne(entity);
            }
            else
            {
                throw new InvalidOperationException("No database context available.");
            }
        }

        // Adiciona uma lista de entidades no banco
        public void AddRange<T>(T[] entity) where T : class
        {
            if (_context != null) // SQLite
            {
                foreach (var item in entity)
                {
                    _context.Add(item);
                }
            }
            else if (_mongoDatabase != null) // MongoDB
            {
                var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
                collection.InsertMany(entity);
            }
            else
            {
                throw new InvalidOperationException("No database context available.");
            }
        }

        // Remove uma entidade do banco
        public void Delete<T>(T entity) where T : class
        {
            if (_context != null) // SQLite
            {
                _context.Remove(entity);
            }
            else if (_mongoDatabase != null) // MongoDB
            {
                var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
                collection.DeleteOne(Builders<T>.Filter.Eq("Id", (entity as dynamic).Id));
            }
            else
            {
                throw new InvalidOperationException("No database context available.");
            }
        }

        // Remove uma lista de entidades do banco
        public void DeleteRange<T>(T[] entity) where T : class
        {
            if (_context != null) // SQLite
            {
                _context.RemoveRange(entity);
            }
            else if (_mongoDatabase != null) // MongoDB
            {
                var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
                var ids = entity.Select(e => (e as dynamic).Id).ToList();
                collection.DeleteMany(Builders<T>.Filter.In("Id", ids));
            }
            else
            {
                throw new InvalidOperationException("No database context available.");
            }
        }

        // Salva as alterações no banco
        public async Task<bool> SaveChangesAsync()
        {
            if (_context != null) // SQLite
            {
                return (await _context.SaveChangesAsync()) > 0;
            }
            else if (_mongoDatabase != null) // MongoDB
            {
                // MongoDB não precisa de uma operação SaveChanges, pois é feito de forma independente
                return true;
            }
            else
            {
                throw new InvalidOperationException("No database context available.");
            }
        }

        // Atualiza uma entidade no banco
        public void Update<T>(T entity) where T : class
        {
            if (_context != null) // SQLite
            {
                _context.Update(entity);
            }
            else if (_mongoDatabase != null) // MongoDB
            {
                var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
                collection.ReplaceOne(Builders<T>.Filter.Eq("Id", (entity as dynamic).Id), entity);
            }
            else
            {
                throw new InvalidOperationException("No database context available.");
            }
        }
    }
}
