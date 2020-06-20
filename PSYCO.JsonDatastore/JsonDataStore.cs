using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace PSYCO.JsonDatastore
{
    /// <summary>
    /// Default implimentation of IJsonDataStore.
    /// Instantiate a new class with given path to file creates a file with the given name.
    /// This class must have defined it's tables with propery of type IList<MyTable> 
    /// </summary>
    /// <typeparam name="TDb">Type of database schema</typeparam>
    public abstract class JsonDataStore<TDb> : IJsonDataStore<TDb> where TDb : class, new()
    {
        /// <summary>
        /// Gets the current path to db file.
        /// </summary>
        public string DbPath { get; private set; }
        /// <summary>
        /// asdasd
        /// </summary>
        public string DbPath2 { get; private set; }

        /// <summary>
        /// A mapping .net type to your json file. read
        /// </summary>
        public TDb Database { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">path to json file</param>
        public JsonDataStore(string path)
        {
            DbPath = path;
            var fileInfo = new FileInfo(path);
            
            var isFileExists = File.Exists(path) ;
            if (isFileExists)
            {
                var stringDb = File.ReadAllText(DbPath);
                var typedDb = JsonConvert.DeserializeObject<TDb>(stringDb);
                if (typedDb != null)
                {
                    Database = typedDb;
                }
                else
                {
                    var db = CreateDbInstance();
                    Database = db;
                    File.WriteAllText(DbPath, JsonConvert.SerializeObject(db));
                }
            }
            else
            {
                bool direcotryExsits = Directory.Exists(fileInfo.Directory.FullName);
                if (!direcotryExsits)
                {
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
                }
                var db = CreateDbInstance();
                Database = db;
                File.WriteAllText(DbPath, JsonConvert.SerializeObject(db));
            }


        }

        private TDb CreateDbInstance()
        {
            var dbInstance = new TDb();
            var propertyInfo = typeof(TDb).GetProperties();
            //var tables = propertyInfo.Where(p => typeof(IList<>).IsAssignableFrom(p.PropertyType));

            foreach (var property in propertyInfo)
            {
                
                var isList = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(IList<>);

                if(isList)
                {
                    var genericArgs = property.PropertyType.GetGenericArguments();
                    var constructed = typeof(List<>).MakeGenericType(genericArgs);

                    property.SetValue(dbInstance, Activator.CreateInstance(constructed));
                }
               
            }
            return dbInstance;
        }
        /// <summary>
        /// Returns table as a collection
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public IList<TData> GetTable<TData>() where TData : class
        {
            var jsonString = File.ReadAllText(DbPath);
            var dynamicJson = JsonConvert.DeserializeObject<TDb>(jsonString);
            var t = dynamicJson.GetType();
            var table = t.GetProperties().FirstOrDefault(p => typeof(IList<TData>).IsAssignableFrom(p.PropertyType));
            return table.GetValue(dynamicJson) as IList<TData> ?? new List<TData>();
        }

        /// <summary>
        /// Call this method after changing the data in Database property to take effect your changes.
        /// </summary>
        public void ApplyChanges()
        {
            File.WriteAllText(DbPath, JsonConvert.SerializeObject(Database));


        }
        //public void Insert<T>(T data, string key)
        //{

        //    string value = JsonConvert.SerializeObject(data);
        //    dynamic jsonModel = new Dictionary<string, object>();
        //    jsonModel[key] = value;

        //    File.WriteAllText(DbPath, JsonConvert.SerializeObject(jsonModel));
        //}

        //public T Read<T>(string key) where T : class
        //{
        //    var jsonString = File.ReadAllText(DbPath);
        //    var dynamicJson = JsonConvert.DeserializeObject(jsonString);
        //    var t = dynamicJson.GetType();
        //    var property = t.GetProperty(key);

        //    return property.GetValue(dynamicJson) as T;

        //}

        //public void Update<T>(T data, string key) where T : class
        //{
        //    var jsonString = File.ReadAllText(DbPath);
        //    var dynamicJson = JsonConvert.DeserializeObject(jsonString);
        //    var t = dynamicJson.GetType();
        //    var property = t.GetProperties().FirstOrDefault(p => p.Name == key);

        //    property.SetValue(dynamicJson, data);
        //    File.WriteAllText(DbPath, JsonConvert.SerializeObject(dynamicJson));

        //}
    }
}
