using AutoMapper;
using MusicLibrary.Shared.Model;
using Google.Protobuf.WellKnownTypes;
using MusicLibrary.Data.Entity;

namespace MusicLibrary.Shared.Mapper;

public class GrpcMapper : Profile
{
    public GrpcMapper()
    {
        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(m => Timestamp.FromDateTime(DateTime.SpecifyKind(m, DateTimeKind.Utc)));

        CreateMap<Timestamp, DateTime>()
            .ConvertUsing(m => m.ToDateTime());

        CreateMap<Musician, MusicianModel>().ReverseMap();
        CreateMap<Album, AlbumModel>().ReverseMap();
    }
}