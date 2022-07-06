using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projet_test_nael.Models;

namespace projet_test_nael
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Text> Texts { get; set; } = null!;

        public MyDbContext()
        {

        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                        .UseSqlServer("Data source=SRV-SQLTEST01;initial catalog=Test_Nael;user id=user_dev;password=user_dev;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
