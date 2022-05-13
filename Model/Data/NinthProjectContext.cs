#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NinthProject
{
    public class NinthProjectContext : DbContext
    {
        public NinthProjectContext(DbContextOptions<NinthProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Courses> Courses { get; set; }

        public DbSet<Groups> Groups { get; set; }

        public DbSet<Students> Students { get; set; }
    }
}