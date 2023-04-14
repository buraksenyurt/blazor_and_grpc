using MusicLibrary.Service.Grpc;
using MusicLibrary.Service.Grpc.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(SmartMapper));

var app = builder.Build();

app.UseGrpcWeb();
app.MapGrpcService<MusicianContractService>().EnableGrpcWeb();
app.MapGrpcService<AlbumContractService>().EnableGrpcWeb();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
