using AutoMapper;
using MusicLibrary.Shared.Model;
using Google.Protobuf.WellKnownTypes;

namespace MusicLibrary.Service.Grpc.Mapper;

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
        CreateMap<Category, CategoryModel>().ReverseMap();
    }
}