using Microsoft.EntityFrameworkCore;
using MusicLibrary.Data.Entity;

namespace MusicLibrary.Data
{
    /*
     * Entity Framework Core context nesnesine ait servis sözleşmemiz.
     * Onu da DI mekanizmasına kolayca dahil edip bağımlılığı çözümletebiliriz.
     * Yani infrastructe katmanındaki Data kütüphanesindeki MusicLibraryDbContext'i bu arayüz üstünden sisteme entegre edeceğiz.
     */
    public interface IApplicationDbContext
    {
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Album> Albums { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}