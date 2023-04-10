using Microsoft.EntityFrameworkCore;

namespace MusicLibrary.Data.Entity;

/*
    Klasik bir Entity Framework DB Context nesnesidir.
    Müzisyenler ve dahil oldukları albümler birer DbSet olarak tutulmakta.

    Server tarafında bağımlılığı enjete ederken işe kolaylaştırmak adına IApplicationDbContext arayüzü de implemente edilmiştir.
*/
public class MusicLibraryDbContext
    : DbContext, IApplicationDbContext
{
    public MusicLibraryDbContext() { }
    public MusicLibraryDbContext(DbContextOptions<MusicLibraryDbContext> options) : base(options) { }
    public DbSet<Musician> Musicians { get; set; }
    public DbSet<Album> Albums { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
            Müzsiyoneler için Name alanı zorunludur ve navigasyonlar sırasında Albumleri otomatik olarak bağlanır.
            Aynı durum albüm modeli için de geçerlidir. Name alanı zorunludur ve  navigasyon da müzisyenler otomatik olarak dahil edilir. 
        */
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

        // Album ve müzisyenler arasındaki many-to-many ilişkiyi tanımlar.
        modelBuilder.Entity<AlbumMusician>(a =>
        {
            a.HasKey(p => new { p.AlbumId, p.MusicianId });
        });
    }
}