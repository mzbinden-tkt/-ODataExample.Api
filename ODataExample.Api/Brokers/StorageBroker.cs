using Microsoft.EntityFrameworkCore;
using ODataExample.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataExample.Api.Brokers
{
    public class StorageBroker : DbContext
    {
        public StorageBroker(DbContextOptions<StorageBroker> options)
            : base(options)
        {
            this.Database.Migrate();
        }
        public DbSet<Customer> customers { get; set; }
    }
}
