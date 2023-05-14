using Microsoft.EntityFrameworkCore;

namespace AcmePublishing.Data;
        
       
public class AcmeContext : DbContext
{
    public DbSet<Subscription> Subscriptions { get; set; }

    public AcmeContext(DbContextOptions<AcmeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityColumns();
        modelBuilder.Entity<Subscription>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<PrintDistributor>()
            .HasMany<Publication>(x => x.Publications)
            .WithMany(x => x.Distributors);
    }

}