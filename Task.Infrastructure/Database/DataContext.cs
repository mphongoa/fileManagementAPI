using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Task.Domain.Files;
using Task.Domain.Users;

namespace Task.Infrastructure.Database
{
    public class DataContext : DbContext
    {
        public DataContext() { }

      
        public DataContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Database=Task;MultipleActiveResultSets=True;Trusted_Connection=true;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files {get; set;}
    }
}
