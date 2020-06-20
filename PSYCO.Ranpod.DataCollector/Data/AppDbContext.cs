using Asanobat.IssueTracker.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSYCO.Ranpod.DataCollector.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.DataCollector.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedIdentity.Run(builder);
            base.OnModelCreating(builder);
            
        }
        public DbSet<LogModel> Logs { get; set; }

    }
}
