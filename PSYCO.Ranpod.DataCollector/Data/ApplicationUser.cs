using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.DataCollector.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Family { get; set; }
    }
}
