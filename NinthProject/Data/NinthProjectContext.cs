#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NinthProject.Models;

namespace NinthProject.Data
{
    public class NinthProjectContext : DbContext
    {
        public NinthProjectContext (DbContextOptions<NinthProjectContext> options)
            : base(options)
        {
        }

        public DbSet<NinthProject.Models.Courses> Courses { get; set; }

        public DbSet<NinthProject.Models.Groups> Groups { get; set; }

        public DbSet<NinthProject.Models.Students> Students { get; set; }
    }
}
