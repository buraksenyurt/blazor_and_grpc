using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using MusicLibrary.Shared.Model;

namespace MusicLibrary.Shared.Mapper;

public class GrpcMapper : Profile
{
    public GrpcMapper()
    {
        // Entity'ler ile modeller arası
        CreateMap<Data.Entity.Musician, MusicianModel>().ForMember(m => m.AlbumIds, map => map.MapFrom(ms => ms.Albums.Select(a => a.AlbumId)));
        CreateMap<MusicianModel, Data.Entity.Musician>().ForMember(m => m.Id, map => map.Ignore()).ForMember(m => m.Albums, map => map.MapFrom((model, entity) =>
        {
            var current = entity.Albums.Select(a => a.AlbumId).ToArray();
            var albums = entity.Albums.ToList();
            albums.AddRange(model.AlbumIds.Where(a => !current.Contains(a)).Select(m => new Data.Entity.AlbumMusician { AlbumId = m }));
            albums.RemoveAll(a => !model.AlbumIds.Contains(a.AlbumId));

            return albums;
        }));
        CreateMap<Data.Entity.Album, AlbumModel>().ForMember(m => m.MusicianIds, map => map.MapFrom(ms => ms.Musicians.Select(m => m.MusicianId)));
        CreateMap<AlbumModel, Data.Entity.Album>().ForMember(m => m.Id, map => map.Ignore()).ForMember(m => m.Musicians, map => map.MapFrom((model, entity) =>
        {
            var current = entity.Musicians.Select(m => m.MusicianId).ToArray();
            var musicians = entity.Musicians.ToList();
            musicians.AddRange(model.MusicianIds.Where(m => !current.Contains(m)).Select(m => new Data.Entity.AlbumMusician { MusicianId = m }));
            musicians.RemoveAll(a => !model.MusicianIds.Contains(a.MusicianId));

            return musicians;
        }));

        // GRPC Proto ile Modeller arası
        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(m => Timestamp.FromDateTime(DateTime.SpecifyKind(m, DateTimeKind.Utc)));

        CreateMap<Timestamp, DateTime>()
            .ConvertUsing(m => m.ToDateTime());

        CreateMap<Musician, MusicianModel>().ReverseMap();
        CreateMap<Album, AlbumModel>().ReverseMap();
        //CreateMap<Category, CategoryModel>().ReverseMap();
    }
}