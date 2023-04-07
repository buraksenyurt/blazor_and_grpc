using Microsoft.EntityFrameworkCore;

namespace MusicLibrary.Server.Data;

public class MusicLibraryDbContext
    : DbContext
{
    public MusicLibraryDbContext() { }
    public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options) : base(options) { }
    public DbSet<Musician> Musicians { get; set; }
    public DbSet<Album> Albums { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musician>(m =>
        {
            m.Property(p => p.Name).IsRequired();
            m.Navigation(p => p.Albums).AutoInclude();
        });

        modelBuilder.Entity<Album>(a =>
        {
            a.Property(p => p.Name).IsRequired();
            a.Navigation(p => p.Musicians).AutoInclude();
        });

        modelBuilder.Entity<AlbumMusician>(a =>
        {
            a.HasKey(p => new { p.AlbumId, p.MusicianId });
        });
    }
}