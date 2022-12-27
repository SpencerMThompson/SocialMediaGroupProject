using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SocialMediaApplication.Models;

namespace SocialMediaApplication.Data
{
    public class SMDBContext : IdentityDbContext
    {
        public SMDBContext(DbContextOptions<SMDBContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(c => c.Posts).WithOne(e => e.User);
            modelBuilder.Entity<Post>().HasMany(c => c.Comments).WithOne(e => e.Post);
            modelBuilder.Entity<Comment>().ToTable("comments").HasOne(c => c.Post);
        }
    }


}
