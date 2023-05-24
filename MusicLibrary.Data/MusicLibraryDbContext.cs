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
            Müzsiyenler için Name alanı zorunludur ve navigasyonlar sırasında Albumleri otomatik olarak bağlanır.
            Aynı durum albüm modeli için de geçerlidir. Name alanı zorunludur ve  navigasyon da müzisyenler otomatik olarak dahil edilir. 
        */
        modelBuilder.Entity<Musician>(m =>
        {
            m.Property(p => p.Name).IsRequired();
            m.Navigation(p => p.Albums).AutoInclude();
            m.HasData(
                new Musician { Id = 1, Name = "John Doe", Bio = "Londra doğumlu biz caz ustasıdır.", StarPoint = 6, Birthdate = new DateTime(1965, 12, 4) },
                new Musician { Id = 2, Name = "Maria Keri Watson", Bio = "Çikolata renkli kadife sesin sahibi Amerikalı R&B şarkıcısıdır.", StarPoint = 8, Birthdate = new DateTime(1980, 1, 1) },
                new Musician { Id = 3, Name = "Ben Deyvis Ya Sen", Bio = "Dublin doğumlu cult rock solistidir. Daha şimdiden sayısız albümde yer almıştır.", StarPoint = 6, Birthdate = new DateTime(1998, 12, 12) }
            );
        });

        modelBuilder.Entity<Album>(a =>
        {
            a.Property(p => p.Name).IsRequired();
            a.Navigation(p => p.Musicians).AutoInclude();
            a.HasData(
                new Album { Id = 1, Name = "Night At The Museum II", Year = 2019, Category = Category.Rock },
                new Album { Id = 2, Name = "Actions & Reactions", Year = 1981, Category = Category.Reggae },
                new Album { Id = 3, Name = "Everything I Do I Do It For You", Year = 1993, Category = Category.Rock }
            );
        });

        // Album ve müzisyenler arasındaki many-to-many ilişkiyi tanımlar.
        modelBuilder.Entity<AlbumMusician>(a =>
        {
            a.HasKey(p => new { p.AlbumId, p.MusicianId });

        });
    }
}