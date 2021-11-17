using Microsoft.EntityFrameworkCore;
using RazorEx.DAL.Configurations;
using RazorEx.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEx.DAL.Context
{
    public class RXContext : DbContext
    {

        public RXContext(DbContextOptions<RXContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
