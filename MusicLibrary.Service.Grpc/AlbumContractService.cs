using AutoMapper;
using Grpc.Core;
using MusicLibrary.Shared.Model;

namespace MusicLibrary.Service.Grpc.Contracts;

public class AlbumContractService
    : AlbumContract.AlbumContractBase
{
    private readonly AlbumService _albumService;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public AlbumContractService(AlbumService albumService, IMapper mapper, ILogger<AlbumContractService> logger)
    {
        _albumService = albumService;
        _mapper = mapper;
        _logger = logger;
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
        _logger.LogInformation($"Page Index/Count {request.Page}/{request.PageSize}");
        var data = await _albumService.GetAllAsync(request.Page, request.PageSize);
        _logger.LogInformation($"Data Count {data.Count()}");
        foreach (var item in data)
        {
            _logger.LogInformation($"Item -> {item.Id},{item.Name},{item.Year},{item.Category}");
            var model = _mapper.Map<Album>(item);
            // var model = new Album
            // {
            //     Id = item.Id,
            //     Name = item.Name,
            //     Year = item.Year,
            //     Category = (Category)item.Category
            // };
            _logger.LogInformation($"Grpc Mode -> {model.Id},{model.Name},{model.Year},{model.Category}");
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