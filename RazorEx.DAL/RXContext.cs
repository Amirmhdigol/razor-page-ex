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
            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new PostsConfigurations());
            
            //Filters Users by IsDelete   ff
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Products>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Post>().HasQueryFilter(u => !u.IsDelete);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductEpisodes> ProductEpisodes { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<ProductLevel> ProductLevels { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<UserProducts> UserProducts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserDiscounts> UserDiscounts { get; set; }
        public DbSet<ProductVote> ProductVotes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}