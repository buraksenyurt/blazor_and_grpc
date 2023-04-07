using Microsoft.EntityFrameworkCore;

namespace MusicLibrary.Server.Data;

public class MusicLibraryDbContext
    : DbContext
{
    public MusicLibraryDbContext() { }
    public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Musician>(m =>
        {
            m.Property(p => p.Fullname).IsRequired();
        });
    }
}