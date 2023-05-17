using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class SignatureContext : IdentityUserContext<IdentityUser>
    {
        public SignatureContext(DbContextOptions<SignatureContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Signature> Signatures { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Signature>().ToTable("Signature");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Promotion>().ToTable("Promotion");
        }
    }
}
