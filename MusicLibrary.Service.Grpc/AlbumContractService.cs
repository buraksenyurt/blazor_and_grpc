using AutoMapper;
using Grpc.Core;
using MusicLibrary.Shared.Model;

namespace MusicLibrary.Service.Grpc.Contracts;

public class AlbumContractService
    : AlbumContract.AlbumContractBase
{
    private readonly AlbumService _albumService;
    private readonly IMapper _mapper;

    public AlbumContractService(AlbumService albumService, IMapper mapper)
    {
        _albumService = albumService;
        _mapper = mapper;
    }

    public override async Task<CreateResponse> Create(Album request, ServerCallContext context)
    {
        var model = _mapper.Map<AlbumModel>(request);
        var created = await _albumService.CreateAsync(model);
        return new CreateResponse { Id = created.Id, Path = $"/albums/{created.Id}" };
    }

    public override async Task<GenericResponse> Delete(SingleItemRequest request, ServerCallContext context)
    {
        await _albumService.DeleteAsync(request.Id);
        return new GenericResponse { Success = true };
    }

    public override async Task<Album> Get(SingleItemRequest request, ServerCallContext context)
    {
        var model = await _albumService.GetByIdAsync(request.Id);
        return _mapper.Map<Album>(model);
    }

    public override async Task GetList(ListItemRequest request, IServerStreamWriter<Album> responseStream, ServerCallContext context)
    {
        var data = await _albumService.GetAllAsync(request.Page, request.PageSize);
        foreach (var item in data)
        {
            var model = _mapper.Map<Album>(item);
            await responseStream.WriteAsync(model);
        }
    }

    public override async Task<GenericResponse> Update(Album request, ServerCallContext context)
    {
        var model = _mapper.Map<AlbumModel>(request);
        await _albumService.UpdateAsync(request.Id, model);
        return new GenericResponse { Success = true };
    }
}