using MusicLibrary.Server.Data;
using MusicLibrary.Server.Service;
using MusicLibrary.Server.Shared.Model;

namespace MusicLibrary.Server.Controllers;

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