using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedModels
{
    public static class MappingData
    {
        public static Dictionary<int, string> ServerNames { get; set; } = new Dictionary<int, string>();
        public static void Add(int serverId, string name)
        {
            //var store = JsonFla
            if (ServerNames.Any(v => v.Key == serverId)) return;
            else ServerNames.Add(serverId, name);
        }
      
    }
}
