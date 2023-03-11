using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Investments.Persistence.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Sdk;

namespace Investments.Tests
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ConfigureTest : BeforeAfterTestAttribute
    {

        public static ConcurrentDictionary<string, InvestmentsContext> _contexts = new ConcurrentDictionary<string, InvestmentsContext>();
        public static string _classNameTest { get; set; }
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1,1);

        public override void Before(MethodInfo methodUnderTest)
        {
            // lock (thisLock)
            // {
            //     // if(!_contexts.Keys.Contains(_dbName))

                _classNameTest =  methodUnderTest.DeclaringType.AssemblyQualifiedName.Split(',')[0];
                _classNameTest = _classNameTest.Split('.')[_classNameTest.Split('.').Count() - 1];

            //     ConfigureDatabase();
                
            //     // else
            //     //     _context = _contexts.Where(x => x.Key == _dbName).Select(x => x.Value).FirstOrDefault();
            // }
        }

        public override void After(MethodInfo methodUnderTest)
        {
            base.After(methodUnderTest);
        }

        public static async Task<InvestmentsContext> ConfigureDatabase()
        {
            
            await semaphoreSlim.WaitAsync(-1);

            try
            {

                // if(_contexts.ContainsKey(_classNameTest))
                // {
                //     _contexts.Where(x=>x.Key == _classNameTest).Select(x=>x.Value).FirstOrDefault().Database.EnsureDeleted();
                //     _contexts.Where(x=>x.Key == _classNameTest).Select(x=>x.Value).FirstOrDefault().Database.EnsureCreated();
                //     return _contexts.Where(x=>x.Key == _classNameTest).Select(x=>x.Value).FirstOrDefault();
                // }

                // IConfigurationRoot _configuration;

                // var builder = new ConfigurationBuilder()
                // .SetBasePath(Directory.GetCurrentDirectory())
                // .AddJsonFile("appsettings.Development.json");

                // _configuration = builder.Build();

                var options = new DbContextOptionsBuilder<InvestmentsContext>()
                .UseSqlite($"Data Source=Investments{_classNameTest}.db")
                .EnableSensitiveDataLogging().Options;

                var _context = new InvestmentsContext(options);

                _contexts.TryAdd(_classNameTest, _context);

                // _context.Database.EnsureDeleted();
                _contexts.Where(x=>x.Key == _classNameTest).Select(x=>x.Value).FirstOrDefault().Database.EnsureCreated();

                return await Task.FromResult(_contexts.Where(x=>x.Key == _classNameTest).Select(x=>x.Value).FirstOrDefault());

            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                semaphoreSlim.Release();
            }

        }

        // private InvestmentsContext ConfigureDatabase()
        // {

        //     IConfigurationRoot _configuration;

        //     // var options = new DbContextOptionsBuilder<InvestmentsContext>()
        //     //                     .UseSqlite($"Data Source=Investments.db")
        //     //                     .EnableSensitiveDataLogging()
        //     //                     .Options;

        //     var builder = new ConfigurationBuilder()
        //       .SetBasePath(Directory.GetCurrentDirectory())
        //       .AddJsonFile("appsettings.Development.json");

        //     _configuration = builder.Build();

        //     var options = new DbContextOptionsBuilder<InvestmentsContext>()
        //     .UseSqlite(_configuration.GetConnectionString("Default"))
        //     .EnableSensitiveDataLogging().Options;

        //     var context = new InvestmentsContext(options);
        //     context.Database.EnsureCreated();

        //     return context;
        // }
    }
}