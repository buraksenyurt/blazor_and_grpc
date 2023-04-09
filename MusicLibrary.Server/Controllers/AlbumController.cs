using MusicLibrary.Server.Data;
using MusicLibrary.Server.Service;
using MusicLibrary.Server.Shared.Model;

namespace MusicLibrary.Server.Controllers;

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