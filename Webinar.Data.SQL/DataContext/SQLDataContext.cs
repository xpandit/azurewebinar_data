using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webinar.Data.SQL.Model;

namespace Webinar.Data.SQL.DataContext
{
    public class SQLDataContext : DbContext
    {
        public SQLDataContext(DbContextOptions<SQLDataContext> options) 
            : base(options)
        {
        }

        public DbSet<TaskList> TaskLists { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
