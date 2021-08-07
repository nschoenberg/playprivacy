using Microsoft.EntityFrameworkCore;

namespace PlayPrivacy.Data
{
    public class PrivacyDbContext : DbContext
    {
        public PrivacyDbContext(DbContextOptions<PrivacyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Counter> Counter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Counter>().ToTable("Counter");
        }
    }
}
