using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MusicLibrary.Client;
using MusicLibrary.Shared.Mapper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(typeof(RestMapper)); //REST
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5200") }); //REST

builder.Services.AddAutoMapper(typeof(GrpcMapper)); // GRPC
// GRPC
builder.Services.AddSingleton(s =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress("http://localhost:5030", new GrpcChannelOptions { HttpClient = httpClient });
    var client = new AlbumContract.AlbumContractClient(channel);
    return client;
});

await builder.Build().RunAsync();