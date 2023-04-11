using Microsoft.EntityFrameworkCore;
using MusicLibrary.Data;
using MusicLibrary.Data.Entity;
using MusicLibrary.Service.Contract;
using MusicLibrary.Service.Rest;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

/*
     Yeni Sürüm
    EF bağımlılığını MusicLibrary.Data kütüphanesindeki 
    DependencyInjection sınıfından gelen AddDataContext ile ekliyoruz.
*/
builder.Services.AddDataContext(configuration);

// Önceki sürüm
// /*
//     SQL Server kullanıyoruz ve DbContext nesnesini middleware'e eklerken 
//     appsettings'te dev_conn ile belirtilen connection string bilgisini kullanıyoruz.
// */
// builder.Services.AddDbContext<MusicLibraryDbContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("dev_conn"));

// #if DEBUG
//     options.EnableDetailedErrors();
//     options.EnableSensitiveDataLogging();
// #endif
// });


// AutoMapper hizmetini ilave ederken özel eşleştirme işleri için SmaryMapper'ı kullanacağını belirtiyoruz.
builder.Services.AddAutoMapper(typeof(SmartMapper));


// Album ve Müzisyneler için ilgili veritabanı işlemlerini üstlenen servisler DI çalışma ortamına eklenir.
builder.Services.AddTransient<AlbumService>();
builder.Services.AddTransient<MusicianService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
