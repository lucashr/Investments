using Investments.Domain;
using Investments.Domain.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Persistence.Contexts
{
    public static class DatabaseSeeder
    {

        private static IMongoDatabase _database;
        private static IMongoClient _mongoClient;

        private static List<string> collectionsMongoDb = new List<string>
        { 
            "DetailedFunds", "FundDividends", "BestFundRanks", "UserAddresses",
            "DetailedStocks", "StockDividends", "BestStockRanks", "Users"
        };

        public static async Task SeedAsync(IMongoDatabase database, IMongoClient mongoClient)
        {

            _database = database;
            _mongoClient = mongoClient;

            if(await DatabaseExists())
                return;
            
            CreateCollections();
            CreateIndexs();

            var users = await SeedUsersAsync();
            await SeedRolesAsync(users);

        }

        private async static Task<bool> DatabaseExists()
        {

            var database = _mongoClient.GetDatabase("InvestmentsDb");
            var collections = database.ListCollectionNames().ToList();

            if(!collections.Contains("InvestmentsDb"))
            {
                _database.CreateCollection("InvestmentsDb");
                return false;
            }

            return true;

        }

        private static void CreateCollections()
        {
            // var database = _mongoClient.GetDatabase("InvestmentsDb");
            var collections = _database.ListCollectionNames().ToList();
            var collectionsToCreate = new List<string>();

            if(collections.ToList().Any() == false)
                collectionsToCreate.AddRange(collectionsMongoDb);
            else
                collectionsToCreate.AddRange(collectionsMongoDb.Except(collections.ToList()));

            foreach (var collection in collectionsToCreate)
            {
                _database.CreateCollection(collection);
            }

        }

        private static async Task SeedRolesAsync(IEnumerable<ApplicationUser> users)
        {
            var usersCollection = _database.GetCollection<ApplicationUser>("Users");
            var rolesCollection = _database.GetCollection<ApplicationRole>("Roles");
            var userRolesCollection = _database.GetCollection<ApplicationUserRole>("UserRoles");

            var allUsers = (await usersCollection.FindAsync(_ => true)).ToList();
            var allRoles = (await rolesCollection.FindAsync(_ => true)).ToList();

            if (!allRoles.Any())
            {
                allRoles = new List<ApplicationRole>
                {
                    new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                    new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }
                };

                await rolesCollection.InsertManyAsync(allRoles);
                
            }

            foreach (var user in users)
            {
                var rolesToAssign = allRoles.Where(role => string.Equals(role.Name, user.UserName, StringComparison.OrdinalIgnoreCase));

                foreach (var role in rolesToAssign)
                {
                    var existingUserRole = await userRolesCollection.Find(ur =>
                        ur.UserId == user.Id && ur.RoleId == role.Id).FirstOrDefaultAsync();

                    if (existingUserRole == null)
                    {
                        var userRole = new ApplicationUserRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = user.Id,
                            RoleId = role.Id
                        };

                        await userRolesCollection.InsertOneAsync(userRole);
                    }
                }
            }
            // var roleee = await rolesCollection.Find(Builders<ApplicationRole>.Filter.Empty).ToListAsync();
            // var usersX= await usersCollection.Find(Builders<ApplicationUser>.Filter.Empty).ToListAsync();
            // var ApplicationRole = await rolesCollection.Find(Builders<ApplicationRole>.Filter.Empty).ToListAsync();
        }


        private static async Task<IEnumerable<ApplicationUser>> SeedUsersAsync()
        {
            var usersCollection = _database.GetCollection<ApplicationUser>("Users");
            var users = new List<ApplicationUser>();
            // string userRoleId = Guid.NewGuid().ToString("D");
            string idUserAdmin = Guid.NewGuid().ToString("D");
            string idUser = Guid.NewGuid().ToString("D");

            if (await usersCollection.CountDocumentsAsync(_ => true) == 0)
            {
                var adminUser = new ApplicationUser
                {
                    Id = idUserAdmin,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "admin"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                };

                await usersCollection.InsertOneAsync(adminUser);
                users.Add(adminUser);

                var userRoles = new ApplicationUser
                {
                    Id = idUser,
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "user"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                };

                await usersCollection.InsertOneAsync(userRoles);

                users.Add(userRoles);

                return users;
            }

            return Enumerable.Empty<ApplicationUser>();
        }

        private static void CreateIndexs()
        {

            var collectionDetailedFund = _database.GetCollection<DetailedFund>(
                                            collectionsMongoDb.Where(x => x.Contains("DetailedFund")).First());

            var indexKeysDetailedFund = Builders<DetailedFund>.IndexKeys.Ascending(u => u.FundCode);
            var indexOptionsDetailedFund = new CreateIndexOptions { Unique = true };
            var indexModelDetailedFund = new CreateIndexModel<DetailedFund>(indexKeysDetailedFund, indexOptionsDetailedFund);

            collectionDetailedFund.Indexes.CreateOne(indexModelDetailedFund);

            var collectionFundDividend = _database.GetCollection<FundDividend>(
                                            collectionsMongoDb.Where(x => x.Contains("FundDividend")).First());
            
            var indexKeysFundDividend = Builders<FundDividend>.IndexKeys.Ascending(u => u.FundCode);
            var indexModelFundDividend = new CreateIndexModel<FundDividend>(indexKeysFundDividend);
            collectionFundDividend.Indexes.CreateOne(indexModelFundDividend);

            var collectionBestFundRank = _database.GetCollection<BestFundRank>(
                                            collectionsMongoDb.Where(x => x.Contains("BestFundRank")).First());
            
            var indexKeysBestFundRank = Builders<BestFundRank>.IndexKeys.Ascending(u => u.FundCode);
            var indexOptionsBestFundRank = new CreateIndexOptions { Unique = true };
            var indexModelBestFundRank = new CreateIndexModel<BestFundRank>(indexKeysBestFundRank, indexOptionsBestFundRank);
            collectionBestFundRank.Indexes.CreateOne(indexModelBestFundRank);

            var collectionUserAddress = _database.GetCollection<UserAddress>(
                                            collectionsMongoDb.Where(x => x.Contains("UserAddress")).First());
            
            var indexKeysUserAddress = Builders<UserAddress>.IndexKeys.Ascending(u => u.UserId);
            var indexOptionsUserAddress = new CreateIndexOptions { Unique = true };
            var indexModelUserAddress = new CreateIndexModel<UserAddress>(indexKeysUserAddress, indexOptionsUserAddress);
            collectionUserAddress.Indexes.CreateOne(indexModelUserAddress);

            var collectionDetailedStock = _database.GetCollection<DetailedStock>(
                                            collectionsMongoDb.Where(x => x.Contains("DetailedStock")).First());
            
            var indexKeysDetailedStock = Builders<DetailedStock>.IndexKeys.Ascending(u => u.FundCode);
            var indexOptionsDetailedStock = new CreateIndexOptions { Unique = true };
            var indexModelDetailedStock = new CreateIndexModel<DetailedStock>(indexKeysDetailedStock, indexOptionsDetailedStock);
            collectionDetailedStock.Indexes.CreateOne(indexModelDetailedStock);
           
            var collectionStockDividend = _database.GetCollection<StockDividend>(
                                            collectionsMongoDb.Where(x => x.Contains("StockDividend")).First());

            var indexKeysStockDividend = Builders<StockDividend>.IndexKeys.Ascending(u => u.FundCode);
            var indexModelStockDividend = new CreateIndexModel<StockDividend>(indexKeysStockDividend);
            collectionStockDividend.Indexes.CreateOne(indexModelStockDividend);

            var collectionBestStockRank = _database.GetCollection<BestStockRank>(
                                            collectionsMongoDb.Where(x => x.Contains("BestStockRank")).First());
            
            var indexKeysBestStockRank = Builders<BestStockRank>.IndexKeys.Ascending(u => u.FundCode);
            var indexModelBestStockRank = new CreateIndexModel<BestStockRank>(indexKeysBestStockRank);
            collectionBestStockRank.Indexes.CreateOne(indexModelBestStockRank);

            var collectionApplicationUser = _database.GetCollection<ApplicationUser>(
                                                collectionsMongoDb.Where(x => x.Contains("Users")).First());
            
            var indexKeysApplicationUser = Builders<ApplicationUser>.IndexKeys.Ascending(u => u.UserName);
            var indexOptionsApplicationUser = new CreateIndexOptions { Unique = true };
            var indexModelApplicationUser = new CreateIndexModel<ApplicationUser>(indexKeysApplicationUser, indexOptionsApplicationUser);
            collectionApplicationUser.Indexes.CreateOne(indexModelApplicationUser);

        }
    }
}
