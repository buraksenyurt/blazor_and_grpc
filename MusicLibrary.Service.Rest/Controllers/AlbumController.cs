using MusicLibrary.Data.Entity;
using MusicLibrary.Shared.Model;

namespace MusicLibrary.Service.Controllers;

/*
    Albümler için kullanılan kontrollör sınıfı. 
    BaseController'dan türer.
*/
public class AlbumController
    : BaseController<AlbumModel, Album, AlbumService>
{
    public AlbumController(AlbumService service)
        : base(service, "/albums")
    {
    }
}