using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> users { get; set; }
        public DbSet<Symptoms> symptoms { get; set; }
         public DbSet<School> schools { get; set; }
         public DbSet<Child> child { get; set; }
    }
}