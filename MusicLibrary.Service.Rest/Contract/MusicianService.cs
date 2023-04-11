using AutoMapper;
using MusicLibrary.Data.Entity;
using MusicLibrary.Shared;

namespace MusicLibrary.Service.Contract;

/*
    Müzisyenlerle ilgili veritabanı işlemlerini ele alan servis sınıfı.
    BaseService'ten türer, Musician ve MusicianModel tiplerini kullanır.
    Constructor metottan, üst sınıf yapıcısına ilgili parametreleri geçer.
    
    İstenirse BaseService'in sunmadığı kendine has ek fonksiyonellikler ile de donatılabilir.
*/
public class MusicianService
    : BaseService<Musician, MusicianModel>
{
    public MusicianService(MusicLibraryDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}