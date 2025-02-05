using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investments.Domain
{
    public class MongoDbSettingsXPTO
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}