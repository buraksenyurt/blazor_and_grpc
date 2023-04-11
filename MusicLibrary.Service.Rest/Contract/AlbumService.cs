using AutoMapper;
using MusicLibrary.Data.Entity;
using MusicLibrary.Model;

namespace MusicLibrary.Service.Contract;

/*
    Albumlerle ilgili veritabanı işlemlerini ele alan servis sınıfı.
    BaseService'ten türer, Album ve AlbumModel tiplerini kullanır.
    Constructor metottan, üst sınıf yapıcısına ilgili parametreleri geçer.
    
    İstenirse BaseService'in sunmadığı kendine has ek fonksiyonellikler ile de donatılabilir.
*/
public class AlbumService
    : BaseService<Album, AlbumModel>
{
    public AlbumService(MusicLibraryDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}