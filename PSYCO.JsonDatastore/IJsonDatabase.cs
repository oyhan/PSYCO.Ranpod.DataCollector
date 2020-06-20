using System;
using System.Collections.Generic;
using System.Text;

namespace PSYCO.JsonDatastore
{
    public interface IJsonDataStore<TDb> where TDb : class, new()
    {
        TDb Database { get; } 
        void ApplyChanges();
        IList<TData> GetTable<TData>() where TData : class;
    }
}
