using Microsoft.EntityFrameworkCore;
using MangaReads.Classes;

namespace MangaReads.Data;

public class MangaReadsDbContext : DbContext
{
    public MangaReadsDbContext(DbContextOptions<MangaReadsDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Manga> Mangas { get; set; }
    public DbSet<Volume> Volumes { get; set; }
    public DbSet<UserManga> UserMangas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Manga>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.title).IsRequired().HasMaxLength(500);
            entity.Property(m => m.description).HasMaxLength(2000);
            entity.Property(m => m.image).HasMaxLength(500);
            entity.Property(m => m.thirdPartyId).HasMaxLength(100);
            entity.HasMany(m => m.volumeData)
                  .WithOne(v => v.Manga)
                  .HasForeignKey(v => v.MangaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Volume>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.id).IsRequired().HasMaxLength(100);
            entity.Property(v => v.fileName).HasMaxLength(500);
            entity.HasOne(v => v.Manga)
                  .WithMany(m => m.volumeData)
                  .HasForeignKey(v => v.MangaId);
        });

        modelBuilder.Entity<UserManga>(entity =>
        {
            entity.HasKey(um => um.Id);
            entity.Property(um => um.name).IsRequired().HasMaxLength(500);
            entity.Property(um => um.status).HasMaxLength(50);
            entity.Property(um => um.volume).HasMaxLength(50);
            entity.Property(um => um.title).HasMaxLength(500);
            entity.Property(um => um.description).HasMaxLength(2000);
            entity.Property(um => um.image).HasMaxLength(500);
            entity.Property(um => um.thirdPartyId).HasMaxLength(100);
            entity.HasOne(um => um.User)
                  .WithMany(u => u.mangas)
                  .HasForeignKey(um => um.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Ignore(um => um.volumeData);
        });

        base.OnModelCreating(modelBuilder);
    }
}