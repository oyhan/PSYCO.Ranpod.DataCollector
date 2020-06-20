using Microsoft.Extensions.Options;
using PSYCO.JsonDatastore;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.Models.Database
{
    public class AppJsonDataStore : JsonDataStore<JsonDatabase>  
    {
        public AppJsonDataStore(IOptionsSnapshot<AppSettings> settigs) :base (settigs.Value.DatabaseFullPath)
        {
            DbPath
        }
    }
}
