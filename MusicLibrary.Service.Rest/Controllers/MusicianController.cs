using MusicLibrary.Data.Entity;
using MusicLibrary.Shared.Model;

namespace MusicLibrary.Service.Controllers;

/*
    Müzisyenler için kullanılan kontrollör sınıfı. 
    BaseController'dan türer.
*/
public class MusicianController
    : BaseController<MusicianModel, Musician, MusicianService>
{
    public MusicianController(MusicianService service)
        : base(service, "/musicians")
    {
    }
}