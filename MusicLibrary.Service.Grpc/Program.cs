using MusicLibrary.Data;
using MusicLibrary.Service;
using MusicLibrary.Service.Grpc.Contracts;
using MusicLibrary.Shared.Mapper;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Logging.AddConsole();

builder.Services.AddGrpc();
builder.Services.AddCors(setupAction =>
{
    setupAction.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});
builder.Services.AddTransient<AlbumService>();
builder.Services.AddTransient<MusicianService>();
builder.Services.AddDataContext(configuration);
builder.Services.AddAutoMapper(typeof(GrpcMapper)); // GRPC

var app = builder.Build();
app.UseGrpcWeb();
app.UseCors();
app.MapGrpcService<MusicianContractService>().EnableGrpcWeb();
app.MapGrpcService<AlbumContractService>().EnableGrpcWeb();

//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
