using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MongoDB.Driver;
using Investments.API;
using Investments.Persistence.Contexts;
using AspNetCore.Identity.MongoDbCore.Extensions;
using Investments.Domain;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Investments.Application.Contracts;
using Investments.Application.Factory;
using MongoDB.Bson;
using Mongo2Go;
using System.Collections.Generic;
using Investments.Domain.Identity;
using System.Configuration;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.TestHost;
using Investments.VariablesManager;
using Microsoft.AspNetCore.Builder;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using System.Net.Http;
using System.Diagnostics;

namespace Investments.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

            builder.ConfigureTestServices(services =>
            {
                // Aqui, você pode substituir o comportamento do SessionContext apenas nos testes
                services.AddScoped<SessionContext>(serviceProvider =>
                {
                    var sessionContext = new SessionContext();
                    // Defina valores personalizados para testes, se necessário
                    sessionContext.SessionId = "test-session-id";
                    return sessionContext;
                });
            });

            IConfiguration _configuration;

            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Pega o diretório atual
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true) // Carrega o appsettings.json
            .AddEnvironmentVariables()
            .Build();

            var databaseSettings = _configuration.GetSection("DatabaseSettings");
            var useMongoDb = _configuration.GetValue<bool>("DatabaseSettings:UseMongoDb");

            if (useMongoDb)
            {

                var mongoConnectionString = databaseSettings.GetSection("MongoDB:ConnectionString").Value;
                var mongoDatabaseName = databaseSettings.GetSection("MongoDB:DatabaseName").Value;

                var runner = MongoDbRunner.Start();

                builder.ConfigureAppConfiguration((context, config) =>
                {
                    var settings = new Dictionary<string, string>
                    {
                        { "DatabaseSettings:UseMongoDb", "true" },
                        { "DatabaseSettings:MongoDB:ConnectionString", runner.ConnectionString },
                        { "DatabaseSettings:MongoDB:DatabaseName", "InvestmentsDb" }
                    };

                    config.AddInMemoryCollection(settings);
                });

                builder.ConfigureServices(services =>
                {
                    
                    var mongoDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMongoClient));
                    if (mongoDescriptor != null)
                    {
                        services.Remove(mongoDescriptor);
                    }

                    var mongoDatabaseDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMongoDatabase));
                    if (mongoDatabaseDescriptor != null)
                    {
                        services.Remove(mongoDatabaseDescriptor);
                    }

                    
                    var mongoClient = new MongoClient(runner.ConnectionString);
                    var mongoDatabase = mongoClient.GetDatabase("InvestmentsDb");

                    services.AddSingleton<IMongoClient>(mongoClient);
                    services.AddSingleton<IMongoDatabase>(mongoDatabase);

                    var mongodbIdentityConfig = new MongoDbIdentityConfiguration
                    {
                        MongoDbSettings = new MongoDbSettings
                        {
                            ConnectionString = runner.ConnectionString,
                            DatabaseName = "InvestmentsDb"
                        },
                        IdentityOptionsAction = options =>
                        {
                            options.Password.RequireDigit = false;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireLowercase = false;
                            options.Password.RequireUppercase = false;
                            options.Password.RequiredLength = 0;
                        }
                    };

                    services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, string>(mongodbIdentityConfig)
                            .AddUserManager<UserManager<ApplicationUser>>()
                            .AddSignInManager<SignInManager<ApplicationUser>>()
                            .AddRoleManager<RoleManager<ApplicationRole>>()
                            .AddDefaultTokenProviders();
                    
                    services.AddScoped<IAccountServiceFactory, AccountServiceMongoDbFactory>();

                });

            }
            else
            {

                builder.ConfigureServices(services =>
                {

                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<InvestmentsContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<InvestmentsContext>(options =>
                    {
                        options.UseSqlite("DataSource=:memory:");
                    });

                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    services.AddDbContext<InvestmentsContext>(options =>
                    {
                        options.UseSqlite(connection);
                    });

                    services.AddIdentityCore<User>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 0;
                    })
                    .AddRoles<Role>()
                    .AddRoleManager<RoleManager<Role>>()
                    .AddSignInManager<SignInManager<User>>()
                    .AddRoleValidator<RoleValidator<Role>>()
                    .AddEntityFrameworkStores<InvestmentsContext>()
                    .AddDefaultTokenProviders();

                    services.AddScoped<IAccountServiceFactory, AccountServiceEntityFactory>();

                });

            }

            
        }
    }
}
