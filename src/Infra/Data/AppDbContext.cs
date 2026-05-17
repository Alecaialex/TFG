using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Commission> Commissions { get; set; }
    public DbSet<CommissionType> CommissionTypes { get; set; }
    public DbSet<CommissionSize> CommissionSizes { get; set; }
    public DbSet<Deliverable> Deliverables { get; set; }
    public DbSet<Reference> References { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Artist>()
            .HasOne(a => a.User)
            .WithOne(u => u.ArtistProfile)
            .HasForeignKey<Artist>(a => a.Id);

        modelBuilder.Entity<Client>()
            .HasOne(c => c.User)
            .WithOne(u => u.ClientProfile)
            .HasForeignKey<Client>(c => c.Id);

        modelBuilder.Entity<CommissionType>()
            .Property(ct => ct.BasePrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CommissionSize>()
            .Property(cs => cs.PriceModifier)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Commission>()
            .Property(c => c.AgreedPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Commission>()
            .HasOne(c => c.Artist)
            .WithMany(a => a.Commissions)
            .HasForeignKey(c => c.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CommissionType>()
            .HasOne(ct => ct.Artist)
            .WithMany(a => a.CommissionTypes)
            .HasForeignKey(ct => ct.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommissionSize>()
            .HasOne(cs => cs.Artist)
            .WithMany(a => a.CommissionSizes)
            .HasForeignKey(cs => cs.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Commission>()
            .HasOne(c => c.Client)
            .WithMany(cl => cl.Commissions)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Commission>()
            .HasOne(c => c.Type)
            .WithMany()
            .HasForeignKey(c => c.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Commission>()
            .HasOne(c => c.Size)
            .WithMany()
            .HasForeignKey(c => c.SizeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Deliverable>()
            .HasOne(d => d.Commission)
            .WithMany(c => c.Deliverables)
            .HasForeignKey(d => d.CommissionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reference>()
            .HasOne(r => r.Commission)
            .WithMany(c => c.References)
            .HasForeignKey(r => r.CommissionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Artist>().ToTable("Artists");
        modelBuilder.Entity<Client>().ToTable("Clients");
        modelBuilder.Entity<Commission>().ToTable("Commissions");
    }
}