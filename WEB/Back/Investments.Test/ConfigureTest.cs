using System;
using System.IO;
using System.Reflection;
using Investments.Test.Persistence.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit.Sdk;

namespace Investments.Test
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ConfigureTest : BeforeAfterTestAttribute
    {
        public static InvestmentsContext _context { get; set; }

        private SqliteConnection connection;
        
        public override void Before(MethodInfo methodUnderTest)
        {
            _context = ConfigureDatabase();
        }

        public override void After(MethodInfo methodUnderTest)
        {
            base.After(methodUnderTest);
        }

        private InvestmentsContext ConfigureDatabase()
        {

            IConfigurationRoot _configuration;

            // var options = new DbContextOptionsBuilder<InvestmentsContext>()
            //                     .UseSqlite($"Data Source=Investments.db")
            //                     .EnableSensitiveDataLogging()
            //                     .Options;

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.Development.json");

            _configuration = builder.Build();

            var options = new DbContextOptionsBuilder<InvestmentsContext>()
            .UseSqlite(_configuration.GetConnectionString("Default"))
            .EnableSensitiveDataLogging().Options;

            InvestmentsContext context = new InvestmentsContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}