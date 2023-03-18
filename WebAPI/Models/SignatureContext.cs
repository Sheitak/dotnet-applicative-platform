using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class SignatureContext: DbContext
    {
        public SignatureContext(DbContextOptions<SignatureContext> options): base(options) {}
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Signature> Signatures { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;
    }
}
