using AutoMapper;
using Grpc.Core;
using MusicLibrary.Shared.Model;

namespace MusicLibrary.Service.Grpc.Contracts;

public class MusicianContractService
    : MusicianContract.MusicianContractBase
{
    private readonly MusicianService _musicianService;
    private readonly IMapper _mapper;

    public MusicianContractService(MusicianService musicianService, IMapper mapper)
    {
        _musicianService = musicianService;
        _mapper = mapper;
    }

    public override async Task<CreateResponse> Create(Musician request, ServerCallContext context)
    {
        var model = _mapper.Map<MusicianModel>(request);
        var created = await _musicianService.CreateAsync(model);
        return new CreateResponse { Id = created.Id, Path = $"/musicians/{created.Id}" };
    }

    public override async Task<GenericResponse> Delete(SingleItemRequest request, ServerCallContext context)
    {
        await _musicianService.DeleteAsync(request.Id);
        return new GenericResponse { Success = true };
    }

    public override async Task<Musician> Get(SingleItemRequest request, ServerCallContext context)
    {
        var model = await _musicianService.GetByIdAsync(request.Id);
        return _mapper.Map<Musician>(model);
    }

    public override async Task GetList(ListItemRequest request, IServerStreamWriter<Musician> responseStream, ServerCallContext context)
    {
        // _logger.LogInformation($"Sayfa no : {request.Page}, Sayı : {request.PageSize}");
        var data = await _musicianService.GetAllAsync(request.Page, request.PageSize);
        // _logger.LogInformation($"{data.Count()} satır geldi");
        foreach (var item in data)
        {
            var model = _mapper.Map<Musician>(item);
            await responseStream.WriteAsync(model);
        }
    }

    public override async Task<GenericResponse> Update(Musician request, ServerCallContext context)
    {
        var model = _mapper.Map<MusicianModel>(request);
        await _musicianService.UpdateAsync(request.Id, model);
        return new GenericResponse { Success = true };
    }
}