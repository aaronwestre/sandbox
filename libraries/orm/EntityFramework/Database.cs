using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using types;

namespace EntityFramework
{
    public class Database : DbContext
    {
        public DbSet<Arm> Arms { get; set; }
        public DbSet<Hand> Hands { get; set; }
        public DbSet<Finger> Fingers { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Choose a database vendor...
            optionsBuilder.UseSqlite(
                "Data Source=/Users/aaron/Code/sandbox/libraries/orm/orm_ef.sqlite");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Map relations...
            modelBuilder
                .Entity<Arm>()
                .HasMany(arm => arm.Hands)
                .WithMany(hand => hand.Arms);
            modelBuilder
                .Entity<Hand>()
                .HasMany(hand => hand.Fingers)
                .WithMany(finger => finger.Hands);
        }
    }
    
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<Database>
    {
        public Database CreateDbContext(string[] args)
        {
            return new Database();
        }
    }
}