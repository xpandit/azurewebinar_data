using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webinar.Data.Cosmos.Model;

namespace Webinar.Data.Cosmos.DataContext
{
    public class CosmosDataContext : DbContext
    {
        public CosmosDataContext(DbContextOptions<CosmosDataContext> options)
            : base(options)
        {
        }

        public DbSet<TaskList> TaskLists { get; set; }
    }
}
