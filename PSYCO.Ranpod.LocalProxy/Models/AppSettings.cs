using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.Models
{
    public class AppSettings
    {
        public string DatabaseFullPath { get; set; } = "database.json";
        public int CheckSessionsIntervalSeconds { get; set; } = 15;

        public int SessionsSendLogIntervalMinutes { get; set; } = 1;

    }
}
