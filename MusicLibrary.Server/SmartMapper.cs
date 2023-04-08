using AutoMapper;
namespace MusicLibrary.Server;

public class SmartMapper : Profile
{
    public SmartMapper()
    {
        CreateMap<Data.Musician, Shared.Model.MusicianModel>().ForMember(m => m.AlbumIds, map => map.MapFrom(ms => ms.Albums.Select(a => a.AlbumId)));
        CreateMap<Shared.Model.MusicianModel, Data.Musician>().ForMember(m => m.Id, map => map.Ignore()).ForMember(m => m.Albums, map => map.MapFrom((model, entity) =>
        {
            var current = entity.Albums.Select(a => a.AlbumId).ToArray();
            var albums = entity.Albums.ToList();
            albums.AddRange(model.AlbumIds.Where(a => !current.Contains(a)).Select(m => new Data.AlbumMusician { AlbumId = m }));
            albums.RemoveAll(a => !model.AlbumIds.Contains(a.AlbumId));

            return albums;
        }));
        CreateMap<Data.Album, Shared.Model.AlbumModel>().ForMember(m => m.MusicianIds, map => map.MapFrom(ms => ms.Musicians.Select(m => m.MusicianId)));
        CreateMap<Shared.Model.AlbumModel, Data.Album>().ForMember(m => m.Id, map => map.Ignore()).ForMember(m => m.Musicians, map => map.MapFrom((model, entity) =>
        {
            var current = entity.Musicians.Select(m => m.MusicianId).ToArray();
            var musicians = entity.Musicians.ToList();
            musicians.AddRange(model.MusicianIds.Where(m => !current.Contains(m)).Select(m => new Data.AlbumMusician { MusicianId = m }));
            musicians.RemoveAll(a => !model.MusicianIds.Contains(a.MusicianId));

            return musicians;
        }));
    }
}