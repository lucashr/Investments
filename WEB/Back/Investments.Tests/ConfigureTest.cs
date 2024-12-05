using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Investments.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
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
            _classNameTest =  methodUnderTest.DeclaringType.AssemblyQualifiedName.Split(',')[0];
            _classNameTest = _classNameTest.Split('.')[_classNameTest.Split('.').Count() - 1];
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

                var options = new DbContextOptionsBuilder<InvestmentsContext>()
                .UseSqlite($"Data Source=Investments{_classNameTest}.db")
                .EnableSensitiveDataLogging().Options;

                var _context = new InvestmentsContext(options);

                _contexts.TryAdd(_classNameTest, _context);

                _contexts.Where(x=>x.Key == _classNameTest).Select(x=>x.Value).FirstOrDefault().Database.EnsureCreated();

                return await Task.FromResult(_contexts.Where(x=>x.Key == _classNameTest).Select(x=>x.Value).FirstOrDefault());

            }
            finally
            {
                semaphoreSlim.Release();
            }

        }

    }
}